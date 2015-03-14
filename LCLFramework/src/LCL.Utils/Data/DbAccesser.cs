using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Data.Common;
using System.Text.RegularExpressions;
using System.Runtime;
using LCL.Data.Providers;

namespace LCL.Data
{
    /// <summary>
    /// Encapsulate the common operations to communicate with database.
    /// <remarks>
    /// It supports the query use DBParameter.(But it doesn't catch any exception, so the client program should deal with it by itself)
    /// 
    /// There are two categories of query method:
    /// 1.The public methods:
    ///     These methods use a special sql sentences which looks like the parameter of String.Format,
    ///     and input the needed parameters follow.
    /// 2.The public methods on IRawDbAccesser property:
    ///     These methods use the normal sql sentences, the input parameters array should be created outside.
    ///     You can use <see cref="IRawDbAccesser.ParameterFactory"/> to create parameters.
    /// </remarks>
    /// </summary>
    /// <author>whiteLight</author>
    /// <createDate>2008-7-6, 20:19:08</createDate>
    /// <modify>2008-7-22，add ConvertParamaters method，and change the interface，use a common formatted sql to search database</modify>
    /// <modify>2008-8-7, If any one of the parameters is null, it is converted to DBNull.Value.</modify>
    public class DbAccesser : IDisposable, IDbAccesser, IRawDbAccesser, IDbParameterFactory
    {
        #region fields

        /// <summary>
        /// Was the connection opened by my self.
        /// </summary>
        private bool _openConnectionBySelf;

        /// <summary>
        /// Is this connection created by my self;
        /// </summary>
        private bool _connectionCreatedBySelf;

        private DbConnectionSchema _connectionSchema;

        /// <summary>
        /// inner db connection
        /// </summary>
        private IDbConnection _connection;

        /// <summary>
        /// abstract db provider factory
        /// </summary>
        private DbProviderFactory _factory;

        /// <summary>
        /// used to format sql and its corresponding parameters.
        /// </summary>
        private ISqlProvider _converter;

        //private IDbTransaction _transaction; 

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// 
        /// this accessor uses <see cref="DbSetting"/> class to find its connection string, and creates connection by itself.
        /// </summary>
        /// <param name="connectionStringSettingName">the setting name in configuration file.</param>
        public DbAccesser(string connectionStringSettingName)
        {
            var setting = DbSetting.FindOrCreate(connectionStringSettingName);
            this.Init(setting);
        }

        /// <summary>
        /// Constructor
        /// 
        /// this accessor creates the db connection by itself.
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        /// <param name="connectionProvider">
        /// The provider.
        /// eg.
        /// "System.Data.SqlClient"
        /// </param>
        public DbAccesser(string connectionString, string connectionProvider)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("connectionString");
            }
            if (string.IsNullOrEmpty(connectionProvider))
            {
                throw new ArgumentNullException("connectionProvider");
            }

            this.Init(new DbConnectionSchema(connectionString, connectionProvider));
        }

        /// <summary>
        /// Constructor
        /// 
        /// this accessor uses schema to find its connection string, and creates connection by itself.
        /// </summary>
        /// <param name="schema">the connection schema.</param>
        public DbAccesser(DbConnectionSchema schema)
        {
            this.Init(schema);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="schema">the connection schema.</param>
        /// <param name="dbConnection">use a exsiting connection, rather than to create a new one.</param>
        public DbAccesser(DbConnectionSchema schema, IDbConnection dbConnection)
        {
            this.Init(schema, dbConnection);
        }

        private void Init(DbConnectionSchema schema, IDbConnection connection = null)
        {
            this._connectionSchema = schema;

            this._factory = ConverterFactory.GetFactory(schema.ProviderName);
            this._converter = ConverterFactory.Create(schema.ProviderName);
            if (connection == null)
            {
                this._connection = this._factory.CreateConnection();
                this._connection.ConnectionString = schema.ConnectionString;
                this._connectionCreatedBySelf = true;
            }
            else
            {
                this._connection = connection;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the connection schema of current database.
        /// </summary>
        public DbConnectionSchema ConnectionSchema
        {
            get { return this._connectionSchema; }
        }

        /// <summary>
        /// The underlying db connection
        /// </summary>
        public IDbConnection Connection
        {
            get
            {
                return this._connection;
            }
        }

        /// <summary>
        /// Gets a raw accesser which is oriented to raw sql and <c>IDbDataParameter</c>。
        /// </summary>
        public IRawDbAccesser RawAccesser
        {
            get { return this; }
        }

        /// <summary>
        /// Open the connection
        /// </summary>
        private void MakeConnectionOpen()
        {
            if (this._connection.State == ConnectionState.Closed)
            {
                this._openConnectionBySelf = true;
                this._connection.Open();
            }
        }

        /// <summary>
        /// This method only close the connection which is opened by this object itself.
        /// </summary>
        private void MakeConnectionClose()
        {
            if (this._openConnectionBySelf)
            {
                this._connection.Close();
                this._openConnectionBySelf = false;
            }
        }
        #endregion

        #region IDbAccesser Members

        /// <summary>
        /// Query out a DataTable object from database by the specific sql.
        /// </summary>
        /// <param name="formattedSql">a formatted sql which format looks like the parameter of String.Format</param>
        /// <param name="parameters">If this sql has some parameters, these are its parameters.</param>
        /// <returns></returns>
        public DataTable QueryDataTable(string formattedSql, params object[] parameters)
        {
            if (parameters.Length > 0)
            {
                formattedSql = _converter.ConvertToSpecialDbSql(formattedSql);
                IDbDataParameter[] dbParameters = ConvertFormatParamaters(parameters);
                return this.DoQueryDataTable(formattedSql, CommandType.Text, dbParameters);
            }
            else
            {
                return this.DoQueryDataTable(formattedSql, CommandType.Text);
            }
        }

        /// <summary>
        /// Query out a row from database.
        /// If there is not any records, return null.
        /// </summary>
        /// <param name="formattedSql">specific sql</param>
        /// <param name="parameters">If this sql has some parameters, these are its parameters.</param>
        /// <returns></returns>
        public DataRow QueryDataRow(string formattedSql, params object[] parameters)
        {
            if (parameters.Length > 0)
            {
                formattedSql = _converter.ConvertToSpecialDbSql(formattedSql);
                IDbDataParameter[] dbParameters = ConvertFormatParamaters(parameters);
                return this.DoQueryDataRow(formattedSql, CommandType.Text, dbParameters);
            }
            else
            {
                return this.DoQueryDataRow(formattedSql, CommandType.Text);
            }
        }

        /// <summary>
        /// Query out a DataTable object from database by the specific sql.
        /// </summary>
        /// <param name="formattedSql">a formatted sql which format looks like the parameter of String.Format</param>
        /// <param name="parameters">If this sql has some parameters, these are its parameters.</param>
        /// <returns></returns>
        public LiteDataTable QueryLiteDataTable(string formattedSql, params object[] parameters)
        {
            var reader = this.QueryDataReader(formattedSql, parameters);
            return ConvertToLiteDataTable(reader);
        }

        /// <summary>
        /// Query out a row from database.
        /// If there is not any records, return null.
        /// </summary>
        /// <param name="formattedSql">specific sql</param>
        /// <param name="parameters">If this sql has some parameters, these are its parameters.</param>
        /// <returns></returns>
        public LiteDataRow QueryLiteDataRow(string formattedSql, params object[] parameters)
        {
            var reader = this.QueryDataReader(formattedSql, parameters);
            return ConvertToLiteDataRow(reader);
        }

        /// <summary>
        /// Query out some data from database.
        /// </summary>
        /// <param name="formattedSql">a formatted sql which format looks like the parameter of String.Format</param>
        /// <param name="parameters">If this sql has some parameters, these are its parameters.</param>
        /// <returns></returns>
        public IDataReader QueryDataReader(string formattedSql, params object[] parameters)
        {
            return this.QueryDataReader(formattedSql, this._connection.State == ConnectionState.Closed, parameters);
        }

        /// <summary>
        /// Query out some data from database.
        /// </summary>
        /// <param name="formattedSql">a formatted sql which format looks like the parameter of String.Format</param>
        /// <param name="closeConnection">Indicates whether to close the corresponding connection when the reader is closed?</param>
        /// <param name="parameters">If this sql has some parameters, these are its parameters.</param>
        /// <returns></returns>
        public IDataReader QueryDataReader(string formattedSql, bool closeConnection, params object[] parameters)
        {
            if (parameters.Length > 0)
            {
                formattedSql = _converter.ConvertToSpecialDbSql(formattedSql);
                IDbDataParameter[] dbParameters = ConvertFormatParamaters(parameters);
                return this.DoQueryDataReader(formattedSql, CommandType.Text, closeConnection, dbParameters);
            }
            else
            {
                return this.DoQueryDataReader(formattedSql, CommandType.Text, closeConnection);
            }
        }

        /// <summary>
        /// Execute the sql, and return the element of first row and first column, ignore the other values.
        /// </summary>
        /// <param name="formattedSql">a formatted sql which format looks like the parameter of String.Format</param>
        /// <param name="parameters">If this sql has some parameters, these are its parameters.</param>
        /// <returns>DBNull or value object.</returns>
        public object QueryValue(string formattedSql, params object[] parameters)
        {
            if (parameters.Length > 0)
            {
                formattedSql = _converter.ConvertToSpecialDbSql(formattedSql);
                IDbDataParameter[] dbParameters = ConvertFormatParamaters(parameters);
                return this.DoQueryValue(formattedSql, CommandType.Text, dbParameters);
            }
            else
            {
                return this.DoQueryValue(formattedSql, CommandType.Text);
            }
        }

        /// <summary>
        /// Execute a sql which is not a database procudure, return rows effected.
        /// </summary>
        /// <param name="formattedSql">a formatted sql which format looks like the parameter of String.Format</param>
        /// <param name="parameters">If this sql has some parameters, these are its parameters.</param>
        /// <returns>The number of rows effected</returns>
        public int ExecuteText(string formattedSql, params object[] parameters)
        {
            if (parameters.Length > 0)
            {
                formattedSql = _converter.ConvertToSpecialDbSql(formattedSql);
                IDbDataParameter[] dbParameters = ConvertFormatParamaters(parameters);
                return this.DoExecuteText(formattedSql, dbParameters);
            }
            else
            {
                return this.DoExecuteText(formattedSql);
            }
        }

        /// <summary>
        /// 此方法提供特定数据库的参数列表。
        /// </summary>
        /// <param name="parametersValues">formattedSql参数列表</param>
        /// <returns>数据库参数列表</returns>
        private IDbDataParameter[] ConvertFormatParamaters(object[] parametersValues)
        {
            IDbDataParameter[] dbParameters = new DbParameter[parametersValues.Length];
            string parameterName = null;
            for (int i = 0, l = parametersValues.Length; i < l; i++)
            {
                parameterName = _converter.GetParameterName(i);
                object value = parametersValues[i];

                //convert null value.
                if (value == null) { value = DBNull.Value; }
                IDbDataParameter param = (this as IDbParameterFactory).CreateParameter(parameterName, value, ParameterDirection.Input);
                dbParameters[i] = param;
            }
            return dbParameters;
        }

        #endregion

        #region Do Query/Execute

        /// <summary>
        /// Prepare a command for communicate with database.
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="type"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected virtual IDbCommand PrepareCommand(string sql, CommandType type, IDbDataParameter[] parameters)
        {
            IDbCommand command = _factory.CreateCommand();
            command.Connection = this._connection;
            command.CommandText = sql;
            command.CommandType = type;
            var pas = command.Parameters;
            if (parameters != null)
                foreach (var p in parameters) { pas.Add(p); }

            var tran = LocalTransactionBlock.GetCurrentTransaction(this._connectionSchema.Database);
            if (tran != null && tran.Connection == this._connection)
            {
                command.Transaction = tran;
            }

            Logger.LogDbAccessed(sql, parameters, this._connectionSchema);

            return command;
        }

        private DataTable DoQueryDataTable(string sql, CommandType type, params IDbDataParameter[] parameters)
        {
            IDbDataAdapter da = _factory.CreateDataAdapter();
            da.SelectCommand = this.PrepareCommand(sql, type, parameters);

            DataSet ds = new DataSet();
            try
            {
                this.MakeConnectionOpen();

                da.Fill(ds);

                return ds.Tables[0];
            }
            finally
            {
                this.MakeConnectionClose();
            }
        }

        private DataRow DoQueryDataRow(string sql, CommandType type, params IDbDataParameter[] parameters)
        {
            using (DataTable dt = this.DoQueryDataTable(sql, type, parameters))
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt.Rows[0];
                }
                return null;
            }
        }

        private IDataReader DoQueryDataReader(string sql, CommandType type, bool closeConnection, params IDbDataParameter[] parameters)
        {
            IDbCommand cmd = this.PrepareCommand(sql, type, parameters);

            if (this._connection.State == ConnectionState.Closed) { this._connection.Open(); }

            IDataReader reader = null;

            if (closeConnection)
            {
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            else
            {
                reader = cmd.ExecuteReader();
            }

            return reader;
        }

        private object DoQueryValue(string sql, CommandType type, params IDbDataParameter[] parameters)
        {
            IDbCommand cmd = this.PrepareCommand(sql, type, parameters);

            try
            {
                this.MakeConnectionOpen();

                return cmd.ExecuteScalar();
            }
            finally
            {
                this.MakeConnectionClose();
            }
        }

        private int DoExecuteProcedure(string procedureName, out int rowsAffect, params IDbDataParameter[] parameters)
        {
            IDbCommand command = this.PrepareCommand(procedureName, CommandType.StoredProcedure, parameters);

            //返回值
            DbParameter paraReturn = this._factory.CreateParameter();
            paraReturn.DbType = DbType.Int32;
            paraReturn.Direction = ParameterDirection.ReturnValue;
            paraReturn.ParameterName = this._converter.ProcudureReturnParameterName;
            paraReturn.Size = 4;
            command.Parameters.Add(paraReturn);

            try
            {
                this.MakeConnectionOpen();

                rowsAffect = command.ExecuteNonQuery();
                return Convert.ToInt32(paraReturn.Value);
            }
            finally
            {
                this.MakeConnectionClose();
            }
        }

        private int DoExecuteText(string sql, params IDbDataParameter[] parameters)
        {
            IDbCommand command = this.PrepareCommand(sql, CommandType.Text, parameters);
            try
            {
                this.MakeConnectionOpen();

                return command.ExecuteNonQuery();
            }
            finally
            {
                this.MakeConnectionClose();
            }
        }

        private static LiteDataTable ConvertToLiteDataTable(IDataReader reader)
        {
            var table = new LiteDataTable();
            using (reader)
            {
                LiteDataTableAdapter.Fill(table, reader);
            }
            return table;
        }

        private static LiteDataRow ConvertToLiteDataRow(IDataReader reader)
        {
            var dt = ConvertToLiteDataTable(reader);
            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }

        #endregion

        #region IDbParameterFactory Members

        /// <summary>
        /// Create a DBParameter
        /// </summary>
        /// <returns></returns>
        IDbDataParameter IDbParameterFactory.CreateParameter()
        {
            return _factory.CreateParameter();
        }

        /// <summary>
        /// Create a DBParameter
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IDbDataParameter IDbParameterFactory.CreateParameter(string name)
        {
            IDbDataParameter para = _factory.CreateParameter();
            para.ParameterName = name;
            return para;
        }

        /// <summary>
        /// Create a DBParameter
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IDbDataParameter IDbParameterFactory.CreateParameter(string name, object value)
        {
            IDbDataParameter para = _factory.CreateParameter();
            para.ParameterName = name;
            para.Value = value;
            return para;
        }

        /// <summary>
        /// Create a DBParameter
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        IDbDataParameter IDbParameterFactory.CreateParameter(string name, object value, DbType type)
        {
            IDbDataParameter para = _factory.CreateParameter();
            para.ParameterName = name;
            para.DbType = type;
            para.Value = value;
            return para;
        }

        /// <summary>
        /// Create a DBParameter
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        IDbDataParameter IDbParameterFactory.CreateParameter(string name, object value, ParameterDirection direction)
        {
            IDbDataParameter para = _factory.CreateParameter();
            para.ParameterName = name;
            para.Value = value;
            para.Direction = direction;
            return para;
        }

        /// <summary>
        /// Create a DBParameter
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        IDbDataParameter IDbParameterFactory.CreateParameter(string name, object value, DbType type, int size)
        {
            IDbDataParameter para = _factory.CreateParameter();
            para.ParameterName = name;
            para.DbType = type;
            para.Size = size;
            para.Value = value;
            return para;
        }

        /// <summary>
        /// Create a DBParameter
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        IDbDataParameter IDbParameterFactory.CreateParameter(string name, object value, DbType type, ParameterDirection direction)
        {
            IDbDataParameter para = _factory.CreateParameter();
            para.ParameterName = name;
            para.DbType = type;
            para.Value = value;
            para.Direction = direction;
            return para;
        }

        /// <summary>
        /// Create a DBParameter
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <param name="size"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        IDbDataParameter IDbParameterFactory.CreateParameter(string name, object value, DbType type, int size, ParameterDirection direction)
        {
            IDbDataParameter para = _factory.CreateParameter();
            para.ParameterName = name;
            para.DbType = type;
            para.Value = value;
            para.Size = size;
            para.Direction = direction;
            return para;
        }

        #endregion

        #region IRawDbAccesser Members

        /// <summary>
        /// A factory to create parameters.
        /// </summary>
        IDbParameterFactory IRawDbAccesser.ParameterFactory
        {
            get { return this; }
        }

        /// <summary>
        /// Query out a DataTable object from database by the specific sql.
        /// </summary>
        /// <param name="sql">specific sql</param>
        /// <param name="parameters">If this sql has some parameters, these are its parameters.</param>
        /// <returns></returns>
        LiteDataTable IRawDbAccesser.QueryLiteDataTable(string sql, params IDbDataParameter[] parameters)
        {
            var reader = (this as IRawDbAccesser).QueryDataReader(sql, parameters);
            return ConvertToLiteDataTable(reader);
        }

        /// <summary>
        /// Query out a DataTable object from database by the specific sql.
        /// </summary>
        /// <param name="sql">specific sql</param>
        /// <param name="type">
        /// Indicates or specifies how the System.Data.IDbCommand.CommandText property
        /// is interpreted.
        /// </param>
        /// <param name="parameters">If this sql has some parameters, these are its parameters.</param>
        /// <returns></returns>
        LiteDataTable IRawDbAccesser.QueryLiteDataTable(string sql, CommandType type, params IDbDataParameter[] parameters)
        {
            var reader = (this as IRawDbAccesser).QueryDataReader(sql, type, parameters);
            return ConvertToLiteDataTable(reader);
        }

        /// <summary>
        /// Query out a row from database.
        /// If there is not any records, return null.
        /// </summary>
        /// <param name="sql">specific sql</param>
        /// <param name="parameters">If this sql has some parameters, these are its parameters.</param>
        /// <returns></returns>
        LiteDataRow IRawDbAccesser.QueryLiteDataRow(string sql, params IDbDataParameter[] parameters)
        {
            var reader = (this as IRawDbAccesser).QueryDataReader(sql, parameters);
            return ConvertToLiteDataRow(reader);
        }

        /// <summary>
        /// Query out a row from database.
        /// If there is not any records, return null.
        /// </summary>
        /// <param name="sql">specific sql</param>
        /// <param name="type">
        /// Indicates or specifies how the System.Data.IDbCommand.CommandText property
        /// is interpreted.
        /// </param>
        /// <param name="parameters">If this sql has some parameters, these are its parameters.</param>
        /// <returns></returns>
        LiteDataRow IRawDbAccesser.QueryLiteDataRow(string sql, CommandType type, params IDbDataParameter[] parameters)
        {
            var reader = (this as IRawDbAccesser).QueryDataReader(sql, type, parameters);
            return ConvertToLiteDataRow(reader);
        }

        /// <summary>
        /// Query out a DataTable object from database by the specific sql.
        /// </summary>
        /// <param name="sql">specific sql</param>
        /// <param name="parameters">If this sql has some parameters, these are its parameters.</param>
        /// <returns></returns>
        DataTable IRawDbAccesser.QueryDataTable(string sql, params IDbDataParameter[] parameters)
        {
            return this.DoQueryDataTable(sql, CommandType.Text, parameters);
        }

        /// <summary>
        /// Query out a DataTable object from database by the specific sql.
        /// </summary>
        /// <param name="sql">specific sql</param>
        /// <param name="type">
        /// Indicates or specifies how the System.Data.IDbCommand.CommandText property
        /// is interpreted.
        /// </param>
        /// <param name="parameters">If this sql has some parameters, these are its parameters.</param>
        /// <returns></returns>
        DataTable IRawDbAccesser.QueryDataTable(string sql, CommandType type, params IDbDataParameter[] parameters)
        {
            return this.DoQueryDataTable(sql, type, parameters);
        }

        /// <summary>
        /// Query out a row from database.
        /// If there is not any records, return null.
        /// </summary>
        /// <param name="sql">specific sql</param>
        /// <param name="parameters">If this sql has some parameters, these are its parameters.</param>
        /// <returns></returns>
        DataRow IRawDbAccesser.QueryDataRow(string sql, params IDbDataParameter[] parameters)
        {
            return this.DoQueryDataRow(sql, CommandType.Text, parameters);
        }

        /// <summary>
        /// Query out a row from database.
        /// If there is not any records, return null.
        /// </summary>
        /// <param name="sql">specific sql</param>
        /// <param name="type">
        /// Indicates or specifies how the System.Data.IDbCommand.CommandText property
        /// is interpreted.
        /// </param>
        /// <param name="parameters">If this sql has some parameters, these are its parameters.</param>
        /// <returns></returns>
        DataRow IRawDbAccesser.QueryDataRow(string sql, CommandType type, params IDbDataParameter[] parameters)
        {
            return this.DoQueryDataRow(sql, type, parameters);
        }

        /// <summary>
        /// Query out some data from database.
        /// </summary>
        /// <param name="sql">specific sql</param>
        /// <param name="closeConnection">Indicates whether to close the corresponding connection when the reader is closed?</param>
        /// <param name="parameters">If this sql has some parameters, these are its parameters.</param>
        /// <returns></returns>
        IDataReader IRawDbAccesser.QueryDataReader(string sql, bool closeConnection, params IDbDataParameter[] parameters)
        {
            return this.DoQueryDataReader(sql, CommandType.Text, closeConnection, parameters);
        }

        /// <summary>
        /// Query out some data from database.
        /// </summary>
        /// <param name="sql">specific sql</param>
        /// <param name="type">
        /// Indicates or specifies how the System.Data.IDbCommand.CommandText property
        /// is interpreted.
        /// </param>
        /// <param name="closeConnection">Indicates whether to close the corresponding connection when the reader is closed?</param>
        /// <param name="parameters">If this sql has some parameters, these are its parameters.</param>
        /// <returns></returns>
        IDataReader IRawDbAccesser.QueryDataReader(string sql, CommandType type, bool closeConnection, params IDbDataParameter[] parameters)
        {
            return this.DoQueryDataReader(sql, type, closeConnection, parameters);
        }

        /// <summary>
        /// Query out some data from database.
        /// </summary>
        /// <param name="sql">specific sql</param>
        /// <param name="parameters">If this sql has some parameters, these are its parameters.</param>
        /// <returns></returns>
        IDataReader IRawDbAccesser.QueryDataReader(string sql, params IDbDataParameter[] parameters)
        {
            return this.DoQueryDataReader(sql, CommandType.Text, this._connection.State == ConnectionState.Closed, parameters);
        }

        /// <summary>
        /// Query out some data from database.
        /// </summary>
        /// <param name="sql">specific sql</param>
        /// <param name="type">
        /// Indicates or specifies how the System.Data.IDbCommand.CommandText property
        /// is interpreted.
        /// </param>
        /// <param name="parameters">If this sql has some parameters, these are its parameters.</param>
        /// <returns></returns>
        IDataReader IRawDbAccesser.QueryDataReader(string sql, CommandType type, params IDbDataParameter[] parameters)
        {
            return this.DoQueryDataReader(sql, type, this._connection.State == ConnectionState.Closed, parameters);
        }

        /// <summary>
        /// Execute the sql, and return the element of first row and first column, ignore the other values.
        /// </summary>
        /// <param name="sql">specific sql</param>
        /// <param name="parameters">If this sql has some parameters, these are its parameters.</param>
        /// <returns>DBNull or object.</returns>
        object IRawDbAccesser.QueryValue(string sql, params IDbDataParameter[] parameters)
        {
            return this.DoQueryValue(sql, CommandType.Text, parameters);
        }

        /// <summary>
        /// Execute the sql, and return the element of first row and first column, ignore the other values.
        /// </summary>
        /// <param name="sql">specific sql</param>
        /// <param name="type">
        /// Indicates or specifies how the System.Data.IDbCommand.CommandText property
        /// is interpreted.
        /// </param>
        /// <param name="parameters">If this sql has some parameters, these are its parameters.</param>
        /// <returns>DBNull or object.</returns>
        object IRawDbAccesser.QueryValue(string sql, CommandType type, params IDbDataParameter[] parameters)
        {
            return this.DoQueryValue(sql, type, parameters);
        }

        /// <summary>
        /// Execute a procudure, and return the value returned by this procedure
        /// </summary>
        /// <param name="procedureName">The name of this procedure</param>
        /// <param name="parameters">If this sql has some parameters, these are its parameters.</param>
        /// <returns>The value returned by procedure</returns>
        int IRawDbAccesser.ExecuteProcedure(string procedureName, params IDbDataParameter[] parameters)
        {
            int rowsAffect;
            return this.DoExecuteProcedure(procedureName, out rowsAffect, parameters);
        }

        /// <summary>
        /// Execute a procudure, and return the value returned by this procedure
        /// </summary>
        /// <param name="procedureName">The name of this procedure</param>
        /// <param name="rowsAffect">The number of rows effected</param>
        /// <param name="parameters">If this sql has some parameters, these are its parameters.</param>
        /// <returns>The value returned by procedure</returns>
        int IRawDbAccesser.ExecuteProcedure(string procedureName, out int rowsAffect, params IDbDataParameter[] parameters)
        {
            return this.DoExecuteProcedure(procedureName, out rowsAffect, parameters);
        }

        /// <summary>
        /// Execute a sql which is not a database procudure, return rows effected.
        /// </summary>
        /// <param name="sql">specific sql</param>
        /// <param name="parameters">If this sql has some parameters, these are its parameters.</param>
        /// <returns>The number of rows effected</returns>
        int IRawDbAccesser.ExecuteText(string sql, params IDbDataParameter[] parameters)
        {
            return this.DoExecuteText(sql, parameters);
        }
        #endregion

        #region IDisposable Members

        ~DbAccesser()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// dispose this accesser.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this._connectionCreatedBySelf && this._connection != null)
                {
                    this._connection.Dispose();
                }
                this._connection = null;
                this._converter = null;
                this._factory = null;
            }
        }

        #endregion
    }
}
