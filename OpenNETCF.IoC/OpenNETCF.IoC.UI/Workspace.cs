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
using System.Drawing;

namespace OpenNETCF.IoC.UI
{
    public class Workspace : ContainerControl, IWorkspace
    {
        public event EventHandler<DataEventArgs<ISmartPart>> SmartPartActivated;
        public event EventHandler<DataEventArgs<ISmartPart>> SmartPartDeactivated;
        public event EventHandler<DataEventArgs<ISmartPart>> SmartPartClosing;

        private SmartPartCollection m_smartParts;

        public Workspace()
        {
            m_smartParts = new SmartPartCollection();
        }

        public ISmartPartCollection SmartParts 
        {
            get { return m_smartParts; }
        }

        public void Show(ISmartPart smartPart)
        {
            this.Show(smartPart, null);
        }

        public void Show(ISmartPart smartPart, ISmartPartInfo smartPartInfo)
        {
            if (smartPart == null) throw new ArgumentNullException("smartPart");
            
            OnShow(smartPart, smartPartInfo);
        }

        protected virtual void OnShow(ISmartPart smartPart, ISmartPartInfo smartPartInfo)
        {
            if (smartPart == null) throw new ArgumentNullException("smartPart");

            Control control = smartPart as Control;
            if (control == null) throw new ArgumentException("smartPart must be a Control");

            if (!SmartParts.Contains(smartPart))
            {
                (smartPart as SmartPart).Workspace = this;
                m_smartParts.Add(smartPart);
                this.Controls.Add(control);
                Activate(smartPart);
                smartPart.VisibleChanged += new EventHandler<GenericEventArgs<bool>>(smartPart_VisibleChanged);
            }
            else
            {
                Activate(smartPart);
            }
        }

        void smartPart_VisibleChanged(object sender, GenericEventArgs<bool> e)
        {
            var smartPart = sender as ISmartPart;
            if(e.Value)
            {
                RaiseSmartPartActivated(smartPart);
                smartPart.OnActivated();
            }
            else
            {
                RaiseSmartPartDeactivated(smartPart);
                smartPart.OnDeactivated();
            }
        }

        public void Hide(ISmartPart smartPart)
        {
            if (smartPart == null) throw new ArgumentNullException("smartPart");

            CheckSmartPartExists(smartPart);
            
            OnHide(smartPart);

            RaiseSmartPartDeactivated(smartPart);
        }

        protected virtual void OnHide(ISmartPart smartPart)
        {
            if (smartPart == null) throw new ArgumentNullException("smartPart");

            Deactivate(smartPart);
        }

        public void Close(ISmartPart smartPart)
        {
            if (smartPart == null) throw new ArgumentNullException("smartPart");

            CheckSmartPartExists(smartPart);

            OnClose(smartPart);
        }

        protected virtual void OnClose(ISmartPart smartPart)
        {
            if (smartPart == null) throw new ArgumentNullException("smartPart");

            Control control = smartPart as Control;
            if (control == null) throw new ArgumentException("smartPart must be a Control");

            RaiseSmartPartDeactivated(smartPart);
            RaiseSmartPartClosing(smartPart);

            this.Controls.Remove(control);
            m_smartParts.Remove(smartPart);
            smartPart.Dispose();
        }

        protected void RaiseSmartPartClosing(ISmartPart smartPart)
        {
            if (SmartPartClosing == null) return;

            SmartPartClosing(this, new DataEventArgs<ISmartPart>(smartPart));
        }

        public void Activate(ISmartPart smartPart)
        {
            if (smartPart == null) throw new ArgumentNullException("smartPart");

            CheckSmartPartExists(smartPart);

            OnActivate(smartPart);
        }

        protected virtual void OnActivate(ISmartPart smartPart)
        {
            if (smartPart == null) throw new ArgumentNullException("smartPart");

            if (!smartPart.Visible)
            {
                smartPart.Visible = true;
            }

            smartPart.OnActivated();

            RaiseSmartPartActivated(smartPart);

            smartPart.BringToFront();
            smartPart.Focus();
        }

        public void Deactivate(ISmartPart smartPart)
        {
            if (smartPart == null) throw new ArgumentNullException("smartPart");

            CheckSmartPartExists(smartPart);

            OnDeactivate(smartPart);
        }

        protected virtual void OnDeactivate(ISmartPart smartPart)
        {
            if (smartPart == null) throw new ArgumentNullException("smartPart");

            if (smartPart.Visible)
            {
                smartPart.Visible = false;
            }
            else
            {
                smartPart.OnDeactivated();
            }
            RaiseSmartPartDeactivated(smartPart);            
        }

        protected internal void RaiseSmartPartActivated(ISmartPart smartPart)
        {
            if (SmartPartActivated == null) return;

            SmartPartActivated(this, new DataEventArgs<ISmartPart>(smartPart));
        }

        protected void RaiseSmartPartDeactivated(ISmartPart smartPart)
        {
            if (SmartPartDeactivated == null) return;

            SmartPartDeactivated(this, new DataEventArgs<ISmartPart>(smartPart));
        }

        public virtual ISmartPart ActiveSmartPart
        {
            get
            {
                return SmartParts.FirstOrDefault(c => c.Focused == true);
            }
        }

        private void CheckSmartPartExists(ISmartPart smartPart)
        {
            if (smartPart == null) throw new ArgumentNullException("smartPart");

            if (!SmartParts.Contains(smartPart))
            {
                throw new Exception("ISmartPart not in Workspace");
            }
        }

        protected void AddSmartPartToCollectionIfRequired(ISmartPart smartPart)
        {
            if (!SmartParts.Contains(smartPart))
            {
                m_smartParts.Add(smartPart);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (IsDesignTime)
            {
                base.OnPaintBackground(e);

                Color c = Color.FromArgb(69, 69, 69);
                e.Graphics.DrawRectangle(
                    new Pen(c),
                    new Rectangle(1, 1, this.Width - 2, this.Height - 2));

                string text = this.GetType().Name;
                SizeF size = e.Graphics.MeasureString(text, this.Font);
                int left = (int)(this.Width - size.Width) / 2;
                int top = (int)(this.Height - size.Height) / 2;
                e.Graphics.DrawString(text, this.Font, new SolidBrush(c), left, top);
            }
            else
            {
                base.OnPaintBackground(e);
            }
        }

        protected bool IsDesignTime
        {
            get
            {
                // Determine if this instance is running against .NET Framework by using the MSCoreLib PublicKeyToken
                System.Reflection.Assembly mscorlibAssembly = typeof(int).Assembly;
                if ((mscorlibAssembly != null))
                {
                    if (mscorlibAssembly.FullName.ToUpper().EndsWith("B77A5C561934E089"))
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }
}
