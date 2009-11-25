using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenNETCF.IoC;
using OpenNETCF.IoC.UI;

namespace FormStackCS
{
    public partial class ContainerForm : Form
    {
        public ContainerForm()
        {
            InitializeComponent();

            // add the workspaces to the Items collection so other objects can get and use them through the DI container
            RootWorkItem.Items.Add(workspace, WorkspaceNames.StackWorkspace);

            // create the stack and put it somewhere anyone can reach it
            var stack = new FormStack(workspace);
            RootWorkItem.Items.Add(stack, ItemNames.ViewStack);

            // create and show the initial view
            stack.Push<ViewA>();
        }
    }
}