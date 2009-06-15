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

namespace WiFiSurvey.Shell
{
    public partial class ContainerForm : Form
    {
        ContainerPresenter Presenter { get; set; }
        public ContainerForm()
        {
            // display all loaded modules
            DoModuleDump();

            InitializeComponent();

            // store the main workspace in the IoC framework
            RootWorkItem.Items.Add(headerWorkspace, WorkspaceNames.MainWorkspace);

            // create the presenter for the container
            Presenter = RootWorkItem.Items.AddNew<ContainerPresenter>(PresenterNames.Container);

            // load up the default view
            ISmartPart view = RootWorkItem.Items.AddNew<APListView>(ViewNames.APList) as ISmartPart;

            // and show it
            headerWorkspace.Show(view);
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