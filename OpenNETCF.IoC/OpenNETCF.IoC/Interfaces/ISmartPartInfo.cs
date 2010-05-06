using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenNETCF.IoC.UI
{
    public interface ISmartPartInfo
    {
        /// <summary>
        /// Description of this SmartPart.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Title of this SmartPart.
        /// </summary>
        string Title { get; set; }
    }
}
