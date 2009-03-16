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
    public partial class SettingsForm : Form
    {
        [EventPublication("Settings Changed")]
        public event EventHandler SettingsChanged;

        public SettingsForm()
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

        private void raiseEvent_Click(object sender, EventArgs e)
        {
            SettingsChanged(this, null);
        }
    }
}