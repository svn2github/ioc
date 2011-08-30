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
using BasicWizard.Presenters;

namespace BasicWizard.Views
{
    public partial class Step2 : SmartPart
    {
        [ServiceDependency]
        WizardPresenter Presenter { get; set; }

        public Step2()
        {
            InitializeComponent();

            express.CheckedChanged += new EventHandler(express_CheckedChanged);
            custom.CheckedChanged += new EventHandler(custom_CheckedChanged);
        }

        void custom_CheckedChanged(object sender, EventArgs e)
        {
            if (custom.Checked)
            {
                Presenter.UseDefaults = false;
            }
        }

        void express_CheckedChanged(object sender, EventArgs e)
        {
            if (express.Checked)
            {
                Presenter.UseDefaults = true;
            }
        }
    }
}
