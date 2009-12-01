using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace WiFiSurvey.Infrastructure
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
