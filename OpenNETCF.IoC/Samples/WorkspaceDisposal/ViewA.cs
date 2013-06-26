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
            textBox1.Text = "Foo";
        }

        ~ViewA()
        {
            Debug.WriteLine("Finalized");
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            Debug.WriteLine("Keypress");
            base.OnKeyPress(e);
        }

        public override void OnActivated()
        {
            textBox1.Focus();
            textBox1.SelectAll();
            Debug.WriteLine("OnActivated");
            base.OnActivated();
        }

        public override void OnDeactivated()
        {
            textBox2.Focus();
            Debug.WriteLine("OnDeactivated");
            base.OnDeactivated();
        }
    }
}
