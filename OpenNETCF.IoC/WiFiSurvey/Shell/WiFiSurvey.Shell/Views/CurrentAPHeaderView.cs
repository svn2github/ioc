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

namespace WiFiSurvey.Shell.Views
{
    public partial class CurrentAPHeaderView : SmartPart
    {
        AccessPointPresenter Presenter;
        Boolean PreviouslyConnected { get; set; }

        public CurrentAPHeaderView()
        {
            InitializeComponent();
            Presenter = RootWorkItem.Items.AddNew<AccessPointPresenter>();
            Presenter.OnCurrentAPUpdate += new EventHandler<GenericEventArgs<INetworkData>>(Presenter_OnCurrentAPUpdate);
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
    }
}
