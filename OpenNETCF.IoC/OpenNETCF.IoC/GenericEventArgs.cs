using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace OpenNETCF.IoC
{
    public class GenericEventArgs<T> : EventArgs
    {
        [DebuggerStepThrough]
        public GenericEventArgs(T value)
        {
            Value = value;
        }

        public T Value { get; private set; }
    }
}
