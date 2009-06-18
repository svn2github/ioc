﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using OpenNETCF.IoC.UI;
using WiFiSurvey.Infrastructure.BusinessObjects;
using WiFiSurvey.Infrastructure.Services;
using OpenNETCF.IoC;

namespace WiFiSurvey.Shell.Views
{
    public partial class ToolsView : SmartPart
    {
        private IDataService DataService {get;set;}

        public ToolsView()
        {
            InitializeComponent();

            this.Name = "Tools";

            DataService = RootWorkItem.Services.Get<IDataService>();

            btnDisableDesktop.Enabled = true;
            btnEnableDesktop.Enabled = false;

            cmbRefreshRate.SelectedIndex = 0;
            cmbRefreshRate.SelectedIndexChanged += new EventHandler(cmbRefreshRate_SelectedIndexChanged);
            // TODO: collection interval, etc.
        }

        void cmbRefreshRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            int m_Refresh = Convert.ToInt32(cmbRefreshRate.SelectedItem.ToString());
            m_Refresh = (m_Refresh * 1000);
            if (WirelessUtility.RefreshRate != m_Refresh)
            {
                WirelessUtility.RefreshRate = m_Refresh;
            }
        }

        public void UpdateTools()
        {
//            lblEventCount.Text = DataService.EventCount.ToString();;
        }

        private void btnEnableDesktop_Click(object sender, EventArgs e)
        {
            WirelessUtility.DesktopAppDisabled = false;

            btnEnableDesktop.Enabled = WirelessUtility.DesktopAppDisabled;
            btnDisableDesktop.Enabled = !WirelessUtility.DesktopAppDisabled;
        }

        private void btnDisableDesktop_Click(object sender, EventArgs e)
        {
            WirelessUtility.DesktopAppDisabled = true;

            btnEnableDesktop.Enabled = WirelessUtility.DesktopAppDisabled;
            btnDisableDesktop.Enabled = !WirelessUtility.DesktopAppDisabled;
        }

        private void btnResetEvents_Click(object sender, EventArgs e)
        {
//            DataService.EventCount = 0;
//            DataService.ClearEvents();
        }
    }
}
