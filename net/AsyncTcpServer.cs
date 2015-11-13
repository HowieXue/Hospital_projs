using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Hik.Communication.Scs.Communication;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.Scs.Communication.Messages;
using Hik.Communication.Scs.Server;

namespace net
{
    /// <summary>
    /// server的connection dropped可以检测出client drop(主动或被动断开连接)
    /// </summary>
    public class AsyncTcpServer : IDisposable
    {
        #region 内部类
        public class ServerClientDictItem
        {
            /// <summary>
            /// 剩余生命数
            /// </summary>
            public int RemainLifeNumber { get; set; }

            /// <summary>
            /// IScsServerClient实例
            /// </summary>
            public IScsServerClient ServerClient { get; set; }

            /// <summary>
            /// 是否连接过？
            /// </summary>
            public bool Connected
            {
                get { return ServerClient != null && 
                    ServerClient.CommunicationState == CommunicationStates.Connected; }
            }

            /// <summary>
            /// ServerClient是否有效
            /// </summary>
            public bool Available
            {
                get { return Connected && RemainLifeNumber > 0; }
            }

            public string SenderName { get; set; }

            public ServerClientDictItem(IScsServerClient sc, int life_number)
            {
                if (sc == null)
                    throw new ArgumentException("ServerClient构造参数不能为null.");
                this.ServerClient = sc;
                this.RemainLifeNumber = life_number;
                this.SenderName = null;
            }
        }
        public class ServerClientStatusItem
        {
            public string IP { get; set; }
            public int Port { get; set; }
            public bool Available { get; set; }
        }
        public class ServerClientGroupStatusItem
        {
            /// <summary>
            /// 客户端类型
            /// </summary>
            public string ClientType { get; set; }

            /// <summary>
            /// IPEndPoint + Available 列表
            /// </summary>
            public List<ServerClientStatusItem> StatusGroup { get; set; }
        }
        #endregion

        #region 变量
        private IScsServer _server = null;

        private System.Timers.Timer _1s_timer = null;

        /// <summary>
        /// 必定非null
        /// </summary>
        private Dictionary<long, ServerClientDictItem> _sclients = new Dictionary<long, ServerClientDictItem>();
        #endregion

        #region 公共属性
        /// <summary>
        /// 初始化的生命数
        /// </summary>
        public int DefaultLifeNumber { get; set; }

        /// <summary>
        /// 是否主动检测客户端的available
        /// true，启动定时器，主动检测来自客户端的消息，15s内未收到，则客户端异常
        /// false，不检测客户端是否异常，但是disconnect消息仍可被动响应
        /// </summary>
        public bool ActiveCheckClientAvailable { get; private set; }

        public int ListenPort { get; private set; }
        #endregion

        #region 事件
        public event Action<IScsServerClient, Cmd> MessageReceived;

        public event Action<IScsServerClient> ConnectionArrived;

        /// <summary>
        /// 可以主动检测available属性，基本不需要响应事件
        /// </summary>
        public event Action<IScsServerClient> ConnectionDropped;
        #endregion

        #region 私有/保护函数
        protected void server_ClientDisconnected(object sender, ServerClientEventArgs e)
        {
            EzLogger.GlobalLogger.info(string.Format("{0} client(s). disconnected: {1}", 
                _server.Clients.Count, e.Client.RemoteEndPoint));

            try { lock (_sclients) { _sclients.Remove(e.Client.ClientId); } }
            catch { }

            if (ConnectionDropped != null)
                ConnectionDropped(e.Client);
        }
        protected void server_ClientConnected(object sender, ServerClientEventArgs e)
        {
            e.Client.MessageReceived += Client_MessageReceived;
            EzLogger.GlobalLogger.info(string.Format("{0} client(s). connected: {1}", 
                _server.Clients.Count, e.Client.RemoteEndPoint));

            try
            {
                ServerClientDictItem sc = new ServerClientDictItem(e.Client, this.DefaultLifeNumber);
                lock (_sclients) { _sclients.Add(e.Client.ClientId, sc); }
            }
            catch (System.Exception ex)
            {
                EzLogger.GlobalLogger.warning(string.Format("{0}{2}{1}{2}",
                    ex.Message, ex.StackTrace, Environment.NewLine));
            }

            if (ConnectionArrived != null)
                ConnectionArrived(e.Client);
        }
        protected void Client_MessageReceived(object sender, MessageEventArgs e)
        {
            try
            {
                var client = sender as IScsServerClient;
                var cmd = e.Message as Cmd;
                try 
                { 
                    ServerClientDictItem sci = _sclients[client.ClientId];
                    sci.RemainLifeNumber = DefaultLifeNumber;
                    if (string.IsNullOrWhiteSpace(sci.SenderName))
                        sci.SenderName = cmd.sender_name; 
                }
                catch { }
                
                EzLogger.GlobalLogger.info(string.Format("tcp server receive message from {0}: {1}", 
                    client.RemoteEndPoint, cmd.ToString()));

                if (client != null)
                {
                    //心跳自动回复，不需要进一步处理
                    if (cmd is HeartBeatCmd)
                        client.SendMessage(cmd);

                    if (MessageReceived != null)
                        MessageReceived(client, cmd);
                }
            }
            catch (System.Exception ex)
            {
                EzLogger.GlobalLogger.warning(string.Format("{0}{2}{1}{2}",
                    ex.Message, ex.StackTrace, Environment.NewLine));
            }
        }

        private bool start_1s_timer()
        {
            return Helper.start_timer(ref _1s_timer, "tcp server 1s timer", 1000, _1s_timer_Elapsed);
        }
        private void stop_1s_timer()
        {
            Helper.stop_timer(ref _1s_timer, "tcp server 1s timer");
        }
        private void _1s_timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                lock (_sclients)
                {
                    List<IScsServerClient> del_clients = new List<IScsServerClient>();
                    foreach (var sc in _sclients)
                    {
                        if (sc.Value == null || sc.Value.ServerClient == null)
                        {
                            _sclients.Remove(sc.Key);
                            continue;
                        }

                        if (sc.Value.RemainLifeNumber > 0)
                        {
                            sc.Value.RemainLifeNumber--;
                            if (sc.Value.RemainLifeNumber <= 0)
                            {
                                EzLogger.GlobalLogger.info("tcp server receive from " +
                                    sc.Value.ServerClient.RemoteEndPoint +
                                    " timeout.");

                                del_clients.Add(sc.Value.ServerClient);
                            }
                        }
                    }

                    foreach (var sc in del_clients)
                    {
                        _server.Clients.Remove(sc.ClientId);
                        _sclients.Remove(sc.ClientId);
                        sc.Disconnect();

                        if (ConnectionDropped != null)
                            ConnectionDropped(sc);
                    }
                }
            }
            catch (Exception ex)
            {
                EzLogger.GlobalLogger.warning(string.Format("{0}{2}{1}{2}",
                    ex.Message, ex.StackTrace, Environment.NewLine));
            }
        }
        #endregion

        #region 公共函数
        public AsyncTcpServer(int listen_port, bool check_client_available)
        {
            this.ListenPort = listen_port;
            this.DefaultLifeNumber = 15;//接收超时设为15s
            this.ActiveCheckClientAvailable = check_client_available;
        }
        public void Dispose()
        {
            try
            {
                stop_1s_timer();

                _sclients.Clear();

                if (_server != null)
                {
                    _server.Stop();
                    _server = null;
                }
            }
            catch { }
        }
        public bool Start()
        {
            try 
            {
                if (_server == null)
                {
                    _server = ScsServerFactory.CreateServer(new ScsTcpEndPoint(this.ListenPort));
                    _server.WireProtocolFactory = new EzWireProtocolFactory();
                    _server.ClientConnected += server_ClientConnected;
                    _server.ClientDisconnected += server_ClientDisconnected;
                    _server.Start();

                    if (this.ActiveCheckClientAvailable)
                        start_1s_timer();
                }
                return true;
            }
            catch (Exception ex)
            {
                EzLogger.GlobalLogger.warning(string.Format("{0}{2}{1}{2}",
                    ex.Message, ex.StackTrace, Environment.NewLine));

                Dispose();

                return false; 
            }
        }
        /// <summary>
        /// 获取列表
        /// 例如：p1 n个ip+port+available
        /// </summary>
        /// <returns></returns>
        public List<ServerClientGroupStatusItem> GetClientStatusGroup()
        {
            if (_sclients == null || _sclients.Count <= 0)
                return null;

            lock (_sclients)
            {
                List<string> group_names = new List<string>();
                foreach (var v in _sclients)
                {
                    string name = v.Value.SenderName ?? "";
                    if (!group_names.Contains(name))
                        group_names.Add(name);
                }

                List<ServerClientGroupStatusItem> groups = new List<ServerClientGroupStatusItem>();
                foreach (var v in group_names)
                {
                    ServerClientGroupStatusItem group = new ServerClientGroupStatusItem();
                    group.ClientType = v;
                    group.StatusGroup = new List<ServerClientStatusItem>();
                    foreach (var client in _sclients)
                    {
                        string name = client.Value.SenderName ?? "";
                        string ep = client.Value.ServerClient.RemoteEndPoint.ToString();
                        int start_digit_index = 0;
                        for (; start_digit_index < ep.Length; start_digit_index++)
                        {
                            if (char.IsDigit(ep, start_digit_index))
                                break;
                        }
                        if (start_digit_index >= ep.Length)
                            return null;

                        ep = ep.Substring(start_digit_index);
                        string[] ss = ep.Split(':');
                        if (name == v)
                        {
                            ServerClientStatusItem scsi = new ServerClientStatusItem();
                            scsi.Available = client.Value.Available;
                            scsi.IP = ss[0].Trim();
                            scsi.Port = int.Parse(ss[1]);
                            group.StatusGroup.Add(scsi);
                        }
                    }
                }
                return groups;
            }
        }
        public List<ServerClientStatusItem> GetClientStatus(string sender)
        {
            if (_sclients == null || _sclients.Count <= 0)
                return null;

            lock (_sclients)
            {
                string sender_name = sender ?? "";
                List<ServerClientStatusItem> group = new List<ServerClientStatusItem>();
                foreach (var client in _sclients)
                {
                    string name = client.Value.SenderName ?? "";
                    string ep = client.Value.ServerClient.RemoteEndPoint.ToString();
                    int start_digit_index = 0;
                    for (; start_digit_index < ep.Length; start_digit_index++)
                    {
                        if (char.IsDigit(ep, start_digit_index))
                            break;
                    }
                    if (start_digit_index >= ep.Length)
                        return null;

                    ep = ep.Substring(start_digit_index);
                    string[] ss = ep.Split(':');
                    if (name == sender_name)
                    {
                        ServerClientStatusItem scsi = new ServerClientStatusItem();
                        scsi.Available = client.Value.Available;
                        scsi.IP = ss[0].Trim();
                        scsi.Port = int.Parse(ss[1]);
                        group.Add(scsi);
                    }
                }
                return group;
            }
        }
        public int GetClientStatusNumber(string sender)
        {
            if (_sclients == null || _sclients.Count <= 0)
                return 0;

            lock (_sclients)
            {
                string sender_name = sender ?? "";
                int number = 0;
                foreach (var client in _sclients)
                {
                    string name = client.Value.SenderName ?? "";
                    if (name == sender_name)
                        number++;
                }
                return number;
            }
        }
        #endregion
    }
}
