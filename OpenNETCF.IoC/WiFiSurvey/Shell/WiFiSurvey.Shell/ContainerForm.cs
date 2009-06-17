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

namespace WiFiSurvey.Shell
{
    public partial class ContainerForm : Form
    {
        ContainerPresenter Presenter { get; set; }
        private IDataService DataService {get;set;}

        private Timer m_containerTimer = new Timer();

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

            view = RootWorkItem.Items.AddNew<CurrentAPHeaderView>(ViewNames.Header) as ISmartPart;
            headerWorkspace.Show(view);

            view = RootWorkItem.Items.AddNew<StatusFooterView>(ViewNames.Footer) as ISmartPart;
            footerWorkspace.Show(view);

            DataService = RootWorkItem.Services.Get<IDataService>();
            DataService.StartListening();

            bodyWorkspace.SelectTab(0);

            this.WindowState = FormWindowState.Maximized;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
        }

        void ContainerForm_Resize(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void OnLoad(EventArgs e)
        {
            //WU.DesktopAppEnabled = true;

            m_containerTimer.Interval = 1000;
            m_containerTimer.Tick += new EventHandler(m_containerTimer_Tick);
            m_containerTimer.Enabled = true;

            UpdateHeader();
            UpdateFooter();

            base.OnLoad(e);
        }

        void m_containerTimer_Tick(object sender, EventArgs e)
        {
            UpdateHeader();
            UpdateFooter();
        }

        public void UpdateHeader()
        {
            CurrentAPHeaderView m_Header = RootWorkItem.Items.Get<CurrentAPHeaderView>(ViewNames.Header);
            AccessPoint accessPoint = Presenter.GetCurrentAP();
            if (accessPoint == null)
            {
                m_Header.SetCurrentAP("[none]", "-", "-");
            }
            else
            {
                m_Header.SetCurrentAP(accessPoint.Name, accessPoint.PhysicalAddress.ToString(), accessPoint.SignalStrength.Decibels.ToString());
            }
        }

        public void UpdateFooter()
        {
            StatusFooterView m_footer = RootWorkItem.Items.Get<StatusFooterView>(ViewNames.Footer);
            m_footer.UpdateConnection(WU.DesktopConnected);
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