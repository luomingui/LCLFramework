
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace LCL.Tools
{
    /// <summary>
    /// 界面数据绑定 接口
    /// </summary>
    public class BLLFactory
    {
        public static BLLFactory Instance = new BLLFactory();
        public IDbo idb = null;
        private BLLFactory( )
        {
            idb = DALFactory.Factory();
        }
        /// <summary>
        /// 数据库列表
        /// </summary>
        /// <param name="cmbDB"></param>
        /// <returns></returns>
        public void BindDataBaseList(ComboBox cmbDB)
        {
            List<DataBaseModel> dbmList = idb.GetDBList();
            foreach (DataBaseModel dbm in dbmList)
            {
                string item = dbm.DatabaseName;
                cmbDB.Items.Add(item);
            }
        }
        public void BindTableNameList(TreeView tv)
        {
            List<TableModel> list = idb.GetTableModelList(Utils.dbName);
            tv.Nodes.Clear();
            foreach (var item in list)
            {
                tv.Nodes.Add(item.TableName);
            }
            tv.ExpandAll();
        }
        public void BinTableInfo(DataGridView dgv, string tableName)
        {
            DataTable dt = idb.GetTablesColumnsList(tableName, Utils.dbName);
            dt.Columns.Remove("创建时间");
            dt.Columns.Remove("更改时间");
            dt.Columns.Remove("小数位数");
            dt.Columns.Remove("精度");

            dgv.DataSource = dt;
        }
        public void BinTableRelation(DataGridView dgv, string tableName)
        {
            DataTable dt = idb.GetTableRelation(tableName, Utils.dbName);
            dgv.DataSource = dt;
        }

        public bool UpdateColumns(string filePath)
        {
            DataTable distinct = MyXlsHelper.GetData(filePath).Tables[0];
            if (distinct == null) return false;
            if (distinct.Columns.Contains("字段名称") && distinct.Columns.Contains("字段说明"))
            {
                DataTable dt = BLLFactory.Instance.idb.GetTablesColumnsList(Utils.dbName, "");
                foreach (DataRow dict in distinct.Rows)
                {
                    string dicoumName = dict["字段名称"].ToString();
                    string dicoumInfo = dict["字段说明"].ToString();
                    foreach (DataRow row in dt.Rows)
                    {
                        string tableName = row["表名"].ToString();
                        string columnName = row["字段名"].ToString().Trim().ToLower();
                        string columnInfoInfo = row["字段说明"].ToString();
                        if (dicoumName.Equals(columnName))
                        {
                            if (!string.IsNullOrEmpty(columnInfoInfo))
                            {
                                //存在就更新
                                BLLFactory.Instance.idb.ExtendedProperty(false, dicoumInfo, tableName, columnName);
                            }
                            else
                            {
                                BLLFactory.Instance.idb.ExtendedProperty(true, dicoumInfo, tableName, columnName);
                            }
                        }
                    }
                }
                return true;
            }
            else
            {
                // MessageBox.Show("xls文件里面必须包含 字段名称，字段说明 这两列，并且文件工作表的名字必须是Sheet1$ ");
                return false;
            }
        }
    }
}
