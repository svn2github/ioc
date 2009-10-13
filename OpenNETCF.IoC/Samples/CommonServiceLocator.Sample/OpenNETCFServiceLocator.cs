using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ServiceLocation;

namespace OpenNETCF.IoC
{
    public class OpenNETCFServiceLocator : ServiceLocatorImplBase
    {
        private WorkItem m_container;

        public OpenNETCFServiceLocator(WorkItem container)
        {
            m_container = container;
        }

        /// <summary>
        /// When implemented by inheriting classes, this method will do the actual work of resolving
        /// the requested service instance.
        /// </summary>
        /// <param name="serviceType">Type of instance requested.</param>
        /// <param name="key">Name of registered service you want. May be null.</param>
        /// <returns>
        /// The requested service instance.
        /// </returns>
        protected override object DoGetInstance(Type serviceType, string key)
        {
            if (key == null)
            {
                // no key provided, just get the first (default) of the requested type
                var instance =  (from i in m_container.Items
                                where i.Value.IsConvertableTo(serviceType)
                                select i.Value).FirstOrDefault();

                if (instance == null) throw new Microsoft.Practices.ServiceLocation.ActivationException("Unable to find an object of the requested Type");

                return instance;
            }
            else
            {
                var instance = (from i in m_container.Items
                                where i.Key == key && i.Value.IsConvertableTo(serviceType)
                                select i.Value).FirstOrDefault();

                if (instance == null) throw new Microsoft.Practices.ServiceLocation.ActivationException("Unable to find an object of the requested Type");

                return instance;
            }
        }

        /// <summary>
        /// When implemented by inheriting classes, this method will do the actual work of
        /// resolving all the requested service instances.
        /// </summary>
        /// <param name="serviceType">Type of service requested.</param>
        /// <returns>
        /// Sequence of service instance objects.
        /// </returns>
        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            var instanceList = (from i in m_container.Items
                            where i.Value.IsConvertableTo(serviceType)
                            select i.Value);

            return instanceList;
        }
    }

    internal static class Extensions
    {
        public static bool IsConvertableTo(this object @object, Type other)
        {
            Type t = @object.GetType();

            if (t.Equals(other)) return true;
            if (t.IsSubclassOf(other)) return true;

            return false;
        }
    }
}
