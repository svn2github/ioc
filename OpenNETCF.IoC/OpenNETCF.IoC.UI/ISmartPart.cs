using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace OpenNETCF.IoC.UI
{
    public interface ISmartPart : IDisposable
    {
//        SmartPartInfo SmartPartInfo { get; set; }
        bool Visible { set; }
        bool Focused { get; }
        void BringToFront();
        bool Focus();
        DockStyle Dock { set; }
        string Name { get; set; }
    }

    public class SmartPartInfo
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class SmartPartCollection : IEnumerable<ISmartPart>
    {
        private List<ISmartPart> m_smartParts = new List<ISmartPart>();

        internal SmartPartCollection()
        {
        }

        internal void Add(ISmartPart smartPart)
        {
            if(smartPart == null) throw new ArgumentNullException();

            m_smartParts.Add(smartPart);
        }

        internal void Remove(ISmartPart smartPart)
        {
            if (smartPart == null) throw new ArgumentNullException();

            m_smartParts.Remove(smartPart);
        }

        public IEnumerator<ISmartPart> GetEnumerator()
        {
            return m_smartParts.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return m_smartParts.GetEnumerator();
        }
    }
}
