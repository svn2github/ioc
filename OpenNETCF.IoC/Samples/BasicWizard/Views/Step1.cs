using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using OpenNETCF.IoC.UI;
using BasicWizard.Presenters;
using OpenNETCF.IoC;

namespace BasicWizard.Views
{
    public partial class Step1 : SmartPart
    {
        WizardPresenter Presenter { get; set; }

        [InjectionConstructor]
        public Step1([ServiceDependency]WizardPresenter presenter)
            : this()
        {
            Presenter = presenter;
        }

        public Step1()
        {
            InitializeComponent();
        }

        private void yes_CheckedChanged(object sender, EventArgs e)
        {
            Presenter.ForwardAllowed = true;
        }

        private void no_CheckedChanged(object sender, EventArgs e)
        {
            Presenter.ForwardAllowed = false;
        }
    }
}
