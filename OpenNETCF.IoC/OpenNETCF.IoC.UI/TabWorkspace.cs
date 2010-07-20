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
    public class TabWorkspace : Workspace
    {
        public event EventHandler SelectedIndexChanged; 

        private TabControl m_tabs;
        private List<TabInfo> m_smartPartTabs;

        private class TabInfo
        {
            public TabPage Page { get; set; }
            public ISmartPart SmartPart { get; set; }
            public ISmartPartInfo SmartPartInfo { get; set; }
        }

        public TabWorkspace()
        {
            m_tabs = new TabControl();
            this.Controls.Add(m_tabs);
            m_tabs.Dock = DockStyle.Fill;
            m_tabs.SelectedIndexChanged += new EventHandler(m_tabs_SelectedIndexChanged);

            DesktopSetup();

            m_smartPartTabs = new List<TabInfo>();
        }

        private void m_tabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            var handler = SelectedIndexChanged;
            if (handler == null) return;

            handler(this, e);
        }


        protected override void OnActivate(ISmartPart smartPart)
        {
            if (smartPart == null) throw new ArgumentNullException("smartPart");

            ShowTab(smartPart, null, false);
        }

        protected override void OnClose(ISmartPart smartPart)
        {
            TabInfo ti = m_smartPartTabs.Find(t => t.SmartPart == smartPart);

            if (ti == null) throw new Exception("Tab not found");

            // "hiding" is removing it from the tabcontrol
            int index = m_tabs.TabPages.IndexOf(ti.Page);
            m_tabs.TabPages.RemoveAt(index);

            RaiseSmartPartClosing(smartPart);
            m_smartPartTabs.Remove(ti);
            ti.Page.Dispose();
        }

        protected override void OnHide(ISmartPart smartPart)
        {
            TabInfo ti = m_smartPartTabs.Find(t => t.SmartPart == smartPart);

            if (ti == null) throw new Exception("Tab not found");

            // "hiding" is removing it from the tabcontrol
            int index = m_tabs.TabPages.IndexOf(ti.Page);
            m_tabs.TabPages.RemoveAt(index);
        }

        protected override void OnShow(ISmartPart smartPart, ISmartPartInfo smartPartInfo)
        {
            ShowTab(smartPart, smartPartInfo, true);
        }

        public void SelectTab(int index)
        {
            if (m_tabs.TabPages.Count > index)
            {
                m_tabs.TabPages[index].BringToFront();
                m_tabs.SelectedIndex = index;
            }
        }

        private void ShowTab(ISmartPart smartPart, ISmartPartInfo smartPartInfo, bool createIfNew)
        {
            TabInfo ti = m_smartPartTabs.Find(t => t.SmartPart == smartPart);

            if (ti == null)
            {
                if (!createIfNew)
                {
                    throw new Exception("Tab not found");
                }

                TabPage page = new TabPage();
                page.Text = smartPartInfo == null ? smartPart.Name : smartPartInfo.Title;
                smartPart.Dock = DockStyle.Fill;
                page.ClientRectangle.Inflate(-2, -2);
                page.Controls.Add((Control)smartPart);
                m_tabs.TabPages.Add(page);

                ti = new TabInfo { Page = page, SmartPart = smartPart, SmartPartInfo = smartPartInfo };
                
                m_smartPartTabs.Add(ti);
            }
            int index = m_tabs.TabPages.IndexOf(ti.Page);
            if (index < 0)
            {
                m_tabs.TabPages.Add(ti.Page);
                index = m_tabs.TabPages.IndexOf(ti.Page);
            }
            m_tabs.SelectedIndex = index;

            AddSmartPartToCollectionIfRequired(smartPart);

            RaiseSmartPartActivated(smartPart);
            smartPart.OnActivated();
        }

        public virtual void ClearTabs()
        {
        }

        public System.Windows.Forms.TabControl.TabPageCollection TabPages
        {
            get { return m_tabs.TabPages; }
        }

        public TabPage SelectedTab
        {
            get 
            {
#if WindowsCE
                return m_tabs.TabPages[m_tabs.SelectedIndex];
#else
                return m_tabs.SelectedTab; 
#endif
            }
        }

        public int SelectedIndex
        {
            get { return m_tabs.SelectedIndex; }
            set { m_tabs.SelectedIndex = value; }
        }
#if !WindowsCE
        public event DrawItemEventHandler DrawItem;

        public TabDrawMode DrawMode
        {
            get { return m_tabs.DrawMode; }
            set { m_tabs.DrawMode = value; }
        }

        public TabSizeMode SizeMode
        {
            get { return m_tabs.SizeMode; }
            set { m_tabs.SizeMode = value; }
        }

        private void m_tabs_DrawItem(object sender, DrawItemEventArgs e)
        {
            var handler = DrawItem;
            if (handler == null) return;

            handler(sender, e);
        }

#endif
        private void DesktopSetup()
        {
#if !WindowsCE
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            m_tabs.DrawItem += new DrawItemEventHandler(m_tabs_DrawItem);
#endif
        }
    }
}
