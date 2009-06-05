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
}
