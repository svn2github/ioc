using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using OpenNETCF.IoC.UI;
using OpenNETCF.IoC;

namespace MVPSample
{
    public partial class Menu : UserControl, ISmartPart
    {
        [ServiceDependency(EnsureExists=true)]
        private MenuPresenter Presenter { get; set; }

        public Menu()
        {
            InitializeComponent();
            loadConfig.Click += new EventHandler(loadConfig_Click);
            showSettings.Click += new EventHandler(showSettings_Click);
            showDataEntry.Click += new EventHandler(showDataEntry_Click);
        }

        private void loadConfig_Click(object sender, EventArgs e)
        {
            Presenter.LoadConfiguration();
        }

        private void showSettings_Click(object sender, EventArgs e)
        {
            Presenter.ShowSettingsView();
        }

        private void showDataEntry_Click(object sender, EventArgs e)
        {
            Presenter.ShowEntryView();
        }
    }

    public class Presenter<TView> : IPresenter<TView>
        where TView : ISmartPart
    {
        protected IWorkspace Workspace { get; set; }
        protected TView View { get; set; }

        public Presenter(TView view, IWorkspace workspace)
        {
            Workspace = workspace;
            View = view;
        }

        public void Run()
        {
            Workspace.Show(View);
        }

        protected void ShowView(Type viewType, string viewName)
        {
            ISmartPart view;
            if (RootWorkItem.Items.Contains(viewName))
            {
                view = RootWorkItem.Items.Get(viewName) as ISmartPart;
            }
            else
            {
                view = RootWorkItem.Items.AddNew(viewType, viewName) as ISmartPart;
            }

            Workspace.Show(view);
        }
    }
}
