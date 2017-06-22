using LCL.LData;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;

namespace LCL.Repositories.EntityFramework
{
    public class SqlCeDataProvider : IDataProvider
    {
        public virtual void InitConnectionFactory()
        {
            var connectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");
            //TODO fix compilation warning (below)
            #pragma warning disable 0618
            Database.DefaultConnectionFactory = connectionFactory;
        }
        public virtual void InitDatabase()
        {
            InitConnectionFactory();
            SetDatabaseInitializer();
        }
        public virtual void SetDatabaseInitializer()
        {
            var initializer = new CreateCeDatabaseIfNotExists<BaseDbContext>();
            Database.SetInitializer(initializer);
        }
        public virtual bool StoredProceduredSupported
        {
            get { return false; }
        }
        public virtual DbParameter GetParameter()
        {
            return new SqlParameter();
        }
    }
}
