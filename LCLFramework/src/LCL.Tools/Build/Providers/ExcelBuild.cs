using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCL.Tools
{
    public class ExcelBuild
    {
        public void GenerateLibBuild(string path)
        {
            List<TableModel> tableNames = BLLFactory.Instance.idb.GetTableModelList(Utils.dbName, true);
            for (int i = 0; i < tableNames.Count; i++)
            {
                TableModel tm = tableNames[i];
                string tablename = tm.TableName;
                string tableInfo = tm.TableNameRemark;

                DataTable dt = new DataTable();
                dt.TableName = tableInfo;
                foreach (var item in tm.Columns)
                {
                    if (!dt.Columns.Contains(item.ColumnRemark))
                    { 
                        dt.Columns.Add(item.ColumnRemark, typeof(string));
                    }
                }

                if (path.Length > 0)
                {
                    string folder = path + @"\LCL\Excel\";
                    Utils.FolderCheck(folder);
                    string filename = folder + tableInfo + "_Excel.xls";
                    MyExcelUtls.DataTable2Sheet(filename, dt);
                }
            }
        }
    }
}
