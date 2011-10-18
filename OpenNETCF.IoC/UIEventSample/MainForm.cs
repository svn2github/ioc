using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenNETCF.IoC;
using UIEventSample.Views;
using System.Diagnostics;
using OpenNETCF.IoC.UI;

namespace UIEventSample
{
    public partial class MainForm : Form
    {
        private const int ViewTypeView1 = 0;
        private const int ViewTypeView2 = 1;
        private const int ActivationTypeShow = 0;
        private const int ActivationTypeActivate = 1;
        private const int WorkspaceTypeDeck = 0;
        private const int WorkspaceTypeTab = 1;

        private Workspace m_activeWorkspace;

        public MainForm()
        {
            InitializeComponent();

            viewType.Items.Add("View1");
            viewType.Items.Add("View2");

            viewType.SelectedIndexChanged += viewType_SelectedIndexChanged;

            workspaceType.Items.Add("Deck");
            workspaceType.Items.Add("Tab");

            workspaceType.SelectedIndexChanged += workspaceType_SelectedIndexChanged;

            activation.Items.Add("Show");
            activation.Items.Add("Activate");
            activation.SelectedIndex = ActivationTypeShow;

        }

        void workspaceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (workspaceType.SelectedIndex)
            {
                case WorkspaceTypeDeck:
                    m_activeWorkspace = deckWorkspace;
                    tabWorkspace.Visible = false;
                    deckWorkspace.Visible = true;
                    break;
                case WorkspaceTypeTab:
                    m_activeWorkspace = tabWorkspace;
                    deckWorkspace.Visible = false;
                    tabWorkspace.Visible = true;
                    break;
            }
        }

        void viewType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_activeWorkspace == null) return;

            Debug.WriteLine("--------");

            switch (viewType.SelectedIndex)
            {
                case ViewTypeView1:
                    switch (activation.SelectedIndex)
                    {
                        case ActivationTypeShow:
                            m_activeWorkspace.Show<View1>();
                            break;
                        case ActivationTypeActivate:
                            var view = m_activeWorkspace.SmartParts.FirstOrDefault(v => v is View1);
                            if (view == null)
                            {
                                view = RootWorkItem.SmartParts.AddNew<View1>();
                            }
                            m_activeWorkspace.Activate(view);
                            break;
                    }
                    break;
                case ViewTypeView2:
                    switch (activation.SelectedIndex)
                    {
                        case ActivationTypeShow:
                            m_activeWorkspace.Show<View2>();
                            break;
                        case ActivationTypeActivate:
                            var view = m_activeWorkspace.SmartParts.FirstOrDefault(v => v is View2);
                            if (view == null)
                            {
                                view = RootWorkItem.SmartParts.AddNew<View2>();
                            }
                            m_activeWorkspace.Activate(view);
                            break;
                    }
                    break;
            }
        }
    }
}
