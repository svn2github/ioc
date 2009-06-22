using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using OpenNETCF.IoC;
using WiFiSurvey.Infrastructure.Constants;
using WiFiSurvey.Shell.Presenters;
using OpenNETCF.IoC.UI;
using WiFiSurvey.Shell.Views;
using OpenNETCF.Net.NetworkInformation;
using WiFiSurvey.Infrastructure.Services;
using WiFiSurvey.Infrastructure.BusinessObjects;
using WiFiSurvey.Infrastructure;

namespace WiFiSurvey.Shell
{
    public partial class ContainerForm : Form
    {
        ContainerPresenter Presenter { get; set; }
        IHistoricEventService DataService { get; set; }
        IDesktopService DesktopService { get; set; }
        IStatisticsService StatisticsService { get; set; }
        IAPMonitorService APMonitorService { get; set; }

        private Stopwatch APDownWatch = new Stopwatch();
        private Boolean PreviouslyConnected { get; set; }

        public ContainerForm()
        {
            // display all loaded modules
            DoModuleDump();

            InitializeComponent();

            // store the main workspace in the IoC framework
            RootWorkItem.Items.Add(headerWorkspace, WorkspaceNames.HeaderWorkspace);
            RootWorkItem.Items.Add(bodyWorkspace, WorkspaceNames.BodyWorkspace);

            // create the presenter for the container
            Presenter = RootWorkItem.Items.AddNew<ContainerPresenter>(PresenterNames.Container);

            // load up the tabs into the display
            ISmartPart view = RootWorkItem.Items.AddNew<APListView>(ViewNames.APList) as ISmartPart;
            bodyWorkspace.Show(view);

            view = RootWorkItem.Items.AddNew<ConfigurationView>(ViewNames.Tools) as ISmartPart;
            bodyWorkspace.Show(view);

            view = RootWorkItem.Items.AddNew<HistoryView>(ViewNames.History) as ISmartPart;
            bodyWorkspace.Show(view);

            //Header and Footer
            view = RootWorkItem.Items.AddNew<CurrentAPHeaderView>(ViewNames.Header) as ISmartPart;
            headerWorkspace.Show(view);

            bodyWorkspace.SelectTab(0);

            this.WindowState = FormWindowState.Normal;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
        }

        void ContainerForm_Resize(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            DesktopService.Shutdown();
            StatisticsService.Shutdown();
            APMonitorService.Shutdown();
        }

        protected override void OnLoad(EventArgs e)
        {
            DesktopService = RootWorkItem.Services.Get<IDesktopService>();
            DesktopService.StartListening();

            StatisticsService = RootWorkItem.Services.Get<IStatisticsService>();

            APMonitorService = RootWorkItem.Services.Get<IAPMonitorService>();

            base.OnLoad(e);
        }

        [Conditional("DEBUG")]
        private void DoModuleDump()
        {
            Debug.WriteLine("\r\nLoaded Modules\r\n==============");
            foreach (var m in RootWorkItem.Services.Get<ModuleInfoStoreService>().LoadedModules)
            {
                Debug.WriteLine(string.Format("    {0}", m.AssemblyFile));
            }
            Debug.WriteLine(string.Empty);
        }
    }
}
