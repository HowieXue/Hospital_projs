using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Timers;
using System.Windows.Forms;
using net;
using Hik.Communication.Scs.Server;
using Hik.Communication.Scs.Communication.Messages;
using System.Threading;
using Hik.Communication.Scs.Communication;
using Hik.Communication.Scs.Client;
using System.ComponentModel;
using System.Reflection;

namespace net
{
    public class Helper
    {
        //P1
        public static readonly int p1_tcp_listen_port = 21001;
        public static readonly int p1_udp_port = 31001;
        //P2
        public static readonly int p2_udp_port = 32001;
        //P3
        public static readonly int p3_udp_port = 33001;
        //P4
        public static readonly int p4_tcp_listen_port = 24001;
        public static readonly int p4_udp_port = 34001;
        //P5
        public static readonly int p5_tcp_listen_port = 25001;
        public static readonly int p5_udp_port = 35001;

        public static IEnumerable<string> get_localhost_ipv4()
        {
            //获取ipv4地址
            List<string> ipv4s = new List<string>();
            IPAddress[] addrs = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (var v in addrs)
            {
                if (v.GetAddressBytes().Length != 4)
                    continue;

                ipv4s.Add(v.ToString());
            }
            return ipv4s;
        }

        public static bool start_tcp_client(ref AsyncTcpClient client, string name,
            bool auto_hb, string sender, Action<AsyncTcpClient, Cmd> recv_func)
        {
            try
            {
                if (client == null)
                {
                    client = new AsyncTcpClient(auto_hb);
                    client.HeartBeatSender = sender;
                    client.MessageReceived += recv_func;
                    EzLogger.GlobalLogger.info("start " + name + " success.");
                }
                return true;
            }
            catch (System.Exception ex)
            {
                stop_tcp_client(ref client, name);

                EzLogger.GlobalLogger.warning(string.Format("start {0} failed: {1}{3}{2}{3}",
                    name, ex.Message, ex.StackTrace, Environment.NewLine));

                return false;
            }
        }
        public static void stop_tcp_client(ref AsyncTcpClient client, string name)
        {
            try
            {
                if (client != null)
                {
                    client.Dispose();
                    client = null;
                    EzLogger.GlobalLogger.info("stop " + name + " success.");
                }
            }
            catch (System.Exception ex)
            {
                EzLogger.GlobalLogger.warning(string.Format("stop {0} failed: {1}{3}{2}{3}",
                    name, ex.Message, ex.StackTrace, Environment.NewLine));
            }
        }
        public static void connect_tcp_client(AsyncTcpClient client, string name, string server_ip, int server_port)
        {
            try
            {
                if (client != null &&
                    !client.Connected &&
                    !client.Connect(server_ip, server_port))
                    throw new Exception();
                EzLogger.GlobalLogger.info(string.Format("connect {0} to {1}:{2} success.", 
                    name, server_ip, server_port));
            }
            catch (System.Exception ex)
            {
                EzLogger.GlobalLogger.warning(string.Format("connect {0} to {1}:{2} failed: {3}{5}{4}{5}", 
                    name, server_ip, server_port, ex.Message, ex.StackTrace, Environment.NewLine));
            }
        }
        public static void tcp_client_connection_dropped_event(AsyncTcpClient client, Action<AsyncTcpClient> connection_dropped_func)
        {
            if (client != null)
            {
                client.ConnectionDropped += connection_dropped_func;
            }
        }
        public static bool start_tcp_client(ref RequestReplyTcpClient client, string name,
            bool auto_hb, string sender, Action<AsyncTcpClient, Cmd> recv_func)
        {
            try
            {
                if (client == null)
                {
                    client = new RequestReplyTcpClient(auto_hb);
                    client.HeartBeatSender = sender;
                    client.MessageReceived += recv_func;
                    EzLogger.GlobalLogger.info("start " + name + " success.");
                }
                return true;
            }
            catch (System.Exception ex)
            {
                stop_tcp_client(ref client, name);

                EzLogger.GlobalLogger.warning(string.Format("start {0} failed: {1}{3}{2}{3}",
                    name, ex.Message, ex.StackTrace, Environment.NewLine));

                return false;
            }
        }
        public static void stop_tcp_client(ref RequestReplyTcpClient client, string name)
        {
            try
            {
                if (client != null)
                {
                    client.Dispose();
                    client = null;
                    EzLogger.GlobalLogger.info("stop " + name + " success.");
                }
            }
            catch (System.Exception ex)
            {
                EzLogger.GlobalLogger.warning(string.Format("stop {0} failed: {1}{3}{2}{3}",
                    name, ex.Message, ex.StackTrace, Environment.NewLine));
            }
        }

        public static bool is_tcp_client_normal(AsyncTcpClient client)
        {
            return (client != null) && client.Available;
        }

        public static bool start_udp_peer(ref EzUdpPeer peer, string name, int port, EventHandler<MessageEventArgs> recv_func)
        {
            try
            {
                if (peer == null)
                {
                    peer = new EzUdpPeer(IPAddress.Any, port);
                    peer.MessageReceived += recv_func;
                    peer.Start();
                    EzLogger.GlobalLogger.info("start " + name + " success.");
                }
                return true;
            }
            catch (System.Exception ex)
            {
                stop_udp_peer(ref peer, name);

                EzLogger.GlobalLogger.warning(string.Format("start {0} failed: {1}{3}{2}{3}",
                    name, ex.Message, ex.StackTrace, Environment.NewLine));

                return false;
            }
        }
        public static void stop_udp_peer(ref EzUdpPeer peer, string name)
        {
            try
            {
                if (peer != null)
                {
                    peer.Dispose();
                    peer = null;
                    EzLogger.GlobalLogger.info("stop " + name + " success.");
                }
            }
            catch (System.Exception ex)
            {
                EzLogger.GlobalLogger.warning(string.Format("stop {0} failed: {1}{3}{2}{3}",
                    name, ex.Message, ex.StackTrace, Environment.NewLine));
            }
        }

        public static bool start_tcp_server(ref AsyncTcpServer server, string name, int port, bool check_client_available, Action<IScsServerClient, Cmd> recv_func)
        {
            try
            {
                if (server == null)
                {
                    server = new AsyncTcpServer(port, check_client_available);
                    server.MessageReceived += recv_func;
                    server.Start(); 
                    EzLogger.GlobalLogger.info("start " + name + " success.");
                }
                return true;
            }
            catch (System.Exception ex)
            {
                stop_tcp_server(ref server, name);

                EzLogger.GlobalLogger.warning(string.Format("start {0} failed: {1}{3}{2}{3}",
                    name, ex.Message, ex.StackTrace, Environment.NewLine));

                return false;
            }
        }
        public static void stop_tcp_server(ref AsyncTcpServer server, string name)
        {
            try
            {
                if (server != null)
                {
                    server.Dispose();
                    server = null;
                    EzLogger.GlobalLogger.info("stop " + name + " success.");
                }
            }
            catch (System.Exception ex)
            {
                EzLogger.GlobalLogger.warning(string.Format("stop {0} failed: {1}{3}{2}{3}",
                    name, ex.Message, ex.StackTrace, Environment.NewLine));
            }
        }

        public static bool start_timer(ref System.Timers.Timer timer, string name, double interval, ElapsedEventHandler func)
        {
            try
            {
                if (timer == null)
                {
                    timer = new System.Timers.Timer();
                    timer.Interval = interval;
                    timer.Elapsed += func;
                    timer.AutoReset = true;
                    timer.Enabled = true;
                    EzLogger.GlobalLogger.info("start " + name + " success.");
                }
                return true;
            }
            catch (System.Exception ex)
            {
                EzLogger.GlobalLogger.warning(string.Format("stop {0} failed: {1}{3}{2}{3}",
                    name, ex.Message, ex.StackTrace, Environment.NewLine));

                return false;
            }
        }
        public static void stop_timer(ref System.Timers.Timer timer, string name)
        {
            try
            {
                if (timer != null)
                {
                    timer.Enabled = false;
                    timer.Dispose();
                    timer = null;
                    EzLogger.GlobalLogger.info("stop " + name + " success.");
                }
            }
            catch (System.Exception ex)
            {
                EzLogger.GlobalLogger.warning(string.Format("stop {0} failed: {1}{3}{2}{3}", 
                    name, ex.Message, ex.StackTrace, Environment.NewLine));
            }
        }

        public static void tcp_client_send(AsyncTcpClient client, Cmd cmd)
        {
            if (cmd == null || client == null || !client.Connected)
                return;

            if (cmd is HeartBeatCmd)
            {
                if (Monitor.TryEnter(client))
                {
                    try { client.SendMessage(cmd); }
                    catch (System.Exception ex) { EzLogger.GlobalLogger.warning(string.Format("{0}{2}{1}{2}", ex.Message, ex.StackTrace, Environment.NewLine)); }
                    finally { Monitor.Exit(client); }
                }
            }
            else
            {
                lock (client)
                {
                    try { client.SendMessage(cmd); }
                    catch (System.Exception ex) { EzLogger.GlobalLogger.warning(string.Format("{0}{2}{1}{2}", ex.Message, ex.StackTrace, Environment.NewLine)); }
                }
            }
        }
        public static void tcp_server_send(IScsServerClient server, Cmd cmd)
        {
            if (server == null || cmd == null || server.CommunicationState != CommunicationStates.Connected)
                return;

            lock (server)
            {
                try { server.SendMessage(cmd); }
                catch (System.Exception ex) { EzLogger.GlobalLogger.warning(string.Format("{0}{2}{1}{2}", ex.Message, ex.StackTrace, Environment.NewLine)); }
            }
        }
        public static Cmd tcp_client_send_and_response(RequestReplyTcpClient client, Cmd cmd, int timeout=15000)
        {
            try
            {
                if (cmd == null || client == null || !client.Connected)
                    return null;

                return client.SendMessageAndWaitForResponse(cmd, timeout);
            }
            catch (System.Exception ex)
            {
                EzLogger.GlobalLogger.warning(string.Format("{0}{2}{1}{2}", ex.Message, ex.StackTrace, Environment.NewLine));

                return null;
            }
        }

        public static int get_server_client_number(AsyncTcpServer server, string client_name)
        {
            try { return server.GetClientStatusNumber(client_name); }
            catch { return 0; }
        }

        #region control helper
        public static void visible_control(Control c, bool visible)
        {
            if (c.InvokeRequired)
                c.Invoke(new Action<Control, bool>(visible_control), new object[] { c, visible });
            else
                c.Visible = visible;
        }
        public static void enable_control(Control c, bool enable)
        {
            if (c.InvokeRequired)
                c.Invoke(new Action<Control, bool>(enable_control), new object[] { c, enable });
            else
                c.Enabled = enable;
        }
        public static void set_text(Control c, object s)
        {
            if (c.InvokeRequired)
                c.Invoke(new Action<Control, object>(set_text), new object[] { c, s });
            else
                c.Text = s.ToString();
        }
        public static void add_text(Control c, object s)
        {
            if (c.InvokeRequired)
                c.Invoke(new Action<Control, object>(add_text), new object[] { c, s });
            else
                c.Text += s.ToString();
        }
        public static void set_datetime(Control c, DateTime dt)
        {
            string s = string.Format("{0}-{1:D2}-{2:D2} {3:D2}:{4:D2}:{5:D2}",
                dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
            set_text(c, s);
        }
        public static void set_text_by_tag(Control c, object[] objs)
        {
            if (c.InvokeRequired)
                c.Invoke(new Action<Control, object[]>(set_text_by_tag), new object[] { c, objs });
            else
                c.Text = string.Format((string)c.Tag, objs);
        }

        public static void add_text(TextBox tb, object s)
        {
            if (tb.InvokeRequired)
                tb.Invoke(new Action<TextBox, object>(add_text), new object[] { tb, s });
            else
            {
                tb.AppendText(s.ToString());
                tb.ScrollToCaret();
            }
        }
        
        public static void set_text(ToolStripItem c, string s)
        {
            c.Text = s;
        }
        public static void set_text_by_tag(ToolStripItem c, object[] objs)
        {
            c.Text = string.Format((string)c.Tag, objs);
        }


        public static DialogResult show_form(Form form, Form parent)
        {
            if (form.InvokeRequired)
                return (DialogResult)form.Invoke(new Func<Form, Form, DialogResult>(show_form), new object[] { form, parent });
            else
                return form.ShowDialog(parent);
        }

        public static void center_in_parent(Control[] cons)
        {
            if (cons == null)
                return;

            Control parent = cons[0].Parent;
            int left = int.MaxValue;
            int right = -int.MaxValue;
            int top = int.MaxValue;
            int bottom = -int.MaxValue;
            foreach (Control c in cons)
            {
                if (left > c.Left)
                    left = c.Left;
                if (right < c.Right)
                    right = c.Right;
                if (top > c.Top)
                    top = c.Top;
                if (bottom < c.Bottom)
                    bottom = c.Bottom;
            }
            int sx = (parent.Width - (right - left)) / 2;
            int sy = (parent.Height - (bottom - top)) / 2;
            int dx = sx - left;
            int dy = sy - top;
            foreach (Control c in cons)
            {
                c.Left += dx;
                c.Top += dy;
            }
        }

        public static void set_property_value(Control c, string property_name, object value)
        {
            property_name = property_name ?? "";
            if (c == null)
                throw new ArgumentException("Helper.set_property_value参数不能为null.");

            if (c.InvokeRequired)
                c.Invoke(new Action<Control, string, object>(set_property_value), new object[] { c, property_name, value });
            else
            {
                PropertyInfo pinfo = c.GetType().GetProperty(property_name);
                if (pinfo == null)
                    throw new ArgumentException(string.Format("Helper.set_property_value获取属性{0}失败.", property_name));
                pinfo.SetValue(c, value);
            }
        }
        public static object get_property_value(Control c, string property_name)
        {
            property_name = property_name ?? "";
            if (c == null)
                throw new ArgumentException("Helper.get_property_value参数不能为null.");

            if (c.InvokeRequired)
                return c.Invoke(new Func<Control, string, object>(get_property_value), new object[] { c, property_name });
            else
            {
                PropertyInfo pinfo = c.GetType().GetProperty(property_name);
                if (pinfo == null)
                    throw new ArgumentException(string.Format("Helper.get_property_value获取属性{0}失败.", property_name));
                return pinfo.GetValue(c);
            }
        }
        #endregion

        public static void warning(string s)
        {
            Console.WriteLine(s);
            StackTrace st = new StackTrace(true);
            for (int i = 1; i < st.FrameCount; i++)
            {
                StackFrame sf = st.GetFrame(i);
                int line = sf.GetFileLineNumber();
                if (line > 0)
                {
                    string fmt = string.Format("\t{0}->{1}[{2}]",
                    sf.GetMethod(), sf.GetFileName(), sf.GetFileLineNumber());
                    Console.WriteLine(fmt);
                }
            }
        }
        public static void info(string s)
        {
            Console.WriteLine(s);
        }
        public static void debug(string s)
        {
            Console.WriteLine(s);
        }

        public static void msgbox_info(string title, string s, Control c = null)
        {
            if (c == null)
            {
                MessageBox.Show(s, title, MessageBoxButtons.OK,
                    MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            else if (c.InvokeRequired)
                c.Invoke(new Action<string, string, Control>(msgbox_info), new object[] { title, s, c });
            else
            {
                MessageBox.Show(s, title, MessageBoxButtons.OK,
                    MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }
    }

    public class MsgBoxException : Exception
    {
        public string Title { get; set; }
        public MsgBoxException(string title, string msg) 
            : base(msg)
        {
            this.Title = title;
        }
    }
}
