using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LCL.Tools
{
    public class ServiceLocatorBuild
    {
        public void BuildServiceLocator(string path)
        {
            List<TableModel> tableNames = BLLFactory.Instance.idb.GetTableModelList(Utils.dbName, true);
            StringBuilder builder = new StringBuilder();
            StringBuilder ben = new StringBuilder();
            StringBuilder end = new StringBuilder();

            for (int i = 0; i < tableNames.Count; i++)
            {
                TableModel tm = tableNames[i];
                string tablename = tm.TableName;
                string tableInfo = tm.TableNameRemark;
                if (tablename != "__MigrationHistory" && tablename != "sysdiagrams")
                {
                    ben.AppendLine("ServiceLocator.Instance.Register<IRepository<" + tablename + ">, EntityFrameworkRepository<" + tablename + ">>();");

                    end.AppendLine("ServiceLocator.Instance.Register<I" + tablename + "Repository, " + tablename + "Repository>();");
                }
            }
            builder.AppendLine(" #region 默认仓库 ");
            builder.AppendLine(ben.ToString());
            builder.AppendLine(" #endregion");
            builder.AppendLine(" #region 扩展仓库 ");
            builder.AppendLine(end.ToString());
            builder.AppendLine(" #endregion");
            if (path.Length > 0)
            {
                string folder = path + @"\LCL\";
                Utils.FolderCheck(folder);
                string filename = folder + @"\" + Utils.dbName + "ICO.cs";
                Utils.CreateFiles(filename, builder.ToString());
            }
        }
    }
}
