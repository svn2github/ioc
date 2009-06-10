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
using App.Shell.Presenters;
using App.Shell.Views;
using Infrastructure.Interface.Constants;

namespace App.Shell
{
    public partial class ContainerForm : Form
    {
        public ContainerForm()
        {
            DoModuleDump();

            // build up our presenters
            RootWorkItem.Services.AddNew<HomePresenter>();

            InitializeComponent();

            // show our first view
            workspace.Show(RootWorkItem.Items.AddNew<HomeView>(ViewNames.Home));
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