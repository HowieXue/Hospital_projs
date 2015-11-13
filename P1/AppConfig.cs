using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using net;

namespace P1
{
    public class AppConfig
    {
        private string get_value(string key, string default_value)
        {
            try { return ConfigurationManager.AppSettings[key]; }
            catch { return default_value; }
        }
        private void set_value(string key, object value)
        {
            try
            {
                Configuration conf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                conf.AppSettings.Settings[key].Value = value.ToString();
                conf.Save();
                ConfigurationManager.RefreshSection("appSettings");
            }
            catch (Exception ex)
            {
                EzLogger.GlobalLogger.warning(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        //P1 server tcp listening port
        //默认21001
        public int P1ServerPort
        {
            get { return Convert.ToInt32(get_value("p1_server_port", "21001")); }
            set { set_value("p1_server_port", Convert.ToString(value)); }
        }

        //P5 server ip
        //默认空
        public string P5ServerIp
        {
            get { return get_value("p5_server_ip", string.Empty); }
            set { set_value("p5_server_ip", value); }
        }

        //P5 server tcp listening port
        //默认25001
        public int P5ServerPort
        {
            get { return Convert.ToInt32(get_value("p5_server_port", "25001")); }
            set { set_value("p5_server_port", Convert.ToString(value)); }
        }

        //是否广播服务器地址，而不是从配置文件获取地址？
        //默认false
        public bool IsBroadcastServerIp
        {
            get { return Convert.ToBoolean(get_value("broadcast_server_ip", "false")); }
            set { set_value("broadcast_server_ip", Convert.ToString(value)); }
        }

        //胶片打印机剩余胶片数报警下界(低于该值报警)
        //默认10
        public int DcmFilmNumberWarningLowerBound
        {
            get { return Convert.ToInt32(get_value("dcm_number_warning_lower_bound", "10")); }
            set { set_value("dcm_number_warning_lower_bound", Convert.ToString(value)); }
        }

        //dcm文件接收文件夹
        //默认空
        public string DcmFileReceiveFolder
        {
            get { return get_value("dcm_receive_folder", string.Empty); }
            set { set_value("dcm_receive_folder", value); }
        }

        //dcm文件存储文件夹
        //默认空
        public string DcmFileStorageFolder
        {
            get { return get_value("dcm_storage_folder", string.Empty); }
            set { set_value("dcm_storage_folder", value); }
        }

        //dcm文件后备存储文件夹
        //默认空
        public string DcmFileBackupStorageFolder
        {
            get { return get_value("dcm_backup_storage_folder", string.Empty); }
            set { set_value("dcm_backup_storage_folder", value); }
        }

        //dcm文件在storage folder中存储时长(day)，如无后备存储过期删除，否则移入后备存储
        //默认30
        public int DcmFileStorageMaxDays
        {
            get { return Convert.ToInt32(get_value("dcm_storage_max_days", "30")); }
            set { set_value("dcm_storage_max_days", Convert.ToString(value)); }
        }

        //报告文件接收文件夹
        //默认空
        public string ReportFileReceiveFolder
        {
            get { return get_value("report_receive_folder", string.Empty); }
            set { set_value("report_receive_folder", value); }
        }

        //报告文件存储文件夹
        //默认空
        public string ReportFileStorageFolder
        {
            get { return get_value("report_storage_folder", string.Empty); }
            set { set_value("report_storage_folder", value); }
        }

        //报告文件后备存储文件夹
        //默认空
        public string ReportFileBackupStorageFolder
        {
            get { return get_value("report_backup_storage_folder", string.Empty); }
            set { set_value("report_backup_storage_folder", value); }
        }

        //报告文件存储时长(day)
        //默认30
        public int ReportFileStorageMaxDays 
        {
            get { return Convert.ToInt32(get_value("report_storage_max_days", "30")); }
            set { set_value("report_storage_max_days", Convert.ToString(value)); }
        }

        //提取pdf文件中的id号，匹配模板
        //默认空
        public string PdfExtractIdPattern
        {
            get { return get_value("pdf_extract_id_pattern", string.Empty); }
            set { set_value("pdf_extract_id_pattern", value); }
        }

        //提取pdf文件中的姓名，匹配模板
        //默认空
        public string PdfExtractNamePattern
        {
            get { return get_value("pdf_extract_name_pattern", string.Empty); }
            set { set_value("pdf_extract_name_pattern", value); }
        }
    }
}
