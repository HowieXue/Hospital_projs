using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace control
{
    public partial class DicomSPUControl : UserControl
    {
        public DicomSPUControl()
        {
            InitializeComponent();

            RemoteAddress = "localhost";
            RemotePort = 8000;
            CallingAE = "SCU";
            CalledAE = "SCP";
        }

        public string RemoteAddress
        {
            get { return txtRAddress.Text; }
            set { txtRAddress.Text = value; }
        }
        public int RemotePort
        {
            get { return int.Parse(txtRPort.Text); }
            set { txtRPort.Text = value.ToString(); }
        }
        public string CallingAE
        {
            get { return txtCallingAE.Text; }
            set { txtCallingAE.Text = value; }
        }
        public string CalledAE
        {
            get { return txtCalledAE.Text; }
            set { txtCalledAE.Text = value; }
        }
    }
}
