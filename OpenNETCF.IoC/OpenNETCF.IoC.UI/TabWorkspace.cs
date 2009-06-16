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
        TabControl m_tabs;
        List<TabInfo> m_smartParts;

        private class TabInfo
        {
            public TabPage Page { get; set; }
            public ISmartPart SmartPart { get; set; }
        }

        public TabWorkspace()
        {
            m_tabs = new TabControl();
            this.Controls.Add(m_tabs);
            m_tabs.Dock = DockStyle.Fill;

            m_smartParts = new List<TabInfo>();
        }

        protected override void OnActivate(ISmartPart smartPart)
        {
            if (smartPart == null) throw new ArgumentNullException("smartPart");

            ShowTab(smartPart, false);
        }

        protected override void OnClose(ISmartPart smartPart)
        {
            TabInfo ti = m_smartParts.Find(t => t.SmartPart == smartPart);

            if (ti == null) throw new Exception("Tab not found");

            // "hiding" is removing it from the tabcontrol
            int index = m_tabs.TabPages.IndexOf(ti.Page);
            m_tabs.TabPages.RemoveAt(index);

            RaiseSmartPartClosing(smartPart);
            m_smartParts.Remove(ti);
            ti.Page.Dispose();
        }

        protected override void OnHide(ISmartPart smartPart)
        {
            TabInfo ti = m_smartParts.Find(t => t.SmartPart == smartPart);

            if (ti == null) throw new Exception("Tab not found");

            // "hiding" is removing it from the tabcontrol
            int index = m_tabs.TabPages.IndexOf(ti.Page);
            m_tabs.TabPages.RemoveAt(index);
        }

        protected override void OnShow(ISmartPart smartPart)
        {
            ShowTab(smartPart, true);
        }

        private void ShowTab(ISmartPart smartPart, bool createIfNew)
        {
            TabInfo ti = m_smartParts.Find(t => t.SmartPart == smartPart);

            if (ti == null)
            {
                if (!createIfNew)
                {
                    throw new Exception("Tab not found");
                }

                TabPage page = new TabPage();
                page.Text = smartPart.Name;
                m_tabs.TabPages.Add(page);

                ti = new TabInfo { Page = page, SmartPart = smartPart };
                m_smartParts.Add(ti);
            }
            int index = m_tabs.TabPages.IndexOf(ti.Page);
            if (index < 0)
            {
                m_tabs.TabPages.Add(ti.Page);
                index = m_tabs.TabPages.IndexOf(ti.Page);
            }
            m_tabs.SelectedIndex = index;

            RaiseSmartPartActivated(smartPart);
            smartPart.OnActivated();
        }
    }
}
