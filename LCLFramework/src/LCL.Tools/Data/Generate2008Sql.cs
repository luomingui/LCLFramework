
using System.Data;
namespace LCL.Tools.Data
{
    public class Generate2008Sql : GenerateSql
    {
        public override void AddTableByKey(string DbName)
        {
            DataTable dt = this.GetTablesColumnsWhereList(DbName, " WHERE a.name='id' ");
            foreach (DataRow row in dt.Rows)
            {
                //添加主键 ALTER TABLE dbo.Floor ADD PRIMARY KEY(ID) 
                string tableName = row["表名"].ToString().Trim().ToLower();
                string columnName = row["字段名"].ToString().Trim().ToLower();
                if (columnName.Equals("id"))
                {
                    string pkey = row["主键"].ToString();
                    if (pkey.Length == 0)
                    {
                        this.ExceSql("ALTER TABLE " + tableName + " ADD PRIMARY KEY(ID) ");
                    }
                    string identifying = row["标识"].ToString();
                    if (identifying.Length == 0)
                    {
                        //给ID列添加标识
                    }
                }
            }
        }
        public override void ExtendedProperty(bool addbyupdate, string describe, string tableName, string columnName)
        {
            string sql_propertyInfo = @"EXEC sys.{0} N'MS_Description',N'{1}',N'SCHEMA',N'dbo',N'TABLE',N'{2}',N'COLUMN',N'{3}'";
            string sql_propertyInfo1 = @"EXEC sys.{0} N'MS_Description',N'{1}',N'SCHEMA',N'dbo',N'TABLE',N'{2}'";
            string sql = "";
            if (addbyupdate)
            {
                if (columnName == null || columnName.Length == 0)
                {
                    sql = string.Format(sql_propertyInfo1, "sp_updateextendedproperty", describe, tableName);
                }
                else
                {
                    sql = string.Format(sql_propertyInfo, "sp_updateextendedproperty", describe, tableName, columnName);
                }
            }
            else
            {
                if (columnName == null || columnName.Length == 0)
                {
                    sql = string.Format(sql_propertyInfo1, "sp_addextendedproperty", describe, tableName);
                }
                else
                {
                    sql = string.Format(sql_propertyInfo, "sp_addextendedproperty", describe, tableName, columnName);
                }
            }
            this.ExceSql(sql);
        }
    }
}
