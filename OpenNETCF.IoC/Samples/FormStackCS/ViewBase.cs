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

namespace FormStackCS
{
    public abstract partial class ViewBase : SmartPart
    {
        protected FormStack Stack { get; set; }

        public ViewBase()
        {
            InitializeComponent();

            // set the text so we know what we're looking at
            this.Name = viewName.Text = this.GetType().Name;

            // get a reference to the stack so we can use it when needed
            Stack = RootWorkItem.Items.Get<FormStack>(ItemNames.ViewStack);
        }

        public override void OnActivated()
        {
            base.OnActivated();

            RefreshStackList();
        }

        protected void RefreshStackList()
        {
            // show the stack
            this.stackList.Items.Clear();

            for (int i = 0; i < Stack.Count; i++)
            {
                string item = (i == Stack.CurrentPosition)
                    ? "*" + Stack[i].Name
                    : Stack[i].Name;

                stackList.Items.Add(item);
            }
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
