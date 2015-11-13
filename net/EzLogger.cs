using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace net
{
    public class EzLogger : IDisposable
    {
        public static EzLogger GlobalLogger = new EzLogger(null, "");

        TextBox _txt_box = null;
        string _file_name = null;
        public EzLogger(TextBox tb, string file_name)
        {
            _txt_box = tb;
            _file_name = file_name;
        }

        public void Dispose()
        {
            
        }

        private void log_to_textbox(string msg)
        {
            try
            {
                if (_txt_box != null)
                {
                    if (_txt_box.InvokeRequired)
                        _txt_box.Invoke(new Action<string>(log_to_textbox), msg);
                    else
                    {
                        lock (_txt_box)
                        {
                            _txt_box.AppendText(msg);
                            _txt_box.AppendText(Environment.NewLine);

                            if (_txt_box.Text.Length > 11 * 1024 * 1024)
                            {
                                _txt_box.SelectionStart = 0;
                                _txt_box.SelectionLength = 1024 * 1024;
                                _txt_box.SelectedText = "";
                            }

                            _txt_box.SelectionStart = _txt_box.TextLength;
                            _txt_box.ScrollToCaret();
                        }
                    }
                }
            }
            catch { }
        }
        public void info(string msg)
        {
            Console.WriteLine(msg);

            log_to_textbox(msg);
        }
        public void debug(string msg)
        {
            Console.WriteLine(msg);

            log_to_textbox(msg);
        }
        public void warning(string msg)
        {
            Console.WriteLine(msg);

            log_to_textbox(msg);

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

                    log_to_textbox(fmt);
                }
            }
        }
    }
}
