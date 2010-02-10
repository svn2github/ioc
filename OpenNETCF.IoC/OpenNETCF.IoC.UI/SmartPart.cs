﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace OpenNETCF.IoC.UI
{
    public class SmartPart : UserControl, ISmartPart
    {
        public event EventHandler<GenericEventArgs<bool>> VisibleChanged;

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
                base.Visible = value;
                RaiseVisibleChanged(Visible);
            }
        }

    }
}
