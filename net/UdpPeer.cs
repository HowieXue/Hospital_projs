using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace net
{
    public class UdpPeer : IDisposable
    {
        UdpClient _udp_client = null;
        IPEndPoint _local_endpoint = null;
        bool _disposed = true;

        public event EventHandler<UdpSendEventArgs> DataSent;
        public event EventHandler<UdpReceiveEventArgs> DataReceived;

        public UdpPeer(IPAddress address, int port)
        {
            _local_endpoint = new IPEndPoint(address, port);
            
            _udp_client = new UdpClient(_local_endpoint);

            uint SIO_UDP_CONNRESET = (uint)0x80000000 | 0x18000000 | 12;
            _udp_client.Client.IOControl((int)SIO_UDP_CONNRESET, new byte[] { Convert.ToByte(false) }, null);

            _disposed = false;
        }
        public void Start()
        {
            try
            {
                if (_udp_client != null)
                {
                    ReceiveInternal();
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void Dispose()
        {
            _disposed = true;

            if (_udp_client != null)
            {
                try { _udp_client.Close(); }
                catch { }
                _udp_client = null;
            }
        }
        public void SendData(byte[] buffer, IPEndPoint remote)
        {
            if (_udp_client != null)
            {
                SendInternal(buffer, remote);
            }
        }
        public void SendData(byte[] buffer, long start_index, IPEndPoint remote)
        {
            if (_udp_client != null)
            {
                SendInternal(buffer, start_index, remote);
            }
        }
        public void JoinMulticastGroup(IPAddress address)
        {
            try
            {
                if (_udp_client != null)
                    _udp_client.JoinMulticastGroup(address);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void DropMulticastGroup(IPAddress address)
        {
            try
            {
                if (_udp_client != null)
                    _udp_client.DropMulticastGroup(address);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected void SendInternal(byte[] buffer, IPEndPoint remote)
        {
            if (_disposed)
                return;

            try
            {
                if (_udp_client != null)
                    _udp_client.BeginSend(
                       buffer,
                       buffer.Length,
                       remote,
                       new AsyncCallback(SendCallback),
                       remote);
            }
            catch (SocketException ex)
            {
                Console.WriteLine("send failed.{0}", ex.Message);
            }
        }
        protected void SendInternal(byte[] buffer, long start_index, IPEndPoint remote)
        {
            if (_disposed)
                return;

            try
            {
                long size = buffer.LongLength - start_index;
                if (size > 0)
                {
                    byte[] tmpbuf = new byte[size];
                    Array.Copy(buffer, start_index, tmpbuf, 0, tmpbuf.LongLength);
                    SendInternal(tmpbuf, remote);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("send failed.{0}", ex.Message);
            }
        }
        private void SendCallback(IAsyncResult ar)
        {
            if (_disposed)
                return;

            try
            {
                IPEndPoint remote = ar.AsyncState as IPEndPoint;
                int sent_len = _udp_client.EndSend(ar);
                if (DataSent != null)
                    DataSent(this, new UdpSendEventArgs(remote, sent_len > 0));
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected void ReceiveInternal()
        {
            if (_disposed)
                return;

            try
            {
                if (_udp_client != null)
                    _udp_client.BeginReceive(new AsyncCallback(ReceiveCallback), null);
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void ReceiveCallback(IAsyncResult ar)
        {
            if (_disposed)
                return;

            IPEndPoint remote = null;
            byte[] buffer = null;
            try
            {
                buffer = _udp_client.EndReceive(ar, ref remote);
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (buffer != null && DataReceived != null)
                DataReceived(this, new UdpReceiveEventArgs(remote, buffer));

            ReceiveInternal();
        }
    }

    public class UdpSendEventArgs : EventArgs
    {
        public bool IsSendSuccess { set; get; }
        public IPEndPoint RemoteEndPoint { get; set; }
        public UdpSendEventArgs(IPEndPoint remote, bool success)
        {
            this.RemoteEndPoint = remote;
            this.IsSendSuccess = success;
        }
    }
    public class UdpReceiveEventArgs : EventArgs
    {
        public byte[] Data { get; set; }
        public IPEndPoint RemoteEndPoint { get; set; }
        public UdpReceiveEventArgs(IPEndPoint remote, byte[] data)
        {
            this.RemoteEndPoint = remote;
            this.Data = data;
        }
    }
}
