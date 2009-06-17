using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using OpenNETCF.Net.NetworkInformation;

namespace WiFiSurvey.Infrastructure.BusinessObjects
{
    public class AccessPointEventArgs : EventArgs
    {
        public AccessPoint AccessPoint { get; set; }

        public AccessPointEventArgs(AccessPoint accessPoint)
        {
            AccessPoint = accessPoint;
        }
    }
}
