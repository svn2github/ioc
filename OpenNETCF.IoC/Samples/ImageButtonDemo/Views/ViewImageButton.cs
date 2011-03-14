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
    public partial class ViewImageButton : ViewBase
    {
        private static int m_count;
        private static string m_typeName;

        private Bitmap toggleOn;
        private Bitmap toggleOff;

        public ViewImageButton()
        {
            InitializeComponent();

            toggleOn = new
                Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("ImageButtonDemo.Resources.Admin_ClockOn.png"));
            toggleOff = new
                Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("ImageButtonDemo.Resources.Admin_ClockOff.png"));

            imgButtonToggle.UpImage = imgButtonToggle.DownImage = toggleOff;
            imgButtonToggle.Tag = false;

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

        private void pushGradient_Click(object sender, EventArgs e)
        {
            Stack.Push<ViewGradient>();
        }

        private void pushMulti_Click(object sender, EventArgs e)
        {
            Stack.Push<ViewMulti>();
        }

        private void imgButtonLeft_Click(object sender, EventArgs e)
        {
            if ((bool)imgButtonToggle.Tag)
            {
                imgButtonToggle.UpImage = imgButtonToggle.DownImage = toggleOff;
            }
            else
            {
                imgButtonToggle.UpImage = imgButtonToggle.DownImage = toggleOn;
            }

            imgButtonToggle.Tag = !(bool)imgButtonToggle.Tag;
        }
    }
}
