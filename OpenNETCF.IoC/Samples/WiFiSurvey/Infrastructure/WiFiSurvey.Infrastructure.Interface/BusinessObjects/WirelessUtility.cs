using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using OpenNETCF.Net.NetworkInformation;

namespace WiFiSurvey.Infrastructure.BusinessObjects
{
    public static class WirelessUtility
    {
        public static Boolean DesktopAppDisabled { get; set; }
        public static Boolean DesktopConnected { get; set; }
        public static AccessPoint CurrentAccessPoint { get; set; }
        public static int RefreshRate { get; set; }
    }
}
