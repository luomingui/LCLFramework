using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LCL.Tools
{
    public class ValidationModelBuild
    {
        public void GenerateValidationModel(string path)
        {
            List<TableModel> tableNames = BLLFactory.Instance.idb.GetTableModelList(Utils.dbName, true);
            for (int i = 0; i < tableNames.Count; i++)
            {
                TableModel tm = tableNames[i];
                string tablename = tm.TableName;
                string tableInfo = tm.TableNameRemark;
                if (tablename == "__MigrationHistory" && tablename == "sysdiagrams")
                {
                    continue;
                }
                StringBuilder builder = new StringBuilder();
                builder.AppendLine("using System; ");
                builder.AppendLine("using System.ComponentModel.DataAnnotations; ");
                builder.AppendLine("using System.Linq; ");
                builder.AppendLine("using System.Text; ");
                builder.AppendLine(" ");
                builder.AppendLine("namespace UIShell.RbacPermissionService ");
                builder.AppendLine("{ ");
                builder.AppendLine("    [Serializable] ");
                builder.AppendLine("    [MetadataType(typeof(" + tablename + "MD))]  ");
                builder.AppendLine("    public partial class " + tablename + "  ");
                builder.AppendLine("    {  ");
                builder.AppendLine("        public class " + tablename + "MD  ");
                builder.AppendLine("        {  ");
                foreach (var item in tm.Columns)
                {
                    if (item.ColumnName != "ID" && item.ColumnName != "AddDate" && item.ColumnName != "UpdateDate")
                    {
                        builder.AppendLine("            [Display(Name = \"" + item.ColumnRemark + "\")]  ");
                        builder.AppendLine("            public " + item.ColumnType + " " + item.ColumnName + " { get; set; }  ");
                    }
                }
                builder.AppendLine("        }  ");
                builder.AppendLine("    }  ");
                builder.AppendLine(" ");
                builder.AppendLine("} ");

                if (path.Length > 0)
                {
                    string folder = path + @"\LCL\ValidationModel\";
                    Utils.FolderCheck(folder);
                    string filename = folder + @"\" + tablename + ".cs";
                    Utils.CreateFiles(filename, builder.ToString());
                }
            }
        }
        
    }
}
