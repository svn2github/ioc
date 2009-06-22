using System;
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

namespace WiFiSurvey.Shell.Views
{
    public partial class CurrentAPHeaderView : SmartPart
    {
        AccessPointPresenter APPresenter;
        DesktopPresenter DeskPresenter;
        Boolean PreviouslyConnected { get; set; }

        public CurrentAPHeaderView()
        {
            InitializeComponent();
            APPresenter = RootWorkItem.Items.AddNew<AccessPointPresenter>();
            APPresenter.OnCurrentAPUpdate += new EventHandler<GenericEventArgs<INetworkData>>(Presenter_OnCurrentAPUpdate);

            DeskPresenter = RootWorkItem.Items.AddNew<DesktopPresenter>(PresenterNames.Desktop);
            DeskPresenter.DesktopConnectionChange += new EventHandler<GenericEventArgs<IDesktopData>>(DeskPresenter_DesktopConnectionChange);
        }

        void DeskPresenter_DesktopConnectionChange(object sender, GenericEventArgs<IDesktopData> e)
        {
            UpdateConnection(e.Value);
        }

        void Presenter_OnCurrentAPUpdate(object sender, GenericEventArgs<INetworkData> e)
        {
            SetCurrentAP(e.Value.AssociatedAP);
        }

        public void SetCurrentAP(APInfo info)
        {
            if (info == null)
            {
                UpdateHeader("[none]", "-", "-");
            }
            else
            {
                UpdateHeader(info.Name, info.MAC, info.SignalStrength.ToString());
            }
        }

        public delegate void UpdateHeaderDelegate(string name, string mac, string strength);

        public void UpdateHeader(string Name, string MAC, string Strength)
        {
            if(this.InvokeRequired)
            {
                this.Invoke(new UpdateHeaderDelegate(UpdateHeader),new object[]{Name, MAC, Strength});
                return;
            }

            lblSSIDName.Text = Name;
            lblSignalStrength.Text = "Signal: " + Strength;
            lblMacAdress.Text = MAC;
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
