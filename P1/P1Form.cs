using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using net;
using Hik.Communication.Scs.Server;
using Hik.Communication.Scs.Communication.Messages;
using System.Threading;
using Hik.Communication.Scs.Client;

namespace P1
{
    public partial class P1Form : Form
    {
        public readonly string this_name = "p1";

        public readonly AppConfig _config = new AppConfig();

        #region p1 udp peer
        //30001端口接收发送
        EzUdpPeer _p1_udp_peer = null;

        System.Timers.Timer _p1_udp_timer = null;
        
        private void start_p1_udp_peer()
        {
            Helper.start_udp_peer(ref _p1_udp_peer, "p1 udp peer", Helper.p1_udp_port, p1_udp_peer_MessageReceived);

            if (_p1_udp_peer != null)
                Helper.start_timer(ref _p1_udp_timer, "p1 udp timer", 5000, p1_udp_timer_Elapsed);
        }
        private void stop_p1_udp_peer()
        {
            Helper.stop_timer(ref _p1_udp_timer, "p1 udp timer");
            Helper.stop_udp_peer(ref _p1_udp_peer, "p1 udp peer");
        }

        private void p2_udp_handler(IPEndPoint remote, Cmd cmd_base)
        {
            if (cmd_base is ConfirmIpCmd)
            {
                ConfirmIpCmd cmd = cmd_base as ConfirmIpCmd;

                BroadcastAddressCmd ack = new BroadcastAddressCmd();
                ack.sender_name = this_name;
                ack.ip = cmd.ip;
                ack.port = Helper.p1_tcp_listen_port;
                _p1_udp_peer.SendMessage(
                    new IPEndPoint(IPAddress.Parse(ack.ip), remote.Port),
                    ack);
            }
        }
        private void p4_udp_handler(IPEndPoint remote, Cmd cmd_base)
        {
            if (cmd_base is ConfirmIpCmd)
            {
                ConfirmIpCmd cmd = cmd_base as ConfirmIpCmd;

                BroadcastAddressCmd ack = new BroadcastAddressCmd();
                ack.sender_name = this_name;
                ack.ip = cmd.ip;
                ack.port = Helper.p1_tcp_listen_port;
                _p1_udp_peer.SendMessage(
                    new IPEndPoint(IPAddress.Parse(ack.ip), remote.Port),
                    ack);
            }
        }
        private void p5_udp_handler(IPEndPoint remote, Cmd cmd_base)
        {
            if (cmd_base is BroadcastIpListCmd)
            {
                BroadcastIpListCmd cmd = cmd_base as BroadcastIpListCmd;
                int size = cmd.ips == null ? 0 : cmd.ips.Length;
                for (int i = 0; i < size; i++)
                {
                    ConfirmIpCmd ack = new ConfirmIpCmd();
                    ack.ip = cmd.ips[i];
                    ack.sender_name = this_name;
                    _p1_udp_peer.SendMessage(
                        new IPEndPoint(IPAddress.Parse(ack.ip), remote.Port),
                        ack);
                }
            }
            else if (cmd_base is BroadcastAddressCmd)
            {
                if (_p5_tcp_client != null && _p5_tcp_client.Connected)
                    return;

                BroadcastAddressCmd cmd = cmd_base as BroadcastAddressCmd;

                _p5_ip = cmd.ip;
                _p5_port = cmd.port;

                Task.Factory.StartNew(() =>
                {
                    start_p5_tcp_client();
                    connect_p5_tcp_client(cmd.ip, cmd.port);
                });
            }
        }
        private void p1_udp_peer_MessageReceived(object sender, MessageEventArgs e)
        {
            EzLogger.GlobalLogger.info(string.Format("udp receive message from {0}: {1}", sender, e.Message));

            Cmd cmd = e.Message as Cmd;
            if (cmd == null)
                return;

            try
            {
                string sender_name = cmd.sender_name.ToLower();
                if (sender_name.Contains("p2"))
                    p2_udp_handler(sender as IPEndPoint, cmd);
                else if (sender_name.Contains("p4"))
                    p4_udp_handler(sender as IPEndPoint, cmd);
                else if (sender_name.Contains("p5"))
                    p5_udp_handler(sender as IPEndPoint, cmd);
            }
            catch (System.Exception ex)
            {
                EzLogger.GlobalLogger.warning(string.Format("{0}{2}{1}{2}", ex.Message, ex.StackTrace, Environment.NewLine));
            }
        }

        private void p1_udp_timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            IEnumerable<string> ipv4s = Helper.get_localhost_ipv4();
            BroadcastIpListCmd cmd = new BroadcastIpListCmd();
            cmd.sender_name = this_name;
            cmd.ips = ipv4s == null ? null : ipv4s.ToArray();

            //向P2进行多播
            _p1_udp_peer.SendMessage(
                new IPEndPoint(_p1_udp_peer.MulticastAddress, Helper.p2_udp_port),
                cmd);

            //向P3进行多播，目前先不加入
            //todo

            //向P4进行多播
            _p1_udp_peer.SendMessage(
                new IPEndPoint(_p1_udp_peer.MulticastAddress, Helper.p4_udp_port),
                cmd);
        }
        #endregion

        #region p1 tcp server
        AsyncTcpServer _p1_tcp_server = null;

        private void start_p1_tcp_server(int port)
        {
            Helper.start_tcp_server(ref _p1_tcp_server, "p1 tcp server",
                port, true, p1_tcp_server_MessageReceived);
        }
        private void stop_p1_tcp_server()
        {
            Helper.stop_tcp_server(ref _p1_tcp_server, "p1 tcp server");
        }

        private void OnLogin(IScsServerClient sc, LoginCmd cmd)
        {
            Thread.Sleep(3000);

            LoginCmdAck ack = new LoginCmdAck();
            ack.RepliedMessageId = cmd.MessageId;
            ack.sender_name = this_name;
            ack.status = 0;

            Helper.tcp_server_send(sc, ack);
        }
        private void OnServerPrintUser(IScsServerClient sc, ServerPrintUserCmd cmd)
        {
            Thread.Sleep(3000);

            ServerPrintUserCmdAck ack = new ServerPrintUserCmdAck();
            ack.RepliedMessageId = cmd.MessageId;
            ack.sender_name = this_name;
            ack.status = 0;
            ack.print_dcm = cmd.print_dcm;
            ack.print_report = cmd.print_report;
            ack.print_report_ack = 0;
            ack.print_dcm_ack = 0;

            Helper.tcp_server_send(sc, ack);
        }
        private void OnClientPrintUser(IScsServerClient sc, ClientPrintUserCmd cmd)
        {
	        Thread.Sleep(3000);

            ClientPrintUserCmdAck ack = new ClientPrintUserCmdAck();
            ack.RepliedMessageId = cmd.MessageId;
            ack.sender_name = this_name;
            ack.print_dcm = cmd.print_dcm;
            ack.print_report = cmd.print_report;
            ack.print_dcm_ack = 0;
            ack.print_report_ack = 0;
            Helper.tcp_server_send(sc, ack);
            
        }
        private void OnQueryUser(IScsServerClient sc, QueryUserCmd cmd)
        {
            Thread.Sleep(5000);

            QueryUserCmdAck ack = new QueryUserCmdAck();
            ack.RepliedMessageId = cmd.MessageId;
            ack.sender_name = this_name;
            ack.user.id = cmd.user_id;
            ack.user.name = "李四";
            ack.user.is_male = true;
            ack.user.age = 42;
            ack.user.dcm_num = 3;
            ack.user.report_num = 1;
            ack.dcm_files_number = 3;
            ack.report_files_number = 1;
            ack.dcm_files_ready = true;
            ack.report_files_ready = true;
            ack.printed_report_times = 0;
            ack.printed_dcm_times = 2;
            ack.transfer_dcm_files = false;//todo
            ack.transfer_report_files = false;//todo
            Helper.tcp_server_send(sc, ack);
        }
        private void OnGetAllReadyUsers(IScsServerClient sc, GetAllReadyUsersCmd cmd)
        {
            GetAllReadyUsersCmdAck ack = new GetAllReadyUsersCmdAck();
            ack.RepliedMessageId = cmd.MessageId;
            ack.sender_name = this_name;
            ack.users = new List<ReadyUserItem>();
            for (int i = 0; i < 30; i++)
            {
                ack.users.Add(new ReadyUserItem()
                {
                    id = i.ToString(),
                    name = "张三" + i.ToString(),
                    is_male = i % 2 == 0,
                    age = (uint)i + 32,
                    dcm_type = "CT",
                    study_department = "放射科",
                    report_num = 1,
                    dcm_num = 1,
                    desc = "",
                    available = true
                });
            }
            Helper.tcp_server_send(sc, ack);
        }
        private void OnGetChangedReadyUsers(IScsServerClient sc, GetChangedReadyUsersCmd cmd)
        {
            GetChangedReadyUsersCmdAck ack = new GetChangedReadyUsersCmdAck();
            ack.RepliedMessageId = cmd.MessageId;
            ack.sender_name = this_name;
            ack.users = new List<ReadyUserItem>();
            ack.users.Add(new ReadyUserItem()
            {
                id = "10",
                name = "张三",
                is_male = false,
                age = 32,
                dcm_type = "CT",
                study_department = "放射科",
                report_num = 1,
                dcm_num = 1,
                desc = "删除",
                available = false
            });
            ack.users.Add(new ReadyUserItem()
            {
                id = "33",
                name = "张三",
                is_male = false,
                age = 32,
                dcm_type = "CT",
                study_department = "放射科",
                report_num = 1,
                dcm_num = 1,
                desc = "新增",
                available = true
            });
            Helper.tcp_server_send(sc, ack);
        }
        private void p2_tcp_handler(IScsServerClient sc, Cmd cmd)
        {
            switch (cmd.id)
            {
                case CmdType.Query_User_Cmd: OnQueryUser(sc, cmd as QueryUserCmd); break;
                case CmdType.Server_Print_User_Cmd: OnServerPrintUser(sc, cmd as ServerPrintUserCmd); break;
                case CmdType.Login_Cmd: OnLogin(sc, cmd as LoginCmd); break;
                case CmdType.Client_Print_User_Cmd: OnClientPrintUser(sc, cmd as ClientPrintUserCmd); break;
            }
        }
        private void p4_tcp_handler(IScsServerClient sc, Cmd cmd)
        {
            switch (cmd.id)
            {
                case CmdType.Get_All_Ready_Users_Cmd: OnGetAllReadyUsers(sc, cmd as GetAllReadyUsersCmd); break;
                case CmdType.Get_Changed_Ready_Users_Cmd: OnGetChangedReadyUsers(sc, cmd as GetChangedReadyUsersCmd); break;
            }
        }
        private void p1_tcp_server_MessageReceived(IScsServerClient sc, Cmd cmd)
        {
            try
            {
                //心跳消息底层处理过
                if (cmd is HeartBeatCmd)
                    return;

                string sender = cmd.sender_name.ToLower();
                if (sender.Contains("p4"))
                    Task.Factory.StartNew(() => p4_tcp_handler(sc, cmd));
                else if (sender.Contains("p2"))
                    Task.Factory.StartNew(() => p2_tcp_handler(sc, cmd));
            }
            catch (System.Exception ex)
            {
                EzLogger.GlobalLogger.warning(string.Format("{0}{2}{1}{2}", ex.Message, ex.StackTrace, Environment.NewLine));
            }
        }
        #endregion

        #region p5 tcp client
        AsyncTcpClient _p5_tcp_client = null;    

        private void start_p5_tcp_client()
        {
            Helper.start_tcp_client(ref _p5_tcp_client, "p5 tcp client", 
                true, this_name, _p5_tcp_client_MessageReceived);
        }
        private void stop_p5_tcp_client()
        {
            Helper.stop_tcp_client(ref _p5_tcp_client, "p5 tcp client");
        }
        private void connect_p5_tcp_client(string ip, int port)
        {
            Helper.connect_tcp_client(_p5_tcp_client, "p5 tcp client", ip, port);
        }

        private void _p5_tcp_client_MessageReceived(AsyncTcpClient client, Cmd cmd)
        {

        }
        #endregion

        #region ui
        private int _paired_user_number = 0;
        private int _printed_user_number = 0;
        private int _report_user_number = 0;
        private int _dcm_user_number = 0;
        private int _dcm_file_number = 0;

        private string _p5_ip = "0.0.0.0";
        private int _p5_port = 0;

        public P1Form()
        {
            InitializeComponent();

            //初始化logger
            EzLogger.GlobalLogger = new EzLogger(this.logger_ui, "");
        }

        private void P1Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            stop_p1_udp_peer();

            stop_p1_tcp_server();
            stop_p5_tcp_client();
        }
        private void P1Form_Load(object sender, EventArgs e)
        {
            if (_config.IsBroadcastServerIp)
            {
                start_p1_tcp_server(Helper.p1_tcp_listen_port);
                start_p1_udp_peer();
            }
            else
            {
                start_p1_tcp_server(_config.P1ServerPort);

                _p5_ip = _config.P5ServerIp;
                _p5_port = _config.P5ServerPort;

                tcp_connect_timer_Tick(null, null);
            }
        }
        private void _100ms_timer_Tick(object sender, EventArgs e)
        {
            string stmp;
            int p2_client_number = Helper.get_server_client_number(_p1_tcp_server, "p2");
            int p3_client_number = Helper.get_server_client_number(_p1_tcp_server, "p3");
            int p4_client_number = Helper.get_server_client_number(_p1_tcp_server, "p4");

            Helper.set_text_by_tag(tsP2Number, new object[] { p2_client_number });
            Helper.set_text_by_tag(tsP3Number, new object[] { p3_client_number });
            Helper.set_text_by_tag(tsP4Number, new object[] { p4_client_number });

            Helper.set_text_by_tag(tsPaired, new object[] { 
                _paired_user_number, 
                _printed_user_number, 
                _printed_user_number - _printed_user_number 
            });
            Helper.set_text_by_tag(tsReport, new object[] { 
                _report_user_number 
            });
            Helper.set_text_by_tag(tsDcm, new object[] {
                _dcm_user_number,
                _dcm_file_number
            });

            stmp = string.Format("{0}:{1} {2}", _p5_ip, _p5_port, 
                Helper.is_tcp_client_normal(_p5_tcp_client) ? "已连接" : "未连接");
            Helper.set_text_by_tag(tsP5, new object[] { stmp });
        }
        private void tcp_connect_timer_Tick(object sender, EventArgs e)
        {
            if (!_config.IsBroadcastServerIp)
            {
                if (_p5_tcp_client == null || !_p5_tcp_client.Connected)
                {
                    Task.Factory.StartNew(() =>
                    {
                        start_p5_tcp_client();
                        connect_p5_tcp_client(_p5_ip, _p5_port);
                    });
                }
            }
        }
        #endregion
    }
}
