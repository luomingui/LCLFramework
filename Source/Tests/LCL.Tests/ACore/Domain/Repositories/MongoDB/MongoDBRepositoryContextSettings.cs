using LCL.Repositories.MongoDB;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCL.Tests.ACore.Domain.Repositories.MongoDB
{
    public class MongoDBRepositoryContextSettings : IMongoDBRepositoryContextSettings
    {
        #region IMongoDBRepositoryContextSettings Members

        public MongoServerSettings ServerSettings
        {
            get
            {
                var settings = new MongoServerSettings();
                settings.Server = new MongoServerAddress("localhost");
                settings.WriteConcern = WriteConcern.Acknowledged;
                return settings;
            }
        }

        public MongoDatabaseSettings GetDatabaseSettings(MongoServer server)
        {
            return new MongoDatabaseSettings();
        }

        public MapTypeToCollectionNameDelegate MapTypeToCollectionName
        {
            get { return null; }
        }

        public string DatabaseName
        {
            get { return Helper.MongoDB_Database; }
        }

        #endregion
    }
}
