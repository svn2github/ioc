using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace OpenNETCF.Net.Sockets
{
    public delegate void MulticastDataDelegate(byte[] data);

    public class MulticastGroupMember
    {
        private Socket m_multicastListener;
        private Socket m_multicastSender;
        private IPEndPoint m_listenerEndPoint;
        private IPEndPoint m_senderEndPoint;
        private int m_sendPort = 0;
        private int m_receivePort = 0;
        private IAsyncResult m_asyncResult = null;
        private bool m_waitingSynchronous = false;
        private bool m_stopAsync = false;

        SocketStateObject m_rxState;

        public event MulticastDataDelegate OnDataReceived;

        public MulticastGroupMember(int sendPort, int receivePort)
            : this(sendPort, receivePort, IPAddress.Broadcast)
        {
        }

        public MulticastGroupMember(int sendPort, int receivePort, IPAddress multicastGroup)
        {
            // Windows CE itself does not support the multicast loopback socket option, so we must use separate
            // ports if we don't want to hear what we send
            m_sendPort = sendPort;
            m_receivePort = receivePort;

            m_multicastListener = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            m_listenerEndPoint = new IPEndPoint(IPAddress.Any, m_receivePort);

            m_multicastListener.Bind(m_listenerEndPoint);
            m_multicastListener.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, 0);
            // see if it's a multicast or broadcast address
            if (multicastGroup != IPAddress.Broadcast)
            {
                m_multicastListener.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership,
                    new MulticastOption(multicastGroup, IPAddress.Any));
            }

            m_multicastSender = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            m_senderEndPoint = new IPEndPoint(multicastGroup, m_sendPort);

            m_multicastSender.Connect(m_senderEndPoint);
            // join the multicast group
            if (multicastGroup != IPAddress.Broadcast)
            {
                m_multicastSender.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership,
                    new MulticastOption(multicastGroup));
            }
            // set the TTL - allow us to pass through 1 router on our way
            m_multicastSender.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, 1);

            m_rxState = new SocketStateObject(m_multicastListener, 1024);
        }

        ~MulticastGroupMember()
        {         
            m_multicastSender.Close();
            m_multicastListener.Close();
        }

        public int Send(byte[] data)
        {
            return m_multicastSender.Send(data);
        }

        private byte[] m_inbuffer = new byte[1024];

        public void StartAsyncEvents()
        {
            m_stopAsync = false;
            m_asyncResult = m_multicastListener.BeginReceive(m_rxState.Data, 0, m_rxState.BufferSize, SocketFlags.Partial, ReceiveCallback, m_rxState);
        }

        public void StopAsyncEvents()
        {
            m_stopAsync = true;
        }

        public byte[] Receive()
        {
            m_waitingSynchronous = true;

            if (m_asyncResult != null)
            {
                // an async call was made - see if it's still pending
                if (!m_asyncResult.IsCompleted)
                {
                    // async call still pending
                    throw new Exception("Receive cannot be called on a socket already waiting for an asynchronous callback");
                }
            }

            int count = m_multicastListener.Receive(m_inbuffer);
            byte[] data = new byte[count];
            Buffer.BlockCopy(m_inbuffer, 0, data, 0, count);
            return data;
        }

        private void ReceiveCallback(IAsyncResult result)
        {

            int count = m_rxState.Socket.EndReceive(result);
            lock (m_multicastListener)
            {
                if (count > 0)
                {
                    if (OnDataReceived != null)
                    {
                        byte[] data = new byte[count];
                        Buffer.BlockCopy(m_rxState.Data, 0, data, 0, count);
                        OnDataReceived(data);
                    }
                }
            }

            m_asyncResult = null;

            // set up to receive more
            if ((! m_waitingSynchronous) && (!m_stopAsync))
            {
                m_multicastListener.BeginReceive(m_rxState.Data, 0, m_rxState.BufferSize, SocketFlags.Partial, ReceiveCallback, m_rxState);
            }
        }

        class SocketStateObject
        {
            public Socket Socket;
            public byte[] Data;

            public SocketStateObject(Socket socket, int bufferSize)
            {
                Socket = socket;
                Data = new byte[bufferSize];
            }

            public int BufferSize
            {
                get { return Data.Length; }
            }
        }
    }
}
