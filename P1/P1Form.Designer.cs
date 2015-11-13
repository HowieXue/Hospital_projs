namespace P1
{
    partial class P1Form
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
            this.menu1 = new System.Windows.Forms.MenuStrip();
            this.系统SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.启动TToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.停止PToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.退出XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.网络参数NToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.文件参数FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助HToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.使用说明MToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.关于AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.人工识别干预MToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tool1 = new System.Windows.Forms.ToolStrip();
            this._100ms_timer = new System.Windows.Forms.Timer(this.components);
            this.status1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsPaired = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsDcm = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsReport = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsP2Number = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsP3Number = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsP4Number = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsP5 = new System.Windows.Forms.ToolStripStatusLabel();
            this._tcp_connect_timer = new System.Windows.Forms.Timer(this.components);
            this.logger_ui = new System.Windows.Forms.TextBox();
            this.menu1.SuspendLayout();
            this.status1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu1
            // 
            this.menu1.Font = new System.Drawing.Font("宋体", 12F);
            this.menu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.系统SToolStripMenuItem,
            this.设置CToolStripMenuItem,
            this.帮助HToolStripMenuItem,
            this.人工识别干预MToolStripMenuItem});
            this.menu1.Location = new System.Drawing.Point(0, 0);
            this.menu1.Name = "menu1";
            this.menu1.Padding = new System.Windows.Forms.Padding(8, 3, 0, 3);
            this.menu1.Size = new System.Drawing.Size(1075, 26);
            this.menu1.TabIndex = 0;
            this.menu1.Text = "menuStrip1";
            // 
            // 系统SToolStripMenuItem
            // 
            this.系统SToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.启动TToolStripMenuItem,
            this.停止PToolStripMenuItem,
            this.toolStripSeparator1,
            this.退出XToolStripMenuItem});
            this.系统SToolStripMenuItem.Name = "系统SToolStripMenuItem";
            this.系统SToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.系统SToolStripMenuItem.Text = "系统(&S)";
            // 
            // 启动TToolStripMenuItem
            // 
            this.启动TToolStripMenuItem.Name = "启动TToolStripMenuItem";
            this.启动TToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.启动TToolStripMenuItem.Text = "启动(&T)";
            // 
            // 停止PToolStripMenuItem
            // 
            this.停止PToolStripMenuItem.Checked = true;
            this.停止PToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.停止PToolStripMenuItem.Enabled = false;
            this.停止PToolStripMenuItem.Name = "停止PToolStripMenuItem";
            this.停止PToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.停止PToolStripMenuItem.Text = "停止(&P)";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(129, 6);
            // 
            // 退出XToolStripMenuItem
            // 
            this.退出XToolStripMenuItem.Name = "退出XToolStripMenuItem";
            this.退出XToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.退出XToolStripMenuItem.Text = "退出(&X)";
            // 
            // 设置CToolStripMenuItem
            // 
            this.设置CToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.网络参数NToolStripMenuItem,
            this.文件参数FToolStripMenuItem});
            this.设置CToolStripMenuItem.Name = "设置CToolStripMenuItem";
            this.设置CToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.设置CToolStripMenuItem.Text = "设置(&C)";
            // 
            // 网络参数NToolStripMenuItem
            // 
            this.网络参数NToolStripMenuItem.Name = "网络参数NToolStripMenuItem";
            this.网络参数NToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.网络参数NToolStripMenuItem.Text = "网络参数(&N)";
            // 
            // 文件参数FToolStripMenuItem
            // 
            this.文件参数FToolStripMenuItem.Name = "文件参数FToolStripMenuItem";
            this.文件参数FToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.文件参数FToolStripMenuItem.Text = "文件参数(&F)";
            // 
            // 帮助HToolStripMenuItem
            // 
            this.帮助HToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.使用说明MToolStripMenuItem,
            this.toolStripSeparator2,
            this.关于AToolStripMenuItem});
            this.帮助HToolStripMenuItem.Name = "帮助HToolStripMenuItem";
            this.帮助HToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.帮助HToolStripMenuItem.Text = "帮助(&H)";
            // 
            // 使用说明MToolStripMenuItem
            // 
            this.使用说明MToolStripMenuItem.Name = "使用说明MToolStripMenuItem";
            this.使用说明MToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.使用说明MToolStripMenuItem.Text = "使用说明(&M)";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(161, 6);
            // 
            // 关于AToolStripMenuItem
            // 
            this.关于AToolStripMenuItem.Name = "关于AToolStripMenuItem";
            this.关于AToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.关于AToolStripMenuItem.Text = "关于P1(&A)";
            // 
            // 人工识别干预MToolStripMenuItem
            // 
            this.人工识别干预MToolStripMenuItem.Name = "人工识别干预MToolStripMenuItem";
            this.人工识别干预MToolStripMenuItem.Size = new System.Drawing.Size(108, 20);
            this.人工识别干预MToolStripMenuItem.Text = "人工干预(&M)";
            // 
            // tool1
            // 
            this.tool1.AutoSize = false;
            this.tool1.Location = new System.Drawing.Point(0, 26);
            this.tool1.Name = "tool1";
            this.tool1.Size = new System.Drawing.Size(1075, 32);
            this.tool1.TabIndex = 1;
            this.tool1.Text = "toolStrip1";
            // 
            // _100ms_timer
            // 
            this._100ms_timer.Enabled = true;
            this._100ms_timer.Tick += new System.EventHandler(this._100ms_timer_Tick);
            // 
            // status1
            // 
            this.status1.Font = new System.Drawing.Font("宋体", 12F);
            this.status1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.tsPaired,
            this.tsDcm,
            this.tsReport,
            this.tsP2Number,
            this.tsP3Number,
            this.tsP4Number,
            this.tsP5});
            this.status1.Location = new System.Drawing.Point(0, 720);
            this.status1.Name = "status1";
            this.status1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.status1.Size = new System.Drawing.Size(1075, 33);
            this.status1.TabIndex = 8;
            this.status1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.AutoSize = false;
            this.toolStripStatusLabel1.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel1.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(200, 28);
            this.toolStripStatusLabel1.Text = "天津市总医院";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsPaired
            // 
            this.tsPaired.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tsPaired.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.tsPaired.Name = "tsPaired";
            this.tsPaired.Size = new System.Drawing.Size(340, 28);
            this.tsPaired.Tag = "检查完成{0}人，已打印{1}人，未打印{2}人  ";
            this.tsPaired.Text = "检查完成{0}人，已打印{1}人，未打印{2}人  ";
            // 
            // tsDcm
            // 
            this.tsDcm.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tsDcm.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.tsDcm.Name = "tsDcm";
            this.tsDcm.Size = new System.Drawing.Size(204, 28);
            this.tsDcm.Tag = "胶片完成{0}人，共{1}张  ";
            this.tsDcm.Text = "胶片完成{0}人，共{1}张  ";
            // 
            // tsReport
            // 
            this.tsReport.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tsReport.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.tsReport.Name = "tsReport";
            this.tsReport.Size = new System.Drawing.Size(132, 28);
            this.tsReport.Tag = "报告完成{0}人  ";
            this.tsReport.Text = "报告完成{0}人  ";
            // 
            // tsP2Number
            // 
            this.tsP2Number.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tsP2Number.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.tsP2Number.Name = "tsP2Number";
            this.tsP2Number.Size = new System.Drawing.Size(76, 28);
            this.tsP2Number.Tag = "P2:{0}  ";
            this.tsP2Number.Text = "P2:{0}  ";
            // 
            // tsP3Number
            // 
            this.tsP3Number.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tsP3Number.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.tsP3Number.Name = "tsP3Number";
            this.tsP3Number.Size = new System.Drawing.Size(76, 28);
            this.tsP3Number.Tag = "P3:{0}  ";
            this.tsP3Number.Text = "P3:{0}  ";
            // 
            // tsP4Number
            // 
            this.tsP4Number.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tsP4Number.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.tsP4Number.Name = "tsP4Number";
            this.tsP4Number.Size = new System.Drawing.Size(76, 20);
            this.tsP4Number.Tag = "P4:{0}  ";
            this.tsP4Number.Text = "P4:{0}  ";
            // 
            // tsP5
            // 
            this.tsP5.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tsP5.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.tsP5.Name = "tsP5";
            this.tsP5.Size = new System.Drawing.Size(76, 20);
            this.tsP5.Tag = "P5:{0}  ";
            this.tsP5.Text = "P5:{0}  ";
            // 
            // _tcp_connect_timer
            // 
            this._tcp_connect_timer.Enabled = true;
            this._tcp_connect_timer.Interval = 20000;
            this._tcp_connect_timer.Tick += new System.EventHandler(this.tcp_connect_timer_Tick);
            // 
            // logger_ui
            // 
            this.logger_ui.AcceptsReturn = true;
            this.logger_ui.AcceptsTab = true;
            this.logger_ui.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logger_ui.Location = new System.Drawing.Point(0, 58);
            this.logger_ui.MaxLength = 0;
            this.logger_ui.Multiline = true;
            this.logger_ui.Name = "logger_ui";
            this.logger_ui.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.logger_ui.Size = new System.Drawing.Size(1075, 662);
            this.logger_ui.TabIndex = 9;
            this.logger_ui.WordWrap = false;
            // 
            // P1Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1075, 753);
            this.Controls.Add(this.logger_ui);
            this.Controls.Add(this.status1);
            this.Controls.Add(this.tool1);
            this.Controls.Add(this.menu1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menu1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "P1Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "P1-数据服务器程序";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.P1Form_FormClosed);
            this.Load += new System.EventHandler(this.P1Form_Load);
            this.menu1.ResumeLayout(false);
            this.menu1.PerformLayout();
            this.status1.ResumeLayout(false);
            this.status1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menu1;
        private System.Windows.Forms.ToolStrip tool1;
        private System.Windows.Forms.ToolStripMenuItem 系统SToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置CToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助HToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 人工识别干预MToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 启动TToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 停止PToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 退出XToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 网络参数NToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 文件参数FToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 使用说明MToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem 关于AToolStripMenuItem;
        private System.Windows.Forms.Timer _100ms_timer;
        private System.Windows.Forms.StatusStrip status1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel tsPaired;
        private System.Windows.Forms.ToolStripStatusLabel tsReport;
        private System.Windows.Forms.ToolStripStatusLabel tsP4Number;
        private System.Windows.Forms.ToolStripStatusLabel tsP2Number;
        private System.Windows.Forms.ToolStripStatusLabel tsDcm;
        private System.Windows.Forms.ToolStripStatusLabel tsP3Number;
        private System.Windows.Forms.ToolStripStatusLabel tsP5;
        private System.Windows.Forms.Timer _tcp_connect_timer;
        private System.Windows.Forms.TextBox logger_ui;
    }
}

