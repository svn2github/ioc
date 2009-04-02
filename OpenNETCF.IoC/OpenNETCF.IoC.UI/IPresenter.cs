using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace OpenNETCF.IoC.UI
{
    public interface IPresenter<TView> 
        where TView : ISmartPart
    {
        void Run();
    }
}
