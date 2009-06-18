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

        public APListView()
        {
            Presenter = RootWorkItem.Items.AddNew<APListPresenter>(PresenterNames.APList);
            InitializeComponent();
            this.Name = "Available APs";

            apList.FullRowSelect = true;
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

        public void RefreshList()
        {
            AccessPointCollection accessPoints = null;

            // don't double-call
            if (m_alreadyQuerying) return;

            // prevent the backlight from shutting off
            ResetBacklightTimer();

            try
            {
                m_alreadyQuerying = true;
                int et = Environment.TickCount;
                try
                {
                    accessPoints = Presenter.AccessPoints;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(string.Format(" Exception getting AP list: {0}", ex.Message));
                    return;
                }

                et = Environment.TickCount - et;
                Debug.WriteLine(string.Format(" Getting AP list took {0}ms", et));

                ListViewItem lvitem;

                if (accessPoints == null)
                {
                    return;
                }

                int expected = accessPoints.Count;
                apList.SuspendLayout();
                apList.Items.Clear();

                foreach (AccessPoint accessPoint in accessPoints)
                {
                    lvitem = new ListViewItem();
                    lvitem.Text = accessPoint.Name;
                    lvitem.SubItems.Add(accessPoint.SignalStrength.Decibels.ToString());
                    lvitem.SubItems.Add(accessPoint.PhysicalAddress.ToString());
                    apList.Items.Add(lvitem);
                }
                if ((!m_columnWidthsSet) && (accessPoints.Count > 0))
                {
                    UpdateColumnsWidth();
                }

                apList.ResumeLayout(true);

            }
            finally
            {
                m_alreadyQuerying = false;
            }
        }

    }
}
