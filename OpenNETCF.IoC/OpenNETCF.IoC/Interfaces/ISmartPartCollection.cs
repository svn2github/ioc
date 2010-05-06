using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenNETCF.IoC.UI
{
    public interface ISmartPartCollection : IEnumerable<ISmartPart>
    {
        int Count { get; }

//        void Add(ISmartPart smartPart);
//        void Remove(ISmartPart smartPart);
    }
}
