using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenNETCF.IoC;

namespace MenuIntegration
{
    public partial class Container : Form
    {
        public Container()
        {
            InitializeComponent();

            // add the workspaces to the Items collection so other objects can get and use them through the DI container
            RootWorkItem.Items.Add(workspace, WorkspaceNames.Main);

            // add the menu to the Items collection so the SmartParts can use it
            RootWorkItem.Items.Add(mainMenu, ItemNames.Menu);

            // create a couple of SmartParts
            var sp1 = new MySmartPart("Goto B", "A", SmartPartNames.B);
            RootWorkItem.SmartParts.Add(sp1, SmartPartNames.A);
            var sp2 = new MySmartPart("Goto C", "B", SmartPartNames.C);
            RootWorkItem.SmartParts.Add(sp2, SmartPartNames.B);
            var sp3 = new MySmartPart("Goto A", "C", SmartPartNames.A);
            RootWorkItem.SmartParts.Add(sp3, SmartPartNames.C);

            // show the first
            workspace.Show(sp1);
        }
    }
}