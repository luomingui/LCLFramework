
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace LCL.Tools.Data
{
    public class GenerateSql : DbHelperSQL, IDbo
    {
        #region sql
        //sql 2005 8
        public string sql_tablesInfos = @"SELECT CASE WHEN a.colorder = 1 THEN d.name ELSE '' END AS 表名, 
CASE WHEN a.colorder = 1 THEN isnull(f.value, '') ELSE '' END AS 表说明, 
a.colorder AS 字段序号, 
a.name AS 字段名,ISNULL(g.[value], '') AS 字段说明, 
CASE WHEN COLUMNPROPERTY(a.id, a.name, 'IsIdentity') = 1 THEN '√' ELSE '' END AS 标识, 
CASE WHEN EXISTS(SELECT 1  FROM dbo.sysindexes si INNER JOIN 
dbo.sysindexkeys sik ON si.id = sik.id AND si.indid = sik.indid INNER JOIN dbo.syscolumns sc 
ON sc.id = sik.id AND sc.colid = sik.colid INNER JOIN  dbo.sysobjects so 
ON so.name = si.name AND so.xtype = 'PK' WHERE sc.id = a.id AND sc.colid = a.colid) THEN '√' ELSE '' END AS 主键, 
b.name AS 类型, a.length AS 长度, COLUMNPROPERTY(a.id, a.name, 'PRECISION')AS 精度, 
ISNULL(COLUMNPROPERTY(a.id, a.name, 'Scale'), 0) AS 小数位数,CASE WHEN a.isnullable = 1 THEN '√' ELSE '' END AS 允许空, 
ISNULL(e.text, '') AS 默认值 FROM dbo.syscolumns a LEFT OUTER JOIN   
dbo.systypes b ON a.xtype = b.xusertype INNER JOIN dbo.sysobjects d ON a.id = d.id AND d.xtype = 'U' AND  
d.status >= 0 LEFT OUTER JOIN   dbo.syscomments e ON a.cdefault = e.id LEFT OUTER JOIN sys.extended_properties g 
ON a.id = g.major_id AND a.colid = g.minor_id AND  g.name = 'MS_Description' LEFT OUTER JOIN  
sys.extended_properties f ON d.id = f.major_id AND f.minor_id = 0 AND f.name = 'MS_Description' 
where d.name<>'dtproperties' AND d.name<>'__MigrationHistory' ";

        public string sql_tables = @"select name from sysdatabases";
        public string sql_tablerelation = @"SELECT 
外键表名称 = object_name(b.fkeyid) ,
外键列名   = (SELECT name FROM syscolumns WHERE colid = b.fkey AND id = b.fkeyid) ,
主键表名   = object_name(b.rkeyid) ,
主键列名   = (SELECT name FROM syscolumns WHERE colid = b.rkey AND id = b.rkeyid) ,
级联更新   = ObjectProperty(a.id,'CnstIsUpdateCascade') ,
级联删除   = ObjectProperty(a.id,'CnstIsDeleteCascade') 
FROM sysobjects a 
JOIN sysforeignkeys b ON a.id = b.constid 
JOIN sysobjects c ON a.parent_obj = c.id 
WHERE a.xtype = 'F' AND c.xtype = 'U'";
        #endregion

        #region Model
        public List<DataBaseModel> GetDBList()
        {
            List<DataBaseModel> dbmList = new List<DataBaseModel>();
            DataTable dt = GetDBTableNameList();
            foreach (DataRow row in dt.Rows)
            {
                string dbName = row["name"].ToString();
                DataBaseModel dbm = new DataBaseModel();
                dbm.DatabaseName = dbName;
                dbmList.Add(dbm);
            }
            return dbmList;
        }
        public DataBaseModel GetDatabaseModel(string dbName, bool flg = false)
        {
            DataBaseModel model = new DataBaseModel();
            model.DatabaseName = dbName;
            List<TableModel> tmList = this.GetTableModelList(dbName, flg);
            model.Tables = tmList;
            return model;
        }
        public TableModel GetTableModel(string TableName, string dbName = "master")
        {
            TableModel model = new TableModel();
            model.TableName = TableName;
            model.TableNameRemark = this.GetTableAlone(dbName, model.TableName, TableFlg.表说明);
            model.TablePK = this.GetTableAlone(dbName, model.TableName, TableFlg.主键列);
            bool istree = false;
            List<TableColumn> columnsList = this.GetColumnsList(TableName, dbName, out istree);
            model.Columns = columnsList;
            model.IsTree = istree;
            return model;
        }
        public List<TableModel> GetTableModelList(string DbName, bool flg = false)
        {
            List<TableModel> list = new List<TableModel>();
            DataTable dt = this.GetTablesColumnsList("", DbName);
            DataRow[] drs = dt.Select(TableFlg.表名.ToString());
            for (int i = 0; i < drs.Length; i++)
            {
                TableModel tm = new TableModel();
                tm.TableName = drs[i]["表名"].ToString();
                tm.TableNameRemark = drs[i]["表说明"].ToString();
                tm.TablePK = this.GetTableAlone(DbName, tm.TableName, TableFlg.主键列);
                bool istree = false;
                if (flg)
                    tm.Columns = this.GetColumnsList(tm.TableName, DbName, out istree);
                tm.IsTree = istree;
                list.Add(tm);
            }
            return list;
        }
        public List<TableColumn> GetColumnsList(string TableName, string DbName, out bool istree)
        {
            istree = false;
            DataTable dtcolumns = this.GetTablesColumnsList(TableName, DbName);
            List<TableColumn> columnsList = new List<TableColumn>();
            foreach (DataRow row in dtcolumns.Rows)
            {
                TableColumn column = new TableColumn();
                column.ColumnName = row["字段名"].ToString();
                column.ColumnRemark = row["字段说明"].ToString();
                column.DefaultValue = row["默认值"].ToString();
                if (column.ColumnRemark.Length == 0) column.ColumnRemark = column.ColumnName;
                column.ColumnType = datatype.IniReadValue("DbToCS", row["类型"].ToString());
                if (row["主键"].ToString().Length > 0)
                {
                    column.PK = true;
                }
                if (row["标识"].ToString().Length > 0)
                {
                    column.Identifying = true;
                }
                if (int.Parse(row["长度"].ToString()) > 1)
                {
                    column.MaxLength = int.Parse(row["长度"].ToString());
                }
                if (row["允许空"].ToString().Length > 0)
                {
                    column.Isnullable = true;
                }
                columnsList.Add(column);
                if (column.ColumnName.ToLower() == "parentid")
                {
                    istree = true;
                }
            }
            return columnsList;
        }
        public virtual void ExtendedProperty(bool addbyupdate, string describe, string tableName, string columnName)
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
        public virtual void AddTableByKey(string DbName)
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
        #endregion

        #region DataTable
        protected DataTable GetDBTableNameList()
        {
            string sQLString = sql_tables;
            DataTable dtdbo = this.Query("master", sQLString).Tables[0];
            DataTable dtDbo = dtdbo.Copy();
            for (int i = 0; i < dtDbo.Rows.Count; i++)
            {
                string tableName = dtDbo.Rows[i]["name"].ToString();
                if (string.IsNullOrEmpty(tableName)) continue;
                if (tableName.ToLower().Trim().Equals("master")
                    || tableName.ToLower().Trim().Equals("tempdb")
                    || tableName.ToLower().Trim().Equals("msdb")
                    || tableName.ToLower().Trim().Equals("model")
                    || tableName.ToLower().Trim().Equals("reportserver")
                    || tableName.ToLower().Trim().Equals("reportservertempdb"))
                {
                    dtDbo.Rows[i].Delete();
                }
            }
            dtDbo.AcceptChanges();
            return dtDbo;
        }
        public DataTable GetTablesColumnsList(string TableName, string DbName)
        {
            string colsSql = "";
            if (TableName.Length > 0)
                colsSql = this.sql_tablesInfos + " and UPPER(d .name) = '" + TableName.ToUpper() + "'";
            else
                colsSql = this.sql_tablesInfos;

            return this.Query(DbName, colsSql).Tables[0];
        }
        public DataTable GetEqualColumnsList(string DbName)
        {//获取数据库里相同的字段名称
            string colsSql = @"SELECT
               DISTINCT a.name AS 字段名称,'' AS 字段说明
        FROM    dbo.syscolumns a
                LEFT OUTER JOIN dbo.systypes b ON a.xtype = b.xusertype
                INNER JOIN dbo.sysobjects d ON a.id = d.id
                                               AND d.xtype = 'U'
                                               AND d.status >= 0
                LEFT OUTER JOIN dbo.syscomments e ON a.cdefault = e.id
                LEFT OUTER JOIN sys.extended_properties g ON a.id = g.major_id
                                                             AND a.colid = g.minor_id
                                                             AND g.name = 'MS_Description'
                LEFT OUTER JOIN sys.extended_properties f ON d.id = f.major_id
                                                             AND f.minor_id = 0
                                                             AND f.name = 'MS_Description' ";
            DataTable dt = new DataTable();
            dt.TableName = "EqualColumns";
            SqlDataReader reader = this.ExecuteReader(DbName, colsSql.ToString());
            dt.Load(reader);
            reader.Close();
            return dt;
        }
        protected string GetTableAlone(string DbName, string TableName, string flg)
        {
            string temp = "";
            DataTable dt = this.GetTablesColumnsList(TableName, DbName);
            DataRow[] drs = dt.Select(flg.ToString());
            if (drs.Length > 0)
                temp = drs[0].ToString();
            else
                temp = TableName;
            return temp;
        }
        protected DataTable GetTablesColumnsWhereList(string DbName, string whereSql)
        {//表名全部显示
            string topSql = @"SELECT CASE WHEN a.colorder = 1 THEN d.name ELSE d.name END AS 表名,  CASE WHEN a.colorder = 1 THEN isnull(f.value, '') ELSE '' END AS 表说明, a.colorder AS 字段序号, a.name AS 字段名,ISNULL(g.[value], '') AS 字段说明, CASE WHEN COLUMNPROPERTY(a.id, a.name, 'IsIdentity') = 1 THEN '√' ELSE '' END AS 标识,  CASE WHEN EXISTS(SELECT 1  FROM dbo.sysindexes si INNER JOIN  dbo.sysindexkeys sik ON si.id = sik.id AND si.indid = sik.indid INNER JOIN dbo.syscolumns sc ON sc.id = sik.id AND sc.colid = sik.colid INNER JOIN  dbo.sysobjects so ON so.name = si.name AND so.xtype = 'PK' WHERE sc.id = a.id AND sc.colid = a.colid) THEN '√' ELSE '' END AS 主键,  b.name AS 类型, a.length AS 长度, COLUMNPROPERTY(a.id, a.name, 'PRECISION')AS 精度, ISNULL(COLUMNPROPERTY(a.id, a.name, 'Scale'), 0) AS 小数位数,CASE WHEN a.isnullable = 1 THEN '√' ELSE '' END AS 允许空, ISNULL(e.text, '') AS 默认值 FROM dbo.syscolumns a LEFT OUTER JOIN    dbo.systypes b ON a.xtype = b.xusertype INNER JOIN dbo.sysobjects d ON a.id = d.id AND d.xtype = 'U' AND   d.status >= 0 LEFT OUTER JOIN   dbo.syscomments e ON a.cdefault = e.id LEFT OUTER JOIN sys.extended_properties g ON a.id = g.major_id AND a.colid = g.minor_id AND  g.name = 'MS_Description' LEFT OUTER JOIN  sys.extended_properties f ON d.id = f.major_id AND f.minor_id = 0 AND  f.name = 'MS_Description' AND d.name<>'__MigrationHistory' ";

            string colsSql = topSql;
            if (whereSql.Length > 0)
            {
                colsSql = topSql + whereSql;
            }
            DataTable dt = new DataTable();
            dt.TableName = "TablesColumns";
            SqlDataReader reader = this.ExecuteReader(DbName, colsSql.ToString());
            dt.Load(reader);
            reader.Close();
            return dt;
        }
        public DataTable GetTableRelation(string TableName, string DbName)
        {
            string colsSql = "";
            if (TableName.Length > 0)
                colsSql = this.sql_tablerelation + " and object_name(b.fkeyid) = '" + TableName.ToUpper() + "'";
            else
                colsSql = this.sql_tablerelation;

            return this.Query(DbName, colsSql).Tables[0];
        }
        #endregion


    }

    public class TableFlg
    {
        public static readonly string 表名 = "Len(表名)>0";
        public static readonly string 表说明 = "Len(表说明)>0";
        public static readonly string 主键列 = "Len(主键)>0";
        public static readonly string 标识列 = "Len(标识)>0";
    }
}
