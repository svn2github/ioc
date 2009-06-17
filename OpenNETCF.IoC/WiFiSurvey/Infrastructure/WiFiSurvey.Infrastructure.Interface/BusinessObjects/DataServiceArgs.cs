using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace WiFiSurvey.Infrastructure.BusinessObjects
{
    public class DataServiceArgs<T> : EventArgs
    {
        public T Value { get; set; }

        public DataServiceArgs(T value)
        {
            Value = value;
        }
    }
}
