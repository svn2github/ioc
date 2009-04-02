using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace OpenNETCF.IoC.UI
{
    public abstract class Workspace : ContainerControl, IWorkspace
    {
        public event EventHandler<DataEventArgs<ISmartPart>> SmartPartActivated;
        public event EventHandler<DataEventArgs<ISmartPart>> SmartPartClosing;

        public Workspace()
        {
            SmartParts = new SmartPartCollection();
        }

        public SmartPartCollection SmartParts { get; private set; }

        public void Show(ISmartPart smartPart)
        {
            OnShow(smartPart);
        }

        protected virtual void OnShow(ISmartPart smartPart)
        {
            Control control = smartPart as Control;
            if (control == null) throw new ArgumentException("smartPart must be a Control");

            if (!SmartParts.Contains(smartPart))
            {
                SmartParts.Add(smartPart);
                this.Controls.Add(control);
            }
            Activate(smartPart);
        }

        public void Hide(ISmartPart smartPart)
        {
            CheckSmartPartExists(smartPart);
            OnHide(smartPart);
        }

        protected virtual void OnHide(ISmartPart smartPart)
        {
            smartPart.Visible = false;
        }

        public void Close(ISmartPart smartPart)
        {
            CheckSmartPartExists(smartPart);

            RaiseSmartPartClosing(smartPart);

            OnClose(smartPart);
        }

        protected virtual void OnClose(ISmartPart smartPart)
        {
            Control control = smartPart as Control;
            if (control == null) throw new ArgumentException("smartPart must be a Control");
            this.Controls.Remove(control);
            SmartParts.Remove(smartPart);
            smartPart.Dispose();
        }

        protected void RaiseSmartPartClosing(ISmartPart smartPart)
        {
            if (SmartPartClosing == null) return;

            SmartPartClosing(this, new DataEventArgs<ISmartPart>(smartPart));
        }

        public void Activate(ISmartPart smartPart)
        {
            CheckSmartPartExists(smartPart);

            OnActivate(smartPart);

            RaiseSmartPartActivated(smartPart);
        }

        protected virtual void OnActivate(ISmartPart smartPart)
        {
            smartPart.Visible = true;
            smartPart.BringToFront();
            smartPart.Focus();
        }

        protected void RaiseSmartPartActivated(ISmartPart smartPart)
        {
            if (SmartPartActivated == null) return;

            SmartPartActivated(this, new DataEventArgs<ISmartPart>(smartPart));
        }

        public ISmartPart ActiveSmartPart
        {
            get
            {
                return SmartParts.FirstOrDefault(c => c.Focused == true);
            }
        }

        private void CheckSmartPartExists(ISmartPart smartPart)
        {
            if (!SmartParts.Contains(smartPart))
            {
                throw new Exception("ISmartPart not in Workspace");
            }
        }
    }
}
