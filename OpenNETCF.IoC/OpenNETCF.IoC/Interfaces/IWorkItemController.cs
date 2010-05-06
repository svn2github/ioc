using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenNETCF.IoC
{
    public interface IWorkItemController
    {
        /// <summary>
        /// Called when the controller is ready to run.
        /// </summary>
        void Run();
    }
}
