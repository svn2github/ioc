﻿using System;
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

        private List<ConnectedDevice> ConnectedDevices = new List<ConnectedDevice>();
        private Dictionary<IPAddress, UIAction> DeviceControls = new Dictionary<IPAddress, UIAction>();

        private int m_listenPort = 11000;
        private int m_broadcastPort = 11001;

        private int pingCount = 0;

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
                    IPEndPoint m_endPoint = new IPEndPoint(IPAddress.Any, m_listenPort);
                    byte[] bytes = m_listener.Receive(ref m_endPoint);
                    PingDevice(m_endPoint, bytes);
                    Thread.Sleep(1000);
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
            UIAction actionItem;
            foreach(var item in ConnectedDevices)
            {
                if (DeviceControls.ContainsKey(item.IPAdress))
                {
                    DateTime lastPing = item.LastPing;
                    DateTime compareTime = DateTime.Now.AddSeconds(-5);

                    if (lastPing.CompareTo(compareTime) == 1)
                    {
                        DeviceControls[item.IPAdress].UpdateData(item.Data);
                        DeviceControls[item.IPAdress].Connected = true;
                    }
                    else
                    {
                        if (DeviceControls[item.IPAdress].Connected == true)
                        {
                            DeviceControls[item.IPAdress].Connected = false;
                        }
                    }
                    DeviceControls[item.IPAdress].Refresh();
                }
                else
                {
                    actionItem = new UIAction(item.IPAdress, item.Data);
                    actionItem.Width = flowLayoutPanel1.Width - 10;
                    actionItem.Connected = true;
                    flowLayoutPanel1.Controls.Add(actionItem);
                    DeviceControls.Add(item.IPAdress, actionItem);
                }
            }
            ResumeLayout(true);
        }

        public void PingDevice(IPEndPoint broadcast, byte[] data)
        {
            Boolean foundDevice = false;
            string[] args = new string[1];
            args[0] = "Wifi";

            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            IPEndPoint m_EndPoint = new IPEndPoint(broadcast.Address, 11001);

            Trace.WriteLine("Ping to " + broadcast.Address.ToString() +" "+ pingCount++);

            byte[] sendbuf = Encoding.ASCII.GetBytes(args[0]);

            s.EnableBroadcast = true;
            s.SendTo(sendbuf, m_EndPoint);

            string dataString = Encoding.ASCII.GetString(data);

            foreach (var item in ConnectedDevices)
            {
                if (item.IPAdress.ToString() == broadcast.Address.ToString())
                {
                    foundDevice = true;
                    item.Data = dataString;
                    item.LastPing = DateTime.Now;
                    break;
                }
            }
            if (!foundDevice)
            {
                ConnectedDevice device = new ConnectedDevice();
                device.IPAdress = broadcast.Address;
                device.Data = dataString;
                ConnectedDevices.Add(device);
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