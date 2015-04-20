
using System.Collections.Generic;
using System.Data;
using LCL.Tools.Data;

namespace LCL.Tools
{
    public interface IDbo
    {
        List<DataBaseModel> GetDBList( );
        DataBaseModel GetDatabaseModel(string dbName, bool flg = false);
        TableModel GetTableModel(string TableName, string dbName = "master");
        List<TableModel> GetTableModelList(string DbName, bool flg = false);
        List<TableColumn> GetColumnsList(string TableName, string DbName, out bool istree);

        void AddTableByKey(string DbName);
        DataTable GetEqualColumnsList(string DbName);
        DataTable GetTablesColumnsList(string TableName, string DbName);
        DataTable GetTableRelation(string TableName, string DbName);
        void ExtendedProperty(bool addbyupdate, string describe, string tableName, string columnName);
    }

    public class DALFactory
    {
        public static IDbo Factory( )
        {
            IDbo idb = null;
            switch (Utils.DataBaseType)
            {
                case DataBaseType.Sql2000:
                    idb = new Generate2000Sql();
                    break;
                case DataBaseType.Sql2005:
                    idb = new Generate2005Sql();
                    break;
                case DataBaseType.Sql2008:
                    idb = new Generate2008Sql();
                    break;
                default:
                    break;
            }
            return idb;
        }
    }
}
