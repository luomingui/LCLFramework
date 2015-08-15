
using LCL.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace LCL.Tools.Data
{
    /// <summary>
    /// 数据库的元数据读取器
    /// </summary>
    public class GenerateSql : IDbo
    {
        private DbSetting _dbSetting;
        private IDbAccesser _db;
        public INIFile datatype = null;
        public GenerateSql(DbSetting dbSetting)
        {
            if (dbSetting == null) throw new ArgumentNullException("dbSetting");
            this._dbSetting = dbSetting;
            this._db = new DbAccesser(dbSetting);
            datatype = new INIFile();
        }
        public IDbAccesser Db
        {
            get { return this._db; }
        }

        #region sql
        //sql 2005 8
        public string sql_tablesInfos = @"SELECT CASE WHEN a.colorder = 1 THEN d.name ELSE '' END AS 表名,  CASE WHEN a.colorder = 1 THEN isnull(f.value, '') ELSE '' END AS 表说明, a.colorder AS 字段序号, a.name AS 字段名,ISNULL(g.[value], '') AS 字段说明, CASE WHEN COLUMNPROPERTY(a.id, a.name, 'IsIdentity') = 1 THEN '√' ELSE '' END AS 标识,  CASE WHEN EXISTS(SELECT 1  FROM dbo.sysindexes si INNER JOIN  dbo.sysindexkeys sik ON si.id = sik.id AND si.indid = sik.indid INNER JOIN dbo.syscolumns sc ON sc.id = sik.id AND sc.colid = sik.colid INNER JOIN  dbo.sysobjects so ON so.name = si.name AND so.xtype = 'PK' WHERE sc.id = a.id AND sc.colid = a.colid) THEN '√' ELSE '' END AS 主键,  b.name AS 类型, a.length AS 长度, COLUMNPROPERTY(a.id, a.name, 'PRECISION')AS 精度, ISNULL(COLUMNPROPERTY(a.id, a.name, 'Scale'), 0) AS 小数位数,CASE WHEN a.isnullable = 1 THEN '√' ELSE '' END AS 允许空, ISNULL(e.text, '') AS 默认值 FROM dbo.syscolumns a LEFT OUTER JOIN    dbo.systypes b ON a.xtype = b.xusertype INNER JOIN dbo.sysobjects d ON a.id = d.id AND d.xtype = 'U' AND   d.status >= 0 LEFT OUTER JOIN   dbo.syscomments e ON a.cdefault = e.id LEFT OUTER JOIN sys.extended_properties g ON a.id = g.major_id AND a.colid = g.minor_id AND  g.name = 'MS_Description' LEFT OUTER JOIN  sys.extended_properties f ON d.id = f.major_id AND f.minor_id = 0 AND f.name = 'MS_Description' where d.name<>'dtproperties' AND d.name<>'__MigrationHistory' ";
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
        public DataBaseModel GetDatabaseModel(bool flg = true)
        {
            DataBaseModel model = new DataBaseModel();
            model.DatabaseName = _dbSetting.Database;
            List<TableModel> tmList = this.GetTableModelList(flg);
            model.Tables = tmList;
            return model;
        }
        private List<TableModel> GetTableModelList(bool flg = true)
        {
            List<TableModel> list = new List<TableModel>();
            DataTable dt = this.GetTablesColumnsList("");
            DataRow[] drs = dt.Select(TableFlg.表名.ToString());
            for (int i = 0; i < drs.Length; i++)
            {
                TableModel tm = new TableModel();
                tm.TableName = drs[i]["表名"].ToString();
                tm.TableNameRemark = drs[i]["表说明"].ToString();
                tm.TablePK = this.GetTableAlone(tm.TableName, TableFlg.主键列);
                bool istree = false;
                tm.Columns = this.GetColumnsList(tm.TableName, out istree);
                tm.IsTree = istree;
                list.Add(tm);
            }
            return list;
        }
        //public TableModel GetTableModel(string TableName)
        //{
        //    TableModel model = new TableModel();
        //    model.TableName = TableName;
        //    model.TableNameRemark = this.GetTableAlone(model.TableName, TableFlg.表说明);
        //    model.TablePK = this.GetTableAlone(model.TableName, TableFlg.主键列);
        //    bool istree = false;
        //    List<TableColumn> columnsList = this.GetColumnsList(TableName, out istree);
        //    model.Columns = columnsList;
        //    model.IsTree = istree;
        //    return model;
        //}
        public List<TableColumn> GetColumnsList(string TableName, out bool istree)
        {
            istree = false;
            DataTable dtcolumns = this.GetTablesColumnsList(TableName);
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
                if (column.ColumnName.ToLower() == "parentid")
                {
                    istree = true;
                }
                if (!arrlist.Contains(column.ColumnName))
                    columnsList.Add(column);
            }
            return columnsList;
        }
        private DataTable GetTablesColumnsList(string TableName)
        {
            string colsSql = "";
            if (TableName.Length > 0)
                colsSql = this.sql_tablesInfos + " and UPPER(d .name) = '" + TableName.ToUpper() + "'";
            else
                colsSql = this.sql_tablesInfos;
            return Db.QueryDataTable(colsSql);
        }
        protected string GetTableAlone(string TableName, string flg)
        {
            string temp = "";
            DataTable dt = this.GetTablesColumnsList(TableName);
            DataRow[] drs = dt.Select(flg.ToString());
            if (drs.Length > 0)
                temp = drs[0][0].ToString();
            else
                temp = TableName;
            return temp;
        }

        ArrayList arrlist = new ArrayList { 
        "ID","IsDelete","AddDate","UpdateDate"
        };
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
