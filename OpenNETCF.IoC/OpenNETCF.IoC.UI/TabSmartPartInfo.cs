using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace OpenNETCF.IoC.UI
{
    public enum TabPosition
    {
        // Summary:
        //     Place tab page at begining.
        Beginning = 0,
        //
        // Summary:
        //     Place tab page at end.
        End = 1,
    }

    public class TabSmartPartInfo : SmartPartInfo
    {
        public TabSmartPartInfo()
        {
        }

        /// <summary>
        /// Specifies whether the tab will get focus when shown.
        /// </summary>
        [DefaultValue(true)]
        public bool ActivateTab { get; set; }

        /// <summary>
        /// Specifies the position of the tab page.
        /// </summary>
        public TabPosition Position { get; set; }
    }
}
