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
    public static class ExtensionMethods
    {
        public static bool Implements<TInterface>(this Type baseType)
        {
            if (!(typeof(TInterface).IsInterface))
            {
                throw new ArgumentException("TInterface must be an interface type.");
            }

            return baseType.GetInterfaces().Contains(typeof(TInterface));
        }

        public static bool Implements(this Type instanceType, Type interfaceType)
        {
            if (!(interfaceType.IsInterface))
            {
                throw new ArgumentException("interfaceType must be an interface type.");
            }

            return instanceType.GetInterfaces().Contains(interfaceType);
        }

        public static TWorkItem GetOrAdd<TWorkItem>(this ManagedObjectCollection<WorkItem> collection, string id)
            where TWorkItem : class
        {
            if (collection.Contains(id))
            {
                return collection.Get<TWorkItem>(id);
            }

            return collection.AddNew<TWorkItem>(id);
        }

        /// <summary>
        /// Gets an existing service of a given type if it already exists in the collection.  If it does not exist, it creates and adds a new instance.
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public static TService GetOrAdd<TService>(this ServiceCollection collection)
            where TService : class
        {
            var service = collection.Get<TService>();
            if (service != null) return service;
            return collection.AddNew<TService>();
        }
    }
}
