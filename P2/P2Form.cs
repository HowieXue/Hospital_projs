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
using Spire.PdfViewer.Forms;
using System.Drawing.Printing;
using Dicom.Imaging;


namespace P2
{
    public partial class P2Form : Form
    {
        public readonly string this_name = "p2";

        //true打开udp，服务器广播地址；false用本地配置
        public readonly bool broadcast_server_ip;
        public readonly string p1_server_ip, p5_server_ip;
        public readonly int p1_server_port, p5_server_port;

        //是否本地打印？
        public readonly bool is_local_print_report;
        public readonly bool is_local_print_dcm;

        #region p2 udp peer
        private EzUdpPeer _p2_udp_peer = null;

        private void start_p2_udp_peer()
        {
            Helper.start_udp_peer(ref _p2_udp_peer, "p2 udp peer", Helper.p2_udp_port, p2_udp_peer_MessageReceived);
        }
        private void stop_p2_udp_peer()
        {
            Helper.stop_udp_peer(ref _p2_udp_peer, "p2 udp peer");
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
                    _p2_udp_peer.SendMessage(
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

                _p1_server_ip = cmd.ip;
                _p1_server_port = cmd.port;
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
                    _p2_udp_peer.SendMessage(
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
        private void p2_udp_peer_MessageReceived(object sender, MessageEventArgs e)
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
                else if (sender_name.Contains("p5"))
                    p5_cmd_handler(sender as IPEndPoint, cmd);
            }
            catch (System.Exception ex)
            {
                EzLogger.GlobalLogger.warning(string.Format("{0}{2}{1}{2}", ex.Message, ex.StackTrace, Environment.NewLine));
            }
        }
        #endregion

        #region p1 tcp client
        private RequestReplyTcpClient _p1_tcp_client = null;

        /// <summary>
        /// p2与p1之间的tcp非长连接
        /// 记录p1的ip+port在需要时连接之
        /// </summary>
        private string _p1_server_ip = null;
        private int _p1_server_port = 0;

        /// <summary>
        /// 查询返回结果
        /// 包含待打印数据
        /// </summary>
        private QueryUserCmdAck _query_ack = null;

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
        }

        private void p1_tcp_client_MessageReceived(AsyncTcpClient client, Cmd cmd)
        {
            if (cmd is HeartBeatCmd)
                return;
        }
        
        /// <summary>
        /// 以id查询用户信息
        /// </summary>
        /// <param name="id"></param>
        private void ui_pre_query()
        {
            //重置确保按钮状态正确
            Helper.enable_control(this.btnPrintReport, false);
            Helper.enable_control(this.btnPrintDcm, false);
            Helper.enable_control(this.btnPrintBoth, false);

            //Helper.enable_control(this.btnQuery, false);
            Helper.enable_control(this.txtUserID, false);

            Helper.set_text(this.lblUserName, "");
            Helper.set_text(this.lblUserID, "");
            Helper.set_text(this.lblDcmNumber, "");
            Helper.set_text(this.lblReportNumber, "");
            Helper.set_text(this.lblReportPrintFlag, "");
            Helper.set_text(this.lblDcmPrintFlag, "");

            Helper.set_text(this.txtMsg, "开始查询操作，请稍候..." + Environment.NewLine);
        }
        private void ui_post_query_failed()
        {
            //重置确保按钮状态正确
            Helper.enable_control(this.btnPrintReport, false);
            Helper.enable_control(this.btnPrintDcm, false);
            Helper.enable_control(this.btnPrintBoth, false);
            
            //Helper.enable_control(this.btnQuery, true);
            Helper.enable_control(this.txtUserID, true);
            Helper.set_text(this.txtUserID, "");

            Helper.set_text(this.lblUserName, "");
            Helper.set_text(this.lblUserID, "");
            Helper.set_text(this.lblDcmNumber, "");
            Helper.set_text(this.lblReportNumber, "");
            Helper.set_text(this.lblReportPrintFlag, "");
            Helper.set_text(this.lblDcmPrintFlag, "");

            Helper.add_text(this.txtMsg, 
                string.Format("{0}查询操作失败...{0}{0}欢迎使用查询/打印客户端程序！{0}{0}请输入'用户号'进行查询.{0}", 
                Environment.NewLine));
        }
        private void ui_post_query_success(QueryUserCmdAck ack)
        {
            Helper.enable_control(this.btnPrintDcm, ack.dcm_files_ready);
            Helper.enable_control(this.btnPrintReport, ack.report_files_ready);
            Helper.enable_control(this.btnPrintBoth, ack.dcm_files_ready || ack.report_files_ready);

            //Helper.enable_control(this.btnQuery, true);
            Helper.enable_control(this.txtUserID, true);
            Helper.set_text(this.txtUserID, "");

            Helper.set_text(this.lblUserName, ack.user.name);
            Helper.set_text(this.lblUserID, ack.user.id);
            Helper.set_text(this.lblReportNumber, ack.report_files_ready ? 
                ack.report_files_number : 0);
            Helper.set_text(this.lblReportPrintFlag, ack.report_files_ready ? 
                (ack.printed_report_times > 0 ? "已打印" : "未打印") : "");
            Helper.set_text(this.lblDcmNumber, ack.dcm_files_ready ? 
                ack.dcm_files_number : 0);
            Helper.set_text(this.lblDcmPrintFlag, ack.dcm_files_ready ? 
                (ack.printed_dcm_times > 0 ? "已打印" : "未打印") : "");

            Helper.add_text(this.txtMsg, Environment.NewLine + "查询操作成功..." + Environment.NewLine);
        }
        private void p1_client_query()
        {
            _query_ack = null;

            ui_pre_query();

            try
            {
                start_p1_tcp_client();
                connect_p1_tcp_client(_p1_server_ip, _p1_server_port);

                Helper.add_text(this.txtMsg, 
                    string.Format("{0}连接数据服务器P1: {1}{0}", 
                    Environment.NewLine, 
                    (Helper.is_tcp_client_normal(_p1_tcp_client) ? "成功" : "失败")));
                Helper.add_text(this.txtMsg, 
                    string.Format("{0}向数据服务器P1发查询命令, 等待服务器P1返回结果...{0}", 
                    Environment.NewLine));

                //1min之内能否传输完成？需要实验确定
                QueryUserCmd qu = new QueryUserCmd();
                qu.sender_name = this_name;
                qu.user_id = this.txtUserID.Text.Trim();    //patient id
                qu.get_dcm_files = is_local_print_dcm;     //本地打印则获取dcm，远程打印不获取
                qu.get_report_files = is_local_print_dcm;  //本地打印则获取dcm，远程打印不获取
                _query_ack = Helper.tcp_client_send_and_response(_p1_tcp_client, qu, 60000) as QueryUserCmdAck;

                Helper.add_text(this.txtMsg, 
                    string.Format("{0}数据服务器P1返回查询结果: {1}{0}", 
                    Environment.NewLine,
                    (_query_ack == null ? "失败" : "成功")));

                //if (_query_ack != null)
                //{
                //    if (ack.transfer_report_files)
                //    {
                //        Helper.add_text(this.txtMsg, Environment.NewLine+"存储报告文件..."+Environment.NewLine);
                //        //_query_ack.report_file_contents
                //    }
                //    if (ack.transfer_dcm_files)
                //    {
                //        Helper.add_text(this.txtMsg, Environment.NewLine+"存储胶片文件..."+Environment.NewLine);
                //        //_query_ack.dcm_file_contents
                //    }
                //}
            }
            catch (System.Exception ex) 
            {
                EzLogger.GlobalLogger.warning(string.Format("{0}{2}{1}{2}", ex.Message, ex.StackTrace, Environment.NewLine));
            }
            finally { stop_p1_tcp_client(); }

            try { ui_post_query_success(_query_ack); }
            catch
            {
                Helper.add_text(this.txtMsg, 
                    string.Format("{0}请检查本机与数据服务器P1的网络连接是否正常.{0}",
                    Environment.NewLine));

                ui_post_query_failed();
            }

        }
        
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="report"></param>
        /// <param name="dcm"></param>
        private void ui_pre_print()
        {
            Helper.enable_control(this.pnlMain, false);

            Helper.set_text(this.txtMsg, "开始打印操作，请稍候..." + Environment.NewLine);
        }
        private void ui_post_print(bool dcm, bool report)
        {
            Helper.enable_control(this.pnlMain, true);

            Helper.add_text(this.txtMsg, Environment.NewLine + "打印任务结束..." + Environment.NewLine);

            string which = "";
            if (report && dcm)
                which = "报告和胶片";
            else if (report)
                which = "报告";
            else if (dcm)
                which = "胶片";
            else
                which = "无";
            Helper.add_text(this.txtMsg, 
                string.Format("{1}本次成功打印: {0}{1}", 
                which,
                Environment.NewLine));
        }
        private LoginUserItem get_login()
        {
            if (this.InvokeRequired)
                return this.Invoke(new Func<LoginUserItem>(get_login)) as LoginUserItem;

            NoTitleLoginForm login_form = new NoTitleLoginForm();
            login_form.TitleText = "该用户报告或胶片已经打印过。请输入医院工作人员密码才能重新打印。";
            login_form.StartPosition = FormStartPosition.Manual;
            login_form.Location = this.pnlMain.Location;
            login_form.Size = this.pnlMain.Size;
            if (login_form.ShowDialog() == DialogResult.Cancel)
                return null;

            return new LoginUserItem()
            {
                login_name = login_form.LoginName,
                login_psw = login_form.LoginPsw
            };
        }
        /// <summary>
        /// 该函数不成功，则抛出异常
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="pid"></param>
        /// <param name="dcm"></param>
        /// <param name="report"></param>
        private void send_client_print_cmd(string uid, string pid, bool dcm, bool report)
        {
            Helper.add_text(this.txtMsg, 
                string.Format("{0}连接数据服务器P1: {1}{0}", 
                Environment.NewLine,
                (Helper.is_tcp_client_normal(_p1_tcp_client) ? "成功" : "失败")));
            Helper.add_text(this.txtMsg, 
                string.Format("{0}向数据服务器P1发打印完成命令, 等待服务器P1返回结果...{0}",
                Environment.NewLine));

            start_p1_tcp_client();
            connect_p1_tcp_client(_p1_server_ip, _p1_server_port);

            ClientPrintUserCmd cpuc = new ClientPrintUserCmd();
            cpuc.sender_name = this_name;
            cpuc.print_dcm = dcm;
            cpuc.print_report = report;
            cpuc.user_id = uid;
            cpuc.printer_id = pid;
            ClientPrintUserCmdAck pcc_ack = Helper.tcp_client_send_and_response(_p1_tcp_client, cpuc) as ClientPrintUserCmdAck;
            Helper.add_text(this.txtMsg, 
                string.Format("{0}数据服务器P1返回打印完成结果: {1}{0}", 
                Environment.NewLine,
                (pcc_ack == null ? "失败" : "成功")));

            if (pcc_ack == null)
                throw new MsgBoxException("通信异常", "请确认与数据服务器P1之间的网络连接是否正常.");
        }
        private bool local_print_dcm(string uid, string pid)
        {
            try
            {
                Helper.add_text(this.txtMsg, 
                    string.Format("{0}本地打印胶片, 请等待...{0}", 
                    Environment.NewLine));
                //todo
                send_client_print_cmd(uid, pid, true, false);
                Helper.add_text(this.txtMsg, 
                    string.Format("{0}打印胶片: {1}{0}",
                    Environment.NewLine,
                    (true ? "成功" : "失败")));
                return true;
            }
            catch (System.Exception ex)
            {
                EzLogger.GlobalLogger.warning(string.Format("本地打印胶片异常: {0}{2}{1}{2}",
                    ex.Message, ex.StackTrace, Environment.NewLine));

                Helper.add_text(this.txtMsg, 
                    string.Format("{1}打印胶片: {0}{1}",
                    (false ? "成功" : "失败"),
                    Environment.NewLine));

                return false;
            }
        }
        private bool local_print_report(string uid, string pid)
        {
            try
            {
                Helper.add_text(this.txtMsg, 
                    string.Format("{0}本地打印报告, 请等待...{0}", 
                    Environment.NewLine));

                //todo
                print_pdf("D:\\liuyh\\个人小客车增量指标申请表.pdf");

                send_client_print_cmd(uid, pid, false, true);
                Helper.add_text(this.txtMsg, 
                    string.Format("{1}打印报告: {0}{1}",
                    true ? "成功" : "失败",
                    Environment.NewLine));
                return true;
            }
            catch (System.Exception ex)
            {
                EzLogger.GlobalLogger.warning(string.Format("本地打印报告异常: {0}{2}{1}{2}",
                    ex.Message, ex.StackTrace, Environment.NewLine));

                Helper.add_text(this.txtMsg, string.Format("{1}打印报告: {0}{1}",
                    false ? "成功" : "失败",
                    Environment.NewLine));

                return false;
            }
        }
        private void print_pdf(string pdf_path)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string>(print_pdf), pdf_path);
                return;
            }

            PdfDocumentViewer pdf = new PdfDocumentViewer();
            pdf.LoadFromFile(pdf_path);
            pdf.Print();
        }
        private void p1_client_print(bool want_report, bool want_dcm)
        {
            bool print_dcm_ok = false;
            bool print_report_ok = false;
            try
            {
                ui_pre_print();

                //根据实际情况更新打印需求
                want_report = want_report && this.lblReportPrintFlag.Text.Contains("打印");
                want_dcm = want_dcm && this.lblDcmPrintFlag.Text.Contains("打印");

                //实际情况不允许打印
                if (!want_report && !want_dcm)
                    return;

                #region 多次打印，需身份验证
                string printer_id = "";
                bool need_verification = (want_report && this.lblReportPrintFlag.Text.Contains("已")) ||
                    (want_dcm && this.lblDcmPrintFlag.Text.Contains("已"));
                if (need_verification)
                {
                    LoginUserItem item = get_login();
                    if (item == null)
                        return;

                    start_p1_tcp_client();
                    connect_p1_tcp_client(_p1_server_ip, _p1_server_port);

                    Helper.add_text(this.txtMsg, 
                        string.Format("{0}连接数据服务器P1: {1}{0}",
                        Environment.NewLine,
                        (Helper.is_tcp_client_normal(_p1_tcp_client) ? "成功" : "失败")));
                    Helper.add_text(this.txtMsg, 
                        string.Format("{0}向数据服务器P1发身份验证命令, 等待服务器P1返回结果...{0}",
                        Environment.NewLine));

                    //发验证命令
                    LoginCmd login = new LoginCmd();
                    login.sender_name = this_name;
                    login.login_user.login_name = item.login_name;
                    login.login_user.login_psw = item.login_psw;
                    LoginCmdAck login_ack = Helper.tcp_client_send_and_response(_p1_tcp_client, login) as LoginCmdAck;

                    Helper.add_text(this.txtMsg, 
                        string.Format("{0}数据服务器P1返回身份验证结果: {1}{0}",
                        Environment.NewLine,
                        (login_ack == null ? "失败" : (login_ack.status != 0 ? "失败" : "成功"))));

                    if (login_ack == null)
                        throw new MsgBoxException("通信异常", "请确认与数据服务器P1之间的网络连接是否正常.");
                    if (login_ack.status != 0)
                        throw new MsgBoxException("验证失败",
                            string.Format("密码错误或者工号不存在.错误代码={0}.", login_ack.status));

                    //记录工号，后面备用
                    printer_id = item.login_name;
                }
                #endregion

                //可以考虑将三项任务中的2项改为并行执行，调用线程等待这2项任务的返回结果
                Task<bool> task_dcm = null;
                Task<bool> task_report = null;
                #region 胶片本地打印
                if (is_local_print_dcm && want_dcm)
                {
                    task_dcm = Task.Factory.StartNew(() => { 
                        return local_print_dcm(this.lblUserID.Text.Trim(), printer_id);
                    });
                }
                #endregion
                #region 报告本地打印
                if (is_local_print_report && want_report)
                {
                    task_report = Task.Factory.StartNew(() => {
                        return local_print_report(this.lblUserID.Text.Trim(), printer_id);
                    });
                }
                #endregion
                #region 报告和(/或者)胶片用服务器打印
                try
                {
                    
                    if ((!is_local_print_report && want_report) ||
                        (!is_local_print_dcm && want_dcm))
                    {
                        Helper.add_text(this.txtMsg, 
                            string.Format("{0}连接数据服务器P1: {1}{0}",
                            Environment.NewLine,
                            (Helper.is_tcp_client_normal(_p1_tcp_client) ? "成功" : "失败")));

                        string which = "";
                        if ((!is_local_print_report && want_report) &&
                            (!is_local_print_dcm && want_dcm))
                            which = "报告和胶片";
                        else if (!is_local_print_report && want_report)
                            which = "报告";
                        else
                            which = "胶片";
                        Helper.add_text(this.txtMsg,
                            string.Format("{1}向数据服务器P1发打印{0}命令, 等待服务器P1返回结果...{1}",
                            which, Environment.NewLine));

                        start_p1_tcp_client();
                        connect_p1_tcp_client(_p1_server_ip, _p1_server_port);

                        //发打印命令
                        ServerPrintUserCmd spuc = new ServerPrintUserCmd();
                        spuc.sender_name = this_name;
                        spuc.user_id = this.lblUserID.Text.Trim();
                        spuc.printer_id = printer_id;
                        spuc.print_dcm = !is_local_print_dcm && want_dcm;
                        spuc.print_report = !is_local_print_report && want_report;
                        ServerPrintUserCmdAck spuc_ack = Helper.tcp_client_send_and_response(_p1_tcp_client, spuc, 60000) as ServerPrintUserCmdAck;

                        Helper.add_text(this.txtMsg, 
                            string.Format("{0}数据服务器P1返回打印结果: {1}{0}",
                            Environment.NewLine, (spuc_ack == null ? "失败" : "成功")));

                        if (spuc_ack == null)
                            throw new MsgBoxException("通信异常", "请确认与数据服务器P1之间的网络连接是否正常.");
                        if (spuc_ack.print_dcm)
                            Helper.add_text(this.txtMsg, 
                                string.Format("{0}打印胶片: {1}{0}", 
                                Environment.NewLine, 
                                (spuc_ack.print_dcm_ack == 0 ? "成功" : "失败")));
                        if (spuc_ack.print_report)
                            Helper.add_text(this.txtMsg, 
                                string.Format("{0}打印报告: {1}{0}",
                                Environment.NewLine,
                                (spuc_ack.print_report_ack == 0 ? "成功" : "失败")));
                        
                        if (spuc_ack.print_dcm)
                            print_dcm_ok = (spuc_ack.print_dcm_ack == 0);
                        if (spuc_ack.print_report)
                            print_report_ok = (spuc_ack.print_report_ack == 0);
                    }
                }
                catch (MsgBoxException ex)
                {
                    Helper.add_text(this.txtMsg, 
                        string.Format("{0}{1}, {2}{0}", 
                        Environment.NewLine, ex.Title, ex.Message));
                }
                catch (System.Exception ex)
                {
                    EzLogger.GlobalLogger.warning(string.Format("{0}{2}{1}{2}", 
                        ex.Message, ex.StackTrace, Environment.NewLine));
                }
                #endregion
                if (task_dcm != null)
                {
                    task_dcm.Wait();
                    print_dcm_ok = task_dcm.Result;
                }
                if (task_report != null)
                {
                    task_report.Wait();
                    print_report_ok = task_report.Result;
                }
            }
            catch (MsgBoxException ex)
            {
                Helper.add_text(this.txtMsg, 
                        string.Format("{0}{1}, {2}{0}", 
                        Environment.NewLine, ex.Title, ex.Message));
            }
            catch (System.Exception ex)
            {
                EzLogger.GlobalLogger.warning(string.Format("{0}{2}{1}{2}",
                    ex.Message, ex.StackTrace, Environment.NewLine));
            }
            finally
            {
                stop_p1_tcp_client();

                ui_post_print(print_dcm_ok, print_report_ok);
            }
        }
        #endregion

        #region p5 tcp client
        private AsyncTcpClient _p5_tcp_client = null;
        
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

        private void p5_tcp_client_MessageReceived(AsyncTcpClient client, Cmd cmd)
        {
        }
        #endregion

        public P2Form()
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
            is_local_print_report = bool.Parse(ConfigurationManager.AppSettings["is_local_print_report"]);
            is_local_print_dcm = bool.Parse(ConfigurationManager.AppSettings["is_local_print_dcm"]);

            InitializeComponent();

            this.Location = Screen.PrimaryScreen.Bounds.Location;
            this.Size = Screen.PrimaryScreen.Bounds.Size;

            this.lblP1Ip.Text = "0.0.0.0";
            this.lblP5Ip.Text = "0.0.0.0";
        }

        private void P2Form_Load(object sender, EventArgs e)
        {
            Helper.center_in_parent(new Control[] { this.pnlMain, this.txtMsg });

            if (broadcast_server_ip)
                start_p2_udp_peer();
            else
            {
                _p1_server_ip = p1_server_ip;
                _p1_server_port = p1_server_port;

                Helper.set_text(this.lblP1Ip, string.Format("{0}:{1}", p1_server_ip, p1_server_port));
                Helper.set_text(this.lblP5Ip, string.Format("{0}:{1}", p5_server_ip, p5_server_port));

                tcp_connect_timer_Tick(null, null);
            }
        }
        private void P2Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            stop_p2_udp_peer();
            stop_p1_tcp_client();
            stop_p5_tcp_client();
        }
        private void _100ms_timer_Tick(object sender, EventArgs e)
        {
            Helper.set_text(this.lblP1Status,
                Helper.is_tcp_client_normal(_p1_tcp_client) ? "已连接" : "未连接");
            Helper.set_text(this.lblP5Status, 
                Helper.is_tcp_client_normal(_p5_tcp_client) ? "已连接" : "未连接");
        }
        private void btnQuery_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(p1_client_query);
        }
        private void btnPrintDcm_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() => p1_client_print(false, true));
        }
        private void btnPrintReport_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() => p1_client_print(true, false));
        }
        private void btnPrintBoth_Click(object sender, EventArgs e)
        {
            bool report = this.btnPrintReport.Enabled;
            bool dcm = this.btnPrintDcm.Enabled;
            Task.Factory.StartNew(() => p1_client_print(report, dcm));
        }
        private void txtUserID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Task.Factory.StartNew(p1_client_query);
        }
        private void P2Form_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!txtUserID.Focused)
            {
                txtUserID.Focus();
                if (txtUserID.Text.Length == 0)
                {
                    txtUserID.AppendText(e.KeyChar.ToString());
                }
            }
        }
        //定时连接tcp server
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
            }
        }
    }
}
