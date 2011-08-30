using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using BasicWizard.Services;
using OpenNETCF.IoC;

namespace BasicWizard.Presenters
{
    public class WizardPresenter : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool m_forwardAllowed;
        private bool m_backwardAllowed;
        private bool m_restartAllowed;

        public bool LastStep { get; set; }

        private WizardService WizardService
        {
            get { return RootWorkItem.Services.Get<WizardService>(); }
        }

        public bool ForwardAllowed
        {
            get { return m_forwardAllowed; }
            set
            {
                m_forwardAllowed = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ForwardAllowed"));
            }
        }

        public bool BackwardAllowed
        {
            get { return m_backwardAllowed; }
            set
            {
                m_backwardAllowed = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("BackwardAllowed"));
            }
        }

        public bool RestartAllowed
        {
            get { return m_restartAllowed; }
            set
            {
                m_restartAllowed = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("RestartAllowed"));
            }
        }

        public bool UseDefaults
        {
            get { return WizardService.UseDefaultSettings; }
            set { WizardService.UseDefaultSettings = value; }
        }

        public bool OptionAEnabled
        {
            get { return WizardService.Options.OptionA; }
            set { WizardService.Options.OptionA = value; }
        }

        public bool OptionBEnabled
        {
            get { return WizardService.Options.OptionB; }
            set { WizardService.Options.OptionB = value; }
        }

        public bool OptionCEnabled
        {
            get { return WizardService.Options.OptionC; }
            set { WizardService.Options.OptionC = value; }
        }
        
        public bool OptionDEnabled
        {
            get { return WizardService.Options.OptionD; }
            set { WizardService.Options.OptionD = value; }
        }
    }
}
