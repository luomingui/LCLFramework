namespace LCL.LicensseManage
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.txt_Name = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txt_VersionUpgrade = new System.Windows.Forms.CheckBox();
            this.txt_PastDate = new System.Windows.Forms.DateTimePicker();
            this.txt_InstallNumber = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_UseNumber = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_AssemblyName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.but_Save = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "许可证名称：";
            // 
            // txt_Name
            // 
            this.txt_Name.Location = new System.Drawing.Point(118, 48);
            this.txt_Name.Name = "txt_Name";
            this.txt_Name.Size = new System.Drawing.Size(322, 21);
            this.txt_Name.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.txt_VersionUpgrade);
            this.groupBox1.Controls.Add(this.txt_PastDate);
            this.groupBox1.Controls.Add(this.txt_InstallNumber);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txt_UseNumber);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txt_AssemblyName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txt_Name);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(669, 231);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "创建许可证文件";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(602, 100);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(32, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txt_VersionUpgrade
            // 
            this.txt_VersionUpgrade.AutoSize = true;
            this.txt_VersionUpgrade.Location = new System.Drawing.Point(228, 167);
            this.txt_VersionUpgrade.Name = "txt_VersionUpgrade";
            this.txt_VersionUpgrade.Size = new System.Drawing.Size(72, 16);
            this.txt_VersionUpgrade.TabIndex = 12;
            this.txt_VersionUpgrade.Text = "版本升级";
            this.txt_VersionUpgrade.UseVisualStyleBackColor = true;
            // 
            // txt_PastDate
            // 
            this.txt_PastDate.CustomFormat = "yyyy-MM-dd";
            this.txt_PastDate.Location = new System.Drawing.Point(119, 164);
            this.txt_PastDate.Name = "txt_PastDate";
            this.txt_PastDate.Size = new System.Drawing.Size(94, 21);
            this.txt_PastDate.TabIndex = 11;
            this.txt_PastDate.Value = new System.DateTime(2014, 12, 27, 0, 0, 0, 0);
            // 
            // txt_InstallNumber
            // 
            this.txt_InstallNumber.Location = new System.Drawing.Point(540, 161);
            this.txt_InstallNumber.Name = "txt_InstallNumber";
            this.txt_InstallNumber.Size = new System.Drawing.Size(82, 21);
            this.txt_InstallNumber.TabIndex = 10;
            this.txt_InstallNumber.Text = "1000";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(466, 168);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "安装次数：";
            // 
            // txt_UseNumber
            // 
            this.txt_UseNumber.Location = new System.Drawing.Point(377, 162);
            this.txt_UseNumber.Name = "txt_UseNumber";
            this.txt_UseNumber.Size = new System.Drawing.Size(81, 21);
            this.txt_UseNumber.TabIndex = 8;
            this.txt_UseNumber.Text = "10000";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(306, 168);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "使用次数：";
            // 
            // txt_AssemblyName
            // 
            this.txt_AssemblyName.Location = new System.Drawing.Point(119, 102);
            this.txt_AssemblyName.Name = "txt_AssemblyName";
            this.txt_AssemblyName.Size = new System.Drawing.Size(470, 21);
            this.txt_AssemblyName.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 170);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "过期时间：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "程序集名称：";
            // 
            // but_Save
            // 
            this.but_Save.Location = new System.Drawing.Point(480, 274);
            this.but_Save.Name = "but_Save";
            this.but_Save.Size = new System.Drawing.Size(154, 51);
            this.but_Save.TabIndex = 3;
            this.but_Save.Text = " 创  建 ";
            this.but_Save.UseVisualStyleBackColor = true;
            this.but_Save.Click += new System.EventHandler(this.but_Save_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 356);
            this.Controls.Add(this.but_Save);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "软件许可证管理";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_Name;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_AssemblyName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_UseNumber;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_InstallNumber;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button but_Save;
        private System.Windows.Forms.DateTimePicker txt_PastDate;
        private System.Windows.Forms.CheckBox txt_VersionUpgrade;
        private System.Windows.Forms.Button button1;
    }
}

