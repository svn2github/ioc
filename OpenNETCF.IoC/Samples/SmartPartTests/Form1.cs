using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenNETCF.IoC;
using SmartPartTests.SmartParts;
using System.Diagnostics;
using SmartPartTests.Presenters;

namespace SmartPartTests
{
    public partial class Form1 : Form
    {
        private int Step { get; set; }
        private Presenter Presenter { get; set; }

        public Form1()
        {
            Step = 1;

            InitializeComponent();

            // add the designer-generated Workspace to the IoC container
            RootWorkItem.Workspaces.Add(workspace, WorkspaceNames.Main);

            workspace.SmartPartActivated += new EventHandler<DataEventArgs<OpenNETCF.IoC.UI.ISmartPart>>(workspace_SmartPartActivated);
            workspace.SmartPartClosing += new EventHandler<DataEventArgs<OpenNETCF.IoC.UI.ISmartPart>>(workspace_SmartPartClosing);
            
            Presenter = RootWorkItem.Items.AddNew<Presenter>();

            UpdateAction();
            action.Click += new EventHandler(action_Click);
        }

        void workspace_SmartPartClosing(object sender, DataEventArgs<OpenNETCF.IoC.UI.ISmartPart> e)
        {
            Debug.WriteLine(string.Format("SmartPart '{0}' Closing", e.Data.Name));
        }

        void workspace_SmartPartActivated(object sender, DataEventArgs<OpenNETCF.IoC.UI.ISmartPart> e)
        {
            Debug.WriteLine(string.Format("SmartPart '{0}' Activated", e.Data.Name));
        }

        void action_Click(object sender, EventArgs e)
        {
            Step = Presenter.DoAction(Step);
            UpdateAction();
        }

        private void UpdateAction()
        {
            action.Text = "Step " + Step.ToString();
        }
    }

    public static class WorkspaceNames
    {
        public const string Main = "wks:mainWorkspace";
    }

    public static class EventNames
    {
        public const string CloseSmartPart = "evt:SmartPartCloseRequested";
    }
}