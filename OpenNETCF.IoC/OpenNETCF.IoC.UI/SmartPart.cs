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
        public IWorkspace Workspace { get; set; }

        public SmartPart()
        {
#if !WindowsCE
            this.SuspendLayout();
            // 
            // SmartPart
            // 
            this.DoubleBuffered = true;
            this.Name = "SmartPart";
            this.Size = new System.Drawing.Size(171, 169);
            this.ResumeLayout(false);
#endif
        }

        public virtual void OnActivated() 
        { 
        }
        
        public virtual void OnDeactivated() 
        { 
        }
        
        public new void Hide()
        {
            var wasVisible = this.Visible;

            SetVisibleCore(false);

            if (wasVisible)
            {
                OnDeactivated();
            }
        }

        public new void Show()
        {
            var wasVisible = this.Visible;

            SetVisibleCore(true);

            if (!wasVisible)
            {
                OnActivated();
            }
        }

        public new bool Visible
        {
            get { return base.Visible; }
            set
            {
                if (Visible == value) return;

                SetVisibleCore(value);

                if (value)
                {
                    OnActivated();
                }
                else
                {
                    OnDeactivated();
                }
            }
        }

#if WindowsCE
        private void SetVisibleCore(bool value)
        {
            if(value)
            {
                base.Show();
            }
            else
            {
                base.Hide();
            }
        }
#endif
    }
}
