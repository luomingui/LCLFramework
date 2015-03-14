using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SF.Tools.WinFrm
{
    public partial class FrmDbTablesLable : Form
    {
        List<TableModel> tmList = null;
        public FrmDbTablesLable( )
        {
            InitializeComponent();
            this.label1.Text = "修改" + Utils.dbName + "数据库表说明";
            this.Text = "修改表说明.....";
            InitFrm();
        }
        public void InitFrm( )
        {
            tmList = BLLFactory.Instance.idb.GetTableModelList(Utils.dbName);
            this.label1.Text = "修改" + Utils.dbName + "数据库表说明,总共" + tmList.Count + "张表";
            this.tableLayoutPanel1.ColumnStyles.Clear();
            this.tableLayoutPanel1.RowStyles.Clear();
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.RowCount = tmList.Count / 4;

            List<Label> labels = new List<Label>(tmList.Count);
            List<TextBox> textbox = new List<TextBox>(tmList.Count);
            for (int i = 0; i < tmList.Count; i++)
            {
                TableModel tm = tmList[i];
                string tableName = tm.TableName;
                string tableNameInfo = tm.TableNameRemark;
                if (tableNameInfo.Length == 0) tableNameInfo = tableName;

                Label lbl = new Label();
                lbl.Dock = DockStyle.Fill;
                lbl.Size = new System.Drawing.Size(155, 30);
                lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                lbl.Name = "lbl" + tableName;
                lbl.Text = tableName;

                TextBox txt = new TextBox();
                txt.Dock = DockStyle.Fill;
                txt.Size = new System.Drawing.Size(205, 30);
                txt.Name = "txt" + tableName;
                txt.Text = tableNameInfo;

                this.tableLayoutPanel1.Controls.Add(lbl);
                this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
                this.tableLayoutPanel1.Controls.Add(txt);
            }
            this.tableLayoutPanel1.Controls.Add(new Label());
            this.tableLayoutPanel1.Controls.Add(new Label());
            this.tableLayoutPanel1.Controls.Add(new Label());
            this.tableLayoutPanel1.Controls.Add(new Label());
        }
        private void but_Save_Click(object sender, EventArgs e)
        {
            try
            {
                #region MyRegion
                foreach (TableModel tm in tmList)
                {
                    string tableName = tm.TableName;
                    string tableNameInfo = tm.TableNameRemark;

                    string txtname = "txt" + tableName;
                    string tInfo = tableLayoutPanel1.Controls[txtname].Text;

                    if (tableNameInfo.Length > 0)
                    {
                        //删除在添加
                        BLLFactory.Instance.idb.ExtendedProperty(true, tInfo, tableName, "");
                    }
                    else
                    {
                        BLLFactory.Instance.idb.ExtendedProperty(false, tInfo, tableName, "");
                    }
                }
                #endregion
                MessageBox.Show("修改数据库表说明成功！");
                ProcessDataSaved(null, null);
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception)
            {
                MessageBox.Show("修改数据库表说明失败！");
            }
        }
        public event EventHandler OnDataSaved;//子窗体数据保存的触发
        /// <summary>
        /// 处理数据保存后的事件触发
        /// </summary>
        public virtual void ProcessDataSaved(object sender, EventArgs e)
        {
            if (OnDataSaved != null)
            {
                OnDataSaved(sender, e);
            }
        }
    }
}

