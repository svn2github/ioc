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
        private double m_dropOutSecondsMax = 8;
        private int m_listenReciever = 11001;
        private int m_broadcastPort = 11000;
        private TimeSpan m_lastRecievedTime;
        private Thread m_broadcastThread;
        private Thread m_listenThread;
        private IPEndPoint m_endPoint;
        private bool m_connected;
        private string m_hostName;
        private IPAddress m_ipAddress;
        private Boolean m_Reconnecting = true;

        private UdpClient Listener { get; set; }
        private UdpClient Broadcaster { get; set; }
        private Boolean Done { get; set; }

        private Boolean m_broadcasting = false;

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

        public void Shutdown()
        {
            Done = true;
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
                Listener = new UdpClient(m_listenReciever);

                while (!Done)
                {
                    if (!WirelessUtility.DesktopAppDisabled && CurrentAP.Name != string.Empty)
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
                            m_dropOutSeconds = 0;

                            DesktopConnectionChange(this, new GenericEventArgs<IDesktopData>(deskData));
                        }
                    }
                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                //DebugService.WriteLine(ex.InnerException.ToString());
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

        [EventSubscription(EventNames.RestartBroadcasting, ThreadOption.UserInterface)]
        public void RestartBroad(object sender, EventArgs e)
        {
            m_broadcasting = false;
            m_Reconnecting = true;
            StartBroadcastProc();
        }

        private void ConnectBroadcaster()
        {
            //the only way the desktop client recieves a packet is using the remote ep of IPAddress.Broadcast
            IPEndPoint m_RemoteEP = new IPEndPoint(IPAddress.Broadcast, m_broadcastPort);

            //Bind the UdpClient to the Local IP for Desktop DebugZ
            IPEndPoint m_localEP = new IPEndPoint(Dns.GetHostEntry(Dns.GetHostName()).AddressList[0], 11000);

            Trace.WriteLine("Local Bind");

            Broadcaster = new UdpClient(m_localEP);

            Trace.WriteLine("Remote Connect");

            Broadcaster.Connect(m_RemoteEP);
        }

        public void Broadcast()
        {
            try
            {
                if (m_broadcasting) return;

                string[] args = new string[1];


                //s.Connect(ep);

                while (!Done)
                {
                    m_broadcasting = true;

                    if (CurrentAP.Name != string.Empty && !WirelessUtility.DesktopAppDisabled)
                    {
                        if (m_Reconnecting)
                        {
                            ConnectBroadcaster();
                        }

                        args[0] = m_hostName + ":" + CurrentAP.Name + ":" + CurrentAP.SignalStrength.ToString();
                        byte[] sendbuf = Encoding.ASCII.GetBytes(args[0]);

                        Trace.WriteLine("Sending");
                        Broadcaster.Send(sendbuf, sendbuf.Length);

                        //Check Connectivity to Desktop
                        m_lastReceivedWatch.Stop();
                        m_dropOutSeconds = m_lastReceivedWatch.Elapsed.TotalSeconds;
                        m_lastReceivedWatch.Start();


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
                        DebugService.WriteLine("Broadcasting on" + Broadcaster.Client.LocalEndPoint.ToString());

                        m_Reconnecting = false;
                    }
                    else
                    {
                        m_Reconnecting = true;
                        m_connected = false;
                    }
                    Thread.Sleep(1000);
                }

                m_broadcasting = false;
            }
            catch(Exception ex)
            {
                DebugService.WriteLine(ex.ToString());
                m_broadcasting = false;
                Broadcast();
            }
        }
    }
}
