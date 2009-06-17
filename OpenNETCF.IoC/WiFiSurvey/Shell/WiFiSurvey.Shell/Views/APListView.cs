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

namespace WiFiSurvey.Shell.Views
{
    public partial class APListView : SmartPart
    {
        public APListPresenter Presenter { get; set; }
        public INetworkService NetworkService { get; set; }

        private Timer m_apRefreshTimer = new Timer();

        public APListView()
        {
            Presenter = RootWorkItem.Items.AddNew<APListPresenter>(PresenterNames.APList);
            InitializeComponent();
            this.Name = "Available APs";

            listView1.FullRowSelect = true;
            listView1.SelectedIndexChanged += new EventHandler(listView1_SelectedIndexChanged);

            m_apRefreshTimer.Interval = 1000;
            m_apRefreshTimer.Tick += new EventHandler(m_apRefreshTimer_Tick);
            m_apRefreshTimer.Enabled = true;

            UpdateColumnsWidth();
        }
        private void UpdateColumnsWidth()
        {
            int totalSize = (int)(ClientRectangle.Width / listView1.Columns.Count);
            listView1.Columns[0].Width = totalSize;
            listView1.Columns[1].Width = totalSize;
            listView1.Columns[2].Width = totalSize;
        }

        void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count > 0)
            {
                ListViewItem item = listView1.Items[listView1.SelectedIndices[0]];
                //Presenter.SetCurrentAP(Presenter.GetAccessPoints().FindBySSID(item.Text));
            }
        }

        void m_apRefreshTimer_Tick(object sender, EventArgs e)
        {
            Boolean Contains = false;
            AccessPointCollection AccessPoints = Presenter.GetAccessPoints();
            ListViewItem lvitem;
            int expected = AccessPoints.Count;
            listView1.Items.Clear();

            // TODO show list of available APs (name, MAC and signal strength)
            foreach (AccessPoint accessPoint in AccessPoints)
            {
                Contains = false;
                lvitem = new ListViewItem();
                lvitem.Text = accessPoint.Name;
                lvitem.SubItems.Add(accessPoint.SignalStrength.Decibels.ToString());
                lvitem.SubItems.Add(accessPoint.PhysicalAddress.ToString());
                listView1.Items.Add(lvitem);
                //    foreach (ListViewItem item in listView1.Items)
                //    {
                //        if (item.Text == lvitem.Text)
                //        {
                //            Contains = true;
                //            item.SubItems[1].Text = accessPoint.SignalStrength.Decibels.ToString();
                //        }
                //    }
                //    if (!Contains)
                //    {
                //        listView1.Items.Add(lvitem);
                //    }
                //}
            }
            if (listView1.Items.Count != expected)
            {
                Trace.WriteLine("Not Whats Expected");
            }
        }

    }
}
