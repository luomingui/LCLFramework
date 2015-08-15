using System.Collections.Generic;
using System.Data;
using LCL.Tools.Data;
using LCL.Data;

namespace LCL.Tools
{
    public interface IDbo
    {
        DataBaseModel GetDatabaseModel( bool flg = false);
        //TableModel GetTableModel(string TableName);
        //List<TableModel> GetTableModelList(bool flg = false);
    }

    public class DALFactory
    {
        public static IDbo Factory(DbSetting dbSetting)
        {
            IDbo idb = null;
            idb = new Generate2008Sql(dbSetting);
            return idb;
        }
    }
}
