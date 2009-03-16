using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenNETCF.IoC;

namespace IoCSample
{
    public partial class DataEntryForm : Form
    {
        private int m_changeCount = 0;

        public DataEntryForm()
        {
            InitializeComponent();
        }

        [ServiceDependency(EnsureExists = true)]
        Configuration Configuration { get; set; }

        private void backToMenu_Click(object sender, EventArgs e)
        {
            RootWorkItem.Items.Get<MenuForm>("menu").Show();
            this.Hide();
        }

        [EventSubscription("Settings Changed", ThreadOption.Caller)]
        public void OnSettingsChanged(object sender, EventArgs args)
        {
            m_changeCount++;
            changeCount.Text = string.Format("Settings have Changed {0} times", m_changeCount);
        }
    }
}