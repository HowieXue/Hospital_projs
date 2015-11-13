namespace P4
{
    partial class P4Form
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lbl = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.scroll = new control.ScrollingText();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblP1Ip = new System.Windows.Forms.Label();
            this.lblP1Status = new System.Windows.Forms.Label();
            this.lblP5Ip = new System.Windows.Forms.Label();
            this.lblP5Status = new System.Windows.Forms.Label();
            this.lblDateTime = new System.Windows.Forms.Label();
            this.p4_100ms_timer = new System.Windows.Forms.Timer(this.components);
            this.lblTotalNumber = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this._tcp_connect_timer = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl
            // 
            this.lbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl.Font = new System.Drawing.Font("宋体", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl.ForeColor = System.Drawing.Color.Black;
            this.lbl.Location = new System.Drawing.Point(22, 4);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(756, 51);
            this.lbl.TabIndex = 1;
            this.lbl.Text = "天津市总医院";
            this.lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.scroll);
            this.panel1.Location = new System.Drawing.Point(22, 73);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(756, 359);
            this.panel1.TabIndex = 2;
            // 
            // scroll
            // 
            this.scroll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.scroll.ColumnHeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.scroll.ColumnHeaderFont = new System.Drawing.Font("宋体", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.scroll.ColumnHeaderForeColor = System.Drawing.Color.Blue;
            this.scroll.ColumnHeaderHeight = 60F;
            this.scroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scroll.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.scroll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.scroll.IsAutoLineNumber = true;
            this.scroll.LastBlankRow = 3;
            this.scroll.Location = new System.Drawing.Point(0, 0);
            this.scroll.Name = "scroll";
            this.scroll.RowHeight = 40F;
            this.scroll.ScrollSpeed = 0.5F;
            this.scroll.ShowColumnHeader = true;
            this.scroll.Size = new System.Drawing.Size(754, 357);
            this.scroll.TabIndex = 0;
            this.scroll.Text = "scrollingText4";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(196, 435);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 40);
            this.label1.TabIndex = 0;
            this.label1.Text = "P1:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(524, 435);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 40);
            this.label2.TabIndex = 1;
            this.label2.Text = "P5:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblP1Ip
            // 
            this.lblP1Ip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblP1Ip.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblP1Ip.Location = new System.Drawing.Point(225, 435);
            this.lblP1Ip.Name = "lblP1Ip";
            this.lblP1Ip.Size = new System.Drawing.Size(161, 40);
            this.lblP1Ip.TabIndex = 2;
            this.lblP1Ip.Text = "192.168.200.200:12200";
            this.lblP1Ip.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblP1Status
            // 
            this.lblP1Status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblP1Status.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblP1Status.Location = new System.Drawing.Point(392, 435);
            this.lblP1Status.Name = "lblP1Status";
            this.lblP1Status.Size = new System.Drawing.Size(59, 40);
            this.lblP1Status.TabIndex = 3;
            this.lblP1Status.Text = "未连接";
            this.lblP1Status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblP5Ip
            // 
            this.lblP5Ip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblP5Ip.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblP5Ip.Location = new System.Drawing.Point(552, 435);
            this.lblP5Ip.Name = "lblP5Ip";
            this.lblP5Ip.Size = new System.Drawing.Size(161, 40);
            this.lblP5Ip.TabIndex = 4;
            this.lblP5Ip.Text = "127.127.127.127:55555";
            this.lblP5Ip.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblP5Status
            // 
            this.lblP5Status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblP5Status.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblP5Status.Location = new System.Drawing.Point(719, 435);
            this.lblP5Status.Name = "lblP5Status";
            this.lblP5Status.Size = new System.Drawing.Size(59, 40);
            this.lblP5Status.TabIndex = 5;
            this.lblP5Status.Text = "未连接";
            this.lblP5Status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDateTime
            // 
            this.lblDateTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDateTime.BackColor = System.Drawing.Color.Transparent;
            this.lblDateTime.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDateTime.Location = new System.Drawing.Point(518, 31);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(260, 40);
            this.lblDateTime.TabIndex = 4;
            this.lblDateTime.Text = "2015-09-28 15:05:35";
            this.lblDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // p4_100ms_timer
            // 
            this.p4_100ms_timer.Enabled = true;
            this.p4_100ms_timer.Tick += new System.EventHandler(this.p4_100ms_timer_Tick);
            // 
            // lblTotalNumber
            // 
            this.lblTotalNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalNumber.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTotalNumber.Location = new System.Drawing.Point(80, 436);
            this.lblTotalNumber.Name = "lblTotalNumber";
            this.lblTotalNumber.Size = new System.Drawing.Size(100, 40);
            this.lblTotalNumber.TabIndex = 6;
            this.lblTotalNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(20, 435);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 40);
            this.label3.TabIndex = 6;
            this.label3.Text = "总人数：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _tcp_connect_timer
            // 
            this._tcp_connect_timer.Enabled = true;
            this._tcp_connect_timer.Interval = 15000;
            this._tcp_connect_timer.Tick += new System.EventHandler(this.tcp_connect_timer_Tick);
            // 
            // P4Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(800, 484);
            this.Controls.Add(this.lblP5Status);
            this.Controls.Add(this.lblP5Ip);
            this.Controls.Add(this.lblP1Status);
            this.Controls.Add(this.lblDateTime);
            this.Controls.Add(this.lbl);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblP1Ip);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTotalNumber);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "P4Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "P4-大屏显示程序";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.P4Form_FormClosed);
            this.Load += new System.EventHandler(this.P4Form_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblP1Status;
        private System.Windows.Forms.Label lblP1Ip;
        private System.Windows.Forms.Label lblP5Ip;
        private System.Windows.Forms.Label lblP5Status;
        private System.Windows.Forms.Label lblDateTime;
        private System.Windows.Forms.Timer p4_100ms_timer;
        private System.Windows.Forms.Label lblTotalNumber;
        private System.Windows.Forms.Label label3;
        private control.ScrollingText scroll;
        private System.Windows.Forms.Timer _tcp_connect_timer;
    }
}

