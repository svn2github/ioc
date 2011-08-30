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
    public partial class Step3 : SmartPart
    {
        WizardPresenter Presenter { get; set; }

        [InjectionConstructor]
        public Step3([ServiceDependency]WizardPresenter presenter)
        {
            InitializeComponent();

            Presenter = presenter;

            optionA.DataBindings.Add("Checked", Presenter, "OptionAEnabled");
            optionB.DataBindings.Add("Checked", Presenter, "OptionBEnabled");
            optionC.DataBindings.Add("Checked", Presenter, "OptionCEnabled");
            optionD.DataBindings.Add("Checked", Presenter, "OptionDEnabled");
        }
    }
}
