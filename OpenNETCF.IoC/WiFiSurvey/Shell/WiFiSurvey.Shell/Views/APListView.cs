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

namespace WiFiSurvey.Shell.Views
{
    public partial class APListView : UserControl, ISmartPart
    {
        public APListPresenter Presenter { get; set; }

        public APListView()
        {
            Presenter = RootWorkItem.Items.AddNew<APListPresenter>(PresenterNames.APList);
            InitializeComponent();
            this.Name = "Available APs";

            // TODO show list of available APs (name, MAC and signal strength)
        }
    }
}
