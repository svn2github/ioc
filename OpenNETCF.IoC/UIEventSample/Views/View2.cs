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
    public partial class View2 : SmartPart
    {
        public View2()
        {
            InitializeComponent();
            this.Name = "View2";
        }

        public override void OnActivated()
        {
            Debug.WriteLine("View2.OnActivated()");
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            Debug.WriteLine(string.Format("View2.OnVisibleChanged({0})", this.Visible));

            base.OnVisibleChanged(e);
        }

        public override void OnDeactivated()
        {
            Debug.WriteLine("View2.OnDeactivated()");
        }
    }
}
