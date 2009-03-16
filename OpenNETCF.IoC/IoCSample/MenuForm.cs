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
    public partial class MenuForm : Form
    {
        private DataEntryForm m_entryForm;
        private SettingsForm m_settingsForm;

        [InjectionConstructor]
        public MenuForm([CreateNew]DataEntryForm entryForm, [CreateNew]SettingsForm settingsForm)
        {
            m_entryForm = entryForm;
            m_settingsForm = settingsForm;
            InitializeComponent();
        }

        [ServiceDependency(EnsureExists=true)]
        Configuration Configuration { get; set; }

        private void showDataEntry_Click(object sender, EventArgs e)
        {
            m_entryForm.Show();
        }

        private void showSettings_Click(object sender, EventArgs e)
        {
            m_settingsForm.Show();
        }

        private void loadConfig_Click(object sender, EventArgs e)
        {
            Configuration config = RootWorkItem.Services.Get<Configuration>();
            // use the config here
        }
    }
}