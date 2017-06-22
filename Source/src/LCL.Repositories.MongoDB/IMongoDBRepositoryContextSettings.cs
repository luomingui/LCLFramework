
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;

namespace LCL.Repositories.MongoDB
{

    public delegate string MapTypeToCollectionNameDelegate(Type type);

    public interface IMongoDBRepositoryContextSettings
    {
        string DatabaseName { get; }
        MongoServerSettings ServerSettings { get; }
        MongoDatabaseSettings GetDatabaseSettings(MongoServer server);
        MapTypeToCollectionNameDelegate MapTypeToCollectionName { get; }
    }
}
