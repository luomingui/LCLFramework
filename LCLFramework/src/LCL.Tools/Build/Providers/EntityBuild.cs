using LCL.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LCL.Tools
{
    public class EntityBuild
    {
        public void BuildEntity(string path)
        {
            List<TableModel> tableNames = BLLFactory.Instance.idb.GetTableModelList(Utils.dbName, true);

            for (int i = 0; i < tableNames.Count; i++)
            {
                TableModel tm = tableNames[i];
                string tablename = tm.TableName;
                string tableInfo = tm.TableNameRemark;

                StringBuilder builder = new StringBuilder();
                builder.AppendLine("using LCL.DomainEntitys; ");
                builder.AppendLine("using System; ");
                builder.AppendLine("using System.Collections.Generic; ");
                builder.AppendLine("using System.Linq; ");
                builder.AppendLine("using System.Text; ");
                builder.AppendLine("using System.Threading.Tasks; ");
                builder.AppendLine(" ");
                builder.AppendLine("namespace " + Utils.NameSpace + " ");
                builder.AppendLine("{ ");
                builder.AppendLine("    /// <summary> ");
                builder.AppendLine("    /// " + tableInfo + " ");
                builder.AppendLine("    /// </summary> ");
                builder.AppendLine("    public partial class " + tablename + " : BaseModel ");
                builder.AppendLine("    { ");
                builder.AppendLine("        public " + tablename + "() ");
                builder.AppendLine("        { ");
                builder.AppendLine("             ");
                builder.AppendLine("        } ");
                foreach (TableColumn column in tm.Columns)
                {
                    builder.AppendLine("        /// <summary> ");
                    builder.AppendLine("        /// " + column.ColumnRemark+ " ");
                    builder.AppendLine("        /// </summary> ");
                    builder.AppendLine("        public " + column.ColumnType + " " + column.ColumnName + " { get; set; } ");
                }
                builder.AppendLine("        ");
                builder.AppendLine(" ");
                builder.AppendLine(" ");
                builder.AppendLine("    } ");
                builder.AppendLine("} ");

                if (path.Length > 0)
                {
                    string folder = path + @"\LCL\Entity\";
                    Utils.FolderCheck(folder);
                    string filename = folder + @"\" + tablename + ".cs";
                    Utils.CreateFiles(filename, builder.ToString());
                }
            }
        }
    }
}
