using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using OpenNETCF.Net.NetworkInformation;
using System.Net;
using OpenNETCF.Diagnostics;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;
using WiFiSurvey.Infrastructure.BusinessObjects;
using OpenNETCF.IoC;

namespace WiFiSurvey.Infrastructure.Services
{
    public class NetworkService : INetworkService
    {
        private IDataService DataService { get; set; }

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

            LastRecievedWatch.Start();

            IPAddress[] array = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
            m_ipAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0];

            HostName = Dns.GetHostName();
        }

        public WirelessZeroConfigNetworkInterface Adapter { get; set; }

        public DateTime LastSentTime { get; set; }

        private System.Diagnostics.Stopwatch LastRecievedWatch = new System.Diagnostics.Stopwatch();
        private double m_dropOutSeconds = 0;
        private double m_dropOutSecondsMax = 5;

        private UdpClient m_listener { get; set; }

        private Boolean done { get; set; }

        private int m_listenReciever = 11001;

        private int m_broadcastPort = 11000;

        private TimeSpan m_lastRecievedTime;

        private Thread m_broadcastThread;
        private Thread m_listenThread;

        private IPEndPoint m_endPoint;
        private Boolean Connected;
        private string HostName;

        private IPAddress m_ipAddress;

        public void StartBroadcastProc()
        {
            m_broadcastThread = new Thread(Broadcast);
            m_broadcastThread.IsBackground = true;
            m_broadcastThread.Start();
        }

        public void StartListenerThread()
        {
            Trace.WriteLine("Waiting for broadcast");

            m_listener = new UdpClient(m_listenReciever);

            m_listenThread = new Thread(ListenerProc);
            m_listenThread.IsBackground = true;
            m_listenThread.Start();
        }

        public void ListenerProc()
        {
            try
            {
                while (!WirelessUtility.DesktopAppDisabled)
                {
                    LastRecievedWatch.Reset();
                    LastRecievedWatch.Start();
                    m_endPoint = new IPEndPoint(IPAddress.Broadcast, m_listenReciever);

                    //byte[] data = m_groupMember.Receive();

                    byte[] data = m_listener.Receive(ref m_endPoint);

                    if (data.Length > 0)
                    {
                        Trace.WriteLine(("Received broadcast from " + m_endPoint.ToString() + " @ " + m_endPoint.Port));
                        LastRecievedWatch.Stop();
                        m_lastRecievedTime = LastRecievedWatch.Elapsed;
                        WirelessUtility.DesktopConnected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.InnerException);
            }
            finally
            {
                m_listener.Close();
            }
        }
        public void StartListening()
        {
            DataService = RootWorkItem.Services.Get<IDataService>();

            StartBroadcastProc();
            StartListenerThread();
        }

        ~NetworkService()
        {
            m_broadcastThread.Abort();
            m_listenThread.Abort();
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

            while (!done)
            {
                if (WirelessUtility.CurrentAccessPoint != null)
                {
                    //if (Adapter.OperationalStatus != OperationalStatus.Up)
                    //{
                    //    DataService.NewEvent("Network Conn.", Adapter.OperationalStatus.ToString());
                    //}

                    args[0] = HostName + ":" + WirelessUtility.CurrentAccessPoint.Name + ":" + WirelessUtility.CurrentAccessPoint.SignalStrength.Decibels.ToString();

                    byte[] sendbuf = Encoding.ASCII.GetBytes(args[0]);

                    //s.Send(sendbuf);z
                    m_BroadcastClient.Send(sendbuf, sendbuf.Length);

                    m_dropOutSeconds = LastRecievedWatch.Elapsed.TotalSeconds;

                    if (m_dropOutSeconds > m_dropOutSecondsMax)
                    {
                        WirelessUtility.DesktopConnected = false;
                        if (Connected)
                        {
                            DataService.NewEvent("Desktop Client", "Lost Desktop Connection");
                            Connected = false;
                        }
                    }
                    else
                    {
                        if (!Connected)
                        {
                            WirelessUtility.DesktopConnected = true;
                            DataService.NewEvent("Desktop Client", "Found Desktop Connection");
                            Connected = true;
                        }
                    }

                    Trace.WriteLine("Message sent to " + m_RemoteEP.ToString());
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
