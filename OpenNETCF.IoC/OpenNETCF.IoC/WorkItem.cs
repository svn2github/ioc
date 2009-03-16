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

namespace OpenNETCF.IoC
{
    public class WorkItem : IDisposable
    {
        internal WorkItem()
        {
            Items = new ManagedObjectCollection(this);
            Services = new ServiceCollection(this);
        }
        /// <summary>
        /// The Items collection contains Components unique by string ID.  Multiple Components of the same type can be added
        /// </summary>
        public ManagedObjectCollection Items { get; private set; }

        /// <summary>
        /// The Services collection contains Components unique by registered type.  Only one Service of a given registering type can exist in the collection.
        /// </summary>
        public ServiceCollection Services { get; private set; }

        private bool m_disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!m_disposed)
            {
                if (disposing)
                {
                    // TODO: remove any managed items if we end up creating them
                }

                m_disposed = true;
            }
        }
    }
}
