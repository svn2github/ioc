using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenNETCF.IoC
{
    public abstract class WorkItemController : IWorkItemController
    {
        /// <summary>
        /// Gets or sets the work item.
        /// </summary>
        /// <value>The work item.</value>
        [ServiceDependency]
        public WorkItem WorkItem { get;set; }

        [EventPublication("topic://ICM/ICMErrorOrWarning", PublicationScope.Global)]
        public event EventHandler<GenericEventArgs<object>> ICMErrorOrWarning;

        public virtual void Run()
        {
        }

        protected void RaiseICMErrorOrWarning(object state)
        {
            if (ICMErrorOrWarning != null) ICMErrorOrWarning(this, new GenericEventArgs<object>(state));
        }
    }
}
