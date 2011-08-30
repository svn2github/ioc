using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using OpenNETCF.IoC.UI;
using OpenNETCF.IoC;
using BasicWizard.Presenters;
using BasicWizard.Services;

namespace BasicWizard
{
    public class Program : SmartClientApplication<WizardForm>
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            new Program().Start();
        }

        public override void AddServices()
        {
            RootWorkItem.Services.AddNew<WizardPresenter>();
            RootWorkItem.Services.AddNew<WizardService>();
        }
    }
}