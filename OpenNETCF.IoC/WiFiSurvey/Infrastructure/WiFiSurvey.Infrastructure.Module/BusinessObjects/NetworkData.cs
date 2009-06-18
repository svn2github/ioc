using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace WiFiSurvey.Infrastructure.BusinessObjects
{
    internal class NetworkData : INetworkData
    {
        public APInfo AssociatedAP { get; set; }
        public APInfo[] NearbyAPs { get; set; }
    }
}
