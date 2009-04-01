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

namespace MVPSample
{
    public partial class Settings : UserControl, ISmartPart
    {
        [ServiceDependency(EnsureExists = true)]
        private SettingsPresenter Presenter { get; set; }

        public Settings()
        {
            InitializeComponent();

            backToMenu.Click += new EventHandler(backToMenu_Click);
        }

        void backToMenu_Click(object sender, EventArgs e)
        {
            Presenter.ShowMenuView();
        }
    }
}
