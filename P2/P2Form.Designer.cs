using System;
namespace P2
{
    partial class P2Form
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.lblUserName = new System.Windows.Forms.Label();
            this.btnPrintBoth = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.lblUserID = new System.Windows.Forms.Label();
            this.btnPrintReport = new System.Windows.Forms.Button();
            this.lblDcmPrintFlag = new System.Windows.Forms.Label();
            this.btnPrintDcm = new System.Windows.Forms.Button();
            this.lblReportPrintFlag = new System.Windows.Forms.Label();
            this.lblReportNumber = new System.Windows.Forms.LinkLabel();
            this.lblDcmNumber = new System.Windows.Forms.LinkLabel();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnQuery = new System.Windows.Forms.Button();
            this.lblP5Status = new System.Windows.Forms.Label();
            this.lblP5Ip = new System.Windows.Forms.Label();
            this.lblP1Status = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblP1Ip = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this._100ms_timer = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlMain = new System.Windows.Forms.TableLayoutPanel();
            this.pnlQuery = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.txtMsg = new System.Windows.Forms.TextBox();
            this._tcp_connect_timer = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlQuery.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(2, 2);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 39);
            this.label1.TabIndex = 0;
            this.label1.Text = "用 户 号：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtUserID
            // 
            this.txtUserID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.pnlQuery.SetColumnSpan(this.txtUserID, 3);
            this.txtUserID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtUserID.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUserID.Location = new System.Drawing.Point(154, 5);
            this.txtUserID.MaxLength = 20;
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(441, 31);
            this.txtUserID.TabIndex = 0;
            this.txtUserID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUserID_KeyDown);
            // 
            // lblUserName
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.lblUserName, 3);
            this.lblUserName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUserName.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblUserName.ForeColor = System.Drawing.Color.Red;
            this.lblUserName.Location = new System.Drawing.Point(152, 3);
            this.lblUserName.Margin = new System.Windows.Forms.Padding(1);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(445, 38);
            this.lblUserName.TabIndex = 0;
            this.lblUserName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnPrintBoth
            // 
            this.pnlMain.SetColumnSpan(this.btnPrintBoth, 2);
            this.btnPrintBoth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPrintBoth.Enabled = false;
            this.btnPrintBoth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintBoth.Location = new System.Drawing.Point(153, 318);
            this.btnPrintBoth.Name = "btnPrintBoth";
            this.btnPrintBoth.Size = new System.Drawing.Size(294, 39);
            this.btnPrintBoth.TabIndex = 1;
            this.btnPrintBoth.Text = "打印胶片和报告";
            this.btnPrintBoth.UseVisualStyleBackColor = true;
            this.btnPrintBoth.Click += new System.EventHandler(this.btnPrintBoth_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(3, 3);
            this.label6.Margin = new System.Windows.Forms.Padding(1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(145, 38);
            this.label6.TabIndex = 0;
            this.label6.Text = "姓　　名：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblUserID
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.lblUserID, 3);
            this.lblUserID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUserID.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblUserID.ForeColor = System.Drawing.Color.Red;
            this.lblUserID.Location = new System.Drawing.Point(152, 45);
            this.lblUserID.Margin = new System.Windows.Forms.Padding(1);
            this.lblUserID.Name = "lblUserID";
            this.lblUserID.Size = new System.Drawing.Size(445, 38);
            this.lblUserID.TabIndex = 0;
            this.lblUserID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnPrintReport
            // 
            this.btnPrintReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPrintReport.Enabled = false;
            this.btnPrintReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintReport.Location = new System.Drawing.Point(452, 131);
            this.btnPrintReport.Name = "btnPrintReport";
            this.btnPrintReport.Size = new System.Drawing.Size(143, 36);
            this.btnPrintReport.TabIndex = 1;
            this.btnPrintReport.Text = "打印报告";
            this.btnPrintReport.UseVisualStyleBackColor = true;
            this.btnPrintReport.Click += new System.EventHandler(this.btnPrintReport_Click);
            // 
            // lblDcmPrintFlag
            // 
            this.lblDcmPrintFlag.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDcmPrintFlag.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDcmPrintFlag.ForeColor = System.Drawing.Color.Red;
            this.lblDcmPrintFlag.Location = new System.Drawing.Point(301, 87);
            this.lblDcmPrintFlag.Margin = new System.Windows.Forms.Padding(1);
            this.lblDcmPrintFlag.Name = "lblDcmPrintFlag";
            this.lblDcmPrintFlag.Size = new System.Drawing.Size(145, 38);
            this.lblDcmPrintFlag.TabIndex = 0;
            this.lblDcmPrintFlag.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnPrintDcm
            // 
            this.btnPrintDcm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPrintDcm.Enabled = false;
            this.btnPrintDcm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintDcm.Location = new System.Drawing.Point(452, 89);
            this.btnPrintDcm.Name = "btnPrintDcm";
            this.btnPrintDcm.Size = new System.Drawing.Size(143, 34);
            this.btnPrintDcm.TabIndex = 0;
            this.btnPrintDcm.Text = "打印胶片";
            this.btnPrintDcm.UseVisualStyleBackColor = true;
            this.btnPrintDcm.Click += new System.EventHandler(this.btnPrintDcm_Click);
            // 
            // lblReportPrintFlag
            // 
            this.lblReportPrintFlag.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblReportPrintFlag.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblReportPrintFlag.ForeColor = System.Drawing.Color.Red;
            this.lblReportPrintFlag.Location = new System.Drawing.Point(301, 129);
            this.lblReportPrintFlag.Margin = new System.Windows.Forms.Padding(1);
            this.lblReportPrintFlag.Name = "lblReportPrintFlag";
            this.lblReportPrintFlag.Size = new System.Drawing.Size(145, 40);
            this.lblReportPrintFlag.TabIndex = 0;
            this.lblReportPrintFlag.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblReportNumber
            // 
            this.lblReportNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblReportNumber.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblReportNumber.ForeColor = System.Drawing.Color.Red;
            this.lblReportNumber.Location = new System.Drawing.Point(152, 129);
            this.lblReportNumber.Margin = new System.Windows.Forms.Padding(1);
            this.lblReportNumber.Name = "lblReportNumber";
            this.lblReportNumber.Size = new System.Drawing.Size(145, 40);
            this.lblReportNumber.TabIndex = 2;
            this.lblReportNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDcmNumber
            // 
            this.lblDcmNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDcmNumber.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDcmNumber.ForeColor = System.Drawing.Color.Red;
            this.lblDcmNumber.Location = new System.Drawing.Point(152, 87);
            this.lblDcmNumber.Margin = new System.Windows.Forms.Padding(1);
            this.lblDcmNumber.Name = "lblDcmNumber";
            this.lblDcmNumber.Size = new System.Drawing.Size(145, 38);
            this.lblDcmNumber.TabIndex = 2;
            this.lblDcmNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Location = new System.Drawing.Point(3, 45);
            this.label11.Margin = new System.Windows.Forms.Padding(1);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(145, 38);
            this.label11.TabIndex = 0;
            this.label11.Text = "用 户 号：";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Location = new System.Drawing.Point(3, 87);
            this.label12.Margin = new System.Windows.Forms.Padding(1);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(145, 38);
            this.label12.TabIndex = 0;
            this.label12.Text = "胶片数量：";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(3, 129);
            this.label5.Margin = new System.Windows.Forms.Padding(1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(145, 40);
            this.label5.TabIndex = 0;
            this.label5.Text = "报告数量：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnQuery
            // 
            this.btnQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuery.Location = new System.Drawing.Point(453, 46);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(143, 33);
            this.btnQuery.TabIndex = 3;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Visible = false;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // lblP5Status
            // 
            this.lblP5Status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblP5Status.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblP5Status.Location = new System.Drawing.Point(212, 0);
            this.lblP5Status.Name = "lblP5Status";
            this.lblP5Status.Size = new System.Drawing.Size(59, 40);
            this.lblP5Status.TabIndex = 11;
            this.lblP5Status.Text = "未连接";
            this.lblP5Status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblP5Ip
            // 
            this.lblP5Ip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblP5Ip.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblP5Ip.Location = new System.Drawing.Point(45, 0);
            this.lblP5Ip.Name = "lblP5Ip";
            this.lblP5Ip.Size = new System.Drawing.Size(161, 40);
            this.lblP5Ip.TabIndex = 10;
            this.lblP5Ip.Text = "127.127.127.127:55555";
            this.lblP5Ip.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblP1Status
            // 
            this.lblP1Status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblP1Status.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblP1Status.Location = new System.Drawing.Point(213, 0);
            this.lblP1Status.Name = "lblP1Status";
            this.lblP1Status.Size = new System.Drawing.Size(59, 40);
            this.lblP1Status.TabIndex = 9;
            this.lblP1Status.Text = "未连接";
            this.lblP1Status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 40);
            this.label2.TabIndex = 7;
            this.label2.Text = "P5:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblP1Ip
            // 
            this.lblP1Ip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblP1Ip.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblP1Ip.Location = new System.Drawing.Point(46, 0);
            this.lblP1Ip.Name = "lblP1Ip";
            this.lblP1Ip.Size = new System.Drawing.Size(161, 40);
            this.lblP1Ip.TabIndex = 8;
            this.lblP1Ip.Text = "192.168.200.200:12200";
            this.lblP1Ip.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 40);
            this.label3.TabIndex = 6;
            this.label3.Text = "P1:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _100ms_timer
            // 
            this._100ms_timer.Enabled = true;
            this._100ms_timer.Tick += new System.EventHandler(this._100ms_timer_Tick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Outset;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.pnlMain.SetColumnSpan(this.tableLayoutPanel1, 4);
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.lblUserName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label11, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label12, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblUserID, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblDcmNumber, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnPrintReport, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblDcmPrintFlag, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnPrintDcm, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblReportPrintFlag, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblReportNumber, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 143);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(600, 172);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // pnlMain
            // 
            this.pnlMain.ColumnCount = 4;
            this.pnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlMain.Controls.Add(this.tableLayoutPanel1, 0, 2);
            this.pnlMain.Controls.Add(this.btnPrintBoth, 1, 3);
            this.pnlMain.Controls.Add(this.pnlQuery, 0, 0);
            this.pnlMain.Controls.Add(this.btnQuery, 3, 1);
            this.pnlMain.Location = new System.Drawing.Point(113, 12);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.RowCount = 4;
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28F));
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48F));
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.pnlMain.Size = new System.Drawing.Size(600, 360);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlQuery
            // 
            this.pnlQuery.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Outset;
            this.pnlQuery.ColumnCount = 4;
            this.pnlMain.SetColumnSpan(this.pnlQuery, 4);
            this.pnlQuery.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlQuery.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlQuery.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlQuery.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlQuery.Controls.Add(this.txtUserID, 1, 0);
            this.pnlQuery.Controls.Add(this.label1, 0, 0);
            this.pnlQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlQuery.Location = new System.Drawing.Point(0, 0);
            this.pnlQuery.Margin = new System.Windows.Forms.Padding(0);
            this.pnlQuery.Name = "pnlQuery";
            this.pnlQuery.RowCount = 1;
            this.pnlQuery.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlQuery.Size = new System.Drawing.Size(600, 43);
            this.pnlQuery.TabIndex = 0;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel2.Controls.Add(this.label3);
            this.flowLayoutPanel2.Controls.Add(this.lblP1Ip);
            this.flowLayoutPanel2.Controls.Add(this.lblP1Status);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(158, 548);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(312, 40);
            this.flowLayoutPanel2.TabIndex = 1;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel3.Controls.Add(this.label2);
            this.flowLayoutPanel3.Controls.Add(this.lblP5Ip);
            this.flowLayoutPanel3.Controls.Add(this.lblP5Status);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(476, 548);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(312, 40);
            this.flowLayoutPanel3.TabIndex = 2;
            // 
            // txtMsg
            // 
            this.txtMsg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.txtMsg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMsg.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtMsg.Location = new System.Drawing.Point(113, 378);
            this.txtMsg.Multiline = true;
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.ReadOnly = true;
            this.txtMsg.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMsg.Size = new System.Drawing.Size(600, 160);
            this.txtMsg.TabIndex = 5;
            this.txtMsg.Text = "欢迎使用查询/打印客户端程序！\r\n\r\n请输入\"用户号\"进行查询.";
            // 
            // _tcp_connect_timer
            // 
            this._tcp_connect_timer.Enabled = true;
            this._tcp_connect_timer.Interval = 15000;
            this._tcp_connect_timer.Tick += new System.EventHandler(this.tcp_connect_timer_Tick);
            // 
            // P2Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.flowLayoutPanel3);
            this.Controls.Add(this.txtMsg);
            this.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "P2Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "";
            this.Text = "P2-查询/打印客户端PC程序";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.P2Form_FormClosed);
            this.Load += new System.EventHandler(this.P2Form_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.P2Form_KeyPress);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlQuery.ResumeLayout(false);
            this.pnlQuery.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Button btnPrintBoth;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblUserID;
        private System.Windows.Forms.Button btnPrintReport;
        private System.Windows.Forms.Label lblDcmPrintFlag;
        private System.Windows.Forms.Button btnPrintDcm;
        private System.Windows.Forms.Label lblReportPrintFlag;
        private System.Windows.Forms.LinkLabel lblReportNumber;
        private System.Windows.Forms.LinkLabel lblDcmNumber;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Label lblP5Status;
        private System.Windows.Forms.Label lblP5Ip;
        private System.Windows.Forms.Label lblP1Status;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblP1Ip;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer _100ms_timer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel pnlMain;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel pnlQuery;
        private System.Windows.Forms.TextBox txtMsg;
        private System.Windows.Forms.Timer _tcp_connect_timer;

    }
}

