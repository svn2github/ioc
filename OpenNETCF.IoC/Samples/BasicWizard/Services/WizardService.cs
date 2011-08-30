using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using OpenNETCF.IoC.UI;
using BasicWizard.Views;
using OpenNETCF.IoC;
using BasicWizard.Presenters;
using System.Windows.Forms;
using BasicWizard.Constants;
using BasicWizard.Entities;

namespace BasicWizard.Services
{
    class WizardService
    {
        private bool m_useDefaults;

        private WizardStep CurrentStep { get; set; }
        public SetupOptions Options { get; private set; }

        [ServiceDependency]
        WizardPresenter Presenter { get; set; }
        public Workspace Workspace { get; set; }

        public WizardService()
        {
            CurrentStep = WizardStep.None;
            UseDefaultSettings = true;
        }

        public bool UseDefaultSettings 
        {
            get { return m_useDefaults; }
            set
            {
                if (value == UseDefaultSettings) return;

                m_useDefaults = value;
                if (UseDefaultSettings)
                {
                    Options = SetupOptions.Default;
                }
            }
        }

        public void Start()
        {
            if (Workspace == null) return;

            Presenter.BackwardAllowed = false;
            Presenter.ForwardAllowed = false;
            CurrentStep = WizardStep.Step1;

            Workspace.Show<Step1>();
        }

        public bool Quit()
        {
            return (MessageBox.Show(
                    "Exit Wizard?", 
                    "Quit?", 
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Question, 
                    MessageBoxDefaultButton.Button2) 
                == DialogResult.Yes);
        }

        public void Restart()
        {
            SetStep(WizardStep.Step1);
            Workspace.Show<Step1>();
        }

        public void Back()
        {
            switch (CurrentStep)
            {
                case WizardStep.Step2:
                    SetStep(WizardStep.Step1);
                    Workspace.Show<Step1>();
                    break;
                case WizardStep.Step3:
                    SetStep(WizardStep.Step2);
                    Workspace.Show<Step2>();
                    break;
                case WizardStep.Step4:
                    if (UseDefaultSettings)
                    {
                        SetStep(WizardStep.Step2);
                        Workspace.Show<Step2>();
                    }
                    else
                    {
                        SetStep(WizardStep.Step3);
                        Workspace.Show<Step3>();
                    }
                    break;
            }
        }

        public void Next()
        {
            switch (CurrentStep)
            {
                case WizardStep.Step1:
                    SetStep(WizardStep.Step2);
                    Workspace.Show<Step2>();
                    break;
                case WizardStep.Step2:
                    if (UseDefaultSettings)
                    {
                        SetStep(WizardStep.Step4);
                        Workspace.Show<Step4>();
                    }
                    else
                    {
                        SetStep(WizardStep.Step3);
                        Workspace.Show<Step3>();
                    }
                    break;
                case WizardStep.Step3:
                    SetStep(WizardStep.Step4);
                    Workspace.Show<Step4>();
                    break;
                case WizardStep.Step4:
                    CompleteWizard();
                    break;
            }
        }

        private void SetStep(WizardStep newStep)
        {
            switch (newStep)
            {
                case WizardStep.Step1:
                    Presenter.RestartAllowed = true;
                    Presenter.BackwardAllowed = false;
                    Presenter.ForwardAllowed = true;
                    Presenter.LastStep = false;
                    break;
                case WizardStep.Step2:
                    Presenter.RestartAllowed = true;
                    Presenter.BackwardAllowed = true;
                    Presenter.ForwardAllowed = true;
                    Presenter.LastStep = false;
                    break;
                case WizardStep.Step3:
                    Presenter.RestartAllowed = true;
                    Presenter.BackwardAllowed = true;
                    Presenter.ForwardAllowed = true;
                    Presenter.LastStep = false;
                    break;
                case WizardStep.Step4:
                    Presenter.RestartAllowed = true;
                    Presenter.BackwardAllowed = true;
                    Presenter.ForwardAllowed = true;
                    Presenter.LastStep = true;
                    break;
            }

            CurrentStep = newStep;
        }

        private void CompleteWizard()
        {
            var message = new StringBuilder();
            message.AppendLine("Selected options:");
            message.AppendLine("Option A: " + Options.OptionA.ToString());
            message.AppendLine("Option B: " + Options.OptionB.ToString());
            message.AppendLine("Option C: " + Options.OptionC.ToString());
            message.AppendLine("Option D: " + Options.OptionD.ToString());

            MessageBox.Show(message.ToString(), "Setup Complete");

            Application.Exit();
        }
    }
}
