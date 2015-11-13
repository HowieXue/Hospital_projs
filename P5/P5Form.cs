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


namespace P5
{
    public partial class P5Form : Form
    {
        public readonly string this_name = "p5";

        //true打开udp，服务器广播地址；false用本地配置
        public readonly bool broadcast_server_ip;
        public readonly int p5_server_port;

        #region net
        AsyncTcpServer _p5_tcp_server = null;
        EzUdpPeer _p5_udp_peer = null;
        System.Timers.Timer _p5_udp_timer = null;

        private void start_p5_tcp_server(int port)
        {
            Helper.start_tcp_server(ref _p5_tcp_server, "p5 tcp server", 
                port, true, p5_tcp_server_MessageReceived);
        }
        private void stop_p5_tcp_server()
        {
            Helper.stop_tcp_server(ref _p5_tcp_server, "p5 tcp server");
        }

        private void start_p5_udp_peer()
        {
            Helper.start_udp_peer(ref _p5_udp_peer, "p5 udp peer", Helper.p5_udp_port, p5_udp_peer_MessageReceived);
            if (_p5_udp_peer != null)
                Helper.start_timer(ref _p5_udp_timer, "p5 udp timer", 5000, p5_udp_timer_Elapsed);
        }
        private void stop_p5_udp_peer()
        {
            Helper.stop_timer(ref _p5_udp_timer, "p5 udp timer");
            Helper.stop_udp_peer(ref _p5_udp_peer, "p5 udp peer");
        }

        private void p1_cmd_handler(IPEndPoint remote, Cmd cmd_base)
        {
            if (cmd_base is ConfirmIpCmd)
            {
                ConfirmIpCmd cmd = cmd_base as ConfirmIpCmd;

                BroadcastAddressCmd ack = new BroadcastAddressCmd();
                ack.sender_name = this_name;
                ack.ip = cmd.ip;
                ack.port = Helper.p5_tcp_listen_port;
                _p5_udp_peer.SendMessage(
                    new IPEndPoint(IPAddress.Parse(ack.ip), remote.Port),
                    ack);
            }
        }
        private void p2_cmd_handler(IPEndPoint remote, Cmd cmd_base)
        {
            if (cmd_base is ConfirmIpCmd)
            {
                ConfirmIpCmd cmd = cmd_base as ConfirmIpCmd;

                BroadcastAddressCmd ack = new BroadcastAddressCmd();
                ack.sender_name = this_name;
                ack.ip = cmd.ip;
                ack.port = Helper.p5_tcp_listen_port;
                _p5_udp_peer.SendMessage(
                    new IPEndPoint(IPAddress.Parse(ack.ip), remote.Port),
                    ack);
            }
        }
        private void p4_cmd_handler(IPEndPoint remote, Cmd cmd_base)
        {
            if (cmd_base is ConfirmIpCmd)
            {
                ConfirmIpCmd cmd = cmd_base as ConfirmIpCmd;

                BroadcastAddressCmd ack = new BroadcastAddressCmd();
                ack.sender_name = this_name;
                ack.ip = cmd.ip;
                ack.port = Helper.p5_tcp_listen_port;
                _p5_udp_peer.SendMessage(
                    new IPEndPoint(IPAddress.Parse(ack.ip), remote.Port),
                    ack);
            }
        }
        void p5_udp_peer_MessageReceived(object sender, MessageEventArgs e)
        {
            EzLogger.GlobalLogger.info(string.Format("udp receive message from {0}: {1}", sender, e.Message));

            Cmd cmd = e.Message as Cmd;
            if (cmd == null)
                return;

            try
            {
                string sender_name = cmd.sender_name.ToLower();
                if (sender_name.Contains("p1"))
                    p1_cmd_handler(sender as IPEndPoint, cmd);
                else if (sender_name.Contains("p2"))
                    p2_cmd_handler(sender as IPEndPoint, cmd);
                else if (sender_name.Contains("p4"))
                    p4_cmd_handler(sender as IPEndPoint, cmd);
            }
            catch (System.Exception ex)
            {
                EzLogger.GlobalLogger.warning(string.Format("{0}{2}{1}{2}", ex.Message, ex.StackTrace, Environment.NewLine));
            }
        }
        void p5_udp_timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            IEnumerable<string> ipv4s = Helper.get_localhost_ipv4();
            BroadcastIpListCmd cmd = new BroadcastIpListCmd();
            cmd.sender_name = this_name;
            cmd.ips = ipv4s == null ? null : ipv4s.ToArray();

            //向P1多播
            _p5_udp_peer.SendMessage(
                new IPEndPoint(_p5_udp_peer.MulticastAddress, Helper.p1_udp_port),
                cmd);

            //向P2多播
            _p5_udp_peer.SendMessage(
                new IPEndPoint(_p5_udp_peer.MulticastAddress, Helper.p2_udp_port),
                cmd);

            //向P4多播
            _p5_udp_peer.SendMessage(
                new IPEndPoint(_p5_udp_peer.MulticastAddress, Helper.p4_udp_port),
                cmd);
        }

        void p5_tcp_server_MessageReceived(IScsServerClient sc, Cmd cmd)
        {
            EzLogger.GlobalLogger.debug("message sender = " + cmd.sender_name ?? "");

            //根据cmd.sender_name设置各个远程设备状态
            //todo

            //p5必须响应的其它命令
            //todo
        }
        #endregion

        public P5Form()
        {
            //读取配置文件
            broadcast_server_ip = bool.Parse(ConfigurationManager.AppSettings["broadcast_server_ip"]);
            if (!broadcast_server_ip)
            {
                p5_server_port = int.Parse(ConfigurationManager.AppSettings["p5_server_port"]);
            }

            InitializeComponent();
        }
        private void P5Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            stop_p5_udp_peer();
            stop_p5_tcp_server();
        }
        private void P5Form_Load(object sender, EventArgs e)
        {
            if (broadcast_server_ip)
            {
                start_p5_tcp_server(Helper.p5_tcp_listen_port);
                start_p5_udp_peer();
            }
            else
            {
                start_p5_tcp_server(p5_server_port);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            virtualModeListView1.ClearItems();
            for (int i = 0; i < 10000; i++ )
            {
                virtualModeListView1.AddItem(i.ToString(),
                    string.Format("张三{0}", i),
                    i + 22);
            }
            virtualModeListView1.Reload();
            virtualModeListView1.ScrollToLastRow();
        }
    }
}
