namespace LCL.Tools.WinFrm
{
    partial class FrmDbBuild
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent( )
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDbBuild));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_OutPath = new System.Windows.Forms.Button();
            this.txtOutPut = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbDBlist = new System.Windows.Forms.ComboBox();
            this.lblServer = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.but_BuildUI = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.labelNum = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btn_OutPath);
            this.groupBox1.Controls.Add(this.txtOutPut);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(494, 63);
            this.groupBox1.TabIndex = 78;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "3：设置项目输出路径";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "输出路径：";
            // 
            // btn_OutPath
            // 
            this.btn_OutPath.Location = new System.Drawing.Point(435, 22);
            this.btn_OutPath.Name = "btn_OutPath";
            this.btn_OutPath.Size = new System.Drawing.Size(45, 25);
            this.btn_OutPath.TabIndex = 2;
            this.btn_OutPath.Text = "...";
            this.btn_OutPath.UseVisualStyleBackColor = true;
            this.btn_OutPath.Click += new System.EventHandler(this.btn_OutPath_Click);
            // 
            // txtOutPut
            // 
            this.txtOutPut.Location = new System.Drawing.Point(83, 25);
            this.txtOutPut.Name = "txtOutPut";
            this.txtOutPut.Size = new System.Drawing.Size(346, 20);
            this.txtOutPut.TabIndex = 1;
            this.txtOutPut.TextChanged += new System.EventHandler(this.txtOutPut_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbDBlist);
            this.groupBox2.Controls.Add(this.lblServer);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 63);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(494, 69);
            this.groupBox2.TabIndex = 80;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "选择数据库";
            // 
            // cmbDBlist
            // 
            this.cmbDBlist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDBlist.Location = new System.Drawing.Point(296, 26);
            this.cmbDBlist.Name = "cmbDBlist";
            this.cmbDBlist.Size = new System.Drawing.Size(184, 21);
            this.cmbDBlist.TabIndex = 2;
            this.cmbDBlist.SelectedIndexChanged += new System.EventHandler(this.cmbDBlist_SelectedIndexChanged);
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Location = new System.Drawing.Point(104, 28);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(35, 13);
            this.lblServer.TabIndex = 1;
            this.lblServer.Text = "label2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "当前服务器：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(216, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "选择数据库：";
            // 
            // but_BuildUI
            // 
            this.but_BuildUI.Location = new System.Drawing.Point(186, 222);
            this.but_BuildUI.Name = "but_BuildUI";
            this.but_BuildUI.Size = new System.Drawing.Size(140, 61);
            this.but_BuildUI.TabIndex = 82;
            this.but_BuildUI.Text = "生成";
            this.but_BuildUI.UseVisualStyleBackColor = true;
            this.but_BuildUI.Click += new System.EventHandler(this.but_BuildUI_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Items.AddRange(new object[] {
            "WinFromBuild",
            "WebBuild",
            "WPFBuild",
            "WCFBuild",
            "EntityFrameworkBuild"});
            this.comboBox1.Location = new System.Drawing.Point(159, 138);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(177, 21);
            this.comboBox1.TabIndex = 85;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(74, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 86;
            this.label4.Text = "选择模板：";
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(10, 169);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(419, 11);
            this.progressBar2.TabIndex = 88;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(10, 184);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(419, 22);
            this.progressBar1.TabIndex = 87;
            // 
            // labelNum
            // 
            this.labelNum.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelNum.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.labelNum.ForeColor = System.Drawing.Color.DeepPink;
            this.labelNum.Location = new System.Drawing.Point(435, 169);
            this.labelNum.Name = "labelNum";
            this.labelNum.Size = new System.Drawing.Size(47, 37);
            this.labelNum.TabIndex = 89;
            this.labelNum.Text = "就绪";
            this.labelNum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmDbBuild
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(494, 295);
            this.Controls.Add(this.progressBar2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.labelNum);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.but_BuildUI);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDbBuild";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "批量生成代码.......";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_OutPath;
        private System.Windows.Forms.TextBox txtOutPut;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbDBlist;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button but_BuildUI;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ProgressBar progressBar2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label labelNum;
    }
}