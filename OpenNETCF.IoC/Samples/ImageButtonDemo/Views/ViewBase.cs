using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using OpenNETCF.IoC.UI;
using OpenNETCF.IoC;
using System.Diagnostics;

namespace ImageButtonDemo
{
    // Note: Ideally this class *should* be declared abstract, but the VS08 designer won't render anything derived from it if it is
    public /*abstract*/ partial class ViewBase : SmartPart
    {
        protected ImageButtonFormStack Stack { get; set; }

        public ViewBase()
        {
            InitializeComponent();

            // set the text so we know what we're looking at
            this.Name = viewName.Text = this.GetType().Name;

            // get a reference to the stack so we can use it when needed
            Stack = RootWorkItem.Items.Get<ImageButtonFormStack>(ItemNames.ViewStack);
        }

        public override void OnActivated()
        {
            base.OnActivated();
        }

        private void back_Click(object sender, EventArgs e)
        {
            Stack.Back();
        }

        private void forward_Click(object sender, EventArgs e)
        {
            Stack.Forward();
        }
    }
}
