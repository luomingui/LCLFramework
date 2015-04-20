using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace LCL.Tools.WinFrm
{
    public partial class FrmDbBuildWinFromUI : Form
    {
        TableModel tableModel = null;
        string tableName = string.Empty;
        public FrmDbBuildWinFromUI(string tableName)
        {
            InitializeComponent();
            this.tableName = tableName;
        }
        private void InitFrm()
        {
            try
            {
                #region MyRegion
                tableModel = BLLFactory.Instance.idb.GetTableModel(tableName, Utils.dbName);
                for (int i = 0; i < tableModel.Columns.Count; i++)
                {
                    TableColumn column = tableModel.Columns[i];
                    chk_CheckFepeat.Items.Add(new SandData(column.ColumnNameRemark, column), true);
                    chk_CheckFields.Items.Add(new SandData(column.ColumnNameRemark, column), true);
                    chk_EditShow.Items.Add(new SandData(column.ColumnNameRemark, column), true);
                    chk_SelectWhere.Items.Add(new SandData(column.ColumnNameRemark, column), true);
                    chk_ShowFields.Items.Add(new SandData(column.ColumnNameRemark, column), true);
                }
                List<TableModel> tmList = BLLFactory.Instance.idb.GetTableModelList(Utils.dbName);
                cmb_Tables.DataSource = tmList;
                cmb_Tables.DisplayMember = "TableName";
                cmb_Tables.ValueMember = "TableName";
                //Grid列
                WinFromComplexBuild build = new WinFromComplexBuild();
                richTextBox1.Text = build.GetGridColumn(Utils.dbName, tableModel.Columns);
                //DataTable列
                StringBuilder list = new StringBuilder();
                string famt = @" this.entityFrm1.AddColumnAlias(""{0}"", ""{1}"", {2});";
                foreach (var item in tableModel.Columns)
                {
                    string proStr = item.ColumnType;
                    string property = item.ColumnName;
                    string propertyinfo = item.ColumnRemark;
                    if (propertyinfo.Length == 0) propertyinfo = property;
                    if (property.ToLower().Equals("id") || property.ToLower().Equals("isdeleted"))
                    {
                        list.AppendLine(string.Format(famt, property, propertyinfo, "false"));
                    }
                    else
                    {
                        list.AppendLine(string.Format(famt, property, propertyinfo, "true"));
                    }
                }
                richTextBox2.Text = list.ToString();
                //Entity
                EntityFrameworkBuild bb = new EntityFrameworkBuild();
                string context= bb.BuildEntity("", tableModel, null);
                richTextBox3.Text = context;
                #endregion
            }
            catch
            {

            }
        }
        private void FrmDbBuildWinFromUI_Load(object sender, EventArgs e)
        {
            try
            {
                InitFrm();
            }
            catch
            {

            }
        }
        private void cmb_Tables_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbTree_Name.Items.Clear();
            cmbTree_Value.Items.Clear();

            if (cmb_Tables.Text == null) return;
            string tablename = cmb_Tables.Text;

            TableModel tableModel = BLLFactory.Instance.idb.GetTableModel(tablename, Utils.dbName);
            for (int i = 0; i < tableModel.Columns.Count; i++)
            {
                TableColumn column = tableModel.Columns[i];
                cmbTree_Name.Items.Add(new SandData(column.ColumnNameRemark, column));
                cmbTree_Value.Items.Add(new SandData(column.ColumnNameRemark, column));
            }
        }
        private void but_Build_Click(object sender, EventArgs e)
        {
            try
            {
                string path = string.Empty;
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    path = dialog.SelectedPath + "\\Template";
                }

                #region  List<TableColumn>
                List<TableColumn> SelectWhere = new List<TableColumn>();
                List<TableColumn> ShowFields = new List<TableColumn>();
                TableModel LeftTree = null;
                List<TableColumn> CheckFields = new List<TableColumn>();
                List<TableColumn> CheckFepeat = new List<TableColumn>();

                for (int i = 0; i < chk_SelectWhere.CheckedItems.Count; i++)
                {
                    SandData sandData = chk_SelectWhere.CheckedItems[i] as SandData;
                    TableColumn column = sandData.Value as TableColumn;
                    SelectWhere.Add(column);
                }
                for (int i = 0; i < chk_ShowFields.CheckedItems.Count; i++)
                {
                    SandData sandData = chk_ShowFields.CheckedItems[i] as SandData;
                    TableColumn column = sandData.Value as TableColumn;
                    ShowFields.Add(column);
                }
                for (int i = 0; i < chk_CheckFields.CheckedItems.Count; i++)
                {
                    SandData sandData = chk_CheckFields.CheckedItems[i] as SandData;
                    TableColumn column = sandData.Value as TableColumn;
                    CheckFields.Add(column);
                }
                for (int i = 0; i < chk_CheckFepeat.CheckedItems.Count; i++)
                {
                    SandData sandData = chk_CheckFepeat.CheckedItems[i] as SandData;
                    TableColumn column = sandData.Value as TableColumn;
                    CheckFepeat.Add(column);
                }
                if (checkBox1.Checked)
                {
                    LeftTree = new TableModel();
                    LeftTree.TableName = cmb_Tables.Text;
                    TableColumn column = (TableColumn)cmbTree_Name.Tag;
                    LeftTree.TablePK = column.ColumnName;
                }
                #endregion

                WinFromComplexBuild wfcb = new WinFromComplexBuild();
                wfcb.Library(path, tableModel, SelectWhere, ShowFields, LeftTree, CheckFields, CheckFields);
                MessageBox.Show("已完成！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
    }
}
