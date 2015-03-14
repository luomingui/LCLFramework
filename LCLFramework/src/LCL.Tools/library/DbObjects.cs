
namespace SF.Tools
{
    //[System.Diagnostics.DebuggerStepThrough]
    //    public class DbObjects
    //    {
    //        public INIFile datatype = null;
    //        private SqlConnection connect;
    //        private string _dbconnectStr;
    //        private string queryFieldStr = "SELECT CASE WHEN a.colorder = 1 THEN d.name ELSE '' END AS 表名,  CASE WHEN a.colorder = 1 THEN isnull(f.value, '') ELSE '' END AS 表说明, a.colorder AS 字段序号, a.name AS 字段名,ISNULL(g.[value], '') AS 字段说明, CASE WHEN COLUMNPROPERTY(a.id, a.name, 'IsIdentity') = 1 THEN '√' ELSE '' END AS 标识,  CASE WHEN EXISTS(SELECT 1  FROM dbo.sysindexes si INNER JOIN  dbo.sysindexkeys sik ON si.id = sik.id AND si.indid = sik.indid INNER JOIN dbo.syscolumns sc ON sc.id = sik.id AND sc.colid = sik.colid INNER JOIN  dbo.sysobjects so ON so.name = si.name AND so.xtype = 'PK' WHERE sc.id = a.id AND sc.colid = a.colid) THEN '√' ELSE '' END AS 主键,  b.name AS 类型, a.length AS 长度, COLUMNPROPERTY(a.id, a.name, 'PRECISION')AS 精度, ISNULL(COLUMNPROPERTY(a.id, a.name, 'Scale'), 0) AS 小数位数,CASE WHEN a.isnullable = 1 THEN '√' ELSE '' END AS 允许空, ISNULL(e.text, '') AS 默认值, d.crdate AS 创建时间, CASE WHEN a.colorder = 1 THEN d.refdate ELSE NULL END AS 更改时间 FROM dbo.syscolumns a LEFT OUTER JOIN    dbo.systypes b ON a.xtype = b.xusertype INNER JOIN dbo.sysobjects d ON a.id = d.id AND d.xtype = 'U' AND   d.status >= 0 LEFT OUTER JOIN   dbo.syscomments e ON a.cdefault = e.id LEFT OUTER JOIN sys.extended_properties g ON a.id = g.major_id AND a.colid = g.minor_id AND  g.name = 'MS_Description' LEFT OUTER JOIN  sys.extended_properties f ON d.id = f.major_id AND f.minor_id = 0 AND   f.name = 'MS_Description' where d.name<>'dtproperties'  ";

    //        public DbObjects(string connStr)
    //        {
    //            this.connect = new SqlConnection();
    //            this._dbconnectStr = connStr;
    //            this.connect.ConnectionString = this._dbconnectStr;
    //            Init();
    //        }
    //        public DbObjects(bool SSPI, string Ip, string User, string Pass)
    //        {
    //            this.connect = new SqlConnection();
    //            if (SSPI)
    //            {
    //                this._dbconnectStr = "Integrated Security=SSPI;Data Source=" + Ip + ";Initial Catalog=master";
    //            }
    //            else if (Pass == "")
    //            {
    //                this._dbconnectStr = "user id=" + User + ";initial catalog=master;data source=" + Ip + ";Connect Timeout=30";
    //            }
    //            else
    //            {
    //                this._dbconnectStr = "user id=" + User + ";password=" + Pass + ";initial catalog=master;data source=" + Ip + ";Connect Timeout=30";
    //            }
    //            this.connect.ConnectionString = this._dbconnectStr;

    //            Init();
    //        }
    //        private void Init( )
    //        {
    //            string path = Application.StartupPath + @"\datatype.ini";
    //            if (!File.Exists(path))
    //            {
    //                Utils.CreateFiles(path, Utils.ini);
    //            }
    //            datatype = new INIFile(path);
    //        }
    //        /// <summary>
    //        /// 获取数据库列表
    //        /// </summary>
    //        /// <returns></returns>
    //        public DataTable GetDBList( )
    //        {
    //            string sQLString = "select name from sysdatabases";
    //            DataTable dtdbo = this.Query("master", sQLString).Tables[0];
    //            DataTable dtDbo = dtdbo.Copy();
    //            for (int i = 0; i < dtDbo.Rows.Count; i++)
    //            {
    //                string tableName =dtDbo.Rows[i]["name"].ToString();
    //                if (string.IsNullOrEmpty(tableName)) continue;
    //                if (tableName.ToLower().Trim().Equals("master")
    //                    || tableName.ToLower().Trim().Equals("tempdb")
    //                    || tableName.ToLower().Trim().Equals("msdb")
    //                    || tableName.ToLower().Trim().Equals("model")
    //                    || tableName.ToLower().Trim().Equals("reportserver")
    //                    || tableName.ToLower().Trim().Equals("reportservertempdb"))
    //                {
    //                    dtDbo.Rows[i].Delete();
    //                }     
    //            }
    //            dtDbo.AcceptChanges();
    //            return dtDbo;
    //        }
    //        /// <summary>
    //        /// 获取用户表
    //        /// </summary>
    //        /// <param name="DbName"></param>
    //        /// <returns></returns>
    //        public DataTable GetTables(string DbName)
    //        { //Edmmetadata
    //            DataTable dtTables = null;
    //            string sQLString = "select [name] from sysobjects where xtype='U'and [name]<>'dtproperties' order by [name]";
    //            dtTables = this.Query(DbName, sQLString).Tables[0];
    //            // dtTables.Copy();
    //            foreach (DataRow row in dtTables.Rows)
    //            {
    //                string tableName = row["name"].ToString();
    //                if (tableName.ToLower().Equals("edmmetadata"))
    //                {
    //                    dtTables.Rows.Remove(row);
    //                    break;
    //                }
    //            }
    //            return dtTables;
    //        }
    //        public DataTable GetColumnInfoList(string DbName, string TableName)
    //        {
    //            string colsSql = "";
    //            if (TableName.Length > 0)
    //                colsSql = this.queryFieldStr + " and UPPER(d .name) = '" + TableName.ToUpper() + "'";
    //            else
    //                colsSql = this.queryFieldStr;
    //            DataTable dt = new DataTable();
    //            SqlDataReader reader = this.ExecuteReader(DbName, colsSql.ToString());
    //            dt.Load(reader);
    //            reader.Close();
    //            return dt;
    //        }

    //        public TableModel GetTableColumnList(string TableName)
    //        {
    //            TableModel model = new TableModel();
    //            model.TableName = TableName;
    //            model.TableNameRemark = this.GetTableInfo(Utils.dbName, model.TableName);
    //            model.TablePK = this.GetTablePkName(Utils.dbName, model.TableName);
    //            DataTable dtcolumns = this.GetColumnInfoList(Utils.dbName, TableName);
    //            List<TableColumn> columnsList = new List<TableColumn>();
    //            foreach (DataRow row in dtcolumns.Rows)
    //            {
    //                TableColumn column = new TableColumn();
    //                column.ColumnName = row["字段名"].ToString();
    //                column.ColumnRemark = row["字段说明"].ToString();
    //                column.DefaultValue = row["默认值"].ToString();
    //                if (column.ColumnRemark.Length == 0) column.ColumnRemark = column.ColumnName;
    //                column.ColumnType = datatype.IniReadValue("DbToCS", row["类型"].ToString());
    //                if (row["主键"].ToString().Length > 0)
    //                {
    //                    column.PK = true;
    //                }
    //                if (row["标识"].ToString().Length > 0)
    //                {
    //                    column.Identifying = true;
    //                }
    //                columnsList.Add(column);
    //            }
    //            model.Columns = columnsList;
    //            return model;
    //        }
    //        /// <summary>
    //        /// 表名全部显示
    //        /// </summary>
    //        /// <param name="DbName"></param>
    //        /// <param name="whereSql"></param>
    //        /// <returns></returns>
    //        public DataTable GetTablesColumnsList(string DbName, string whereSql)
    //        {
    //            string topSql = @"SELECT CASE WHEN a.colorder = 1 THEN d.name ELSE d.name END AS 表名,  CASE WHEN a.colorder = 1 THEN isnull(f.value, '') ELSE '' END AS 表说明, a.colorder AS 字段序号, a.name AS 字段名,ISNULL(g.[value], '') AS 字段说明, CASE WHEN COLUMNPROPERTY(a.id, a.name, 'IsIdentity') = 1 THEN '√' ELSE '' END AS 标识,  CASE WHEN EXISTS(SELECT 1  FROM dbo.sysindexes si INNER JOIN  dbo.sysindexkeys sik ON si.id = sik.id AND si.indid = sik.indid INNER JOIN dbo.syscolumns sc ON sc.id = sik.id AND sc.colid = sik.colid INNER JOIN  dbo.sysobjects so ON so.name = si.name AND so.xtype = 'PK' WHERE sc.id = a.id AND sc.colid = a.colid) THEN '√' ELSE '' END AS 主键,  b.name AS 类型, a.length AS 长度, COLUMNPROPERTY(a.id, a.name, 'PRECISION')AS 精度, ISNULL(COLUMNPROPERTY(a.id, a.name, 'Scale'), 0) AS 小数位数,CASE WHEN a.isnullable = 1 THEN '√' ELSE '' END AS 允许空, ISNULL(e.text, '') AS 默认值, d.crdate AS 创建时间, CASE WHEN a.colorder = 1 THEN d.refdate ELSE NULL END AS 更改时间 FROM dbo.syscolumns a LEFT OUTER JOIN    dbo.systypes b ON a.xtype = b.xusertype INNER JOIN dbo.sysobjects d ON a.id = d.id AND d.xtype = 'U' AND   d.status >= 0 LEFT OUTER JOIN   dbo.syscomments e ON a.cdefault = e.id LEFT OUTER JOIN sys.extended_properties g ON a.id = g.major_id AND a.colid = g.minor_id AND  g.name = 'MS_Description' LEFT OUTER JOIN  sys.extended_properties f ON d.id = f.major_id AND f.minor_id = 0 AND  f.name = 'MS_Description'";

    //            string colsSql = topSql;
    //            if (whereSql.Length > 0)
    //            {
    //                colsSql = topSql + whereSql;
    //            }
    //            DataTable dt = new DataTable();
    //            dt.TableName = "TablesColumns";
    //            SqlDataReader reader = this.ExecuteReader(DbName, colsSql.ToString());
    //            dt.Load(reader);
    //            reader.Close();
    //            return dt;
    //        }
    //        /// <summary>
    //        /// 获取数据库里相同的字段名称
    //        /// </summary>
    //        /// <param name="DbName"></param>
    //        /// <returns></returns>
    //        public DataTable GetEqualColumnsList(string DbName)
    //        {
    //            string colsSql = @"SELECT
    //       DISTINCT a.name AS 字段名称,'' AS 字段说明
    //FROM    dbo.syscolumns a
    //        LEFT OUTER JOIN dbo.systypes b ON a.xtype = b.xusertype
    //        INNER JOIN dbo.sysobjects d ON a.id = d.id
    //                                       AND d.xtype = 'U'
    //                                       AND d.status >= 0
    //        LEFT OUTER JOIN dbo.syscomments e ON a.cdefault = e.id
    //        LEFT OUTER JOIN sys.extended_properties g ON a.id = g.major_id
    //                                                     AND a.colid = g.minor_id
    //                                                     AND g.name = 'MS_Description'
    //        LEFT OUTER JOIN sys.extended_properties f ON d.id = f.major_id
    //                                                     AND f.minor_id = 0
    //                                                     AND f.name = 'MS_Description' ";
    //            DataTable dt = new DataTable();
    //            dt.TableName = "EqualColumns";
    //            SqlDataReader reader = this.ExecuteReader(DbName, colsSql.ToString());
    //            dt.Load(reader);
    //            reader.Close();
    //            return dt;
    //        }
    //        /// <summary>
    //        /// 表说明
    //        /// </summary>
    //        /// <param name="DbName"></param>
    //        /// <param name="tableName"></param>
    //        /// <returns></returns>
    //        public string GetTableInfo(string DbName, string tableName)
    //        {
    //            object retval = null;
    //            string tableinfo = "select value from sys.extended_properties where major_id in(select id from sysobjects where name='" + tableName + "') and minor_id=0";
    //            try
    //            {
    //                if (this.connect.ConnectionString == "")
    //                {
    //                    this.connect.ConnectionString = this._dbconnectStr;
    //                }
    //                if (this.connect.ConnectionString != this._dbconnectStr)
    //                {
    //                    this.connect.Close();
    //                    this.connect.ConnectionString = this._dbconnectStr;
    //                }
    //                SqlCommand command = new SqlCommand();
    //                command.Connection = this.connect;
    //                if (this.connect.State == ConnectionState.Closed)
    //                {
    //                    this.connect.Open();
    //                }
    //                command.CommandText = "use [" + DbName + "]" + tableinfo;
    //                retval = command.ExecuteScalar();
    //                return retval.ToString();
    //            }
    //            catch (Exception exception)
    //            {
    //                string message = exception.Message;
    //                return "";
    //            }
    //        }
    //        public DataRow[] GetTableInfo(string DbName)
    //        {
    //            object retval = null;
    //            string tableinfo = queryFieldStr;
    //            DataTable dt = this.Query(DbName, tableinfo).Tables[0];
    //            DataRow[] drs = dt.Select("Len(表名)>0", "表说明");
    //            return drs;

    //        }
    //        /// <summary>
    //        /// 表主键
    //        /// </summary>
    //        /// <param name="DbName"></param>
    //        /// <param name="tableName"></param>
    //        /// <returns></returns>
    //        public string GetTablePkName(string DbName, string tableName)
    //        {
    //            string pkname = "";
    //            string pkSql = this.queryFieldStr + " and UPPER(d .name) = '" + tableName.ToUpper() + "' and a.colorder = 1";
    //            DataTable dt = this.Query(DbName, pkSql).Tables[0];
    //            try
    //            {
    //                pkname = dt.Rows[0]["字段名"].ToString();
    //            }
    //            catch
    //            {
    //                return "";
    //            }
    //            return pkname;
    //        }
    //        public SqlDataReader ExecuteReader(string DbName, string strSQL)
    //        {
    //            SqlDataReader reader2;
    //            try
    //            {
    //                this.OpenDB(DbName);
    //                reader2 = new SqlCommand(strSQL, this.connect).ExecuteReader(CommandBehavior.CloseConnection);
    //            }
    //            catch (Exception exception)
    //            {
    //                throw exception;
    //            }
    //            return reader2;
    //        }
    //        public DataSet Query(string DbName, string SQLString)
    //        {
    //            DataSet dataSet = new DataSet();
    //            try
    //            {
    //                this.OpenDB(DbName);
    //                new SqlDataAdapter(SQLString, this.connect).Fill(dataSet, "ds");
    //            }
    //            catch (SqlException exception)
    //            {
    //                throw new Exception(exception.Message);
    //            }
    //            return dataSet;
    //        }
    //        private SqlCommand OpenDB(string DbName)
    //        {
    //            try
    //            {
    //                if (this.connect.ConnectionString == "")
    //                {
    //                    this.connect.ConnectionString = this._dbconnectStr;
    //                }
    //                if (this.connect.ConnectionString != this._dbconnectStr)
    //                {
    //                    this.connect.Close();
    //                    this.connect.ConnectionString = this._dbconnectStr;
    //                }
    //                SqlCommand command = new SqlCommand();
    //                command.Connection = this.connect;
    //                if (this.connect.State == ConnectionState.Closed)
    //                {
    //                    this.connect.Open();
    //                }
    //                command.CommandText = "use [" + DbName + "]";
    //                command.ExecuteNonQuery();
    //                return command;
    //            }
    //            catch (Exception exception)
    //            {
    //                string message = exception.Message;
    //                return null;
    //            }
    //        }
    //        public void ExceSql(string sql)
    //        {
    //            try
    //            {
    //                if (this.connect.State == ConnectionState.Closed)
    //                {
    //                    this.connect.Open();
    //                }
    //                SqlCommand command = OpenDB(Utils.dbName);
    //                command.Connection = this.connect;
    //                command.CommandText = sql;
    //                command.ExecuteNonQuery();
    //            }
    //            catch (Exception)
    //            {

    //            }
    //        }
    //    }
}
