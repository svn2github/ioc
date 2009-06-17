using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Diagnostics;
using WiFiSurveryDesktop.Components;

namespace WiFiSurveryDesktop
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.Timer m_DisplayRefreshTimer = new System.Windows.Forms.Timer();

        private Boolean done = false;
        private Thread m_listenThread;
        private UdpClient m_listener;

        private Dictionary<string, IPAddress> m_ConnectedItems = new Dictionary<string, IPAddress>();

        private int m_listenPort = 11000;
        private int m_broadcastPort = 11001;

        public Form1()
        {
            InitializeComponent();

            m_DisplayRefreshTimer.Interval = 1000;
            m_DisplayRefreshTimer.Tick += new EventHandler(m_DisplayRefreshTimer_Tick);
            m_DisplayRefreshTimer.Enabled = true;

            m_listener = new UdpClient(m_listenPort);
            StartListenProc();       
        }

        void m_DisplayRefreshTimer_Tick(object sender, EventArgs e)
        {
            RefreshConnectedDevices();
        }

        public void StartListenProc()
        {
            m_listenThread = new Thread(new ThreadStart(this.ListenToDevice));
            m_listenThread.IsBackground = true;
            m_listenThread.Start();
        }

        public void ListenToDevice()
        {
            try
            {
                while (!done)
                {
                    Trace.WriteLine("Waiting for broadcast");
                    IPEndPoint m_endPoint = new IPEndPoint(IPAddress.Any, m_listenPort);
                    byte[] bytes = m_listener.Receive(ref m_endPoint);

                    PingDevice(m_endPoint, bytes);
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

        public void RefreshConnectedDevices()
        {
            SuspendLayout();
            while (flowLayoutPanel1.Controls.Count > 0)
            {
                flowLayoutPanel1.Controls[0].Dispose();
            }
            UIAction actionItem;
            foreach(var item in m_ConnectedItems)
            {
                actionItem = new UIAction(item.Value, item.Key);
                actionItem.Width = flowLayoutPanel1.Width - 10;
                flowLayoutPanel1.Controls.Add(actionItem);
            }
            ResumeLayout(true);
        }

        public void PingDevice(IPEndPoint broadcast, byte[] data)
        {
            string[] args = new string[1];
            args[0] = "Wifi";

            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            IPEndPoint m_EndPoint = new IPEndPoint(broadcast.Address, 11001);

            byte[] sendbuf = Encoding.ASCII.GetBytes(args[0]);

            s.EnableBroadcast = true;
            s.SendTo(sendbuf, m_EndPoint);

            string dataString = Encoding.ASCII.GetString(data);

            Trace.WriteLine(("Sent Packet To " + m_EndPoint.ToString() + " @ " + m_EndPoint.Address));

            if (!m_ConnectedItems.Values.Contains(broadcast.Address))
            {
                m_ConnectedItems.Add(dataString, broadcast.Address);
            }

        }

        protected override void Dispose(bool disposing)
        {
            done = true;
            m_listenThread.Abort();

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
