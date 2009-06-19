using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using OpenNETCF.IoC;
using WiFiSurvey.Shell.Presenters;
using WiFiSurvey.Infrastructure.Constants;
using OpenNETCF.IoC.UI;
using WiFiSurvey.Infrastructure.Services;
using System.Diagnostics;
using OpenNETCF.Net.NetworkInformation;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using OpenNETCF.Threading;
using WiFiSurvey.Infrastructure;
using WiFiSurvey.Infrastructure.BusinessObjects;

namespace WiFiSurvey.Shell.Views
{
    public partial class APListView : SmartPart
    {
        public APListPresenter Presenter { get; set; }
        public INetworkService NetworkService { get; set; }

        private Timer m_apRefreshTimer = new Timer();
        private bool m_columnWidthsSet = false;
        private bool m_alreadyQuerying = false;
        private EventWaitHandle m_activityEvent;

        private Control m_invoker = new Control();

        public APListView()
        {
            Presenter = RootWorkItem.Items.AddNew<APListPresenter>(PresenterNames.APList);
            Presenter.NetworkDataChanged += new EventHandler<GenericEventArgs<INetworkData>>(Presenter_NetworkDataChanged);

            InitializeComponent();
            this.Name = "Available APs";

            apList.FullRowSelect = true;
        }

        void Presenter_NetworkDataChanged(object sender, GenericEventArgs<INetworkData> e)
        {
            if (m_invoker.InvokeRequired)
            {
                m_invoker.Invoke(new EventHandler<GenericEventArgs<INetworkData>>(Presenter_NetworkDataChanged), new object[] { sender, e });
                return;
            }

            ResetBacklightTimer();

            // update the UI
            apList.SuspendLayout();

            apList.Items.Clear();

            try
            {
                foreach (var ap in e.Value.NearbyAPs)
                {
                    ListViewItem lvitem = new ListViewItem();
                    lvitem.Text = ap.Name;
                    lvitem.SubItems.Add(ap.SignalStrength.ToString());
                    lvitem.SubItems.Add(ap.MAC);
                    apList.Items.Add(lvitem);
                }
                if ((!m_columnWidthsSet) && (e.Value.NearbyAPs.Length > 0))
                {
                    UpdateColumnsWidth();
                }
            }
            finally
            {
                apList.ResumeLayout(true);
            }

        }

        private void UpdateColumnsWidth()
        {
            apList.Columns[0].Width = -1;
            apList.Columns[1].Width = -2;
            apList.Columns[2].Width = -2;
            m_columnWidthsSet = true;
        }

        [DllImport("coredll", SetLastError=true)]
        private static extern void SystemIdleTimerReset();

        private void ResetBacklightTimer()
        {
            if (Environment.OSVersion.Version.Major <= 5)
            {
                SystemIdleTimerReset();
            }
            else
            {
                if (m_activityEvent == null)
                {
                    using (var key = Registry.LocalMachine.OpenSubKey("System\\GWE"))
                    {
                        object value = key.GetValue("ActivityEvent");
                        key.Close();
                        if (value == null) return;
                        string activityEventName = (string)value;
                        m_activityEvent = new EventWaitHandle(false, EventResetMode.AutoReset, activityEventName);
                    }
                }

                m_activityEvent.Set();
            }
        }
    }
}
