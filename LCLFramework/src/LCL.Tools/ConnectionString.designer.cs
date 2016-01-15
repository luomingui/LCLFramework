namespace LCL.Tools
{
    partial class ConnectionString
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectionString));
            this.comboBoxServer = new System.Windows.Forms.ComboBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.txtAuthor = new System.Windows.Forms.TextBox();
            this.comboBoxDBType = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btn_DbOK = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtEntityNameSpace = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtTargetFolder = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxServer
            // 
            this.comboBoxServer.FormattingEnabled = true;
            this.comboBoxServer.Items.AddRange(new object[] {
            "LUOMG\\MSSQL2000",
            "LUOMG-PC"});
            this.comboBoxServer.Location = new System.Drawing.Point(130, 57);
            this.comboBoxServer.Name = "comboBoxServer";
            this.comboBoxServer.Size = new System.Drawing.Size(288, 20);
            this.comboBoxServer.TabIndex = 94;
            this.comboBoxServer.Text = ".";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(130, 82);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(287, 21);
            this.txtUser.TabIndex = 92;
            this.txtUser.Text = "sa";
            this.txtUser.Validating += new System.ComponentModel.CancelEventHandler(this.txtUser_Validating);
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(129, 109);
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(288, 21);
            this.txtPass.TabIndex = 91;
            this.txtPass.Text = "123456";
            // 
            // txtAuthor
            // 
            this.txtAuthor.Location = new System.Drawing.Point(119, 93);
            this.txtAuthor.Name = "txtAuthor";
            this.txtAuthor.Size = new System.Drawing.Size(293, 21);
            this.txtAuthor.TabIndex = 138;
            this.txtAuthor.Text = "罗敏贵";
            // 
            // comboBoxDBType
            // 
            this.comboBoxDBType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDBType.FormattingEnabled = true;
            this.comboBoxDBType.Items.AddRange(new object[] {
            "Sql2000",
            "Sql2005",
            "Sql2008",
            "Oracle",
            "MySql",
            "MongoDB",
            "SQLite"});
            this.comboBoxDBType.Location = new System.Drawing.Point(131, 30);
            this.comboBoxDBType.Name = "comboBoxDBType";
            this.comboBoxDBType.Size = new System.Drawing.Size(288, 20);
            this.comboBoxDBType.TabIndex = 96;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.comboBoxDBType);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.comboBoxServer);
            this.groupBox4.Controls.Add(this.txtUser);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.txtPass);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Location = new System.Drawing.Point(12, 11);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(466, 155);
            this.groupBox4.TabIndex = 115;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "1：设置数据库";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 12);
            this.label1.TabIndex = 95;
            this.label1.Text = "数据库类型(&T)：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 89;
            this.label3.Text = "登录名(&L)：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 62);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 12);
            this.label7.TabIndex = 87;
            this.label7.Text = "服务器名称(&S)：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(48, 115);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 12);
            this.label8.TabIndex = 90;
            this.label8.Text = "密码(&P)：";
            // 
            // btn_DbOK
            // 
            this.btn_DbOK.BackColor = System.Drawing.Color.Transparent;
            this.btn_DbOK.Location = new System.Drawing.Point(308, 327);
            this.btn_DbOK.Name = "btn_DbOK";
            this.btn_DbOK.Size = new System.Drawing.Size(124, 44);
            this.btn_DbOK.TabIndex = 116;
            this.btn_DbOK.Text = "确定";
            this.btn_DbOK.UseVisualStyleBackColor = false;
            this.btn_DbOK.Click += new System.EventHandler(this.btn_DbOK_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtEntityNameSpace);
            this.groupBox2.Controls.Add(this.txtAuthor);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtTargetFolder);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Location = new System.Drawing.Point(12, 172);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(466, 135);
            this.groupBox2.TabIndex = 118;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "2：参数设置";
            // 
            // txtEntityNameSpace
            // 
            this.txtEntityNameSpace.FormattingEnabled = true;
            this.txtEntityNameSpace.Items.AddRange(new object[] {
            "UIShell.PluginService",
            "UIShell.HeatMeteringService",
            "UIShell.HeatMeteringPlugin",
            "UIShell.RbacPermissionService"});
            this.txtEntityNameSpace.Location = new System.Drawing.Point(119, 15);
            this.txtEntityNameSpace.Name = "txtEntityNameSpace";
            this.txtEntityNameSpace.Size = new System.Drawing.Size(293, 20);
            this.txtEntityNameSpace.TabIndex = 119;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(39, 96);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 137;
            this.label10.Text = "作者：";
            // 
            // txtTargetFolder
            // 
            this.txtTargetFolder.Location = new System.Drawing.Point(119, 48);
            this.txtTargetFolder.Multiline = true;
            this.txtTargetFolder.Name = "txtTargetFolder";
            this.txtTargetFolder.Size = new System.Drawing.Size(295, 36);
            this.txtTargetFolder.TabIndex = 136;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(35, 51);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 135;
            this.label11.Text = "截取位：";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 23);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(83, 12);
            this.label13.TabIndex = 131;
            this.label13.Text = "命名空间(&S)：";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ConnectionString
            // 
            this.AcceptButton = this.btn_DbOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(493, 391);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btn_DbOK);
            this.Controls.Add(this.groupBox4);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConnectionString";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ConnectionString";
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        public System.Windows.Forms.ComboBox comboBoxServer;
        public System.Windows.Forms.TextBox txtUser;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox txtPass;
        public System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btn_DbOK;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtAuthor;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtTargetFolder;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.Label label13;
        public System.Windows.Forms.ComboBox comboBoxDBType;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox txtEntityNameSpace;
    }
}