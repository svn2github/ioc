using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenNETCF.IoC;
using TabGestureSample.Views;
using OpenNETCF.IoC.UI;
using TabGestureSample.Presenters;

namespace TabGestureSample
{
    public partial class MainForm : Form
    {
        private SmartPart m_start;
        private SmartPart m_mid;
        private SmartPart m_end;

        public MainForm()
        {
            InitializeComponent();

            // register workspaces
            RootWorkItem.Workspaces.Add(mainWorkspace);

            mainWorkspace.GesturesEnabled = true;
            mainWorkspace.GestureReceived += new OpenNETCF.IoC.UI.GestureHandler(mainWorkspace_GestureReceived);

            // register presenters
            RootWorkItem.Services.AddNew<MyPresenter>();

            // show the initial View
            m_start = mainWorkspace.Show<StartView>();
        }

        void mainWorkspace_GestureReceived(OpenNETCF.IoC.UI.IWorkspace workspace, OpenNETCF.IoC.UI.GestureDirection direction)
        {
            switch(direction)
            {
                case OpenNETCF.IoC.UI.GestureDirection.Right:
                    PreviousTab();
                    break;
                case OpenNETCF.IoC.UI.GestureDirection.Left:
                    switch(mainWorkspace.TabPages.Count)
                    {
                        case 1:
                            m_mid = mainWorkspace.Show<MidView>();
                            break;
                        case 2:
                            m_end = mainWorkspace.Show<EndView>();
                            break;
                        case 3:
                            NextTab();
                            break;
                    }

                    break;
            }
        }

        private void NextTab()
        {
            switch (mainWorkspace.SelectedIndex)
            {
                case 0:
                    mainWorkspace.Show(m_mid);
                    break;
                case 1:
                    mainWorkspace.Show(m_end);
                    break;
            }
        }

        private void PreviousTab()
        {
            switch (mainWorkspace.SelectedIndex)
            {
                case 2:
                    mainWorkspace.Show(m_mid);
                    break;
                case 1:
                    mainWorkspace.Show(m_start);
                    break;
            }
        }
    }
}
