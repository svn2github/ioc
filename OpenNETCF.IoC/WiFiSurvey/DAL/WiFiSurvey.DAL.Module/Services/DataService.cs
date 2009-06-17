using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using WiFiSurvey.Infrastructure.Services;
using System.Net.Sockets;
using System.Net;
using System.Diagnostics;
using System.Threading;
using OpenNETCF.IoC;
using WiFiSurvey.Infrastructure.BusinessObjects;
using OpenNETCF.Net.Sockets;

namespace WiFiSurvey.DAL.Services
{
    public class DataService : IDataService
    {
        public DateTime LastSentTime { get; set; }

        private Stopwatch LastRecievedWatch = new Stopwatch();
        private int m_dropOutSeconds = 0;
        private int m_dropOutSecondsMax = 1;

        private UdpClient m_listener { get; set; }

        private Boolean done { get; set; }

        private int m_listenReciever = 11001;

        private int m_broadcastPort = 11000;

        private DateTime m_lastRecievedTime;

        private Thread m_broadcastThread;
        private Thread m_listenThread;

        private IPEndPoint m_endPoint;

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
                while (!WU.DesktopAppDisabled)
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
                        WU.DesktopConnected = true;
                        //m_lastIpAdress = m_endPoint.Address;
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

        public DataService()
        {
            LastRecievedWatch.Start();
            IPAddress[] array = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
            m_ipAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0];
        }

        public void StartListening()
        {
            StartBroadcastProc();
            StartListenerThread();
        }

        public void Broadcast()
        {
            string[] args = new string[1];
            args[0] = "Wifi";

            //Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            byte[] sendbuf = Encoding.ASCII.GetBytes(args[0]);

            //the only way the desktop client recieves a packet is using the remote ep of IPAddress.Broadcast
            IPEndPoint m_RemoteEP = new IPEndPoint(IPAddress.Broadcast, m_broadcastPort);

            //Bind the UdpClient to the Local IP for Desktop Debug
            IPEndPoint m_localEP = new IPEndPoint(Dns.GetHostEntry(Dns.GetHostName()).AddressList[0], 11000);

            UdpClient m_BroadcastClient = new UdpClient(m_localEP);

            m_BroadcastClient.Connect(m_RemoteEP);
            //s.Connect(ep);

            while (!done)
            {
                //s.Send(sendbuf);
                m_BroadcastClient.Send(sendbuf, 4);

                m_dropOutSeconds = LastRecievedWatch.Elapsed.Seconds;
                if (m_dropOutSeconds > m_dropOutSecondsMax)
                {
                    WU.DesktopConnected = false;
                }

                Trace.WriteLine("Message sent to " + m_RemoteEP.ToString());
                Thread.Sleep(1000);
            }
        }

        //
    }
}
