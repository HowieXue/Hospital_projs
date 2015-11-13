using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hik.Communication.Scs.Client;
using Hik.Communication.Scs.Server;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.Scs.Communication.Messages;
using Hik.Communication.Scs.Communication;
using Hik.Communication.Scs.Client.Tcp;
using System.Net;

namespace net
{
    public class AsyncTcpClient : IDisposable
    {
        #region 变量
        protected IScsClient _client = null;

        /// <summary>
        /// connect后自动启动，每1s将Life减1
        /// Life<=0表示连接不正常
        /// </summary>
        protected System.Timers.Timer _1s_timer = null;

        /// <summary>
        /// 剩余生命数
        /// </summary>
        protected int _remain_life_number = 15;
        /// <summary>
        /// 初始化生命数
        /// </summary>
        protected int _default_life_number = 15;

        /// <summary>
        /// 心跳剩余数
        /// <=0表示连接不正常
        /// </summary>
        protected int _remain_heart_beat_number;
        #endregion

        #region 事件
        /// <summary>
        /// 收到消息事件
        ///     必须是收到完整消息
        /// </summary>
        public event Action<AsyncTcpClient, Cmd> MessageReceived;

        /// <summary>
        /// 连接到来
        /// </summary>
        public event Action<AsyncTcpClient> ConnectionArrived;

        /// <summary>
        /// 连接失效事件
        ///     disconnection，或者其它未知原因造成连接失效
        /// </summary>
        public event Action<AsyncTcpClient> ConnectionDropped;
        #endregion

        #region 属性
        /// <summary>
        /// server ip : port
        /// </summary>
        public string ServerIp { get; protected set; }
        public int ServerPort { get; protected set; }

        /// <summary>
        /// Connected不能作为连接是否正常的标准
        /// 只能判断是否正常连接过
        /// 中间断开后无法判断，用Life>0表示连接正常
        /// </summary>
        public bool Connected 
        {
            get
            {
                return _client == null ? false : _client.CommunicationState == CommunicationStates.Connected;
            }
        }

        /// <summary>
        /// 初始化的生命数
        /// </summary>
        public int DefaultLifeNumber
        {
            get { return _default_life_number; }
            set
            {
                _default_life_number = value;
                _remain_life_number = value;
            }
        }

        /// <summary>
        /// 该属性能作为连接是否正常的标准
        /// true，连接有效；false，连接失效
        /// </summary>
        public bool Available
        {
            get
            {
                return Connected && (_remain_life_number > 0);
            }
        }

        /// <summary>
        /// connect之前设置有效
        /// </summary>
        public bool AutoHeartBeat { get; set; }

        /// <summary>
        /// 单位:s。默认5.
        /// 心跳数初始值
        /// </summary>
        public int DefaultHeartBeatNumber { get; set; }

        /// <summary>
        /// 发送者名称
        /// </summary>
        public string HeartBeatSender { get; set; }
        #endregion

        #region 私有函数
        protected virtual void OnMessageReceived(Cmd cmd)
        {
            EzLogger.GlobalLogger.debug(string.Format("tcp client receive {2} from {0}:{1}",
                    this.ServerIp, this.ServerPort, cmd.GetType().Name));
            EzLogger.GlobalLogger.debug(cmd.ToString());

            if (MessageReceived != null)
                MessageReceived(this, cmd);
        }
        protected virtual void OnConnectionArrived()
        {
            EzLogger.GlobalLogger.info(string.Format("tcp client connect to [{0}:{1}]: {2}",
                this.ServerIp, this.ServerPort, this.Connected));

            if (ConnectionArrived != null)
                ConnectionArrived(this);
        }
        protected virtual void OnConnectionDropped()
        {
            EzLogger.GlobalLogger.info(string.Format("tcp client disconnect from [{0}:{1}]: {2}",
                this.ServerIp, this.ServerPort, this.Connected));

            if (ConnectionDropped != null)
                ConnectionDropped(this);
        }

        protected void _1s_timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (this._remain_life_number > 0)
            {
                this._remain_life_number--;
                if (this._remain_life_number <= 0)
                {
                    EzLogger.GlobalLogger.info("tcp client receive timeout, life = " + this._remain_life_number);

                    this.Dispose();

                    OnConnectionDropped();
                }
            }

            if (this.AutoHeartBeat && (_remain_heart_beat_number-- <= 0))
            {
                _remain_heart_beat_number = this.DefaultHeartBeatNumber;

                if (this.Connected)
                {
                    HeartBeatCmd hb = new HeartBeatCmd();
                    hb.sender_name = this.HeartBeatSender;
                    Helper.tcp_client_send(this, hb);
                }
            }

        }
        protected void protocol_DataReceived(object sender, byte[] e)
        {
            this._remain_life_number = this.DefaultLifeNumber;
        }
        protected void client_MessageReceived(object sender, MessageEventArgs e)
        {
            try
            {
                var client = sender as IScsClient;
                Cmd cmd = e.Message as Cmd;
                if (cmd == null)
                    return;

                OnMessageReceived(cmd);
            }
            catch (System.Exception ex)
            {
                EzLogger.GlobalLogger.warning(string.Format("{0}{2}{1}{2}", 
                    ex.Message, ex.StackTrace, Environment.NewLine));
            }

        }
        protected void client_Disconnected(object sender, EventArgs e)
        {
            var client = sender as IScsClient;

            OnConnectionDropped();

            this.Dispose();
        }
        protected void client_Connected(object sender, EventArgs e)
        {
            var client = sender as IScsClient;

            OnConnectionArrived();
        }
        #endregion

        #region 公共函数
        public AsyncTcpClient(bool auto_heart_beat)
        {
            this.ServerIp = "0.0.0.0";
            this.ServerPort = 0;

            this.DefaultLifeNumber = 15;//15s内收不到数据则意味着连接失效

            this.AutoHeartBeat = auto_heart_beat;
            this.DefaultHeartBeatNumber = 5;//5s发1次
            this._remain_heart_beat_number = 0;
        }
        public virtual bool Connect(string remote_ip, int remote_port, int timeout=10000)
        {
            try
            {
                this.ServerIp = remote_ip;
                this.ServerPort = remote_port;

                EzWireProtocol protocol = new EzWireProtocol();
                protocol.DataReceived += protocol_DataReceived;//接收到数据，但不一定是message

                _client = ScsClientFactory.CreateClient(new ScsTcpEndPoint(remote_ip, remote_port));
                _client.WireProtocol = protocol;
                _client.Connected += client_Connected;
                _client.Disconnected += client_Disconnected;
                _client.MessageReceived += client_MessageReceived;
                _client.ConnectTimeout = timeout;
                _client.Connect();

                this._remain_life_number = this.DefaultLifeNumber;

                Helper.start_timer(ref _1s_timer, "1s timer", 1000, _1s_timer_Elapsed);
            }
            catch (Exception ex)
            {
                EzLogger.GlobalLogger.warning(string.Format("{0}{2}{1}{2}",
                    ex.Message, ex.StackTrace, Environment.NewLine));

                Dispose();

                return false; 
            }
            return true;
        }
        public virtual void Dispose()
        {
            Helper.stop_timer(ref _1s_timer, "1s timer");

            try { _client.Disconnect(); }
            catch { }
            _client = null;
        }
        public void SendMessage(IScsMessage message)
        {
            try { if (Connected) { _client.SendMessage(message); } }
            catch (System.Exception ex) { EzLogger.GlobalLogger.debug(ex.Message); }
        }
        #endregion
    }
}
