
using System;
using System.Data.Common;

namespace LCL.LData
{
    public interface IDataProvider
    {
        void InitConnectionFactory();

        void SetDatabaseInitializer();

        void InitDatabase();

        bool StoredProceduredSupported { get; }

        DbParameter GetParameter();
    }

    public abstract class BaseDataProviderManager
    {
        protected BaseDataProviderManager(DbConnectionSchema settings)
        {
            if (settings == null)
                throw new ArgumentNullException("settings");
            this.Settings = settings;
        }
        protected DbConnectionSchema Settings { get; private set; }
        public abstract IDataProvider LoadDataProvider();
    }
}
