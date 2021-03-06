﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using OpenNETCF.Net;
using WiFiSurvey.Infrastructure.BusinessObjects;
using OpenNETCF.Net.NetworkInformation;
using OpenNETCF.IoC;
using WiFiSurvey.Infrastructure.Constants;
using System.Diagnostics;
using OpenNETCF;

namespace WiFiSurvey.Infrastructure.Services
{
    public class APMonitorService : IAPMonitorService
    {
        private Timer m_adapterPollTimer;
        private bool m_pollingAPs = false;
        private Stopwatch m_stopwatch = new Stopwatch();
        private Thread m_apThread;
        private bool m_stopThreads = false;

        [EventPublication(EventNames.NetworkDataChange)]
        public event EventHandler<GenericEventArgs<INetworkData>> NetworkDataChange;

        private IConfigurationService ConfigurationService { get; set; }
        private IDebugService DebugService { get; set; }
        private WirelessZeroConfigNetworkInterface Adapter { get; set; }

        [InjectionConstructor]
        public APMonitorService(
            [ServiceDependency]IConfigurationService configService,
            [ServiceDependency]IDebugService debugService)
        {
            // we'll assume that you *must* have an adapter, and we'll use the first
            var intf = NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault(
                i => i is WirelessZeroConfigNetworkInterface);

            if (intf == null)
            {
                throw new Exception("No WZC adapter found!");
            }

            ConfigurationService = configService;
            DebugService = debugService;

            Adapter = intf as WirelessZeroConfigNetworkInterface;

            CreateAPThread();
        }

        public void Shutdown()
        {
            m_stopThreads = true;
        }

        private void CreateAPThread()
        {
            m_apThread = new Thread(AdapterPollProc);
            m_apThread.IsBackground = true;
            m_apThread.Start();
        }

        void AdapterPollProc()
        {
            while (!m_stopThreads)
            {
                m_stopwatch.Reset();
                m_stopwatch.Start();
                try
                {
                    m_pollingAPs = true;

                    // refresh the nearby list
                    Adapter.NearbyAccessPoints.Refresh();

                    NetworkData data = new NetworkData();
                    data.AssociatedAP = new APInfo();
                    if (Adapter != null)
                    {
                        data.AssociatedAP.Name = Adapter.AssociatedAccessPoint;
                        if (Adapter.AssociatedAccessPointMAC != null)
                        {
                            data.AssociatedAP.MAC = Adapter.AssociatedAccessPointMAC.ToString();
                        }
                        else
                        {
                            data.AssociatedAP.MAC = "";
                        }
                        data.AssociatedAP.SignalStrength = Adapter.SignalStrength.Decibels;
                    }

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
                catch (Exception ex)
                {
                    DebugService.WriteLine(ex.InnerException.ToString());
                }
                finally
                {
                    m_pollingAPs = false;
                }

                m_stopwatch.Stop();
                DebugService.WriteLine("AP Refresh took " + m_stopwatch.Elapsed.TotalSeconds.ToString());

                Thread.Sleep(1000);
            }

        }

        void RaiseNetworkDataChange(INetworkData data)
        {
            if (NetworkDataChange == null) return;

            NetworkDataChange(this, new GenericEventArgs<INetworkData>(data));
        }
    }
}
