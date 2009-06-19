using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using OpenNETCF.Diagnostics;
using OpenNETCF.IoC;
using WiFiSurvey.Infrastructure.Constants;
using WiFiSurvey.Infrastructure.BusinessObjects;

namespace WiFiSurvey.Infrastructure.Services
{
    public class StatisticsService : IStatisticsService
    {
        [InjectionConstructor]
        public StatisticsService()
        {
        }

        private Stopwatch m_NetworkWatch = new Stopwatch();
        private Stopwatch m_AccessPointWatch = new Stopwatch();

        [EventPublication(EventNames.DesktopConnectionChange)]
        public event EventHandler<GenericEventArgs<IDesktopData>> DesktopConnectionChange;

        [EventPublication(EventNames.NewAPConnection)]
        public event EventHandler<GenericEventArgs<INetworkData>> APConnectionChange;

        //Network Stats
        public TimeSpan NetworkDownTime { get; set; }
        public void FoundNetwork()
        {
            m_NetworkWatch.Stop();
            NetworkDownTime = m_NetworkWatch.Elapsed;
        }
        public void LostNetwork()
        {
            m_NetworkWatch.Reset();
            m_NetworkWatch.Start();
        }

        //Access Point Stats
        public TimeSpan AcessPointDownTime { get; set; }
        public void FoundAccessPoint()
        {
            m_AccessPointWatch.Stop();
            AcessPointDownTime = m_AccessPointWatch.Elapsed;
        }
        public void LostAccessPoint()
        {
            m_AccessPointWatch.Reset();
            m_AccessPointWatch.Start();
        }
    }
}
