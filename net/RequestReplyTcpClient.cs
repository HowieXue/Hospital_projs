using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hik.Communication.Scs.Client;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.Scs.Communication.Messengers;

namespace net
{
    public class RequestReplyTcpClient : AsyncTcpClient
    {
        /// <summary>
        /// 
        /// </summary>
        public RequestReplyMessenger<IScsClient> Messenger { get; set; }

        public RequestReplyTcpClient(bool auto_heart_beat)
            : base(auto_heart_beat)
        {
            this.Messenger = null;
        }
        public override void Dispose()
        {
            try
            {
                if (Messenger != null)
                {
                    Messenger.Dispose();
                    Messenger = null;
                }

                base.Dispose();
            }
            catch { }
        }
        public override bool Connect(string remote_ip, int remote_port, int timeout = 10000)
        {
            try
            {
                this.ServerIp = remote_ip;
                this.ServerPort = remote_port;

                EzWireProtocol protocol = new EzWireProtocol();
                protocol.DataReceived += protocol_DataReceived;//接收到数据，但不一定是message
                
                _client = ScsClientFactory.CreateClient(new ScsTcpEndPoint(remote_ip, remote_port));
                _client.WireProtocol = protocol;
                _client.ConnectTimeout = timeout;
                _client.Connected += client_Connected;
                _client.Disconnected += client_Disconnected;

                Messenger = new RequestReplyMessenger<IScsClient>(_client);
                Messenger.MessageReceived += messenger_MessageReceived;
                Messenger.Start();

                _client.Connect();

                this._remain_life_number = this.DefaultLifeNumber;

                Helper.start_timer(ref _1s_timer, "1s timer", 1000, _1s_timer_Elapsed);
            }
            catch (Exception ex)
            {
                EzLogger.GlobalLogger.warning(string.Format("{0}{2}{1}{2}", ex.Message, ex.StackTrace, Environment.NewLine));

                Dispose();

                return false;
            }
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="timeout">ms为单位. 1000 stands for 1s.</param>
        /// <returns></returns>
        public virtual Cmd SendMessageAndWaitForResponse(Cmd cmd, int timeout = 15000)
        {
            if (Messenger == null)
                return null;

            lock (Messenger)
            {
                return Messenger.SendMessageAndWaitForResponse(cmd, timeout) as Cmd;
            }
        }

        private void messenger_MessageReceived(object sender, Hik.Communication.Scs.Communication.Messages.MessageEventArgs e)
        {
            try
            {
                Cmd cmd = e.Message as Cmd;
                if (cmd == null)
                    return;

                OnMessageReceived(cmd);
            }
            catch (System.Exception ex)
            {
                EzLogger.GlobalLogger.warning(string.Format("{0}{2}{1}{2}", ex.Message, ex.StackTrace, Environment.NewLine));
            }
        }
    }
}
