
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using LCL.Domain.Repositories;

namespace LCL.Repositories.MongoDB
{
    public interface IMongoDBRepositoryContext : IRepositoryContext
    {
        IMongoDBRepositoryContextSettings Settings { get; }
        MongoCollection GetCollectionForType(Type type);
    }
}
