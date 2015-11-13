using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Hik.Communication.Scs.Communication.Messages;
using Hik.Communication.Scs.Communication.Protocols;

namespace net
{
    public class EzUdpPeer : IDisposable
    {
        UdpPeer _udp_peer = null;

        public event EventHandler<MessageEventArgs> MessageReceived;

        public IScsWireProtocol WireProtocol { get; set; }
        /// <summary>
        /// 必须在Start之前调用
        /// Start之后调用不起作用
        /// </summary>
        public IPAddress MulticastAddress { get; set; }
        public bool IsJoinMulticast { get; set; }

        void udp_peer_DataReceived(object sender, UdpReceiveEventArgs e)
        {
            EzLogger.GlobalLogger.debug(string.Format("udp data from {0}: {1}", 
                e.RemoteEndPoint, e.Data == null ? 0 : e.Data.Length));
            
            IEnumerable<IScsMessage> msgs = this.WireProtocol.CreateMessages(e.Data);
            foreach (var v in msgs)
            {
                OnMessageReceived(e.RemoteEndPoint, v);
            }
        }

        protected virtual void OnMessageReceived(IPEndPoint remote, IScsMessage msg)
        {
            EzLogger.GlobalLogger.debug(string.Format("udp message from {0}: {1}", remote, msg));

            if (MessageReceived != null)
            {
                MessageReceived(remote, new MessageEventArgs(msg));
            }
        }

        public EzUdpPeer(IPAddress address, int port)
        {
            this.WireProtocol = new EzUdpWireProtocol();
            this.MulticastAddress = IPAddress.Parse("224.1.1.100");
            this.IsJoinMulticast = true;
            _udp_peer = new UdpPeer(address, port);
            _udp_peer.DataReceived += udp_peer_DataReceived;
        }
        public EzUdpPeer(string address, int port)
            : this(IPAddress.Parse(address), port)
        {

        }
        public void Start()
        {
            if (_udp_peer != null)
            {
                try
                {
                    if (this.IsJoinMulticast)
                        _udp_peer.JoinMulticastGroup(this.MulticastAddress);
                }
                catch (Exception ex)
                {
                    EzLogger.GlobalLogger.warning(string.Format("{0}{2}{1}{2}",
                        ex.Message, ex.StackTrace, Environment.NewLine));
                }
                _udp_peer.Start();
            }
        }
        public void Dispose()
        {
            if (_udp_peer != null)
            {
                try
                {
                    if (this.IsJoinMulticast)
                        _udp_peer.DropMulticastGroup(this.MulticastAddress);
                }
                catch { }
                
                _udp_peer.Dispose();
                _udp_peer = null;
            }
        }

        public void SendMessage(IPEndPoint remote, IScsMessage msg)
        {
            try
            {
                byte[] buffer = this.WireProtocol.GetBytes(msg);
                if (buffer != null)
                    _udp_peer.SendData(buffer, remote);
            }
            catch (System.Exception ex)
            {
                EzLogger.GlobalLogger.warning(string.Format("{0}{2}{1}{2}",
                    ex.Message, ex.StackTrace, Environment.NewLine));
            }
            
        }
    }
}
