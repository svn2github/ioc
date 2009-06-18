using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using OpenNETCF.Net;
using WiFiSurvey.Infrastructure.BusinessObjects;
using OpenNETCF.Net.NetworkInformation;
using OpenNETCF.IoC;
using WiFiSurvey.Infrastructure.Constants;

namespace WiFiSurvey.Infrastructure.Services
{
    public class APMonitorService : IAPMonitorService
    {
        private Timer m_adapterPollTimer;
        private bool m_pollingAPs = false;

        [EventPublication(EventNames.NetworkDataChange)]
        public event EventHandler<GenericEventArgs<INetworkData>> NetworkDataChange;

        public WirelessZeroConfigNetworkInterface Adapter { get; set; }
        private IConfigurationService ConfigurationService { get; set; }

        [InjectionConstructor]
        public APMonitorService([ServiceDependency]IConfigurationService configService)
        {
            ConfigurationService = configService;

            // we'll assume that you *must* have an adapter, and we'll use the first
            var intf = NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault(
                i => i is WirelessZeroConfigNetworkInterface);

            if (intf == null)
            {
                throw new Exception("No WZC adapter found!");
            }

            Adapter = intf as WirelessZeroConfigNetworkInterface;

            m_adapterPollTimer = new Timer(AdapterPollProc, null, ConfigurationService.ApplicationConfig.AdapterPollInterval, ConfigurationService.ApplicationConfig.AdapterPollInterval);
        }

        ~APMonitorService()
        {
            m_adapterPollTimer.Dispose();
        }

        void AdapterPollProc(object o)
        {
            if (m_pollingAPs) return;

            try
            {
                m_pollingAPs = true;

                // refresh the nearby list
                Adapter.NearbyAccessPoints.Refresh();

                NetworkData data = new NetworkData();
                data.AssociatedAP = new APInfo
                {
                    Name = Adapter.AssociatedAccessPoint,
                    MAC = Adapter.AssociatedAccessPointMAC.ToString(),
                    SignalStrength = Adapter.SignalStrength.Decibels
                };

                List<APInfo> nearbyList = new List<APInfo>();
                foreach (var ap in Adapter.NearbyAccessPoints)
                {
                    nearbyList.Add(new APInfo
                    {
                        Name = ap.Name,
                        MAC = ap.PhysicalAddress.ToString(),
                        SignalStrength = ap.SignalStrength.Decibels
                    });
                }

                data.NearbyAPs = nearbyList.ToArray();

                RaiseNetworkDataChange(data);
            }
            finally
            {
                m_pollingAPs = false;
            }
        }

        void RaiseNetworkDataChange(INetworkData data)
        {
            if (NetworkDataChange == null) return;

            NetworkDataChange(this, new GenericEventArgs<INetworkData>(data));
        }
    }
}
