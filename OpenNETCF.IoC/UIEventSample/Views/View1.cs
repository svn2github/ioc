using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenNETCF.IoC.UI;
using System.Diagnostics;

namespace UIEventSample.Views
{
    public partial class View1 : SmartPart
    {
        public View1()
        {
            InitializeComponent();
            this.Name = "View1";
        }

        public override void OnActivated()
        {
            Debug.WriteLine("View1.OnActivated()");
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            Debug.WriteLine(string.Format("View1.OnVisibleChanged({0})", this.Visible));

            base.OnVisibleChanged(e);
        }

        public override void OnDeactivated()
        {
            Debug.WriteLine("View1.OnDeactivated()");
        }
    }
}
