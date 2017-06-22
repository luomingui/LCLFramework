using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Common;
using System.Text.RegularExpressions;

namespace LCL.LData
{
    /// <summary>
    /// 数据库配置
    /// </summary>
    public class DbSetting : DbConnectionSchema
    {
        private DbSetting() { }

        /// <summary>
        /// 配置名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 查找或者根据约定创建连接字符串
        /// </summary>
        /// <param name="dbSetting"></param>
        /// <returns></returns>
        public static DbSetting FindOrCreate(string dbSetting)
        {
            if (dbSetting == null) throw new ArgumentNullException("dbSetting");//可以是空字符串。

            DbSetting setting = null;

            if (!_generatedSettings.TryGetValue(dbSetting, out setting))
            {
                lock (_generatedSettings)
                {
                    if (!_generatedSettings.TryGetValue(dbSetting, out setting))
                    {
                        var config = ConfigurationManager.ConnectionStrings[dbSetting];
                        if (config != null)
                        {
                            setting = new DbSetting
                            {
                                ConnectionString = config.ConnectionString,
                                ProviderName = config.ProviderName,
                            };
                        }
                        else
                        {
                            setting = Create(dbSetting);
                        }

                        setting.Name = dbSetting;

                        _generatedSettings.Add(dbSetting, setting);
                    }
                }
            }

            return setting;
        }

        /// <summary>
        /// 添加一个数据库连接配置。
        /// </summary>
        /// <param name="name"></param>
        /// <param name="connectionString"></param>
        /// <param name="providerName"></param>
        public static DbSetting SetSetting(string name, string connectionString, string providerName)
        {
            if (string.IsNullOrEmpty(name)) throw new InvalidOperationException("string.IsNullOrEmpty(dbSetting.Name) must be false.");
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException("connectionString");
            if (string.IsNullOrEmpty(providerName)) throw new ArgumentNullException("providerName");

            var setting = new DbSetting
            {
                Name = name,
                ConnectionString = connectionString,
                ProviderName = providerName
            };

            lock (_generatedSettings)
            {
                _generatedSettings[name] = setting;
            }

            return setting;
        }

        /// <summary>
        /// 获取当前已经被生成的 DbSetting。
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<DbSetting> GetGeneratedSettings()
        {
            return _generatedSettings.Values;
        }

        private static Dictionary<string, DbSetting> _generatedSettings = new Dictionary<string, DbSetting>();

        private static DbSetting Create(string dbSetting)
        {
            //查找连接字符串时，根据用户的 LocalSqlServer 来查找。
            var local = ConfigurationManager.ConnectionStrings[DbName_LocalServer];
            if (local != null && local.ProviderName == Provider_SqlClient)
            {
                var builder = new SqlConnectionStringBuilder(local.ConnectionString);

                var newCon = new SqlConnectionStringBuilder();
                newCon.DataSource = builder.DataSource;
                newCon.InitialCatalog = dbSetting;
                newCon.IntegratedSecurity = builder.IntegratedSecurity;
                if (!newCon.IntegratedSecurity)
                {
                    newCon.UserID = builder.UserID;
                    newCon.Password = builder.Password;
                }

                return new DbSetting
                {
                    ConnectionString = newCon.ToString(),
                    ProviderName = local.ProviderName
                };
            }

            return new DbSetting
            {
                ConnectionString = string.Format(@"Data Source={0}.sdf", dbSetting),
                ProviderName = Provider_SqlCe
            };

            //return new DbSetting
            //{
            //    ConnectionString = string.Format(@"Data Source=.\SQLExpress;Initial Catalog={0};Integrated Security=True", dbSetting),
            //    ProviderName = "System.Data.SqlClient"
            //};
        }
    }
    /// <summary>
    /// 数据库连接结构/方案
    /// </summary>
    public class DbConnectionSchema
    {
        public const string Provider_SQLite = "System.Data.SQLite";
        public const string Provider_SqlClient = "System.Data.SqlClient";
        public const string Provider_SqlCe = "System.Data.SqlServerCe";
        public const string Provider_Oracle = "System.Data.OracleClient";
        //public const string Provider_Oracle = "Oracle.DataAccess.Client";
        public const string Provider_Odbc = "System.Data.Odbc";

        public const string DbName_LocalServer = "LocalSqlServer";

        private string _database;

        public DbConnectionSchema(string connectionString, string providerName)
        {
            this.ConnectionString = connectionString;
            this.ProviderName = providerName;
        }

        /// <summary>
        /// 子类使用
        /// </summary>
        internal DbConnectionSchema() { }

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; internal set; }

        /// <summary>
        /// 连接的提供器名称
        /// </summary>
        public string ProviderName { get; internal set; }
        /// <summary>
        /// sqlerver
        /// sqlite
        /// sqlce
        /// oracle
        /// </summary>
        public DbProviderType ProviderNameType
        {
            get
            {
                switch (this.ProviderName)
                {
                    case Provider_SqlClient:
                        return DbProviderType.SqlServer;
                    case Provider_SQLite:
                        return DbProviderType.SQLite;
                    case Provider_SqlCe:
                        return DbProviderType.SqlServerCe;
                    case Provider_Oracle:
                        return DbProviderType.Oracle;
                    case Provider_Odbc:
                        return DbProviderType.ODBC;
                    default:
                        return DbProviderType.SqlServer;
                }
            }
        }
        /// <summary>
        /// 对应的数据库名称
        /// </summary>
        public string Database
        {
            get
            {
                if (this._database == null)
                {
                    this.ParseDbName();
                }

                return this._database;
            }
        }

        private void ParseDbName()
        {
            var factory = DbProviderFactories.GetFactory(this.ProviderName);
            var con = factory.CreateConnection();
            con.ConnectionString = this.ConnectionString;
            var database = con.Database;

            //System.Data.OracleClient 解析不出这个值，需要特殊处理。
            if (string.IsNullOrWhiteSpace(database))
            {
                //Oracle 中，把用户名（Schema）认为数据库名。
                var match = Regex.Match(this.ConnectionString, @"User Id=\s*(?<dbName>\w+)\s*");
                if (!match.Success)
                {
                    throw new NotSupportedException("无法解析出此数据库连接字符串中的数据库名：" + this.ConnectionString);
                }
                database = match.Groups["dbName"].Value;
            }

            this._database = database;
        }
    }
}