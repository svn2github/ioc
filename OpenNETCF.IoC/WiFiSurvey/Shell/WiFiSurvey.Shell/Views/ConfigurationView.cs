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
using WiFiSurvey.Shell.Presenters;
using WiFiSurvey.Infrastructure.Constants;
using WiFiSurvey.Infrastructure;

namespace WiFiSurvey.Shell.Views
{
    public partial class ConfigurationView : SmartPart
    {
        AccessPointPresenter APPresenter;
        DesktopPresenter DesktopPresenter;
        HistoryPresenter HistoryPresenter;

        IConfigurationService Configuration { get; set; }

        Timer ConfigTimer = new Timer();

        public ConfigurationView()
        {
            InitializeComponent();

            this.Name = "Tools";

            btnDisableDesktop.Enabled = true;
            btnEnableDesktop.Enabled = false;

            ConfigTimer.Interval = 1000;
            ConfigTimer.Tick += new EventHandler(ConfigTimer_Tick);
            ConfigTimer.Enabled = true;

            cmbRefreshRate.SelectedIndex = 0;
            cmbRefreshRate.SelectedIndexChanged += new EventHandler(cmbRefreshRate_SelectedIndexChanged);
            // TODO: collection interval, etc.

            APPresenter = RootWorkItem.Items.Get<AccessPointPresenter>(PresenterNames.APList);
            DesktopPresenter = RootWorkItem.Items.Get<DesktopPresenter>(PresenterNames.Desktop);
            HistoryPresenter = RootWorkItem.Items.Get<HistoryPresenter>(PresenterNames.History);

            Configuration = RootWorkItem.Services.Get<IConfigurationService>();
        }

        void ConfigTimer_Tick(object sender, EventArgs e)
        {
            if (DesktopPresenter == null)
            {
                DesktopPresenter = RootWorkItem.Items.Get<DesktopPresenter>(PresenterNames.Desktop);
            }
            if (HistoryPresenter == null)
            {
                HistoryPresenter = RootWorkItem.Items.Get<HistoryPresenter>(PresenterNames.History);
            }

            if (HistoryPresenter != null)
            {
                lblEventCount.Text = HistoryPresenter.EventCount.ToString();
            }
        }

        void cmbRefreshRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            int m_Refresh = Convert.ToInt32(cmbRefreshRate.SelectedItem.ToString());
            m_Refresh = (m_Refresh * 1000);
            if (WirelessUtility.RefreshRate != m_Refresh)
            {
                WirelessUtility.RefreshRate = m_Refresh;
            }


            if (Configuration.ApplicationConfig.AdapterPollInterval != m_Refresh)
            {
                Configuration.ApplicationConfig.AdapterPollInterval = m_Refresh;
            }
        }

        private void btnEnableDesktop_Click(object sender, EventArgs e)
        {
            DesktopPresenter.NewDesktopStatus(this, new GenericEventArgs<IDesktopData>(new DesktopData() { Status = DesktopStatus.Enabled }));

            WirelessUtility.DesktopAppDisabled = false;

            btnEnableDesktop.Enabled = WirelessUtility.DesktopAppDisabled;
            btnDisableDesktop.Enabled = !WirelessUtility.DesktopAppDisabled;
        }

        private void btnDisableDesktop_Click(object sender, EventArgs e)
        {
            DesktopPresenter.NewDesktopStatus(this, new GenericEventArgs<IDesktopData>(new DesktopData(){Status = DesktopStatus.Disabled}));

            WirelessUtility.DesktopAppDisabled = true;

            btnEnableDesktop.Enabled = WirelessUtility.DesktopAppDisabled;
            btnDisableDesktop.Enabled = !WirelessUtility.DesktopAppDisabled;
        }

        private void btnResetEvents_Click(object sender, EventArgs e)
        {
            HistoryPresenter.ClearHistory();
        }
    }
}
