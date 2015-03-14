using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SF.Tools
{
    public class WinFromBaseBuild : BuildBase
    {
        public override void BuildMyMenusClass(string path, List<TableModel> tableNames)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("using MinGuiLuo; ");
            builder.AppendLine("using MinGuiLuo.MetaModel; ");
            builder.AppendLine("using MinGuiLuo.PluginSystem; ");
            builder.AppendLine("using MinGuiLuo.ORM; ");
            builder.AppendLine("namespace " + Utils.NameSpace);
            builder.AppendLine("{ ");
            builder.AppendLine("    public class MyMenus : ModulePlugin ");
            builder.AppendLine("    { ");
            builder.AppendLine("        public override void Initialize(IClientApp app) ");
            builder.AppendLine("        { ");
            builder.AppendLine("            app.ModuleOperations += (o, e) => ");
            builder.AppendLine("            { ");
            builder.AppendLine("                var moduleBookImport = CommonModel.Modules.AddRoot(new ModuleMeta ");
            builder.AppendLine("                { ");
            builder.AppendLine("                    Label = \"" + Utils.dbName + "\", ");
            builder.AppendLine("                    Children = ");
            builder.AppendLine("                    { ");

            for (int i = 0; i < tableNames.Count; i++)
            {
                TableModel tm = tableNames[i];
                string tablename = tm.TableName;
                string tableInfo = tm.TableNameRemark;
                builder.AppendLine("                        new ModuleMeta{ Label = \"" + tableInfo + "\",WinFromTemplateType=typeof(" + tablename + "Frm)}, ");
            }

            builder.AppendLine("                    } ");
            builder.AppendLine("                }); ");
            builder.AppendLine("            }; ");

            builder.AppendLine("            EFDbContextUtils.ContextListAdd(new Db" + Utils.dbName + "Context()); ");
            builder.AppendLine("            DatabaseInitializer.Initialize(); ");

            builder.AppendLine("        } ");
            builder.AppendLine("    } ");
            builder.AppendLine("} ");


            if (path.Length > 0)
            {
                string folder = path + @"\WinFromBuild\";
                Utils.FolderCheck(folder);
                string filename = folder + @"\MyMenus.cs";
                Utils.CreateFiles(filename, builder.ToString());
            }

        }
        public string GetGridColumn(string className, DataTable chklistCols)
        {
            StringBuilder list = new StringBuilder();
            string famt = @" 
            // 
            // Col{2}
            // 
            System.Windows.Forms.DataGridViewTextBoxColumn Col{2} = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Col{2}.DataPropertyName = ""{0}"";
            Col{2}.HeaderText = ""{1}"";
            Col{2}.Name = ""Col{2}"";
            Col{2}.ReadOnly = true;
            Col{2}.Visible = {3};
            Col{2}.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;";
            List<string> ls = new List<string>();
            foreach (DataRow row in chklistCols.Rows)
            {
                string proStr = datatype.IniReadValue("DbToCS", row["类型"].ToString());
                string property = Utils.GetCapFirstName(row["字段名"].ToString()).Replace("_", "");
                string propertyinfo = Utils.ClearLine(row["字段说明"].ToString().Trim());
                if (propertyinfo.Length == 0) propertyinfo = property;

                if (row["主键"].ToString().Length > 0)
                {
                    list.AppendLine(string.Format(famt, property, propertyinfo, property, "false"));
                }
                else
                {
                    if (property.ToLower().Equals("id") || property.ToLower().Equals("isdeleted"))
                    {
                        list.AppendLine(string.Format(famt, property, propertyinfo, property, "false"));
                    }
                    else
                    {
                        list.AppendLine(string.Format(famt, property, propertyinfo, property, "true"));
                    }
                }
                ls.Add("Col" + property);
            }
            //*,*,*,*,*,*,*,*,*
            string s = string.Join(",", ls.ToArray());
            list.AppendLine();
            list.AppendLine(@"this.entityFrm1.DataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {" + s + "});");
            return list.ToString();
        }
        public string GetGridColumn(string className, List<TableColumn> chklistCols)
        {
            StringBuilder list = new StringBuilder();
            string famt = @" 
            // 
            // Col{2}
            // 
            System.Windows.Forms.DataGridViewTextBoxColumn Col{2} = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Col{2}.DataPropertyName = ""{0}"";
            Col{2}.HeaderText = ""{1}"";
            Col{2}.Name = ""Col{2}"";
            Col{2}.ReadOnly = true;
            Col{2}.Visible = {3};
            Col{2}.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;";
            List<string> ls = new List<string>();
            foreach (TableColumn item in chklistCols)
            {
                string proStr = item.ColumnType;
                string property = item.ColumnName;
                string propertyinfo = item.ColumnRemark;
                if (propertyinfo.Length == 0) propertyinfo = property;
                if (property.ToLower().Equals("id") || property.ToLower().Equals("isdeleted"))
                {
                    list.AppendLine(string.Format(famt, property, propertyinfo, property, "false"));
                }
                else
                {
                    list.AppendLine(string.Format(famt, property, propertyinfo, property, "true"));
                }
                ls.Add("Col" + property);
            }
            //*,*,*,*,*,*,*,*,*
            string s = string.Join(",", ls.ToArray());
            list.AppendLine();
            list.AppendLine(@"this.entityFrm1.DataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {" + s + "});");
            return list.ToString();
        }
    }
    public class WinFromComplexBuild : WinFromBaseBuild, IBuild
    {
        #region Library
        public string Library(string path, TableModel tableModel, System.Windows.Forms.ProgressBar progressBar)
        {
            //Entity
            BuildEntity(path, tableModel, progressBar);
            //列表页面
            WinFrmListPage(path, tableModel, tableModel.Columns, tableModel.Columns, null);
            //Edit页面
            WinFrmEditPage(path, tableModel, tableModel.Columns, tableModel.Columns, tableModel.Columns);

            return "";
        }
        public string Library(string path, TableModel tableModel, List<TableColumn> SelectWhere, List<TableColumn> ShowFields, TableModel LeftTree, List<TableColumn> CheckFields, List<TableColumn> CheckFepeat)
        {
            //Entity
            BuildEntity(path, tableModel, null);
            //列表页面
            WinFrmListPage(path, tableModel, SelectWhere, ShowFields, LeftTree);
            //Edit页面
            WinFrmEditPage(path, tableModel, ShowFields, CheckFields, CheckFields);
            return "";
        }
        #endregion

        //TODO:查询
        private void WinFrmListPage(string path, TableModel tableModel, List<TableColumn> SelectWhere, List<TableColumn> ShowFields, TableModel LeftTree)
        {
            #region {tablename}Frm.cs
            string className = tableModel.TableName;
            string tableinfo = tableModel.TableNameRemark;
            StringBuilder AddColumnAlias = new StringBuilder();
            foreach (TableColumn item in ShowFields)
            {
                AddColumnAlias.AppendLine("//this.entityFrm1.AddColumnAlias(\"" + item.ColumnName + "\", \"" + item.ColumnRemark + "\", false);");
            }
            // 查询

            StringBuilder builder = new StringBuilder();
            builder.AppendLine(" ");
            builder.AppendLine("using System; ");
            builder.AppendLine("using System.Windows.Forms; ");
            builder.AppendLine("using MinGuiLuo.Logging; ");
            builder.AppendLine("using MinGuiLuo.ORM; ");
            builder.AppendLine("using SF.Threading; ");
            builder.AppendLine("using MinGuiLuo.WinFromControls.Forms; ");
            builder.AppendLine("using MinGuiLuo.WinFromControls.Controls; ");
            builder.AppendLine("using MinGuiLuo; ");
            builder.AppendLine("namespace  " + Utils.NameSpaceUI);
            builder.AppendLine("{ ");
            builder.AppendLine("    public partial class " + className + "Frm : DockFrm ");
            builder.AppendLine("    { ");
            builder.AppendLine("        public " + className + "Frm( ) ");
            builder.AppendLine("        { ");
            builder.AppendLine("            InitializeComponent(); ");
            builder.AppendLine("        } ");
            builder.AppendLine("        public void BindData( ) ");
            builder.AppendLine("        { ");
            builder.AppendLine("            try ");
            builder.AppendLine("            { ");
            if (LeftTree != null)
            {
                builder.AppendLine("             var rf = RF.Create<" + LeftTree.TableName + ">();var treeList = rf.GetAll(); ");
                builder.AppendLine("              if (treeList.Count > 0) ");
                builder.AppendLine("                { ");
                builder.AppendLine("                    foreach (" + LeftTree.TableName + " item in treeList) ");
                builder.AppendLine("                    { ");
                builder.AppendLine("                        TreeNode tn = new TreeNode(); ");
                builder.AppendLine("                        tn.Text = item.Name; ");
                builder.AppendLine("                        tn.Tag = item; ");
                builder.AppendLine("                        this.entityFrm1.TreeView.Nodes.Add(tn); ");
                builder.AppendLine("                    } ");
                builder.AppendLine("                } ");
                builder.AppendLine("             this.entityFrm1.TreeViewVisible = true; ");
                builder.AppendLine("             this.entityFrm1.TreeView.AfterSelect += TreeView_AfterSelect; ");
            }
            else
            {
                builder.AppendLine("                var rflist = RF.Create<" + className + ">(); ");
                builder.AppendLine("                this.entityFrm1.DataGridView.DataSource =rflist.GetAll(); ");
                builder.AppendLine("              " + AddColumnAlias.ToString());
            }
            builder.AppendLine("            } ");
            builder.AppendLine("            catch (Exception ex) ");
            builder.AppendLine("            { ");
            builder.AppendLine("                LogUtil.LogError(ex); ");
            builder.AppendLine("            } ");
            builder.AppendLine("        } ");
            if (LeftTree != null)
            {
                builder.AppendLine("        void TreeView_AfterSelect(object sender, TreeViewEventArgs e) ");
                builder.AppendLine("        { ");
                builder.AppendLine("            if (e == null || e.Node == null || e.Node.Tag == null) ");
                builder.AppendLine("                return; ");
                builder.AppendLine("            " + LeftTree.TableName + " tree = (" + LeftTree.TableName + ")e.Node.Tag; ");
                builder.AppendLine("            this.entityFrm1.DataGridView.DataSource = BLLFactory<" + className + ">.Instance().LoadEntities(p => p." + LeftTree.TableName + ".ID == tree.ID); ");
                builder.AppendLine("              " + AddColumnAlias.ToString());
                builder.AppendLine("        } ");
            }
            builder.AppendLine("        private void entityFrm1_AddButtonClick( ) ");
            builder.AppendLine("        { ");
            builder.AppendLine("            " + className + "FrmEdit frm = new " + className + "FrmEdit(); ");
            builder.AppendLine("            frm.Text = \"" + tableinfo + " 添加\"; ");
            builder.AppendLine("            if (DialogResult.OK == frm.ShowDialog())");
            builder.AppendLine("            {");
            builder.AppendLine("               BindData();");
            builder.AppendLine("             }");
            builder.AppendLine("        } ");
            builder.AppendLine("        private void entityFrm1_EditButtonClick( ) ");
            builder.AppendLine("        { ");
            builder.AppendLine("            DataGridView grid = this.entityFrm1.DataGridView; ");
            builder.AppendLine("            if (grid.SelectedRows.Count == 0) return; ");
            builder.AppendLine("            int ID = 0; ");
            builder.AppendLine("            if (grid.Columns.Contains(\"ColID\")) ");
            builder.AppendLine("                ID = grid.SelectedRows[0].Cells[\"ColID\"].Value.ToString().CastTo<int>(); ");
            builder.AppendLine("            else if (grid.Columns.Contains(\"ID\")) ");
            builder.AppendLine("                ID = grid.SelectedRows[0].Cells[\"ID\"].Value.ToString().CastTo<int>(); ");
            builder.AppendLine("            if (ID > 0) ");
            builder.AppendLine("            { ");
            builder.AppendLine("                " + className + "FrmEdit frm = new " + className + "FrmEdit(); ");
            builder.AppendLine("                frm.Text = \"" + className + " 修改\"; ");
            builder.AppendLine("                frm.ID = ID; ");
            builder.AppendLine("                frm.OnDataSaved += frm_OnDataSaved; ");
            builder.AppendLine("                if (DialogResult.OK == frm.ShowDialog()) ");
            builder.AppendLine("                { ");
            builder.AppendLine("                    BindData(); ");
            builder.AppendLine("                } ");
            builder.AppendLine("            } ");
            builder.AppendLine("        } ");
            builder.AppendLine("        void frm_OnDataSaved(object sender, EventArgs e) ");
            builder.AppendLine("        { ");
            builder.AppendLine("            BindData(); ");
            builder.AppendLine("        } ");
            builder.AppendLine("        private void entityFrm1_RefreshButtonClick( ) ");
            builder.AppendLine("        { ");
            builder.AppendLine("            BindData(); ");
            builder.AppendLine("        } ");
            builder.AppendLine("        private void entityFrm1_DelButtonClick( ) ");
            builder.AppendLine("        { ");
            builder.AppendLine("            if (MessageBox.Show(\"您确定删除选定的记录么？\",\"提示\") == DialogResult.No) ");
            builder.AppendLine("            { ");
            builder.AppendLine("                return; ");
            builder.AppendLine("            } ");
            builder.AppendLine("            DataGridView grid = this.entityFrm1.DataGridView; ");
            builder.AppendLine("            if (grid != null) ");
            builder.AppendLine("            { ");
            builder.AppendLine("                bool flg = false; ");
            builder.AppendLine("                foreach (DataGridViewRow row in grid.SelectedRows) ");
            builder.AppendLine("                { ");
            builder.AppendLine("                    int ID = 0; ");
            builder.AppendLine("                    if (grid.Columns.Contains(\"ColID\")) ");
            builder.AppendLine("                        ID = row.Cells[\"ColID\"].Value.ToString().CastTo<int>(); ");
            builder.AppendLine("                    else if (grid.Columns.Contains(\"ID\")) ");
            builder.AppendLine("                        ID = row.Cells[\"ID\"].Value.ToString().CastTo<int>(); ");
            builder.AppendLine("                    if (ID > 0) ");
            builder.AppendLine("                    { ");
            builder.AppendLine("                        BLLFactory<" + className + ">.Instance().DeleteEntity(ID); ");
            builder.AppendLine("                        flg = true; ");
            builder.AppendLine("                    } ");
            builder.AppendLine("                } ");
            builder.AppendLine("                if (flg) ");
            builder.AppendLine("                    BindData(); ");
            builder.AppendLine("            } ");
            builder.AppendLine("        } ");
            builder.AppendLine("        private void entityFrm1_CloseFrmButtonClick( ) ");
            builder.AppendLine("        { ");
            builder.AppendLine("            this.Close(); ");
            builder.AppendLine("        } ");
            builder.AppendLine("        private void entityFrm1_ExportButtonClick( ) ");
            builder.AppendLine("        { ");
            builder.AppendLine("            // 正在开发中....");
            builder.AppendLine("        } ");
            builder.AppendLine("        /// <summary> ");
            builder.AppendLine("        /// 编写初始化窗体的实现，可以用于刷新 ");
            builder.AppendLine("        /// </summary> ");
            builder.AppendLine("        public override void FormOnLoad( ) ");
            builder.AppendLine("        { ");
            builder.AppendLine("            BindData(); ");
            builder.AppendLine("        } ");
            builder.AppendLine("    } ");
            builder.AppendLine("} ");
            if (path.Length > 0)
            {
                string folder = path + @"\WinFromBuild\" + Utils.NameSpace;
                Utils.FolderCheck(folder);
                string filename = folder + @"\" + className + "Frm.cs";
                Utils.CreateFiles(filename, builder.ToString());
            }
            #endregion

            #region {tablename}Frm.Designer.cs
            builder = new StringBuilder();
            builder.AppendLine("namespace " + Utils.NameSpaceUI);
            builder.AppendLine("{ ");
            builder.AppendLine("    partial class " + className + "Frm ");
            builder.AppendLine("    { ");
            builder.AppendLine("        private System.ComponentModel.IContainer components = null; ");
            builder.AppendLine("        protected override void Dispose(bool disposing) ");
            builder.AppendLine("        { ");
            builder.AppendLine("            if (disposing && (components != null)) ");
            builder.AppendLine("            { ");
            builder.AppendLine("                components.Dispose(); ");
            builder.AppendLine("            } ");
            builder.AppendLine("            base.Dispose(disposing); ");
            builder.AppendLine("        } ");
            builder.AppendLine("        #region Windows Form Designer generated code ");
            builder.AppendLine("        private void InitializeComponent() ");
            builder.AppendLine("        { ");
            builder.AppendLine("            this.entityFrm1 = new MinGuiLuo.WinFromControls.Controls.EntityFrm(); ");
            builder.AppendLine("            this.SuspendLayout(); ");
            builder.AppendLine(" ");
            builder.AppendLine("      " + GetGridColumn(className, ShowFields));
            builder.AppendLine("            //  ");
            builder.AppendLine("            // entityFrm1 ");
            builder.AppendLine("            //  ");
            builder.AppendLine("            this.entityFrm1.Dock = System.Windows.Forms.DockStyle.Fill; ");
            builder.AppendLine("            this.entityFrm1.Location = new System.Drawing.Point(0, 0); ");
            builder.AppendLine("            this.entityFrm1.Name = \"entityFrm1\"; ");
            builder.AppendLine("            this.entityFrm1.Size = new System.Drawing.Size(576, 364); ");
            builder.AppendLine("            this.entityFrm1.TabIndex = 0; ");
            builder.AppendLine("            this.entityFrm1.AddButtonClick += new MinGuiLuo.WinFromControls.Controls.EntityFrm.ButtonClick(this.entityFrm1_AddButtonClick); ");
            builder.AppendLine("            this.entityFrm1.EditButtonClick += new MinGuiLuo.WinFromControls.Controls.EntityFrm.ButtonClick(this.entityFrm1_EditButtonClick); ");
            builder.AppendLine("            this.entityFrm1.DelButtonClick += new MinGuiLuo.WinFromControls.Controls.EntityFrm.ButtonClick(this.entityFrm1_DelButtonClick); ");
            builder.AppendLine("            this.entityFrm1.ExportButtonClick += new MinGuiLuo.WinFromControls.Controls.EntityFrm.ButtonClick(this.entityFrm1_ExportButtonClick); ");
            builder.AppendLine("            this.entityFrm1.RefreshButtonClick += new MinGuiLuo.WinFromControls.Controls.EntityFrm.ButtonClick(this.entityFrm1_RefreshButtonClick); ");
            builder.AppendLine("            this.entityFrm1.CloseFrmButtonClick += new MinGuiLuo.WinFromControls.Controls.EntityFrm.ButtonClick(this.entityFrm1_CloseFrmButtonClick); ");
            builder.AppendLine("            //  ");
            builder.AppendLine("            // " + className + "Frm ");
            builder.AppendLine("            //  ");
            builder.AppendLine("            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F); ");
            builder.AppendLine("            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font; ");
            builder.AppendLine("            this.ClientSize = new System.Drawing.Size(576, 364); ");
            builder.AppendLine("            this.Controls.Add(this.entityFrm1); ");
            builder.AppendLine("            this.Font = new System.Drawing.Font(\"宋体\", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134))); ");
            builder.AppendLine("            this.Name = \"" + className + "Frm\"; ");
            builder.AppendLine("            this.Text = \"" + tableinfo + " \"; ");
            builder.AppendLine("            this.ResumeLayout(false); ");
            builder.AppendLine("        } ");
            builder.AppendLine("        #endregion ");
            builder.AppendLine("        private MinGuiLuo.WinFromControls.Controls.EntityFrm entityFrm1; ");
            builder.AppendLine("    } ");
            builder.AppendLine("} ");
            if (path.Length > 0)
            {
                string folder = path + @"\WinFromBuild\" + Utils.NameSpace;
                Utils.FolderCheck(folder);
                string filename = folder + @"\" + className + "Frm.Designer.cs";
                Utils.CreateFiles(filename, builder.ToString());
            }
            #endregion
        }
        //TODO:必填项，重复项
        private void WinFrmEditPage(string path, TableModel tableModel, List<TableColumn> ShowFields, List<TableColumn> CheckFields, List<TableColumn> CheckFepeat)
        {
            //Edit页面
            #region {tablename}FrmEdit.cs
            string className = tableModel.TableName;
            string tableinfo = tableModel.TableNameRemark;
            StringBuilder newControl = new StringBuilder();
            StringBuilder newControl1 = new StringBuilder();
            StringBuilder newControl2 = new StringBuilder();
            StringBuilder tableLayoutPanel1 = new StringBuilder();
            StringBuilder RowStyles = new StringBuilder();
            StringBuilder CheckInput = new StringBuilder();
            StringBuilder SetInfo = new StringBuilder();
            StringBuilder DisplayData = new StringBuilder();
            int num = 0;
            foreach (TableColumn item in ShowFields)
            {
                num++;
                string proStr = item.ColumnType;
                string property = item.ColumnName;
                string propertyinfo = item.ColumnRemark;
                if (propertyinfo.Length == 0) propertyinfo = property;

                if (property.ToLower().Equals("adddate") || property.ToLower().Equals("isdeleted")) continue;

                #region {tablename}FrmEdit.Designer.cs
                //创建控件
                newControl2.AppendLine("            private System.Windows.Forms.Label lbl" + property + "; ");
                newControl2.AppendLine("            private System.Windows.Forms.TextBox txt" + property + "; ");
                newControl.AppendLine("             this.lbl" + property + " = new System.Windows.Forms.Label(); ");
                newControl.AppendLine("             this.txt" + property + " = new System.Windows.Forms.TextBox(); ");
                //初始化控件
                newControl1.AppendLine("            //  ");
                newControl1.AppendLine("            // lbl" + property + "");
                newControl1.AppendLine("            //  ");
                newControl1.AppendLine("            this.lbl" + property + ".Dock = System.Windows.Forms.DockStyle.Fill; ");
                newControl1.AppendLine("            this.lbl" + property + ".Name = \"lbl" + property + "\"; ");
                newControl1.AppendLine("            this.lbl" + property + ".Text =  \"" + propertyinfo + "\"; ");
                newControl1.AppendLine("            this.lbl" + property + ".TabIndex = " + num + "; ");
                newControl1.AppendLine("            this.lbl" + property + ".TextAlign = System.Drawing.ContentAlignment.MiddleLeft; ");
                newControl1.AppendLine("            //  ");
                newControl1.AppendLine("            // txt" + property + "");
                newControl1.AppendLine("            //  ");
                newControl1.AppendLine("            this.txt" + property + ".Dock = System.Windows.Forms.DockStyle.Fill; ");
                newControl1.AppendLine("            this.txt" + property + ".Name = \"txt" + property + "\"; ");
                newControl1.AppendLine("            this.txt" + property + ".TabIndex = " + num + "; ");
                newControl1.AppendLine("            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));");
                //表格控件初始化
                tableLayoutPanel1.AppendLine("     this.tableLayoutPanel1.Controls.Add(this.lbl" + property + "); ");
                tableLayoutPanel1.AppendLine("     this.tableLayoutPanel1.Controls.Add(this.txt" + property + "); ");
                #endregion

                #region {tablename}FrmEdit.cs

                CheckInput.AppendLine("            if (this.txt" + property + ".Text.Trim().Length == 0) ");
                CheckInput.AppendLine("            { ");
                CheckInput.AppendLine("                msgList.AppendLine(\"" + propertyinfo + "不能为空。\"); ");
                CheckInput.AppendLine("                this.txt" + property + ".Focus(); ");
                CheckInput.AppendLine("                result = false; ");
                CheckInput.AppendLine("            } ");

                if (item.ColumnName.ToLower().Equals("id") || item.ColumnName.ToLower().Equals("isdeleted") || item.ColumnName.ToLower().Equals("adddate"))
                {
                    DisplayData.AppendLine("         txt" + property + ".Enabled = false; txt" + property + ".BackColor = System.Drawing.SystemColors.Menu;");
                    DisplayData.AppendLine("         txt" + property + ".Text = TypeConversion.GetObjTranNull<String>(info." + property + "); ");
                }
                else
                {
                    DisplayData.AppendLine("         txt" + property + ".Text = TypeConversion.GetObjTranNull<String>(info." + property + "); ");
                }

                SetInfo.AppendLine("             info." + property + " =TypeConversion.GetObjTranNull<" + proStr + ">(txt" + property + ".Text); ");
                #endregion
            }

            StringBuilder builder = new StringBuilder();
            builder.AppendLine("using System; ");
            builder.AppendLine("using System.Text; ");
            builder.AppendLine("using System.Data; ");
            builder.AppendLine("using System.Drawing; ");
            builder.AppendLine("using System.Windows.Forms; ");
            builder.AppendLine("using System.ComponentModel; ");
            builder.AppendLine("using System.Collections.Generic; ");
            builder.AppendLine("using MinGuiLuo;");
            builder.AppendLine("using MinGuiLuo.ORM; ");
            builder.AppendLine("using MinGuiLuo.Logging;");
            builder.AppendLine("using MinGuiLuo.Module.WinFrom.Forms; ");
            builder.AppendLine("namespace " + Utils.NameSpaceUI);
            builder.AppendLine("{ ");
            builder.AppendLine("    public partial class " + className + "FrmEdit : BaseEditForm ");
            builder.AppendLine("    { ");
            builder.AppendLine("        public " + className + "FrmEdit() ");
            builder.AppendLine("        { ");
            builder.AppendLine("            InitializeComponent(); ");
            builder.AppendLine("        } ");
            builder.AppendLine("                 ");
            builder.AppendLine("        /// <summary> ");
            builder.AppendLine("        /// 实现控件输入检查的函数 ");
            builder.AppendLine("        /// </summary> ");
            builder.AppendLine("        /// <returns></returns> ");
            builder.AppendLine("        public override bool CheckInput() ");
            builder.AppendLine("        { ");
            builder.AppendLine("            bool result = true;//默认是可以通过 ");
            builder.AppendLine("            #region MyRegion ");
            builder.AppendLine("            System.Text.StringBuilder msgList = new System.Text.StringBuilder(); ");
            builder.AppendLine(CheckInput.ToString());
            builder.AppendLine("            if (!result && msgList.Length > 2) ");
            builder.AppendLine("            { ");
            builder.AppendLine("                MessageBoxForm.ShowMessage(msgList.ToString()); ");
            builder.AppendLine("            } ");
            builder.AppendLine("            #endregion ");
            builder.AppendLine("            return result; ");
            builder.AppendLine("        } ");
            builder.AppendLine(" ");
            builder.AppendLine("        /// <summary> ");
            builder.AppendLine("        /// 数据显示的函数 ");
            builder.AppendLine("        /// </summary> ");
            builder.AppendLine("        public override void DisplayData() ");
            builder.AppendLine("        { ");
            builder.AppendLine("            if (ID>0) ");
            builder.AppendLine("            { ");
            builder.AppendLine("                #region 显示客户信息 ");
            builder.AppendLine("                " + className + " info =BLLFactory<" + className + ">.Instance().FindByID(ID); ");
            builder.AppendLine("                if (info != null) ");
            builder.AppendLine("                { ");
            builder.AppendLine("                    " + DisplayData.ToString());
            builder.AppendLine("                }  ");
            builder.AppendLine("                #endregion ");
            builder.AppendLine("            } ");
            builder.AppendLine("        } ");
            builder.AppendLine(" ");
            builder.AppendLine("        /// <summary> ");
            builder.AppendLine("        /// 编辑或者保存状态下取值函数 ");
            builder.AppendLine("        /// </summary> ");
            builder.AppendLine("        /// <param name=\"info\"></param> ");
            builder.AppendLine("        private void SetInfo(" + className + " info) ");
            builder.AppendLine("        { ");
            builder.AppendLine("         " + SetInfo.ToString());
            builder.AppendLine("         } ");
            builder.AppendLine("          ");
            builder.AppendLine("        /// <summary> ");
            builder.AppendLine("        /// 新增状态下的数据保存 ");
            builder.AppendLine("        /// </summary> ");
            builder.AppendLine("        /// <returns></returns> ");
            builder.AppendLine("        public override bool SaveAddNew() ");
            builder.AppendLine("        { ");
            builder.AppendLine("            " + className + " info = new " + className + "(); ");
            builder.AppendLine("            SetInfo(info); ");
            builder.AppendLine(" ");
            builder.AppendLine("            try ");
            builder.AppendLine("            { ");
            builder.AppendLine("                #region 新增数据 ");
            builder.AppendLine("                bool succeed = BLLFactory<" + className + ">.Instance().AddEntity(info);");
            builder.AppendLine("                if (succeed) ");
            builder.AppendLine("                { ");
            builder.AppendLine("                    //可添加其他关联操作 ");
            builder.AppendLine("                    return true; ");
            builder.AppendLine("                } ");
            builder.AppendLine("                #endregion ");
            builder.AppendLine("            } ");
            builder.AppendLine("            catch (Exception ex) ");
            builder.AppendLine("            { ");
            builder.AppendLine("                LogUtil.LogError(ex);  ");
            builder.AppendLine("            } ");
            builder.AppendLine("            return false; ");
            builder.AppendLine("        }                  ");
            builder.AppendLine(" ");
            builder.AppendLine("        /// <summary> ");
            builder.AppendLine("        /// 编辑状态下的数据保存 ");
            builder.AppendLine("        /// </summary> ");
            builder.AppendLine("        /// <returns></returns> ");
            builder.AppendLine("        public override bool SaveUpdated() ");
            builder.AppendLine("        { ");
            builder.AppendLine("			//检查不同ID是否还有其他相同关键字的记录 ");
            builder.AppendLine("            " + className + " info =BLLFactory<" + className + ">.Instance().FindByID(ID);");
            builder.AppendLine("            if (info != null) ");
            builder.AppendLine("            { ");
            builder.AppendLine("                SetInfo(info); ");
            builder.AppendLine("                try ");
            builder.AppendLine("                { ");
            builder.AppendLine("                    #region 更新数据 ");
            builder.AppendLine("                    bool succeed = BLLFactory<" + className + ">.Instance().UpdateEntity(info);");
            builder.AppendLine("                    if (succeed)");
            builder.AppendLine("                    { ");
            builder.AppendLine("                        //可添加其他关联操作 ");
            builder.AppendLine("                        return true; ");
            builder.AppendLine("                    } ");
            builder.AppendLine("                    #endregion ");
            builder.AppendLine("                } ");
            builder.AppendLine("                catch (Exception ex) ");
            builder.AppendLine("                { ");
            builder.AppendLine("                   LogUtil.LogError(ex);  ");
            builder.AppendLine("                } ");
            builder.AppendLine("            } ");
            builder.AppendLine("           return false; ");
            builder.AppendLine("        } ");
            builder.AppendLine("    } ");
            builder.AppendLine("} ");
            if (path.Length > 0)
            {
                string folder = path + @"\WinFromBuild\" + Utils.NameSpace;
                Utils.FolderCheck(folder);
                string filename = folder + @"\" + className + "FrmEdit.cs";
                Utils.CreateFiles(filename, builder.ToString());
            }
            #endregion

            #region {tablename}FrmEdit.Designer.cs
            int ShowFieldsCount = ShowFields.Count;
            int rowsstyles = ShowFieldsCount / 2;
            if (rowsstyles == 1) rowsstyles = 2;
            builder = new StringBuilder();
            builder.AppendLine("namespace " + Utils.NameSpaceUI);
            builder.AppendLine("{ ");
            builder.AppendLine("    partial class " + className + "FrmEdit");
            builder.AppendLine("    { ");
            builder.AppendLine("        private System.ComponentModel.IContainer components = null; ");
            builder.AppendLine("        protected override void Dispose(bool disposing) ");
            builder.AppendLine("        { ");
            builder.AppendLine("            if (disposing && (components != null)) ");
            builder.AppendLine("            { ");
            builder.AppendLine("                components.Dispose(); ");
            builder.AppendLine("            } ");
            builder.AppendLine("            base.Dispose(disposing); ");
            builder.AppendLine("        } ");
            builder.AppendLine("        #region Windows Form Designer generated code ");
            builder.AppendLine("        private void InitializeComponent() ");
            builder.AppendLine("        { ");
            builder.AppendLine("             this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel(); ");
            builder.AppendLine(newControl.ToString());
            builder.AppendLine("            this.tableLayoutPanel1.SuspendLayout(); ");
            builder.AppendLine("            this.SuspendLayout(); ");
            builder.AppendLine("            // tableLayoutPanel1 ");
            builder.AppendLine("            //  ");
            builder.AppendLine("            this.tableLayoutPanel1.AutoScroll = true; ");
            builder.AppendLine("			this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single; ");
            builder.AppendLine("            this.tableLayoutPanel1.ColumnCount = 2; ");
            builder.AppendLine("            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F)); ");
            builder.AppendLine("            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F)); ");
            builder.AppendLine(tableLayoutPanel1.ToString());
            builder.AppendLine("            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;");
            builder.AppendLine("            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0); ");
            builder.AppendLine("            this.tableLayoutPanel1.Name = \"tableLayoutPanel1\"; ");
            builder.AppendLine("            this.tableLayoutPanel1.RowCount = " + (ShowFieldsCount + 1) + "; ");
            for (int i = 0; i < ShowFields.Count; i++)
            {
                builder.AppendLine("        this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F)); ");
            }
            int tlpHeight = ShowFieldsCount * 21 + 21;
            int frmHeight = tlpHeight + 26 + 26 + 26 + 30;
            builder.AppendLine("            this.tableLayoutPanel1.Size = new System.Drawing.Size(419, " + tlpHeight + "); ");
            builder.AppendLine("            this.tableLayoutPanel1.TabIndex = 6; ");
            builder.AppendLine(newControl1.ToString());
            builder.AppendLine("            //  ");
            builder.AppendLine("            // " + className + "FrmEdit ");
            builder.AppendLine("            //  ");
            builder.AppendLine("            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F); ");
            builder.AppendLine("            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font; ");
            builder.AppendLine("            this.ClientSize = new System.Drawing.Size(479, " + frmHeight + "); ");
            builder.AppendLine("            this.Controls.Add(this.tableLayoutPanel1); ");
            builder.AppendLine("            this.Name = \"" + className + "FrmEdit\"; ");
            builder.AppendLine("            this.Text = \"" + className + "\"; ");
            builder.AppendLine("            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0); ");
            builder.AppendLine("            this.tableLayoutPanel1.ResumeLayout(false); ");
            builder.AppendLine("            this.tableLayoutPanel1.PerformLayout(); ");
            builder.AppendLine("            this.ResumeLayout(false); ");
            builder.AppendLine("            this.PerformLayout(); ");
            builder.AppendLine("        } ");
            builder.AppendLine("        #endregion             ");
            builder.AppendLine("        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;             ");
            builder.AppendLine(newControl2.ToString());
            builder.AppendLine("    } ");
            builder.AppendLine("}             ");
            if (path.Length > 0)
            {
                string folder = path + @"\WinFromBuild\" + Utils.NameSpace;
                Utils.FolderCheck(folder);
                string filename = folder + @"\" + className + "FrmEdit.Designer.cs";
                Utils.CreateFiles(filename, builder.ToString());
            }
            #endregion
        }
    }
}
