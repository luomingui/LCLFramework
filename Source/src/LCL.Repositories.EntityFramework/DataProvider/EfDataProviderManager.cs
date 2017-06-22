using System;
using LCL.LData;

namespace LCL.Repositories.EntityFramework
{
    public partial class EfDataProviderManager : BaseDataProviderManager
    {
        public EfDataProviderManager(DbSetting settings)
            : base(settings)
        {
        }

        public override IDataProvider LoadDataProvider()
        {
            var providerName = Settings.ProviderName;
            if (String.IsNullOrWhiteSpace(providerName))
                throw new Exception("Data Settings doesn't contain a providerName");
            switch (providerName.ToLowerInvariant())
            {
                case "sqlserver":
                case "system.data.sqlclient":
                    return new SqlServerDataProvider();
                case "sqlce":
                case "system.data.sqlserverce":
                    return new SqlCeDataProvider();
                case "oracle":
                case "system.data.oracleclient":
                default:
                    throw new Exception(string.Format("Not supported dataprovider name: {0}", providerName));
            }
        }

    }
}
