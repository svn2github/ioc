using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenNETCF.IoC.UI
{
    public interface ISmartPartInfoProvider
    {
        ISmartPartInfo GetSmartPartInfo(Type smartPartInfoType);
    }
}
