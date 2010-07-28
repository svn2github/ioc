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
    }
}
