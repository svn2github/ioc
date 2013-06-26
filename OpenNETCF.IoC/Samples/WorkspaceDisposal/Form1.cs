using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WorkspaceDisposal.FFx.VS10;

namespace WorkspaceDisposal
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void showForm_Click(object sender, EventArgs e)
        {
            Form f = new Form2();
            f.Show();
        }

        UserControl1 m_ctl;

        private void button1_Click(object sender, EventArgs e)
        {
            if (m_ctl == null)
            {
                m_ctl = new UserControl1();
                m_ctl.Visible = true;
                this.Controls.Add(m_ctl);
            }
            else
            {
                this.Controls.Remove(m_ctl);
                m_ctl.Dispose();
                m_ctl = null;
                GC.Collect();
            }
        }
    }
}