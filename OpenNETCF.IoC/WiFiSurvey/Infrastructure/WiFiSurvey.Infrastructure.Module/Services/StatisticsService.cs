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
        public StatisticsService()
        {
        }

        private Stopwatch m_NetworkWatch = new Stopwatch();
        private Stopwatch m_AccessPointWatch = new Stopwatch();

        private APInfo m_previousAP = null;

        [EventSubscription(EventNames.DesktopConnectionChange, ThreadOption.UserInterface)]
        public void DesktopChanged(object sender, GenericEventArgs<IDesktopData> args)
        {
            if (args.Value.Status != DesktopStatus.Disabled)
            {
                switch (args.Value.Status)
                {
                    case DesktopStatus.Connected: FoundNetwork(args.Value.IPAddress); break;
                    case DesktopStatus.Disconnected: LostNetwork(args.Value.IPAddress); break;
                }
            }
        }

        [EventSubscription(EventNames.NetworkDataChange, ThreadOption.UserInterface)]
        public void AccessPointUpdate(object sender, GenericEventArgs<INetworkData> args)
        {
            if ((args.Value.AssociatedAP == null) || (args.Value.AssociatedAP.Name.ToString() == String.Empty))
            {
                if (m_previousAP != null)
                {
                    LostAccessPoint();
                    m_previousAP = null;
                }
            }
            else
            {
                if (m_previousAP == null)
                {
                    FoundAccessPoint(args.Value.AssociatedAP);
                    m_previousAP = args.Value.AssociatedAP;
                }
                else if(m_previousAP.MAC != args.Value.AssociatedAP.MAC)
                {
                    FoundAccessPoint(args.Value.AssociatedAP);
                    m_previousAP = args.Value.AssociatedAP;
                }

            }
        }

        [EventPublication(EventNames.NewStatisticsEvent)]
        public event EventHandler<GenericEventArgs<IStatisticsData>> StatisticEvent;

        //Network Stats
        public TimeSpan NetworkDownTime { get; set; }
        public void FoundNetwork(string IPAddress)
        {
            m_NetworkWatch.Stop();
            NetworkDownTime = m_NetworkWatch.Elapsed;

            IStatisticsData stats = new StatisticsData();
            stats.Event = StatsEvent.GainDesktop;
            stats.EventTime =  (int)NetworkDownTime.TotalMilliseconds;
            stats.Description = "Desktop Connection to " + IPAddress + " Found";

            StatisticEvent(this, new GenericEventArgs<IStatisticsData>(stats));
        }
        public void LostNetwork(string IPAddress)
        {
            IStatisticsData stats = new StatisticsData();
            stats.Event = StatsEvent.LostDesktop;
            stats.EventTime = 0;
            stats.Description = "Desktop Connection to " + IPAddress + " Lost";  
            StatisticEvent(this, new GenericEventArgs<IStatisticsData>(stats));

            m_NetworkWatch.Reset();
            m_NetworkWatch.Start();
        }

        //Access Point Stats
        public TimeSpan AcessPointDownTime { get; set; }
        public void FoundAccessPoint(APInfo info)
        {
            m_AccessPointWatch.Stop();
            AcessPointDownTime = m_AccessPointWatch.Elapsed;

            IStatisticsData stats = new StatisticsData();
            stats.Event = StatsEvent.APConnectionChange;
            stats.EventTime = (int)AcessPointDownTime.TotalMilliseconds;
            if (m_previousAP == null)
            {
                stats.Description = "Connected to AP " + info.Name;
            }
            else
            {
                stats.Description = "AP Connection Changed From " + m_previousAP.Name + " to " + info.Name;
            }

            StatisticEvent(this, new GenericEventArgs<IStatisticsData>(stats));
        }
        public void LostAccessPoint()
        {
            m_AccessPointWatch.Reset();
            m_AccessPointWatch.Start();
        }

        public void Shutdown()
        {
            m_AccessPointWatch.Stop();
            m_AccessPointWatch = null;

            m_NetworkWatch.Stop();
            m_NetworkWatch = null;
        }
    }
}
