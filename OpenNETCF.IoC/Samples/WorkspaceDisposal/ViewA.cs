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

namespace WorkspaceDisposal
{
    public partial class ViewA : SmartPart
    {
        public ViewA()
        {
            InitializeComponent();

            this.VisibleChanged += new EventHandler<OpenNETCF.IoC.GenericEventArgs<bool>>(ViewA_VisibleChanged);
        }

        void ViewA_VisibleChanged(object sender, OpenNETCF.IoC.GenericEventArgs<bool> e)
        {
            Debug.WriteLine("Visible = " + e.Value.ToString());
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
