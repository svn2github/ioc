using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace WiFiSurvey.Infrastructure.BusinessObjects
{
    public interface INetworkData
    {
        APInfo AssociatedAP { get; set; }
        APInfo[] NearbyAPs { get; set; }
    }
}
