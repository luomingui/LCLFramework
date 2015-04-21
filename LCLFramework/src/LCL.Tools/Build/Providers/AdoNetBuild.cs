using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LCL.Tools
{
    public class AdoNetBuild
    {
        public void GenerateDAL(string path)
        {
                List<TableModel> tableNames = BLLFactory.Instance.idb.GetTableModelList(Utils.dbName, true);
                for (int i = 0; i < tableNames.Count; i++)
                {
                    TableModel tm = tableNames[i];
                    string tablename = tm.TableName;
                    string tableInfo = tm.TableNameRemark;
                    var ls= tm.Columns.Select<TableColumn,string>(e => e.ColumnName);
                    string[] arr=ls.ToArray();
                    string insert = string.Join(",", arr);
                    List<string> _str = new List<string>();
                    for (int j = 0; j < arr.Length; j++)
                    {
                        _str.Add("@" + arr[j]);
                    }
                    arr= _str.ToArray();
                    string _insert = string.Join(",", arr);

                    StringBuilder builder = new StringBuilder();
                    builder.AppendLine("       public int Add" + tablename + "(" + tablename + " entity) ");
                    builder.AppendLine("        { ");
                    builder.AppendLine("            string sql = @\"INSERT INTO (" + insert + ")VALUES(" + _insert + ")\"; ");
                    builder.AppendLine(" ");
                
                    foreach (var item in tm.Columns)
                    {
                       
                        builder.AppendLine("            sql = SqlUtil.setString(sql, \"@" + item.ColumnName + "\", entity." + item.ColumnName + "); ");
                    }

                    builder.AppendLine("       ");
                    builder.AppendLine("            int ret = dba.ExecuteText(sql); ");
                    builder.AppendLine(" ");
                    builder.AppendLine("            return ret; ");
                    builder.AppendLine(" ");
                    builder.AppendLine("        } ");

                    if (path.Length > 0)
                    {
                        string folder = path + @"\LCL\DAL\";
                        Utils.FolderCheck(folder);
                        string filename = folder + @"\" + tablename + ".cs";
                        Utils.CreateFiles(filename, builder.ToString());
                    }
      
                }


        }
    }
}
