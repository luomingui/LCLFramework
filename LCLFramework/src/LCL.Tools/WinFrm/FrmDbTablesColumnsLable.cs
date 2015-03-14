using System;
using System.Data;
using System.Windows.Forms;

namespace SF.Tools.WinFrm
{
    public partial class FrmDbTablesColumnsLable : Form
    {
        DataTable columnInfoList = null;
        string tableName = string.Empty;
        public FrmDbTablesColumnsLable(string tableName)
        {
            InitializeComponent();
            this.tableName = tableName;
            this.groupBox1.Text = "修改" + tableName + "表字段说明";
            this.Text = "修改表字段说明.....";
            InitFrm();
        }
        public void InitFrm( )
        {
            columnInfoList = BLLFactory.Instance.idb.GetTablesColumnsList(tableName, Utils.dbName);
            int count = columnInfoList.Rows.Count;

            this.tableLayoutPanel1.ColumnStyles.Clear();
            this.tableLayoutPanel1.RowStyles.Clear();
            this.tableLayoutPanel1.Controls.Clear();
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.RowCount = count / 4;

            for (int i = 1; i < columnInfoList.Rows.Count; i++)
            {
                string tableColumnName = columnInfoList.Rows[i]["字段名"].ToString();
                string tableColumnNameInfo = columnInfoList.Rows[i]["字段说明"].ToString();
                if (tableColumnNameInfo.Length == 0) tableColumnNameInfo = tableColumnName;

                Label lbl = new Label();
                lbl.Dock = DockStyle.Fill;
                lbl.Size = new System.Drawing.Size(155, 30);
                lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                lbl.Name = "lbl" + tableColumnName;
                lbl.Text = tableColumnName;

                TextBox txt = new TextBox();
                txt.Dock = DockStyle.Fill;
                txt.Size = new System.Drawing.Size(205, 30);
                txt.Name = "txt" + tableColumnName;
                txt.Text = tableColumnNameInfo;

                this.tableLayoutPanel1.Controls.Add(lbl);
                this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
                this.tableLayoutPanel1.Controls.Add(txt);
            }
            this.tableLayoutPanel1.Controls.Add(new Label());
        }
        private void but_Save_Click(object sender, EventArgs e)
        {
            try
            {
                #region MyRegion
                for (int i = 1; i < columnInfoList.Rows.Count; i++)
                {
                    string columnName = columnInfoList.Rows[i]["字段名"].ToString();
                    string columnInfoInfo = columnInfoList.Rows[i]["字段说明"].ToString();

                    string txtname = "txt" + columnName;
                    string tInfo = tableLayoutPanel1.Controls[txtname].Text;

                    if (columnInfoInfo.Length > 0)
                    {
                        //存在就更新
                        BLLFactory.Instance.idb.ExtendedProperty(true, tInfo, tableName, columnName);
                    }
                    else
                    {
                        BLLFactory.Instance.idb.ExtendedProperty(false, tInfo, tableName, columnName);
                    }
                }
                #endregion
                MessageBox.Show("修改数据库表说明成功！");
                ProcessDataSaved(null, null);
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show("修改数据库表说明失败！" + ex.Message);
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
