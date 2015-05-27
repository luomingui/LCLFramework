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

                if (tablename == "__MigrationHistory" && tablename == "sysdiagrams")
                {
                    continue;
                }

                string fileListPath = path + @"\LCL\ViewModels\" + tablename + @"\Index.cshtml";
                string fileAddOrEditPath = path + @"\LCL\ViewModels\" + tablename + @"\AddOrEdit.cshtml";
                Utils.FolderCheck(path + @"\LCL\ViewModels\" + tablename);

                StringBuilder builder = new StringBuilder();

                

                
            }
        }
    }
}
