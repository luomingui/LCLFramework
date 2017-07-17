using LCL.Infrastructure;
using LCL.LData;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace LCL.Domain.Repositories
{
    public class EfStartupTask : IStartupTask
    {
        public int Order => 1;

        public void Execute()
        {
            var dbtype = RF.Service<DbSetting>();
            switch (dbtype.ProviderNameType)
            {
                case DbProviderType.SqlServer:
                    Database.DefaultConnectionFactory = new SqlConnectionFactory();
                    break;
                case DbProviderType.MySql:
                    break;
                case DbProviderType.SQLite:
                    break;
                case DbProviderType.Oracle:
                    break;
                case DbProviderType.ODBC:
                    break;
                case DbProviderType.OleDb:
                    break;
                case DbProviderType.MongoDB:
                    break;
                case DbProviderType.Firebird:
                    break;
                case DbProviderType.PostgreSql:
                    break;
                case DbProviderType.DB2:
                    break;
                case DbProviderType.Informix:
                    break;
                case DbProviderType.SqlServerCe:
                    Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");
                    break;
                default:
                    break;
            }

        }
    }
}
