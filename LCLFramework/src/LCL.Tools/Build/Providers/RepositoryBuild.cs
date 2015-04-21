using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LCL.Tools
{
    public class RepositoryBuild
    {
        public void BuildRepository(string path)
        {
            List<TableModel> tableNames = BLLFactory.Instance.idb.GetTableModelList(Utils.dbName, true);
            for (int i = 0; i < tableNames.Count; i++)
            {
                TableModel tm = tableNames[i];
                string tablename = tm.TableName;
                string tableInfo = tm.TableNameRemark;

                StringBuilder builder = new StringBuilder();
                builder.AppendLine("/*******************************************************  ");
                builder.AppendLine("*   ");
                builder.AppendLine("* 作者：罗敏贵  ");
                builder.AppendLine("* 说明： " + tableInfo + " ");
                builder.AppendLine("* 运行环境：.NET 4.5.0  ");
                builder.AppendLine("* 版本号：1.0.0  ");
                builder.AppendLine("*   ");
                builder.AppendLine("* 历史记录：  ");
                builder.AppendLine("*    创建文件 罗敏贵 " + DateTime.Now.ToLongDateString() + " ");
                builder.AppendLine("*   ");
                builder.AppendLine("*******************************************************/  ");
                builder.AppendLine("using LCL.Repositories;  ");
                builder.AppendLine("using LCL.Repositories.EntityFramework;  ");
                builder.AppendLine("using System;  ");
                builder.AppendLine("using System.Collections.Generic;  ");
                builder.AppendLine("using System.Linq;  ");
                builder.AppendLine("using System.Text;  ");
                builder.AppendLine("using System.Threading.Tasks;  ");
                builder.AppendLine("  ");
                builder.AppendLine("namespace " + Utils.NameSpace + "  ");
                builder.AppendLine("{  ");
                builder.AppendLine("    public interface I" + tablename + "Repository : IRepository<" + tablename + ">  ");
                builder.AppendLine("    {  ");
                builder.AppendLine("  ");
                builder.AppendLine("    }  ");
                builder.AppendLine("    public class " + tablename + "Repository : EntityFrameworkRepository<" + tablename + ">, I" + tablename + "Repository  ");
                builder.AppendLine("    {  ");
                builder.AppendLine("        public " + tablename + "Repository(IRepositoryContext context)  ");
                builder.AppendLine("            : base(context)  ");
                builder.AppendLine("        {   ");
                builder.AppendLine("          ");
                builder.AppendLine("        }  ");
                builder.AppendLine("    }  ");
                builder.AppendLine("}  ");

                if (path.Length > 0)
                {
                    string folder = path + @"\LCL\Repository\";
                    Utils.FolderCheck(folder);
                    string filename = folder + @"\" + tablename + "Repository.cs";
                    Utils.CreateFiles(filename, builder.ToString());
                }
            }
     
        }
    }
}
