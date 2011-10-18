using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using OpenNETCF.IoC.UI;
using System.Diagnostics;
using OpenNETCF;

namespace WorkspaceDisposal
{
    public partial class ViewA : SmartPart
    {
        public ViewA()
        {
            InitializeComponent();

//            this.VisibleChanged += ViewA_VisibleChanged;
        }

        void ViewA_VisibleChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("Visible = " + this.Visible.ToString());
        }

        public override void OnActivated()
        {
            Debug.WriteLine("OnActivated");
            base.OnActivated();
        }

        public override void OnDeactivated()
        {
            Debug.WriteLine("OnDeactivated");
            base.OnDeactivated();
        }
    }
}
