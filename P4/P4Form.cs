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
using control;
using Hik.Communication.Scs.Server;
using Hik.Communication.Scs.Communication.Messages;
using Hik.Communication.Scs.Client;

namespace P4
{
    public partial class P4Form : Form
    {
        public readonly string this_name = "p4";

        //true打开udp，服务器广播地址；false用本地配置
        public readonly bool broadcast_server_ip;
        public readonly string p1_server_ip, p5_server_ip;
        public readonly int p1_server_port, p5_server_port;

        #region net
        EzUdpPeer _p4_udp_peer = null;
        
        AsyncTcpClient _p1_tcp_client = null;
        AsyncTcpClient _p5_tcp_client = null;

        private bool start_p4_udp_peer()
        {
            return Helper.start_udp_peer(ref _p4_udp_peer, "p4 udp peer", 
                Helper.p4_udp_port, p4_udp_peer_MessageReceived);
        }
        private void stop_p4_udp_peer()
        {
            Helper.stop_udp_peer(ref _p4_udp_peer, "p4 udp peer");
        }

        private void start_p1_tcp_client()
        {
            Helper.start_tcp_client(ref _p1_tcp_client, "p1 tcp client", 
                true, this_name, p1_tcp_client_MessageReceived);
        }
        private void stop_p1_tcp_client()
        {
            Helper.stop_tcp_client(ref _p1_tcp_client, "p1 tcp client");
        }
        private void connect_p1_tcp_client(string ip, int port)
        {
            Helper.connect_tcp_client(_p1_tcp_client, "p1 tcp client", ip, port);

            if (Helper.is_tcp_client_normal(_p1_tcp_client))
            {
                //get all users命令连接成功发一次
                _get_changed_users_time = DateTime.Now;
                Helper.tcp_client_send(_p1_tcp_client, new GetAllReadyUsersCmd()
                {
                    sender_name = this_name,
                    time1 = DateTime.Today,
                    time2 = _get_changed_users_time
                });
            }
        }

        private void start_p5_tcp_client()
        {
            Helper.start_tcp_client(ref _p5_tcp_client, "p5 tcp client", 
                true, this_name, p5_tcp_client_MessageReceived);
        }
        private void stop_p5_tcp_client()
        {
            Helper.stop_tcp_client(ref _p5_tcp_client, "p5 tcp client");
        }
        private void connect_p5_tcp_client(string ip, int port)
        {
            Helper.connect_tcp_client(_p5_tcp_client, "p5 tcp client", ip, port);
        }

        private void p1_cmd_handler(IPEndPoint remote, Cmd cmd_base)
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
                    _p4_udp_peer.SendMessage(
                        new IPEndPoint(IPAddress.Parse(ack.ip), remote.Port),
                        ack);
                }
            }
            else if (cmd_base is BroadcastAddressCmd)
            {
                if (_p1_tcp_client != null && _p1_tcp_client.Connected)
                    return;

                BroadcastAddressCmd cmd = cmd_base as BroadcastAddressCmd;

                Helper.set_text(this.lblP1Ip, string.Format("{0}:{1}", cmd.ip, cmd.port));

                Task.Factory.StartNew(() =>
                {
                    start_p1_tcp_client();
                    connect_p1_tcp_client(cmd.ip, cmd.port);
                });
            }
        }
        private void p5_cmd_handler(IPEndPoint remote, Cmd cmd_base)
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
                    _p4_udp_peer.SendMessage(
                        new IPEndPoint(IPAddress.Parse(ack.ip), remote.Port),
                        ack);
                }
            }
            else if (cmd_base is BroadcastAddressCmd)
            {
                if (_p5_tcp_client != null && _p5_tcp_client.Connected)
                    return;

                BroadcastAddressCmd cmd = cmd_base as BroadcastAddressCmd;

                Helper.set_text(this.lblP5Ip, string.Format("{0}:{1}", cmd.ip, cmd.port));

                Task.Factory.StartNew(() =>
                {
                    start_p5_tcp_client();
                    connect_p5_tcp_client(cmd.ip, cmd.port);
                });
            }
        }
        void p4_udp_peer_MessageReceived(object sender, MessageEventArgs e)
        {
            Cmd cmd = e.Message as Cmd;
            if (cmd == null)
                return;

            try
            {
                string sender_name = cmd.sender_name.ToLower();
                if (sender_name.Contains("p5"))
                    p5_cmd_handler(sender as IPEndPoint, cmd);
                else if (sender_name.Contains("p1"))
                    p1_cmd_handler(sender as IPEndPoint, cmd);
            }
            catch (System.Exception ex)
            {
                EzLogger.GlobalLogger.warning(string.Format("{0}{2}{1}{2}", ex.Message, ex.StackTrace, Environment.NewLine));
            }
        }

        void p1_tcp_client_MessageReceived(AsyncTcpClient client, Cmd cmd)
        {
            if (cmd == null || (cmd is HeartBeatCmd))
                return;

            if (!cmd.sender_name.ToLower().Contains("p1"))
                return;

            try
            {
                switch (cmd.id)
                {
                    case CmdType.Get_All_Ready_Users_Cmd_Ack: OnGetAllReadyUsersAck(_p1_tcp_client, cmd as GetAllReadyUsersCmdAck); break;
                    case CmdType.Get_Changed_Ready_Users_Cmd_Ack: OnGetChangedUsersAck(_p1_tcp_client, cmd as GetChangedReadyUsersCmdAck); break;
                }
            }
            catch (System.Exception ex)
            {
                EzLogger.GlobalLogger.debug(ex.Message);
            }
        }
        private void OnGetChangedUsersAck(AsyncTcpClient client, GetChangedReadyUsersCmdAck ack)
        {
            lock (_users_lock)
            {
                //改变显示
                UpdateChangedUsersToDisplay(ack);
            }
        }
        private void OnGetAllReadyUsersAck(AsyncTcpClient client, GetAllReadyUsersCmdAck ack)
        {
            lock (_users_lock)
            {
                this._users = ack.users;
                ReplaceAllUsersToDisplay(this._users);
            }
            scroll.Reset();
        }
        private void ReplaceAllUsersToDisplay(List<ReadyUserItem> users)
        {
            scroll.Rows.Clear();

            if (users == null)
                return;

            users.ForEach(user =>
            {
                scroll.AddRow(new object[]
                {
                    user.id,
                    user.name ?? "",
                    user.is_male ? "男" : "女",
                    user.age,
                    user.dcm_type ?? "",
                    user.study_department ?? "",
                    user.desc ?? ""
                });
            });
        }
        private void UpdateChangedUsersToDisplay(IEnumerable<ReadyUserItem> add_users,
            IEnumerable<ReadyUserItem> remove_users)
        {
            List<ReadyUserItem> tmp = this._users ?? new List<ReadyUserItem>();
            List<ReadyUserItem> real_add = add_users == null ? new List<ReadyUserItem>() :
                add_users.Where(user =>
                {
                    return !tmp.Select(au => au.id).Contains(user.id);
                }).ToList();
            List<ReadyUserItem> real_remove = remove_users == null ? new List<ReadyUserItem>() :
                remove_users.Where(user =>
                {
                    return tmp.Select(ru => ru.id).Contains(user.id);
                }).ToList();

            if (real_add != null)
            {
                tmp.AddRange(real_add);

                real_add.ForEach(user =>
                {
                    scroll.AddRow(new object[]
                    {
                        user.id,
                        user.name ?? "",
                        user.is_male ? "男" : "女",
                        user.age,
                        user.dcm_type ?? "",
                        user.study_department ?? "",
                        user.desc ?? ""
                    });
                });
            }

            if (real_remove != null)
            {
                tmp = tmp.Where(user =>
                {
                    return !real_remove.Select(ru => ru.id).Contains(user.id);
                }).ToList();

                scroll.Rows.RemoveAll(row =>
                {
                    string id = row.Cells[1].Text;
                    return real_remove.Select(ru => ru.id).Contains(id);
                });
            }

            this._users = tmp;
        }
        private void UpdateChangedUsersToDisplay(GetChangedReadyUsersCmdAck ack)
        {
            IEnumerable<ReadyUserItem> add_users = ack.users.Where(user => { return user.available; });
            IEnumerable<ReadyUserItem> remove_users = ack.users.Where(user => { return !user.available; });
            UpdateChangedUsersToDisplay(add_users, remove_users);
        }

        void p5_tcp_client_MessageReceived(AsyncTcpClient client, Cmd cmd)
        {
            if (cmd == null || cmd is HeartBeatCmd)
                return;

            if (!cmd.sender_name.ToLower().Contains("p5"))
                return;

            try
            {
                //只向p5发心跳、报告消息，其返回不关心
            }
            catch (System.Exception ex)
            {
                EzLogger.GlobalLogger.debug(ex.Message);
            }
        }
        #endregion

        //待显示用户信息列表
        List<ReadyUserItem> _users = null;
        object _users_lock = new object();

        //记录获取改变列表的时刻
        DateTime _get_changed_users_time = DateTime.Today;
        //用100ms定时器计时值10s
        int _get_changed_users_tick_set = 100;
        int _get_changed_users_tick = 100;

        public P4Form()
        {
            //读取配置文件
            broadcast_server_ip = bool.Parse(ConfigurationManager.AppSettings["broadcast_server_ip"]);
            if (!broadcast_server_ip)
            {
                p1_server_ip = ConfigurationManager.AppSettings["p1_server_ip"];
                p1_server_port = int.Parse(ConfigurationManager.AppSettings["p1_server_port"]);
                p5_server_ip = ConfigurationManager.AppSettings["p5_server_ip"];
                p5_server_port = int.Parse(ConfigurationManager.AppSettings["p5_server_port"]);
            }

            InitializeComponent();

            this.Location = Screen.PrimaryScreen.Bounds.Location;
            this.Size = Screen.PrimaryScreen.Bounds.Size;

            this.lblP1Ip.Text = "";
            this.lblP5Ip.Text = "";
        }

        private void P4Form_Load(object sender, EventArgs e)
        {
            if (broadcast_server_ip)
            {
                if (!start_p4_udp_peer())
                {
                    Helper.msgbox_info(this_name, 
                        string.Format("启动udp peer失败.{0}请检查端口{1}是否已经被占用.{0}", 
                        Environment.NewLine, Helper.p4_udp_port));
                    this.Close();
                }
            }
            else
            {
                Helper.set_text(this.lblP1Ip, string.Format("{0}:{1}", p1_server_ip, p1_server_port));
                Helper.set_text(this.lblP5Ip, string.Format("{0}:{1}", p5_server_ip, p5_server_port));

                tcp_connect_timer_Tick(null, null);
            }

            scroll.AddColumnHeader("Id号", 160, ContentAlignment.MiddleCenter, ContentAlignment.MiddleCenter);
            scroll.AddColumnHeader("姓名", 160, ContentAlignment.MiddleCenter, ContentAlignment.MiddleCenter);
            scroll.AddColumnHeader("性别", 120, ContentAlignment.MiddleCenter, ContentAlignment.MiddleCenter);
            scroll.AddColumnHeader("年龄", 120, ContentAlignment.MiddleCenter, ContentAlignment.MiddleCenter);
            scroll.AddColumnHeader("胶片类型", -0.3f, ContentAlignment.MiddleCenter, ContentAlignment.MiddleCenter);
            scroll.AddColumnHeader("科室", -0.3f, ContentAlignment.MiddleCenter, ContentAlignment.MiddleCenter);
            scroll.AddColumnHeader("备注", -0.4f, ContentAlignment.MiddleCenter, ContentAlignment.MiddleCenter);
            scroll.StartScroll();
        }
        private void P4Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            scroll.Dispose();

            stop_p4_udp_peer();
            stop_p1_tcp_client();
            stop_p5_tcp_client();
        }
        private void p4_100ms_timer_Tick(object sender, EventArgs e)
        {
            try
            {
                //显示时间
                Helper.set_datetime(this.lblDateTime, DateTime.Now);

                //显示患者数
                int total = this._users == null ? 0 : this._users.Count;
                Helper.set_text(this.lblTotalNumber, total.ToString());

                //显示p1/p5连接状态
                Helper.set_text(this.lblP1Status, Helper.is_tcp_client_normal(_p1_tcp_client) ? "已连接" : "未连接");
                Helper.set_text(this.lblP5Status, Helper.is_tcp_client_normal(_p5_tcp_client) ? "已连接" : "未连接");

                //每10s获取1次患者变换列表
                if (Helper.is_tcp_client_normal(_p1_tcp_client))
                {
                    if (_get_changed_users_tick-- <= 0)
                    {
                        _get_changed_users_tick = _get_changed_users_tick_set;

                        //更新用户列表
                        DateTime now = DateTime.Now;
                        Helper.tcp_client_send(_p1_tcp_client, new GetChangedReadyUsersCmd()
                        {
                            sender_name = this_name,
                            time1 = _get_changed_users_time,
                            time2 = now
                        });
                        _get_changed_users_time = now;
                    }
                }
            }
            catch (System.Exception ex)
            {
                EzLogger.GlobalLogger.warning(string.Format("{0}{2}{1}{2}", ex.Message, ex.StackTrace, Environment.NewLine));
            }
        }
        private void tcp_connect_timer_Tick(object sender, EventArgs e)
        {
            if (!broadcast_server_ip)
            {
                if (_p5_tcp_client == null || !_p5_tcp_client.Connected)
                {
                    Task.Factory.StartNew(() =>
                    {
                        start_p5_tcp_client();
                        connect_p5_tcp_client(p5_server_ip, p5_server_port);
                    });
                }
                if (_p1_tcp_client == null || !_p1_tcp_client.Connected)
                {
                    Task.Factory.StartNew(() =>
                    {
                        start_p1_tcp_client();
                        connect_p1_tcp_client(p1_server_ip, p1_server_port);
                    });
                }
            }
        }
    }
}
