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
        IDataService DataService { get; set; }
        INetworkService NetworkService { get; set; }
        IStatisticsService StatisticsService { get; set; }

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
            RootWorkItem.Items.Add(footerWorkspace, WorkspaceNames.FooterWorkspace);

            // create the presenter for the container
            Presenter = RootWorkItem.Items.AddNew<ContainerPresenter>(PresenterNames.Container);

            // load up the tabs into the display
            ISmartPart view = RootWorkItem.Items.AddNew<APListView>(ViewNames.APList) as ISmartPart;
            bodyWorkspace.Show(view);

            view = RootWorkItem.Items.AddNew<ToolsView>(ViewNames.Tools) as ISmartPart;
            bodyWorkspace.Show(view);

            view = RootWorkItem.Items.AddNew<HistoryView>(ViewNames.History) as ISmartPart;
            bodyWorkspace.Show(view);

            //Header and Footer
            view = RootWorkItem.Items.AddNew<CurrentAPHeaderView>(ViewNames.Header) as ISmartPart;
            headerWorkspace.Show(view);

            view = RootWorkItem.Items.AddNew<StatusFooterView>(ViewNames.Footer) as ISmartPart;
            footerWorkspace.Show(view);

            bodyWorkspace.SelectTab(0);

            Presenter.APConnectionChanged += new EventHandler<WiFiSurvey.Infrastructure.GenericEventArgs<INetworkData>>(Presenter_APConnectionChanged);
            Presenter.DesktopConnectionChanged += new EventHandler<GenericEventArgs<IDesktopData>>(Presenter_DesktopConnectionChanged);

            this.WindowState = FormWindowState.Normal;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
        }

        void Presenter_DesktopConnectionChanged(object sender, GenericEventArgs<IDesktopData> e)
        {
            UpdateFooter(e.Value);
        }

        void Presenter_APConnectionChanged(object sender, GenericEventArgs<INetworkData> e)
        {
            UpdateHeader(e.Value);
        }

        void ContainerForm_Resize(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void OnLoad(EventArgs e)
        {
            NetworkService = RootWorkItem.Services.Get<INetworkService>();
            NetworkService.StartListening();

            DataService = RootWorkItem.Services.Get<IDataService>();

            StatisticsService = RootWorkItem.Services.Get<IStatisticsService>();

            UpdateFooter(new DesktopData() { Status = DesktopStatus.Disconnected });
            base.OnLoad(e);
        }

        public void UpdateHeader(INetworkData data)
        {
            CurrentAPHeaderView m_Header = RootWorkItem.Items.Get<CurrentAPHeaderView>(ViewNames.Header);
            if (data.AssociatedAP == null)
            {
                if (PreviouslyConnected)
                {
                    StatisticsService.LostAccessPoint();
                    PreviouslyConnected = false;
                }
                m_Header.SetCurrentAP("[none]", "-", "-");
            }
            else
            {
                if (!PreviouslyConnected)
                {
                    StatisticsService.FoundAccessPoint();
                    PreviouslyConnected = true;
                }
                m_Header.SetCurrentAP(data.AssociatedAP.Name, data.AssociatedAP.MAC, data.AssociatedAP.SignalStrength.ToString());
            }
        }

        public void UpdateFooter(IDesktopData data)
        {
            StatusFooterView m_footer = RootWorkItem.Items.Get<StatusFooterView>(ViewNames.Footer);
            m_footer.UpdateConnection(data);
        }

        public void UpdateTools()
        {
            ToolsView m_Tools = RootWorkItem.Items.Get<ToolsView>(ViewNames.Tools);
            m_Tools.UpdateTools();
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
