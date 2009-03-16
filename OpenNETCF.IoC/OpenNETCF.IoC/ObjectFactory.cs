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
using System.Reflection;

namespace OpenNETCF.IoC
{
    internal class ObjectFactory
    {
        internal static string GenerateServiceName(Type t)
        {
            return t.Name + "Service";
        }

        internal static string GenerateItemName(Type t)
        {
            string name = string.Empty;
            int i = 0;
            do
            {
                name = t.Name + (++i).ToString();
            } while (RootWorkItem.Items[name] != null);
            return name;
        }

        internal class PublicationDescriptor
        {
            public EventPublication Publication { get; set; }
            public EventInfo EventInfo { get; set; }
        }

        internal class SubscriptionDescriptor
        {
            public EventSubscription Subscription { get; set; }
            public MethodInfo MethodInfo { get; set; }
        }

        private static Dictionary<Type, SubscriptionDescriptor[]> m_subscriptionDescriptorCache = 
            new Dictionary<Type, SubscriptionDescriptor[]>();

        private static Dictionary<Type, PublicationDescriptor[]> m_publicationDescriptorCache =
            new Dictionary<Type, PublicationDescriptor[]>();

        internal static SubscriptionDescriptor[] GetEventSinks(Type type)
        {
            if(m_subscriptionDescriptorCache.ContainsKey(type))
            {
                return m_subscriptionDescriptorCache[type];
            }

            var methods =
            from n in type.GetMethods(BindingFlags.Public | BindingFlags.Instance)
            where n.GetCustomAttributes(typeof(EventSubscription), false).Length > 0
            select n;

            List<SubscriptionDescriptor> descriptors = new List<SubscriptionDescriptor>();
            foreach (MethodInfo mi in methods)
            {
                descriptors.Add(new SubscriptionDescriptor
                {
                    MethodInfo = mi,
                    Subscription = mi.GetCustomAttributes(typeof(EventSubscription), false).FirstOrDefault() as EventSubscription
                });
            }

            SubscriptionDescriptor[] result = descriptors.ToArray();
            m_subscriptionDescriptorCache.Add(type, result);
            return result;
        }

        internal static PublicationDescriptor[] GetEventSources(Type type)
        {
            if (m_publicationDescriptorCache.ContainsKey(type))
            {
                return m_publicationDescriptorCache[type];
            }

            // there has to be a less convoluted LINQ query to pull this off, I just can't work it out yet

            var events =
            from n in type.GetEvents(BindingFlags.Public | BindingFlags.Instance)
            where n.GetCustomAttributes(typeof(EventPublication), false).Length > 0
            select n;

            List<PublicationDescriptor> descriptors = new List<PublicationDescriptor>();
            foreach (EventInfo ei in events)
            {
                descriptors.Add(new PublicationDescriptor
                {
                    EventInfo = ei,
                    Publication = ei.GetCustomAttributes(typeof(EventPublication), false).FirstOrDefault() as EventPublication
                });
            }
            PublicationDescriptor[] result = descriptors.ToArray();
            m_publicationDescriptorCache.Add(type, result);
            return result;
        }

        internal static string[] GetEventSourceNames(Type type)
        {
            return (from n in (type.GetEvents(BindingFlags.Public | BindingFlags.Instance).Select(
                e => e.GetCustomAttributes(typeof(EventPublication), false).First() as EventPublication))
                    select n.EventName).Distinct().ToArray();
        }

        internal static EventSubscription[] GetEventSinkSubscriptions(Type type)
        {
            return (from n in (type.GetMethods(BindingFlags.Public | BindingFlags.Instance).Select(
                m => m.GetCustomAttributes(typeof(EventSubscription), false).FirstOrDefault() as EventSubscription))
                    select n).Distinct().Where(e => e != null).ToArray();
        }

        internal static EventInfo[] GetEventSourcesFromTypeByName(Type type, string eventName)
        {
            return (from e in type.GetEvents(BindingFlags.Public | BindingFlags.Instance)
                    where
            (from a in e.GetCustomAttributes(typeof(EventPublication), false) as EventPublication[]
             where a.EventName == eventName
             select a).Count() > 0
                    select e).ToArray();
        }

        internal static MethodInfo[] GetEventSinksFromTypeByName(Type type, string eventName)
        {
            return (from e in type.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                    where
            (from a in e.GetCustomAttributes(typeof(EventSubscription), false) as EventSubscription[]
             where a.EventName == eventName
             select a).Count() > 0
                    select e).ToArray();
        }

        internal static void AddEventHandlers(object instance)
        {
            // TODO: check cache
            // get all of the sources from the object
            var sourceEvents = GetEventSources(instance.GetType());

            // get all of the sinks in the object
            // TODO: check cache
            var eventSinks = GetEventSinks(instance.GetType());

            // find any items that subscribe to the source events
            foreach (var item in RootWorkItem.Items)
            {
                foreach(var source in sourceEvents)
                {
                    // TODO: check cache
                    // wire up events
                    foreach (var sink in GetEventSinksFromTypeByName(item.Value.GetType(), source.Publication.EventName))
                    {
                        Delegate d = Delegate.CreateDelegate(source.EventInfo.EventHandlerType, item.Value, sink);
                        source.EventInfo.AddEventHandler(instance, d);

                    }
                }

                // TODO: back-wire any sinks
                foreach (var sink in eventSinks)
                {
                    foreach (var ei in GetEventSourcesFromTypeByName(item.Value.GetType(), sink.Subscription.EventName))
                    {
                        // (type, consumer instance, consumer method)
                        Delegate d = Delegate.CreateDelegate(ei.EventHandlerType, instance, sink.MethodInfo);
                        ei.AddEventHandler(item.Value, d);
                    }
                }
            }
        }

        internal static object CreateObject(Type t, WorkItem root)
        {
            object instance = null;

            // TODO: build cache for ctor info by type

            // see if there is an injection ctor
            var ctors = (from c in t.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                         where c.IsPublic == true
                         && c.GetCustomAttributes(typeof(InjectionConstructorAttribute), false).Count() > 0
                         select c);

            if (ctors.Count() == 0)
            {
                // no injection ctor, get the default, parameterless ctor
                var parameterlessCtors = (from c in t.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                            where c.GetParameters().Length == 0
                            select c);
                if (parameterlessCtors.Count() == 0)
                {
                    throw new ArgumentException(string.Format("Type '{0}' has no public parameterless constructor or injection constructor", t));
                }

                // create the object
                ConstructorInfo ci = parameterlessCtors.First();
                instance = ci.Invoke(null);
            }
            else if (ctors.Count() == 1)
            {
                // call the injection ctor
                ConstructorInfo ci = ctors.First();
                ParameterInfo[] paramList = ci.GetParameters();
                object[] inputs = GetParameterObjectsForParameterList(paramList, root, t.Name);
                instance = ci.Invoke(inputs);
            }
            else
            {
                throw new ArgumentException(string.Format("Type '{0}' has {1} defined injection constructors.  Only one is allowed", t.Name, ctors.Count()));
            }

            // TODO: cache these

            // see if there are any injection methods (we can inject into public *or internal/private* methods)
            var injectionmethods = (from c in t.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                         where c.GetCustomAttributes(typeof(InjectionMethodAttribute), false).Count() > 0
                         select c);

            foreach (MethodInfo mi in injectionmethods)
            {
                ParameterInfo[] paramList = mi.GetParameters();
                object[] inputs = GetParameterObjectsForParameterList(paramList, root, t.Name);
                mi.Invoke(instance, inputs);
            }

            // TODO: cache these

            // look for service dependecy setters
            var serviceDependecyProperties = from p in t.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                                             where p.GetCustomAttributes(typeof(ServiceDependencyAttribute), false).Count() > 0
                                             select p;

            foreach (PropertyInfo pi in serviceDependecyProperties)
            {
                // we know this is > 0 since they came through the LINQ filter above
                var attrib = pi.GetCustomAttributes(typeof(ServiceDependencyAttribute), false).Cast<ServiceDependencyAttribute>().First();

                if (attrib.RegistrationType == null) attrib.RegistrationType = pi.PropertyType;

                // see if we have the service already created
                if (!root.Services.Contains(attrib.RegistrationType))
                {
                    if (!attrib.EnsureExists)
                    {
                        throw new ServiceMissingException(string.Format("Type '{0}' has a service dependency on type '{1}'",
                            t.Name, attrib.RegistrationType.Name));
                    }
                    // create the service
                    root.Services.AddNew(pi.PropertyType, attrib.RegistrationType);
                }
                pi.SetValue(instance, root.Services.Get(attrib.RegistrationType), null);
            }

            AddEventHandlers(instance);

            return instance;
        }

        private static object[] GetParameterObjectsForParameterList(ParameterInfo[] paramList, WorkItem root, string typeName)
        {
            List<object> paramObjects = new List<object>();

            foreach (var pi in paramList)
            {
                if (pi.ParameterType.IsValueType)
                {
                    throw new ArgumentException(string.Format("Injection on type '{0}' cannot have value type parameters",
                        typeName));
                }

                object item = null;

                // see if there is an item that matches the type
                object[] itemList = root.Items.FindByType(pi.ParameterType).ToArray();

                if (itemList.Length == 0)
                {
                    object[] sdAttribs = pi.GetCustomAttributes(typeof(ServiceDependencyAttribute), false);

                    // see if it's marked with a CreateNew attribute
                    if (pi.GetCustomAttributes(typeof(CreateNewAttribute), false).Count() > 0)
                    {
                        // create a new one
                        root.Items.AddNew(pi.ParameterType);

                        // now go find it
                        itemList = root.Items.FindByType(pi.ParameterType).ToArray();
                    }
                    else if((sdAttribs != null) && (sdAttribs.Count() > 0))
                    {
                        ServiceDependencyAttribute attrib = sdAttribs[0] as ServiceDependencyAttribute;
                        if(attrib.RegistrationType == null) attrib.RegistrationType = pi.ParameterType;

                        // see if the service exists
                        object svc = root.Services.Get(attrib.RegistrationType);
                        if ((svc == null) && (attrib.EnsureExists))
                        {
                            // doesn't exist but the attribute says create it
                            root.Services.AddNew(pi.ParameterType, attrib.RegistrationType);

                            // go get the created service and pass it along
                            svc = root.Services.Get(attrib.RegistrationType);
                        }

                        // see if we found or created the service
                        if (svc != null)
                        {
                            // put the service in the array for later consumption
                            itemList = new object[] { svc };
                        }
                        else
                        {
                            throw new ServiceMissingException(string.Format("Type '{0}' has a service dependency on type '{1}'",
                                typeName, attrib.RegistrationType.Name));
                        }
                    }
                    
                    if (itemList.Length == 0)
                    {
                        throw new ArgumentException(string.Format("Injection on type '{0}' requires an item of type '{1}'",
                            typeName, pi.ParameterType.Name));
                    }
                }

                item = itemList[0];

                paramObjects.Add(item);
            }

            return paramObjects.ToArray();
        }
    }
}
