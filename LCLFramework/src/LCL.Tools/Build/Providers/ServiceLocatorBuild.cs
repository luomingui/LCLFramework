using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SF.Tools
{
    public class ServiceLocatorBuild
    {
        public void BuildServiceLocator(string path)
        {
            List<TableModel> tableNames = BLLFactory.Instance.idb.GetTableModelList(Utils.dbName, true);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < tableNames.Count; i++)
            {
                TableModel tm = tableNames[i];
                string tablename = tm.TableName;
                string tableInfo = tm.TableNameRemark;
                builder.AppendLine("ServiceLocator.Instance.Register<IRepository<" + tablename + ">, EntityFrameworkRepository<" + tablename + ">>();");
            }
            if (path.Length > 0)
            {
                string folder = path + @"\Entities\";
                Utils.FolderCheck(folder);
                string filename = folder + @"\" + Utils.dbName + "ICO.cs";
                Utils.CreateFiles(filename, builder.ToString());
            }
        }
    }
}
