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
    }
}
