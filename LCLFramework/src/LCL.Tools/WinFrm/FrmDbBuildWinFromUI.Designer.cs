namespace SF.Tools.WinFrm
{
    partial class FrmDbBuildWinFromUI
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.but_Build = new System.Windows.Forms.Button();
            this.cmbTree_Value = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbTree_Name = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmb_Tables = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chk_CheckFepeat = new System.Windows.Forms.CheckedListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chk_CheckFields = new System.Windows.Forms.CheckedListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chk_EditShow = new System.Windows.Forms.CheckedListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chk_ShowFields = new System.Windows.Forms.CheckedListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chk_SelectWhere = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.richTextBox3 = new System.Windows.Forms.RichTextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(854, 611);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(846, 585);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "界面";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBox1);
            this.groupBox3.Controls.Add(this.but_Build);
            this.groupBox3.Controls.Add(this.cmbTree_Value);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.cmbTree_Name);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.cmb_Tables);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Location = new System.Drawing.Point(513, 16);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(263, 258);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "左边树形设置";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(13, 223);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(72, 16);
            this.checkBox1.TabIndex = 10;
            this.checkBox1.Text = "左边树形";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // but_Build
            // 
            this.but_Build.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.but_Build.Location = new System.Drawing.Point(59, 139);
            this.but_Build.Name = "but_Build";
            this.but_Build.Size = new System.Drawing.Size(125, 45);
            this.but_Build.TabIndex = 9;
            this.but_Build.Text = "生成文件";
            this.but_Build.UseVisualStyleBackColor = true;
            this.but_Build.Click += new System.EventHandler(this.but_Build_Click);
            // 
            // cmbTree_Value
            // 
            this.cmbTree_Value.FormattingEnabled = true;
            this.cmbTree_Value.Location = new System.Drawing.Point(74, 91);
            this.cmbTree_Value.Name = "cmbTree_Value";
            this.cmbTree_Value.Size = new System.Drawing.Size(176, 20);
            this.cmbTree_Value.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 96);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 7;
            this.label9.Text = "关联字段";
            // 
            // cmbTree_Name
            // 
            this.cmbTree_Name.FormattingEnabled = true;
            this.cmbTree_Name.Location = new System.Drawing.Point(74, 55);
            this.cmbTree_Name.Name = "cmbTree_Name";
            this.cmbTree_Name.Size = new System.Drawing.Size(176, 20);
            this.cmbTree_Name.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 3;
            this.label7.Text = "树形NAME";
            // 
            // cmb_Tables
            // 
            this.cmb_Tables.FormattingEnabled = true;
            this.cmb_Tables.Location = new System.Drawing.Point(73, 18);
            this.cmb_Tables.Name = "cmb_Tables";
            this.cmb_Tables.Size = new System.Drawing.Size(176, 20);
            this.cmb_Tables.TabIndex = 1;
            this.cmb_Tables.SelectedIndexChanged += new System.EventHandler(this.cmb_Tables_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "表列表";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chk_CheckFepeat);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.chk_CheckFields);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.chk_EditShow);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(8, 279);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(768, 258);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "查询编辑类设置";
            // 
            // chk_CheckFepeat
            // 
            this.chk_CheckFepeat.FormattingEnabled = true;
            this.chk_CheckFepeat.HorizontalExtent = 1;
            this.chk_CheckFepeat.HorizontalScrollbar = true;
            this.chk_CheckFepeat.Location = new System.Drawing.Point(486, 42);
            this.chk_CheckFepeat.Name = "chk_CheckFepeat";
            this.chk_CheckFepeat.Size = new System.Drawing.Size(204, 196);
            this.chk_CheckFepeat.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(483, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "不添加重复的字段";
            // 
            // chk_CheckFields
            // 
            this.chk_CheckFields.FormattingEnabled = true;
            this.chk_CheckFields.HorizontalExtent = 1;
            this.chk_CheckFields.HorizontalScrollbar = true;
            this.chk_CheckFields.Location = new System.Drawing.Point(254, 42);
            this.chk_CheckFields.Name = "chk_CheckFields";
            this.chk_CheckFields.Size = new System.Drawing.Size(204, 196);
            this.chk_CheckFields.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(251, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "检查输入必填字段";
            // 
            // chk_EditShow
            // 
            this.chk_EditShow.FormattingEnabled = true;
            this.chk_EditShow.HorizontalExtent = 1;
            this.chk_EditShow.HorizontalScrollbar = true;
            this.chk_EditShow.Location = new System.Drawing.Point(26, 42);
            this.chk_EditShow.Name = "chk_EditShow";
            this.chk_EditShow.Size = new System.Drawing.Size(204, 196);
            this.chk_EditShow.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "数据显示字段";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chk_ShowFields);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.chk_SelectWhere);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(485, 258);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询类设置";
            // 
            // chk_ShowFields
            // 
            this.chk_ShowFields.FormattingEnabled = true;
            this.chk_ShowFields.HorizontalExtent = 1;
            this.chk_ShowFields.HorizontalScrollbar = true;
            this.chk_ShowFields.Location = new System.Drawing.Point(254, 44);
            this.chk_ShowFields.Name = "chk_ShowFields";
            this.chk_ShowFields.Size = new System.Drawing.Size(204, 196);
            this.chk_ShowFields.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(251, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "列表显示字段";
            // 
            // chk_SelectWhere
            // 
            this.chk_SelectWhere.FormattingEnabled = true;
            this.chk_SelectWhere.HorizontalExtent = 1;
            this.chk_SelectWhere.HorizontalScrollbar = true;
            this.chk_SelectWhere.Location = new System.Drawing.Point(26, 42);
            this.chk_SelectWhere.Name = "chk_SelectWhere";
            this.chk_SelectWhere.Size = new System.Drawing.Size(204, 196);
            this.chk_SelectWhere.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "查询条件字段";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.richTextBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(846, 585);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Guid列生成";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(3, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(840, 579);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.richTextBox2);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(846, 585);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "entityFrm->AddColumnAlias";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // richTextBox2
            // 
            this.richTextBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox2.Location = new System.Drawing.Point(3, 3);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(840, 579);
            this.richTextBox2.TabIndex = 1;
            this.richTextBox2.Text = "";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.richTextBox3);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(846, 585);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Entity";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // richTextBox3
            // 
            this.richTextBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox3.Location = new System.Drawing.Point(3, 3);
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.Size = new System.Drawing.Size(840, 579);
            this.richTextBox3.TabIndex = 2;
            this.richTextBox3.Text = "";
            // 
            // FrmDbBuildWinFromUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(854, 611);
            this.Controls.Add(this.tabControl1);
            this.Name = "FrmDbBuildWinFromUI";
            this.Text = "数据表属性设置";
            this.Load += new System.EventHandler(this.FrmDbBuildWinFromUI_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckedListBox chk_CheckFepeat;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckedListBox chk_CheckFields;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckedListBox chk_EditShow;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox chk_ShowFields;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckedListBox chk_SelectWhere;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ComboBox cmb_Tables;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbTree_Name;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbTree_Value;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button but_Build;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.RichTextBox richTextBox3;


    }
}