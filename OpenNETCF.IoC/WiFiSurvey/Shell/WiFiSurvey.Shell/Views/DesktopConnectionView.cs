using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using OpenNETCF.IoC.UI;
using WiFiSurvey.Infrastructure.BusinessObjects;
using WiFiSurvey.Infrastructure.Constants;
using WiFiSurvey.Shell.Presenters;
using OpenNETCF.IoC;
using WiFiSurvey.Infrastructure;

namespace WiFiSurvey.Shell.Views
{
    public partial class DesktopConnectionView : SmartPart
    {
        DesktopPresenter Presenter;

        public DesktopConnectionView()
        {
            InitializeComponent();

            label1.Text = "Desktop Not Connected";

            Presenter = RootWorkItem.Items.AddNew<DesktopPresenter>(PresenterNames.Desktop);
            Presenter.DesktopConnectionChange += new EventHandler<GenericEventArgs<IDesktopData>>(Presenter_DesktopConnectionChange);
        }

        void Presenter_DesktopConnectionChange(object sender, GenericEventArgs<IDesktopData> e)
        {
            UpdateConnection(e.Value);
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
                case DesktopStatus.Connected   : label1.Text = "Desktop Connected";           break;
                case DesktopStatus.Disabled    : label1.Text = "Desktop Connection Disabled"; break;
                case DesktopStatus.Disconnected: label1.Text = "Desktop Not Connected";       break;
                case DesktopStatus.Enabled     : label1.Text = "Desktop Connection Enabled"; break;
            }
        }
    }
}
