using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;

namespace ImageButtonDemo
{
    public partial class ViewCheckbox : ViewBase
    {
        private static int m_count;
        private static string m_typeName;
        private Bitmap checkedImg;
        private Bitmap uncheckedImg;

        public ViewCheckbox()
        {
            InitializeComponent();

            checkedImg = new
                Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("ImageButtonDemo.Resources.CheckBoxChecked.png"));
            uncheckedImg = new
                Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("ImageButtonDemo.Resources.CheckBoxClear.png"));

            chkOne.Tag = chkTwo.Tag = false;

            if (m_typeName == null) m_typeName = this.GetType().Name;
            InstanceCount++;
        }

        protected static int InstanceCount
        {
            get { return m_count; }
            set
            {
                m_count = value;
                Debug.WriteLine(string.Format("There are now {0} instances of {1}", InstanceCount, m_typeName));
            }
        }

        private void pushImgButton_Click(object sender, EventArgs e)
        {
            Stack.Push<ViewImageButton>();
        }

        private void pushGradient_Click(object sender, EventArgs e)
        {
            Stack.Push<ViewGradient>();
        }

        private void chkOne_Click(object sender, EventArgs e)
        {
            if ((bool)chkOne.Tag)
            {
                chkOne.UpImage = chkOne.DownImage = uncheckedImg;
            }
            else
            {
                chkOne.UpImage = chkOne.DownImage = checkedImg;
            }

            chkOne.Tag = !(bool)chkOne.Tag;
            label1.Text = String.Format("Checkbox 1: {0}", (bool)chkOne.Tag ? "Checked" : "Unchecked");
        }

        private void chkTwo_Click(object sender, EventArgs e)
        {
            if ((bool)chkTwo.Tag)
            {
                chkTwo.UpImage = chkTwo.DownImage = uncheckedImg;
            }
            else
            {
                chkTwo.UpImage = chkTwo.DownImage = checkedImg;
            }

            chkTwo.Tag = !(bool)chkTwo.Tag;
            label2.Text = String.Format("Checkbox 2: {0}", (bool)chkTwo.Tag ? "Checked" : "Unchecked");
        }
    }
}
