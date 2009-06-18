using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace WiFiSurvey.Infrastructure.BusinessObjects
{
    public class APInfo
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string MAC { get; set; }
        public int SignalStrength { get; set; }
    }
}
