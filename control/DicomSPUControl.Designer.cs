namespace control
{
    partial class DicomSPUControl
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtCalledAE = new System.Windows.Forms.TextBox();
            this.txtCallingAE = new System.Windows.Forms.TextBox();
            this.txtRPort = new System.Windows.Forms.TextBox();
            this.txtRAddress = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtCalledAE
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtCalledAE, 4);
            this.txtCalledAE.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCalledAE.Location = new System.Drawing.Point(108, 103);
            this.txtCalledAE.Margin = new System.Windows.Forms.Padding(4);
            this.txtCalledAE.MaxLength = 50;
            this.txtCalledAE.Name = "txtCalledAE";
            this.txtCalledAE.Size = new System.Drawing.Size(414, 26);
            this.txtCalledAE.TabIndex = 5;
            // 
            // txtCallingAE
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtCallingAE, 4);
            this.txtCallingAE.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCallingAE.Location = new System.Drawing.Point(108, 70);
            this.txtCallingAE.Margin = new System.Windows.Forms.Padding(4);
            this.txtCallingAE.MaxLength = 50;
            this.txtCallingAE.Name = "txtCallingAE";
            this.txtCallingAE.Size = new System.Drawing.Size(414, 26);
            this.txtCallingAE.TabIndex = 4;
            // 
            // txtRPort
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtRPort, 4);
            this.txtRPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRPort.Location = new System.Drawing.Point(108, 37);
            this.txtRPort.Margin = new System.Windows.Forms.Padding(4);
            this.txtRPort.MaxLength = 6;
            this.txtRPort.Name = "txtRPort";
            this.txtRPort.Size = new System.Drawing.Size(414, 26);
            this.txtRPort.TabIndex = 3;
            // 
            // txtRAddress
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtRAddress, 4);
            this.txtRAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRAddress.Location = new System.Drawing.Point(108, 4);
            this.txtRAddress.Margin = new System.Windows.Forms.Padding(4);
            this.txtRAddress.MaxLength = 20;
            this.txtRAddress.Name = "txtRAddress";
            this.txtRAddress.Size = new System.Drawing.Size(414, 26);
            this.txtRAddress.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(4, 99);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 36);
            this.label4.TabIndex = 0;
            this.label4.Text = "被请求端AET";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(4, 66);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 33);
            this.label3.TabIndex = 0;
            this.label3.Text = "请求端AET";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(4, 33);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 33);
            this.label2.TabIndex = 0;
            this.label2.Text = "远程端口";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(4, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "远程地址";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 133F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 133F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtRAddress, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtRPort, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtCallingAE, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtCalledAE, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(526, 135);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // DicomSPUControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DicomSPUControl";
            this.Size = new System.Drawing.Size(526, 135);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtCalledAE;
        private System.Windows.Forms.TextBox txtCallingAE;
        private System.Windows.Forms.TextBox txtRPort;
        private System.Windows.Forms.TextBox txtRAddress;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;


    }
}
