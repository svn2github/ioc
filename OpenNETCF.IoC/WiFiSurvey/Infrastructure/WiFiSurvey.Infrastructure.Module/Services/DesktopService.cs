using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using OpenNETCF.Net.NetworkInformation;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;
using WiFiSurvey.Infrastructure.BusinessObjects;
using OpenNETCF.IoC;
using WiFiSurvey.Infrastructure.Constants;

namespace WiFiSurvey.Infrastructure.Services
{
    public class DesktopService : IDesktopService
    {
        private Stopwatch m_lastReceivedWatch = new Stopwatch();
        private double m_dropOutSeconds = 0;
        private double m_dropOutSecondsMax = 5;
        private int m_listenReciever = 11001;
        private int m_broadcastPort = 11000;
        private TimeSpan m_lastRecievedTime;
        private Thread m_broadcastThread;
        private Thread m_listenThread;
        private IPEndPoint m_endPoint;
        private bool m_connected;
        private string m_hostName;
        private IPAddress m_ipAddress;

        private UdpClient Listener { get; set; }
        private Boolean Done { get; set; }

        private string m_lastDesktop = "";

        public APInfo CurrentAP { get; set; }

        private IHistoricEventService DataService { get; set; }
        private IAPMonitorService APService { get; set; }
        private IStatisticsService StatisticsService { get; set; }
        private IConfigurationService ConfigurationService { get; set; }

        public DateTime LastSentTime { get; set; }

        [EventSubscription(EventNames.NetworkDataChange, ThreadOption.UserInterface)]
        public void OnNewAP(object sender, GenericEventArgs<INetworkData> args)
        {
            CurrentAP = args.Value.AssociatedAP;
        }

        [InjectionConstructor]
        public DesktopService([ServiceDependency]IConfigurationService configService)
        {
            ConfigurationService = configService;
            m_lastReceivedWatch.Start();

            IPAddress[] array = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
            m_ipAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0];

            m_hostName = Dns.GetHostName();
        }

        ~DesktopService()
        {
            // TODO: fix this - it is bad, bad form.
            if (m_broadcastThread != null)
            {
                m_broadcastThread.Abort();
            }
            if (m_listenThread != null)
            {
                m_listenThread.Abort();
            }
        }

        public void StartBroadcastProc()
        {
            m_broadcastThread = new Thread(Broadcast);
            m_broadcastThread.IsBackground = true;
            m_broadcastThread.Start();
        }

        public void StartListenerThread()
        {
            Listener = new UdpClient(m_listenReciever);
            m_listenThread = new Thread(ListenerProc);
            m_listenThread.IsBackground = true;
            m_listenThread.Start();
        }

        [EventPublication(EventNames.DesktopConnectionChange)]
        public event EventHandler<GenericEventArgs<IDesktopData>> DesktopConnectionChange;

        public void ListenerProc()
        {
            try
            {
                while (!Done)
                {
                    if (!WirelessUtility.DesktopAppDisabled)
                    {
                        m_lastReceivedWatch.Reset();
                        m_lastReceivedWatch.Start();
                        m_endPoint = new IPEndPoint(IPAddress.Broadcast, m_listenReciever);

                        byte[] data = Listener.Receive(ref m_endPoint);

                        m_lastReceivedWatch.Stop();
                        m_lastRecievedTime = m_lastReceivedWatch.Elapsed;

                        if (!m_connected)
                        {
                            m_connected = true;

                            IDesktopData deskData = new DesktopData();
                            deskData.Status = DesktopStatus.Connected;
                            m_lastDesktop = m_endPoint.Address.ToString();
                            deskData.IPAddress = m_lastDesktop;

                            DesktopConnectionChange(this, new GenericEventArgs<IDesktopData>(deskData));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.InnerException);
            }
            finally
            {
                Listener.Close();
            }
        }
        public void StartListening()
        {
            DataService = RootWorkItem.Services.Get<IHistoricEventService>();

            StartBroadcastProc();
            StartListenerThread();
        }

        public void Broadcast()
        {
            string[] args = new string[1];

            //the only way the desktop client recieves a packet is using the remote ep of IPAddress.Broadcast
            IPEndPoint m_RemoteEP = new IPEndPoint(IPAddress.Broadcast, m_broadcastPort);

            //Bind the UdpClient to the Local IP for Desktop DebugZ
            IPEndPoint m_localEP = new IPEndPoint(Dns.GetHostEntry(Dns.GetHostName()).AddressList[0], 11000);

            UdpClient m_BroadcastClient = new UdpClient(m_localEP);

            m_BroadcastClient.Connect(m_RemoteEP);
            //s.Connect(ep);

            while (!Done)
            {
                if (CurrentAP != null && !WirelessUtility.DesktopAppDisabled)
                {
                    args[0] = m_hostName + ":" + CurrentAP.Name + ":" + CurrentAP.SignalStrength.ToString();
                    byte[] sendbuf = Encoding.ASCII.GetBytes(args[0]);

                    m_BroadcastClient.Send(sendbuf, sendbuf.Length);

                    //Check Connectivity to Desktop
                    m_dropOutSeconds = m_lastReceivedWatch.Elapsed.TotalSeconds;

                    if (m_dropOutSeconds > m_dropOutSecondsMax)
                    {
                        if (m_connected)
                        {
                            m_connected = false;
                            IDesktopData deskData = new DesktopData();
                            deskData.Status = DesktopStatus.Disconnected;
                            deskData.IPAddress = m_lastDesktop;
                            DesktopConnectionChange(this, new GenericEventArgs<IDesktopData>(deskData));
                        }
                    }
                }
                else
                {
                    m_connected = false;
                }
                Thread.Sleep(1000);
            }
        }
    }
}
