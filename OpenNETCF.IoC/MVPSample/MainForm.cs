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
using System.Diagnostics;

namespace MVPSample
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            // add the workspace to the RootWorkItem
            RootWorkItem.Items.Add(mainWorkspace, WorkspaceNames.MainWorkspace);

            mainWorkspace.SmartPartActivated += new EventHandler<DataEventArgs<ISmartPart>>(mainWorkspace_SmartPartActivated);
            mainWorkspace.SmartPartClosing += new EventHandler<DataEventArgs<ISmartPart>>(mainWorkspace_SmartPartClosing);
            
            // create an show the menu
            ISmartPart menuView = RootWorkItem.Items.AddNew<Menu>(ViewNames.MenuView);
            this.mainWorkspace.Show(menuView);
        }

        void mainWorkspace_SmartPartClosing(object sender, DataEventArgs<ISmartPart> e)
        {
            throw new NotImplementedException();
        }

        void mainWorkspace_SmartPartActivated(object sender, DataEventArgs<ISmartPart> e)
        {
            Debug.WriteLine(string.Format("Smart Part Activated. Type = {0}  Name = {1}", e.Data.GetType().Name, e.Data.Name));
        }
    }
}