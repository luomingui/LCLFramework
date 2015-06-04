using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LCL.Tools
{
    public class MVCViewModelBuild
    {
        public void GenerateEntityViewsModeel(string path)
        {
            List<TableModel> tableNames = BLLFactory.Instance.idb.GetTableModelList(Utils.dbName, true);
            for (int i = 0; i < tableNames.Count; i++)
            {
                TableModel tm = tableNames[i];
                string tablename = tm.TableName;
                string tableInfo = tm.TableNameRemark;

                if (tablename == "__MigrationHistory" || tablename == "sysdiagrams")
                {
                    continue;
                }

                StringBuilder builder = new StringBuilder();
                builder.AppendLine("using System.Collections.Generic; ");
                builder.AppendLine("using UIShell.RbacPermissionService; ");
                builder.AppendLine(" ");
                builder.AppendLine("namespace LCL.MvcExtensions ");
                builder.AppendLine("{ ");
                builder.AppendLine("   public class " + tablename + "AddOrEditViewModel : AddOrEditViewModel<" + tablename + "> ");
                builder.AppendLine("    { ");
                builder.AppendLine("        ");
                builder.AppendLine("    } ");

                builder.AppendLine("   public class " + tablename + "PagedListViewModel : PagedListViewModel<" + tablename + "> ");
                builder.AppendLine("    { ");
                builder.AppendLine("        public " + tablename + "PagedListViewModel(int currentPageNum, int pageSize, List<" + tablename + "> allModels) ");
                builder.AppendLine("            : base(currentPageNum, pageSize, allModels) ");
                builder.AppendLine("        { ");
                builder.AppendLine(" ");
                builder.AppendLine("        } ");
                builder.AppendLine("    } ");
                builder.AppendLine(" ");
                builder.AppendLine("} ");
                if (path.Length > 0)
                {
                    string folder = path + @"\LCL\ViewModels\";
                    Utils.FolderCheck(folder);
                    string filename = folder + @"\" + tablename + "ViewModel.cs";
                    Utils.CreateFiles(filename, builder.ToString());
                }
            }
        }
    }
}
