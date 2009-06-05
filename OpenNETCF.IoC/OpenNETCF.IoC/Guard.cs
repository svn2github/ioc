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
    internal static class Guard
    {
        public static void ArgumentNotNull(object argumentValue, string argumentName)
        {
            if (argumentValue == null) throw new ArgumentNullException(argumentName);
        }

        public static void ArgumentNotNullOrEmptyString(string argumentValue, string argumentName)
        {
            if (argumentValue == null) throw new ArgumentNullException(argumentName);
            if (argumentValue == string.Empty) throw new ArgumentException(string.Format("Argument '{0}' cannot be an empty string.", argumentName));
        }
    }
}
