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
    public partial class Step4 : SmartPart
    {
        [ServiceDependency]
        WizardPresenter Presenter { get; set; }

        public Step4()
        {
            InitializeComponent();

            selectedOptions.KeyDown += new KeyEventHandler(selectedOptions_KeyDown);
        }

        public override void OnActivated()
        {
            Update();
        }

        public void Update()
        {
            var optionsText = new StringBuilder();
            if (Presenter.OptionAEnabled) optionsText.AppendLine("Option A");
            if (Presenter.OptionBEnabled) optionsText.AppendLine("Option B");
            if (Presenter.OptionCEnabled) optionsText.AppendLine("Option C");
            if (Presenter.OptionDEnabled) optionsText.AppendLine("Option D");

            selectedOptions.Text = optionsText.ToString();
        }

        void selectedOptions_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }
    }
}
