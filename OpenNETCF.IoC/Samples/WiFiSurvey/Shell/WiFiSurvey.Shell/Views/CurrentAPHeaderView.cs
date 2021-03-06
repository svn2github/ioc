﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using OpenNETCF.IoC.UI;
using WiFiSurvey.Shell.Presenters;
using WiFiSurvey.Infrastructure;
using WiFiSurvey.Infrastructure.BusinessObjects;
using WiFiSurvey.Infrastructure.Services;
using OpenNETCF.IoC;
using WiFiSurvey.Infrastructure.Constants;
using System.Diagnostics;
using OpenNETCF;

namespace WiFiSurvey.Shell.Views
{
    public partial class CurrentAPHeaderView : SmartPart
    {
        AccessPointPresenter APPresenter;
        DesktopPresenter DeskPresenter;
        Boolean PreviouslyConnected { get; set; }

        APInfo CurrentAP { get; set; }

        private Stopwatch m_timeConnected = new Stopwatch();

        private Timer m_HeaderTimer = new Timer();

        public CurrentAPHeaderView()
        {
            InitializeComponent();
            APPresenter = RootWorkItem.Items.Get<AccessPointPresenter>(PresenterNames.AccessPoint);
            APPresenter.NetworkDataChanged += Presenter_OnCurrentAPUpdate;

            DeskPresenter = RootWorkItem.Items.AddNew<DesktopPresenter>(PresenterNames.Desktop);
            DeskPresenter.DesktopConnectionChange += DeskPresenter_DesktopConnectionChange;

            m_HeaderTimer.Interval = 1000;
            m_HeaderTimer.Tick += delegate { SetCurrentAP(); };
            m_HeaderTimer.Enabled = true;
        }

        void DeskPresenter_DesktopConnectionChange(object sender, GenericEventArgs<IDesktopData> e)
        {
            UpdateConnection(e.Value);
        }

        void Presenter_OnCurrentAPUpdate(object sender, GenericEventArgs<INetworkData> e)
        {
            if (CurrentAP != null)
            {
                if (CurrentAP.MAC != e.Value.AssociatedAP.MAC)
                {
                    if (e.Value.AssociatedAP.Name != String.Empty)
                    {
                        m_timeConnected.Reset();
                        m_timeConnected.Start();
                    }
                    else
                    {
                        m_timeConnected.Reset();
                    }
                }
            }
            else
            {
                m_timeConnected.Reset();
            }

            CurrentAP = e.Value.AssociatedAP;
            SetCurrentAP();
        }

        public void SetCurrentAP()
        {
            if (CurrentAP == null || CurrentAP.Name == String.Empty)
            {
                UpdateHeader("none", "-", "-");
            }
            else
            {
                m_timeConnected.Stop();
                UpdateHeader(CurrentAP.Name, CurrentAP.MAC, CurrentAP.SignalStrength.ToString());
                m_timeConnected.Start();
            }
        }

        public delegate void UpdateHeaderDelegate(string name, string mac, string strength);

        public void UpdateHeader(string Name, string MAC, string Strength)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new UpdateHeaderDelegate(UpdateHeader), new object[] { Name, MAC, Strength });
                    return;
                }

                lblSSIDName.Text = "[" + Name + "] " + m_timeConnected.Elapsed.Hours.ToString("D2") + ":" + m_timeConnected.Elapsed.Minutes.ToString("D2") + ":" + m_timeConnected.Elapsed.Seconds.ToString("D2");
                lblSignalStrength.Text = "Signal: " + Strength;
                lblMacAdress.Text = MAC;
            }
            catch (ObjectDisposedException)
            {
                // this can happen when shutting down the app (getting an event after the Form has been disposed)
            }
        }

        delegate void UC(IDesktopData data);

        public void UpdateConnection(IDesktopData data)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new UC(UpdateConnection), new object[] { data });
                return;
            }

            switch (data.Status)
            {
                case DesktopStatus.Connected: desktopStatusPictureBox.Image = ildesktopStatus.Images[1]; break;
                case DesktopStatus.Disabled: desktopStatusPictureBox.Image = ildesktopStatus.Images[0]; break;
                case DesktopStatus.Disconnected: desktopStatusPictureBox.Image = ildesktopStatus.Images[0]; break;
                case DesktopStatus.Enabled: desktopStatusPictureBox.Image = ildesktopStatus.Images[0]; break;
            }
        }
    }
}
