using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace control
{
    public partial class NoTitleLoginForm : Form
    {
        private int _default_life_number = 15;
        private int _life_number = 15;

        public NoTitleLoginForm()
        {
            InitializeComponent();

            this.TitleText = "";
        }

        public string TitleText
        {
            get { return this.lblTitle.Text; }
            set { this.lblTitle.Text = value; }
        }

        public string LoginName { get { return this.txtLoginName.Text; } }
        public string LoginPsw { get { return this.txtLoginPsw.Text; } }

        private void NoTitleLoginForm_KeyDown(object sender, KeyEventArgs e)
        {
            _life_number = _default_life_number;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _life_number--;
            if (_life_number <= 0)
                this.btnCancel.PerformClick();
        }
    }
}
