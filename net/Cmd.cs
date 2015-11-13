using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hik.Communication.Scs.Communication.Messages;

namespace net
{
    public enum CmdType : int
    {
        None = -1,

        Heart_Beat_Cmd,                         //心跳
        Heart_Beat_Cmd_Ack,                     //用HeartBeatCmd替代，无

        Broadcast_Address_Cmd,                  //广播地址
        Broadcast_Address_Cmd_Ack,              //udp不回复，无

        Broadcast_Ip_List_Cmd,                  //广播ip地址列表
        Broadcast_Ip_List_Cmd_Ack,              //udp不回复，无

        Confirm_Ip_Cmd,                         //客户端确认服务器端的ip
        Confirm_Ip_Cmd_Ack,						//udp不回复，无

        Get_All_Ready_Users_Cmd,                //获取所有的就绪用户列表, c-->s
        Get_All_Ready_Users_Cmd_Ack,            //s-->c

        Get_Changed_Ready_Users_Cmd,            //获取改变的就绪用户列表，c-->s
        Get_Changed_Ready_Users_Cmd_Ack,        //s-->c

        Msg_Cmd,                                //报告消息, c-->s
        Msg_Cmd_Ack,                            //s-->c

        Query_User_Cmd,                         //查询1个用户
        Query_User_Cmd_Ack,

        Server_Print_User_Cmd,                  //远程服务器打印1个用户
        Server_Print_User_Cmd_Ack,

        Client_Print_User_Cmd,                  //本地客户端打印1个用户
        Client_Print_User_Cmd_Ack,

        Login_Cmd,                              //登录命令
        Login_Cmd_Ack,
        
        
        Identity_Cmd,                           //表明身份
        Identity_Cmd_Ack,
    }

    /// <summary>
    /// 为了使用scs通信框架，令Cmd派生自ScsMessage
    /// </summary>
    public class Cmd : ScsMessage
    {
        /// <summary>
        /// 命令类型标志
        /// </summary>
        public CmdType id { get; protected set; }

        /// <summary>
        /// 命令序号
        /// 每个client都有1个序号发生器，唯一标志该命令
        /// </summary>
        public ulong no { get; set; }

        /// <summary>
        /// 发送者名称
        /// </summary>
        public string sender_name { get; set; }

        /// <summary>
        /// 不允许直接构造Cmd类，只能通过子类调用父类构造
        /// </summary>
        protected Cmd()
        {
            this.id = CmdType.None;
            this.no = 0;
        }

        public virtual bool from_byte(byte[] buffer, ref long pos)
        {
            try
            {
                this.id = (CmdType)to_int(buffer, ref pos);
                this.no = to_ulong(buffer, ref pos);
                this.sender_name = to_string(buffer, ref pos);
                this.MessageId = Cmd.to_string(buffer, ref pos);
                this.RepliedMessageId = Cmd.to_string(buffer, ref pos);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public virtual List<byte> to_byte()
        {
            List<byte> buffer = new List<byte>();
            buffer.AddRange(get_bytes((int)this.id));
            buffer.AddRange(get_bytes(this.no));
            buffer.AddRange(get_bytes(this.sender_name));
            buffer.AddRange(Cmd.get_bytes(this.MessageId));
            buffer.AddRange(Cmd.get_bytes(this.RepliedMessageId));
            return buffer;
        }


        /// <summary>
        /// 各种转换函数
        /// 都转为小端存储
        /// string为utf8编码
        /// </summary>
        public static IEnumerable<byte> get_bytes(bool value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return BitConverter.IsLittleEndian ? buffer : buffer.Reverse();
        }
        public static IEnumerable<byte> get_bytes(char value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return BitConverter.IsLittleEndian ? buffer : buffer.Reverse();
        }
        public static IEnumerable<byte> get_bytes(double value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return BitConverter.IsLittleEndian ? buffer : buffer.Reverse();
        }
        public static IEnumerable<byte> get_bytes(float value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return BitConverter.IsLittleEndian ? buffer : buffer.Reverse();
        }
        public static IEnumerable<byte> get_bytes(int value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return BitConverter.IsLittleEndian ? buffer : buffer.Reverse();
        }
        public static IEnumerable<byte> get_bytes(long value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return BitConverter.IsLittleEndian ? buffer : buffer.Reverse();
        }
        public static IEnumerable<byte> get_bytes(short value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return BitConverter.IsLittleEndian ? buffer : buffer.Reverse();
        }
        public static IEnumerable<byte> get_bytes(uint value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return BitConverter.IsLittleEndian ? buffer : buffer.Reverse();
        }
        public static IEnumerable<byte> get_bytes(ulong value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return BitConverter.IsLittleEndian ? buffer : buffer.Reverse();
        }
        public static IEnumerable<byte> get_bytes(ushort value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            return BitConverter.IsLittleEndian ? buffer : buffer.Reverse();
        }
        public static IEnumerable<byte> get_bytes(string s)
        {
            List<byte> buffer = new List<byte>();
            byte[] tmp = Encoding.UTF8.GetBytes(s ?? "");
            buffer.AddRange(get_bytes(tmp == null ? 0 : tmp.Length));
            buffer.AddRange(tmp);
            return buffer;
        }
        public static IEnumerable<byte> get_bytes(DateTime value)
        {
            long v = value.ToBinary();
            return get_bytes(v);
        }

        public static bool to_bool(byte[] buffer, ref long pos)
        {
            byte[] copy = new byte[sizeof(bool)];
            Array.Copy(buffer, pos, copy, 0, copy.LongLength);

            if (!BitConverter.IsLittleEndian)
                Array.Reverse(copy);

            bool value = BitConverter.ToBoolean(copy, 0);
            pos += copy.LongLength;
            return value;
        }
        public static char to_char(byte[] buffer, ref long pos)
        {
            byte[] copy = new byte[sizeof(char)];
            Array.Copy(buffer, pos, copy, 0, copy.LongLength);

            if (!BitConverter.IsLittleEndian)
                Array.Reverse(copy);

            char value = BitConverter.ToChar(copy, 0);
            pos += copy.LongLength;
            return value;
        }
        public static double to_double(byte[] buffer, ref long pos)
        {
            byte[] copy = new byte[sizeof(double)];
            Array.Copy(buffer, pos, copy, 0, copy.LongLength);
            
            if (!BitConverter.IsLittleEndian)
                Array.Reverse(copy);

            double value = BitConverter.ToDouble(copy, 0);
            pos += copy.LongLength;
            return value;
        }
        public static float to_float(byte[] buffer, ref long pos)
        {
            byte[] copy = new byte[sizeof(float)];
            Array.Copy(buffer, pos, copy, 0, copy.LongLength);

            if (!BitConverter.IsLittleEndian)
                Array.Reverse(copy);

            float value = BitConverter.ToSingle(copy, 0);
            pos += copy.LongLength;
            return value;
        }
        public static int to_int(byte[] buffer, ref long pos)
        {
            byte[] copy = new byte[sizeof(int)];
            Array.Copy(buffer, pos, copy, 0, copy.LongLength);

            if (!BitConverter.IsLittleEndian)
                Array.Reverse(copy);

            int value = BitConverter.ToInt32(copy, 0);
            pos += copy.LongLength;
            return value;
        }
        public static long to_long(byte[] buffer, ref long pos)
        {
            byte[] copy = new byte[sizeof(long)];
            Array.Copy(buffer, pos, copy, 0, copy.LongLength);

            if (!BitConverter.IsLittleEndian)
                Array.Reverse(copy);

            long value = BitConverter.ToInt64(copy, 0);
            pos += copy.LongLength;
            return value;
        }
        public static short to_short(byte[] buffer, ref long pos)
        {
            byte[] copy = new byte[sizeof(short)];
            Array.Copy(buffer, pos, copy, 0, copy.LongLength);

            if (!BitConverter.IsLittleEndian)
                Array.Reverse(copy);

            short value = BitConverter.ToInt16(copy, 0);
            pos += copy.LongLength;
            return value;
        }
        public static uint to_uint(byte[] buffer, ref long pos)
        {
            byte[] copy = new byte[sizeof(uint)];
            Array.Copy(buffer, pos, copy, 0, copy.LongLength);

            if (!BitConverter.IsLittleEndian)
                Array.Reverse(copy);

            uint value = BitConverter.ToUInt32(copy, 0);
            pos += copy.LongLength;
            return value;
        }
        public static ulong to_ulong(byte[] buffer, ref long pos)
        {
            byte[] copy = new byte[sizeof(ulong)];
            Array.Copy(buffer, pos, copy, 0, copy.LongLength);

            if (!BitConverter.IsLittleEndian)
                Array.Reverse(copy);

            ulong value = BitConverter.ToUInt64(copy, 0);
            pos += copy.LongLength;
            return value;
        }
        public static ushort to_ushort(byte[] buffer, ref long pos)
        {
            byte[] copy = new byte[sizeof(ushort)];
            Array.Copy(buffer, pos, copy, 0, copy.LongLength);

            if (!BitConverter.IsLittleEndian)
                Array.Reverse(copy);

            ushort value = BitConverter.ToUInt16(copy, 0);
            pos += copy.LongLength;
            return value;
        }
        public static string to_string(byte[] buffer, ref long pos)
        {
            Int32 len = to_int(buffer, ref pos);
            byte[] copy = new byte[len];
            Array.Copy(buffer, pos, copy, 0, len);
            string s = Encoding.UTF8.GetString(copy, 0, len);
            pos += len;
            return s;
        }
        public static DateTime to_datetime(byte[] buffer, ref long pos)
        {
            long v = to_long(buffer, ref pos);
            return DateTime.FromBinary(v);
        }

        public override string ToString()
        {
            return string.Format("{0}{6}\t id:{1}{6}\t no:{2}{6}\t sender name:{3}{6}\t MessageId:{4}{6}\t RepliedMessageId:{5}",
                this.GetType().ToString(),
                this.id,
                this.no,
                this.sender_name ?? "",
                this.MessageId ?? "",
                this.RepliedMessageId ?? "",
                Environment.NewLine);
        }
    }
    public class CmdAck : Cmd
    {
        /// <summary>
        /// 操作结果
        /// 0   表示成功
        /// 非0 表示失败代码
        /// </summary>
        public Int32 status { get; set; }

        protected CmdAck()
        {
            this.status = 0;
        }

        public override List<byte> to_byte()
        {
            List<byte> buffer = base.to_byte();
            buffer.AddRange(get_bytes(this.status));
            return buffer;
        }
        public override bool from_byte(byte[] buffer, ref long pos)
        {
            if (!base.from_byte(buffer, ref pos))
                return false;
            this.status = to_int(buffer, ref pos);
            return true;
        }

        public override string ToString()
        {
            return base.ToString() + string.Format("{1}\t status:{0}", 
                this.status, Environment.NewLine);
        }
    }

    public class Time2Cmd : Cmd
    {
        public DateTime time1 { get; set; }
        public DateTime time2 { get; set; }

        protected Time2Cmd()
        {
            this.time1 = DateTime.Today;
            this.time2 = DateTime.Today;
        }

        public override bool from_byte(byte[] buffer, ref long pos)
        {
            if (!base.from_byte(buffer, ref pos))
                return false;
            this.time1 = Cmd.to_datetime(buffer, ref pos);
            this.time2 = Cmd.to_datetime(buffer, ref pos);
            return true;
        }
        public override List<byte> to_byte()
        {
            List<byte> buffer = base.to_byte();
            buffer.AddRange(Cmd.get_bytes(this.time1));
            buffer.AddRange(Cmd.get_bytes(this.time2));
            return buffer;
        }
        public override string ToString()
        {
            return base.ToString() + string.Format("{4}\t time1:[{0} {1}]   time2:[{2} {3}]",
                this.time1.ToShortDateString(), this.time1.ToLongTimeString(),
                this.time2.ToShortDateString(), this.time2.ToLongTimeString(),
                Environment.NewLine);
        }
    }

    public class HeartBeatCmd : Cmd
    {
        public HeartBeatCmd()
        {
            this.id = CmdType.Heart_Beat_Cmd;
        }
    }

    #region udp cmd
    public class BroadcastIpListCmd : Cmd
    {
        public string[] ips { get; set; }

        public BroadcastIpListCmd()
        {
            this.id = CmdType.Broadcast_Ip_List_Cmd;
        }

        public override bool from_byte(byte[] buffer, ref long pos)
        {
            if (!base.from_byte(buffer, ref pos))
                return false;
            int size = Cmd.to_int(buffer, ref pos);
            this.ips = size > 0 ? new string[size] : null;
            for (int i = 0; i < size; i++)
            {
                this.ips[i] = Cmd.to_string(buffer, ref pos);
            }
            return true;
        }
        public override List<byte> to_byte()
        {
            List<byte> buffer = base.to_byte();
            int size = this.ips == null ? 0 : this.ips.Length;
            buffer.AddRange(Cmd.get_bytes(size));
            for (int i = 0; i < size; i++)
            {
                buffer.AddRange(Cmd.get_bytes(this.ips[i]));
            }
            return buffer;
        }
        public override string ToString()
        {
            int size = this.ips == null ? 0 : this.ips.Length;
            string s = string.Format("{1}\t ip list:{0}", size, Environment.NewLine);
            for (int i = 0; i < size;  i++)
            {
                s += string.Format("{1}\t\t {0}", this.ips[i], Environment.NewLine);
            }
            return base.ToString() + s;
        }
    }

    public class ConfirmIpCmd : Cmd
    {
        public string ip { get; set; }

        public ConfirmIpCmd()
        {
            this.id = CmdType.Confirm_Ip_Cmd;
        }

        public override bool from_byte(byte[] buffer, ref long pos)
        {
            if (!base.from_byte(buffer, ref pos))
                return false;
            this.ip = Cmd.to_string(buffer, ref pos);
            return true;
        }
        public override List<byte> to_byte()
        {
            List<byte> buffer = base.to_byte();
            buffer.AddRange(Cmd.get_bytes(this.ip));
            return buffer;
        }
        public override string ToString()
        {
            return base.ToString() + string.Format("{1}\t ip:{0}",
                this.ip, Environment.NewLine);
        }
    }

    public class BroadcastAddressCmd : Cmd
    {
        public string ip { get; set; }
        public int port { get; set; }

        public BroadcastAddressCmd()
        {
            this.id = CmdType.Broadcast_Address_Cmd;
        }

        public override bool from_byte(byte[] buffer, ref long pos)
        {
            if (!base.from_byte(buffer, ref pos))
                return false;
            this.ip = Cmd.to_string(buffer, ref pos);
            this.port = Cmd.to_int(buffer, ref pos);
            return true;
        }
        public override List<byte> to_byte()
        {
            List<byte> buffer = base.to_byte();
            buffer.AddRange(Cmd.get_bytes(this.ip));
            buffer.AddRange(Cmd.get_bytes(this.port));
            return buffer;
        }
        public override string ToString()
        {
            return base.ToString() + string.Format("{2}\t address:{0}:{1}",
                this.ip, this.port, Environment.NewLine);
        }
    }
    #endregion

    public class ReadyUserItem
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool is_male { get; set; }
        public uint age { get; set; }
        /// <summary>
        /// 类别，DR、CT、X光
        /// </summary>
        public string dcm_type { get; set; }
        public string study_department { get; set; }
        public uint report_num { get; set; }
        public uint dcm_num { get; set; }
        public string desc { get; set; }
        /// <summary>
        /// 增删用户时有效
        /// true增加
        /// false删除
        /// </summary>
        public bool available { get; set; }

        public List<byte> to_byte()
        {
            List<byte> buffer = new List<byte>();
            buffer.AddRange(Cmd.get_bytes(this.id));
            buffer.AddRange(Cmd.get_bytes(this.name));
            buffer.AddRange(Cmd.get_bytes(this.is_male));
            buffer.AddRange(Cmd.get_bytes(this.age));
            buffer.AddRange(Cmd.get_bytes(this.dcm_type));
            buffer.AddRange(Cmd.get_bytes(this.study_department));
            buffer.AddRange(Cmd.get_bytes(this.report_num));
            buffer.AddRange(Cmd.get_bytes(this.dcm_num));
            buffer.AddRange(Cmd.get_bytes(this.desc));
            buffer.AddRange(Cmd.get_bytes(this.available));
            return buffer;
        }
        public bool from_byte(byte[] buffer, ref long pos)
        {
            this.id = Cmd.to_string(buffer, ref pos);
            this.name = Cmd.to_string(buffer, ref pos);
            this.is_male = Cmd.to_bool(buffer, ref pos);
            this.age = Cmd.to_uint(buffer, ref pos);
            this.dcm_type = Cmd.to_string(buffer, ref pos);
            this.study_department = Cmd.to_string(buffer, ref pos);
            this.report_num = Cmd.to_uint(buffer, ref pos);
            this.dcm_num = Cmd.to_uint(buffer, ref pos);
            this.desc = Cmd.to_string(buffer, ref pos);
            this.available = Cmd.to_bool(buffer, ref pos);
            return true;
        }

        public override string ToString()
        {
            return string.Format("{10}\t\t id:{0}  name:{1}  male:{2}  age:{3}  dcm_type:{4}  study_department:{5}  report_num:{6}  dcm_num:{7}  desc:{8}  available:{9}",
                this.id ?? "",
                this.name ?? "",
                this.is_male,
                this.age,
                this.dcm_type ?? "",
                this.study_department ?? "",
                this.report_num,
                this.dcm_num,
                this.desc ?? "",
                this.available,
                Environment.NewLine);
        }
    }
    public class GetAllReadyUsersCmd : Time2Cmd
    {
        public GetAllReadyUsersCmd()
        {
            this.id = CmdType.Get_All_Ready_Users_Cmd;
        }
    }
    public class GetAllReadyUsersCmdAck : CmdAck
    {
        /// <summary>
        /// 用户列表
        /// </summary>
        public List<ReadyUserItem> users { get; set; }

        /// <summary>
        /// 用户数
        /// </summary>
        public Int32 users_count
        {
            get
            {
                return (this.users != null) ? this.users.Count : 0;
            }
        }

        public GetAllReadyUsersCmdAck()
        {
            this.id = CmdType.Get_All_Ready_Users_Cmd_Ack;
            this.users = new List<ReadyUserItem>();
        }

        public override List<byte> to_byte()
        {
            List<byte> buffer = base.to_byte();
            buffer.AddRange(get_bytes(this.users_count));
            foreach (var v in this.users)
            {
                buffer.AddRange(v.to_byte());
            }
            return buffer;
        }
        public override bool from_byte(byte[] buffer, ref long pos)
        {
            if (!base.from_byte(buffer, ref pos))
                return false;

            this.users.Clear();

            Int32 count = to_int(buffer, ref pos);
            for (int i = 0; i < count; i++)
            {
                ReadyUserItem item = new ReadyUserItem();
                item.from_byte(buffer, ref pos);
                this.users.Add(item);
            }
            return true;
        }

        public override string ToString()
        {
            string s = base.ToString() + string.Format("{1}\t users:{0}",
                this.users_count, Environment.NewLine);
            for (int i = 0; i < this.users_count; i++)
            {
                s += users[i].ToString();
            }
            return s;
        }
    }
    public class GetChangedReadyUsersCmd : Time2Cmd
    {
        public GetChangedReadyUsersCmd()
        {
            this.id = CmdType.Get_Changed_Ready_Users_Cmd;
        }
    }
    public class GetChangedReadyUsersCmdAck : GetAllReadyUsersCmdAck
    {
        public GetChangedReadyUsersCmdAck()
        {
            this.id = CmdType.Get_Changed_Ready_Users_Cmd_Ack;
        }
    }

    public class MsgCmd : Cmd
    {
        public string msg { get; set; }

        /// <summary>
        /// 0-info
        /// -1-warning-服务器闪烁报警
        /// -2-error-服务器闪烁报警+声音，必须紧急处理
        /// </summary>
        public int msg_level { get; set; }

        public MsgCmd()
        {
            this.id = CmdType.Msg_Cmd;
            this.msg = null;
            this.msg_level = 0;
        }

        public override bool from_byte(byte[] buffer, ref long pos)
        {
            if (!base.from_byte(buffer, ref pos))
                return false;
            this.msg = to_string(buffer, ref pos);
            this.msg_level = to_int(buffer, ref pos);
            return true;
        }
        public override List<byte> to_byte()
        {
            List<byte> buffer = base.to_byte();
            buffer.AddRange(get_bytes(this.msg));
            buffer.AddRange(Cmd.get_bytes(this.msg_level));
            return buffer;
        }

        public override string ToString()
        {
            return base.ToString() + string.Format("{2}\t msg:{0}{2}\t msg level:{1}",
                this.msg, this.msg_level, Environment.NewLine);
        }
    }
    public class MsgCmdAck : CmdAck
    {
        public MsgCmdAck()
        {
            this.id = CmdType.Msg_Cmd_Ack;
        }
    }

    public class FileNameContentItem
    {
        public string name { get; set; }
        public byte[] content { get; set; }

        public List<byte> to_byte()
        {
            List<byte> buffer = new List<byte>();
            buffer.AddRange(Cmd.get_bytes(this.name));
            long len = this.content == null ? 0 : this.content.LongLength;
            buffer.AddRange(Cmd.get_bytes(len));
            if (len > 0)
            {
                buffer.AddRange(this.content);
            }
            return buffer;
        }
        public bool from_byte(byte[] buffer, ref long pos)
        {
            this.name = Cmd.to_string(buffer, ref pos);
            long len = Cmd.to_long(buffer, ref pos);
            this.content = null;
            if (len > 0)
            {
                this.content = new byte[len];
                Array.Copy(buffer, pos, this.content, 0, len);
                pos += len;
            }
            return true;
        }

        public static List<byte> get_bytes(List<FileNameContentItem> items)
        {
            List<byte> buffer = new List<byte>();
            int size = items == null ? 0 : items.Count();
            buffer.AddRange(Cmd.get_bytes(size));
            for (int i = 0; i < size; i++)
            {
                buffer.AddRange(items[i].to_byte());
            }
            return buffer;
        }
        public static List<FileNameContentItem> to_contents(byte[] buffer, ref long pos)
        {
            List<FileNameContentItem> items = new List<FileNameContentItem>();
            int size = Cmd.to_int(buffer, ref pos);
            for (int i = 0; i < size; i++)
            {
                FileNameContentItem item = new FileNameContentItem();
                if (!item.from_byte(buffer, ref pos))
                    return null;
                items.Add(item);
            }
            return items;
        }

        public override string ToString()
        {
            return string.Format("{2}\t\t file name:{0}    content length:{1}",
                this.name,
                this.content == null ? 0 : this.content.LongLength,
                Environment.NewLine);
        }
    }
    public class QueryUserCmd : Cmd
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public string user_id { get; set; }

        /// <summary>
        /// 是否需要回传数据
        /// </summary>
        public bool get_report_files { get; set; }
        public bool get_dcm_files { get; set; }

        public QueryUserCmd()
        {
            this.id = CmdType.Query_User_Cmd;
            this.get_report_files = false;//true,必须设置report_files
            this.get_dcm_files = false;
        }

        public override bool from_byte(byte[] buffer, ref long pos)
        {
            if (!base.from_byte(buffer, ref pos))
                return false;
            this.user_id = to_string(buffer, ref pos);
            this.get_report_files = to_bool(buffer, ref pos);
            this.get_dcm_files = to_bool(buffer, ref pos);
            return true;
        }
        public override List<byte> to_byte()
        {
            List<byte> buffer = base.to_byte();
            buffer.AddRange(get_bytes(this.user_id));
            buffer.AddRange(get_bytes(this.get_report_files));
            buffer.AddRange(get_bytes(this.get_dcm_files));
            return buffer;
        }

        public override string ToString()
        {
            return base.ToString() + string.Format("{3}\t user id:{0}{3}\t get report files:{1}{3}\t get dcm files:{2}",
                this.user_id,
                this.get_report_files,
                this.get_dcm_files,
                Environment.NewLine);
        }
    }
    public class QueryUserCmdAck : CmdAck
    {
        public ReadyUserItem user { get; private set; }

        /// <summary>
        /// 报告、胶片是否就绪
        /// </summary>
        public bool report_files_ready { get; set; }
        public bool dcm_files_ready { get; set; }

        /// <summary>
        /// ready为true，则填充相应的文件数量
        /// </summary>
        public int report_files_number { get; set; }
        public int dcm_files_number { get; set; }

        /// <summary>
        /// 是否需要回传数据
        /// </summary>
        public bool transfer_report_files { get; set; }
        public bool transfer_dcm_files { get; set; }

        /// <summary>
        /// 打印过的次数
        /// 首次打印，次数为0
        /// </summary>
        public int printed_report_times { get; set; }
        public int printed_dcm_times { get; set; }

        /// <summary>
        /// 报告文件名，dcm文件名+文件内容，server填充
        /// client接收后，解析得到
        /// </summary>
        public List<FileNameContentItem> report_file_contents { get; set; }
        public List<FileNameContentItem> dcm_file_contents { get; set; }

        public QueryUserCmdAck()
        {
            this.id = CmdType.Query_User_Cmd_Ack;
            this.user = new ReadyUserItem();
            this.transfer_report_files = false;
            this.transfer_dcm_files = false;
            this.report_files_ready = false;
            this.dcm_files_ready = false;
            this.report_files_number = 0;
            this.dcm_files_number = 0;
            this.report_file_contents = null;
            this.dcm_file_contents = null;
            this.printed_dcm_times = 0;
            this.printed_report_times = 0;
        }

        public override bool from_byte(byte[] buffer, ref long pos)
        {
            if (!base.from_byte(buffer, ref pos))
                return false;
            if (!this.user.from_byte(buffer, ref pos))
                return false;
            this.report_files_ready = to_bool(buffer, ref pos);
            this.dcm_files_ready = to_bool(buffer, ref pos);
            this.report_files_number = Cmd.to_int(buffer, ref pos);
            this.dcm_files_number = Cmd.to_int(buffer, ref pos);
            this.transfer_report_files = to_bool(buffer, ref pos);
            this.transfer_dcm_files = to_bool(buffer, ref pos);
            
            this.report_file_contents = null;
            if (this.transfer_report_files)
            {
                this.report_file_contents = FileNameContentItem.to_contents(buffer, ref pos);
            }
            this.dcm_file_contents = null;
            if (this.transfer_dcm_files)
            {
                this.dcm_file_contents = FileNameContentItem.to_contents(buffer, ref pos);
            }

            this.printed_report_times = Cmd.to_int(buffer, ref pos);
            this.printed_dcm_times = Cmd.to_int(buffer, ref pos);
            return true;
        }
        public override List<byte> to_byte()
        {
            List<byte> buffer = base.to_byte();
            buffer.AddRange(this.user.to_byte());
            buffer.AddRange(get_bytes(this.report_files_ready));
            buffer.AddRange(get_bytes(this.dcm_files_ready));
            buffer.AddRange(Cmd.get_bytes(this.report_files_number));
            buffer.AddRange(Cmd.get_bytes(this.dcm_files_number));
            buffer.AddRange(get_bytes(this.transfer_report_files));
            buffer.AddRange(get_bytes(this.transfer_dcm_files));
            if (this.transfer_report_files)
                buffer.AddRange(FileNameContentItem.get_bytes(this.report_file_contents));
            if (this.transfer_dcm_files)
                buffer.AddRange(FileNameContentItem.get_bytes(this.dcm_file_contents));
            buffer.AddRange(Cmd.get_bytes(this.printed_report_times));
            buffer.AddRange(Cmd.get_bytes(this.printed_dcm_times));
            return buffer;
        }

        public override string ToString()
        {
            string s = base.ToString() + string.Format("{0}{9}\t report files ready:{1}{9}\t dcm files ready:{2}{9}\t transport report files:{3}{9}\t transport dcm files:{4}{9}\t report files number:{5}{9}\t dcm files number:{6}{9}\t printed report times:{7}{9}\t printed dcm times:{8}",
                this.user.ToString(),
                this.report_files_ready,
                this.dcm_files_ready,
                this.transfer_report_files,
                this.transfer_dcm_files,
                this.report_files_number,
                this.dcm_files_number,
                this.printed_report_times,
                this.printed_dcm_times,
                Environment.NewLine);
            if (transfer_report_files)
            {
                s += string.Format("{1}\t report files:{0}", 
                    this.report_file_contents == null ? 0 : this.report_file_contents.Count,
                    Environment.NewLine);
                for (int i = 0; i < report_file_contents.Count; i++)
                {
                    s += report_file_contents[i].ToString();
                }
            }
            if (transfer_dcm_files)
            {
                s += string.Format("{1}\t dcm files:{0}", 
                    this.dcm_file_contents == null ? 0 : this.dcm_file_contents.Count,
                    Environment.NewLine);
                for (int i = 0; i < dcm_file_contents.Count; i++)
                {
                    s += dcm_file_contents[i].ToString();
                }
            }
            return s;
        }
    }

    public class ServerPrintUserCmd : Cmd
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public string user_id { get; set; }

        /// <summary>
        /// 打印者id（工号）
        /// 首次打印为空，多次打印时要提供打印者id
        /// </summary>
        public string printer_id { get; set; }

        /// <summary>
        /// 打印报告或者胶片
        /// </summary>
        public bool print_report { get; set; }
        public bool print_dcm { get; set; }

        public ServerPrintUserCmd()
        {
            this.id = CmdType.Server_Print_User_Cmd;
            this.print_report = false;
            this.print_dcm = false;
            this.printer_id = null;
            this.user_id = null;
        }

        public override bool from_byte(byte[] buffer, ref long pos)
        {
            if (!base.from_byte(buffer, ref pos))
                return false;
            this.user_id = to_string(buffer, ref pos);
            this.printer_id = to_string(buffer, ref pos);
            this.print_report = to_bool(buffer, ref pos);
            this.print_dcm = to_bool(buffer, ref pos);
            return true;
        }
        public override List<byte> to_byte()
        {
            List<byte> buffer = base.to_byte();
            buffer.AddRange(get_bytes(this.user_id));
            buffer.AddRange(Cmd.get_bytes(this.printer_id));
            buffer.AddRange(get_bytes(this.print_report));
            buffer.AddRange(get_bytes(this.print_dcm));
            return buffer;
        }

        public override string ToString()
        {
            return base.ToString() + string.Format("{4}\t user id:{0}{4}\t printer id:{1}{4}\t print report:{2}{4}\t print dcm:{3}",
                this.user_id,
                this.printer_id,
                this.print_report,
                this.print_dcm,
                Environment.NewLine);
        }
    }
    public class ServerPrintUserCmdAck : CmdAck
    {
        /// <summary>
        /// 是否打印
        /// </summary>
        public bool print_report { get; set; }
        public bool print_dcm { get; set; }

        /// <summary>
        ///  0=成功无错误
        /// -1=失败
        /// </summary>
        public int print_report_ack { get; set; }
        public int print_dcm_ack { get; set; }

        public ServerPrintUserCmdAck()
        {
            this.id = CmdType.Server_Print_User_Cmd_Ack;
            this.print_dcm = false;
            this.print_report = false;
            this.print_dcm_ack = 0;
            this.print_report_ack = 0;
        }

        public override bool from_byte(byte[] buffer, ref long pos)
        {
            if (!base.from_byte(buffer, ref pos))
                return false;
            this.print_report = Cmd.to_bool(buffer, ref pos);
            this.print_dcm = Cmd.to_bool(buffer, ref pos);
            this.print_report_ack = Cmd.to_int(buffer, ref pos);
            this.print_dcm_ack = Cmd.to_int(buffer, ref pos);
            return true;
        }
        public override List<byte> to_byte()
        {
            List<byte> buffer = base.to_byte();
            buffer.AddRange(Cmd.get_bytes(this.print_report));
            buffer.AddRange(Cmd.get_bytes(this.print_dcm));
            buffer.AddRange(Cmd.get_bytes(this.print_report_ack));
            buffer.AddRange(Cmd.get_bytes(this.print_dcm_ack));
            return buffer;
        }
        public override string ToString()
        {
            return base.ToString() + string.Format("{4}\t print report:{0}[{1}]{4}\t print dcm:{2}[{3}]",
                this.print_report,
                this.print_report_ack,
                this.print_dcm,
                this.print_dcm_ack,
                Environment.NewLine);
        }
    }

    public class ClientPrintUserCmd : ServerPrintUserCmd
    {
        public ClientPrintUserCmd()
        {
            this.id = CmdType.Client_Print_User_Cmd;
        }
    }
    public class ClientPrintUserCmdAck : ServerPrintUserCmdAck
    {
        public ClientPrintUserCmdAck()
        {
            this.id = CmdType.Client_Print_User_Cmd_Ack;
        }
    }

    public class LoginUserItem
    {
        /// <summary>
        /// 工号
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        public string login_name { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string login_psw { get; set; }

        public LoginUserItem()
        {
            this.id = null;
            this.login_name = null;
            this.login_psw = null;
        }

        public List<byte> to_byte()
        {
            List<byte> buffer = new List<byte>();
            buffer.AddRange(Cmd.get_bytes(this.id));
            buffer.AddRange(Cmd.get_bytes(login_name));
            buffer.AddRange(Cmd.get_bytes(login_psw));
            return buffer;
        }
        public bool from_byte(byte[] buffer, ref long pos)
        {
            this.id = Cmd.to_string(buffer, ref pos);
            this.login_name = Cmd.to_string(buffer, ref pos);
            this.login_psw = Cmd.to_string(buffer, ref pos);
            return true;
        }
        public override string ToString()
        {
            return string.Format("LoginUserItem [id={0}, name={1}, psw={2}]", 
                this.id ?? "",
                this.login_name ?? "",
                this.login_psw ?? "");
        }
    }
    public class LoginCmd : Cmd
    {
        public LoginUserItem login_user { get; private set; }

        public LoginCmd()
        {
            this.id = CmdType.Login_Cmd;
            this.login_user = new LoginUserItem();
        }

        public override bool from_byte(byte[] buffer, ref long pos)
        {
            if (!base.from_byte(buffer, ref pos))
                return false;
            if (!this.login_user.from_byte(buffer, ref pos))
                return false;
            return true;
        }
        public override List<byte> to_byte()
        {
            List<byte> buffer = base.to_byte();
            buffer.AddRange(this.login_user.to_byte());
            return buffer;
        }
        public override string ToString()
        {
            return base.ToString() + string.Format("{1}\t {0}", 
                this.login_user.ToString(), Environment.NewLine);
        }
    }
    /// <summary>
    /// status:
    ///      0=登录信息正确
    ///     -1=用户不存在
    ///     -2=密码错误
    ///     -3=权限不足
    ///     -1000=其它错误
    /// </summary>
    public class LoginCmdAck : CmdAck
    {
        public LoginCmdAck()
        {
            this.id = CmdType.Login_Cmd_Ack;
        }
    }













    public class IdentityCmd : Cmd
    {
        public IdentityCmd()
        {
            this.id = CmdType.Identity_Cmd;
        }
    }
    public class IdentityCmdAck : CmdAck
    {
        public IdentityCmdAck()
        {
            this.id = CmdType.Identity_Cmd_Ack;
        }
    }

   
}
