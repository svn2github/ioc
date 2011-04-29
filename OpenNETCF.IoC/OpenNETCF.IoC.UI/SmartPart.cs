// LICENSE
// -------
// This software was originally authored by Christopher Tacke of OpenNETCF Consulting, LLC
// On March 10, 2009 is was placed in the public domain, meaning that all copyright has been disclaimed.
//
// You may use this code for any purpose, commercial or non-commercial, free or proprietary with no legal 
// obligation to acknowledge the use, copying or modification of the source.
//
// OpenNETCF will maintain an "official" version of this software at www.opennetcf.com and public 
// submissions of changes, fixes or updates are welcomed but not required
//

using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace OpenNETCF.IoC.UI
{
    public class SmartPart : UserControl, ISmartPart
    {
        public new event EventHandler<GenericEventArgs<bool>> VisibleChanged;

        public IWorkspace Workspace { get; set; }

        public virtual void OnActivated() 
        { 
        }
        
        public virtual void OnDeactivated() 
        { 
        }

        private void RaiseVisibleChanged(bool visible)
        {
            var handler = VisibleChanged;
            if(handler == null) return;
            
            VisibleChanged(this, new GenericEventArgs<bool>(visible));
        }

        public new void Hide()
        {
            base.Hide();
            RaiseVisibleChanged(Visible);
        }

        public new void Show()
        {
            base.Show();
            RaiseVisibleChanged(Visible);
        }

        public new bool Visible
        {
            get { return base.Visible; }
            set
            {
                if (Visible == value) return;

                base.Visible = value;
                RaiseVisibleChanged(value);
            }
        }

        private void InitializeComponent()
        {
#if !WindowsCE
            this.SuspendLayout();
            // 
            // SmartPart
            // 
            this.DoubleBuffered = true;
            this.Name = "SmartPart";
            this.ResumeLayout(false);

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
#endif
        }

    }
}
