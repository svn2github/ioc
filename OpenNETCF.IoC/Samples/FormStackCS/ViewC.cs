using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace FormStackCS
{
    public partial class ViewC : ViewBase
    {
        private static int m_count;
        private static string m_typeName;

        public ViewC()
        {
            InitializeComponent();

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

        private void pushA_Click(object sender, EventArgs e)
        {
            Stack.Push<ViewA>();
        }

        private void pushB_Click(object sender, EventArgs e)
        {
            Stack.Push<ViewB>();
        }
    }
}
