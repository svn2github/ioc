using System;
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
                if (Visible == value) return;

                base.Visible = value;
                RaiseVisibleChanged(Visible);
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
