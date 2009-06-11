using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using OpenNETCF.Net.NetworkInformation;

namespace WiFiSurvey.Infrastructure.Services
{
    public class NetworkService : INetworkService
    {
        public NetworkService()
        {
            // we'll assume that you *must* have an adapter, and we'll use the first
            var intf = NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault(
                i => i is WirelessZeroConfigNetworkInterface);

            if (intf == null)
            {
                throw new Exception("No WZC adapter found!");
            }

            Adapter = intf as WirelessZeroConfigNetworkInterface;
        }

        public WirelessZeroConfigNetworkInterface Adapter { get; set; }
    }
}
