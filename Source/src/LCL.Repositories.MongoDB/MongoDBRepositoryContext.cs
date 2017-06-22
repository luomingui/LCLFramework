
using LCL.Domain.Repositories;
using LCL.Repositories.MongoDB.Conventions;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace LCL.Repositories.MongoDB
{
    public class MongoDBRepositoryContext : RepositoryContext, IMongoDBRepositoryContext
    {
        #region Private Fields
        private readonly Guid id = Guid.NewGuid();
        private readonly MongoServer server;
        private readonly MongoDatabase database;
        private readonly IMongoDBRepositoryContextSettings settings;
        private readonly object syncObj = new object();
        private readonly Dictionary<Type, MongoCollection> mongoCollections = new Dictionary<Type, MongoCollection>();
        #endregion

        #region Ctor
        public MongoDBRepositoryContext(IMongoDBRepositoryContextSettings settings)
        {
            this.settings = settings;
            server = new MongoServer(settings.ServerSettings);
            database = server.GetDatabase(settings.DatabaseName, settings.GetDatabaseSettings(server));
        }
        #endregion

        #region Protected Methods
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                server.Disconnect();
            }
            base.Dispose(disposing);
        }
        #endregion

        #region Public Static Methods

        public static void RegisterConventions(bool autoGenerateID = true, bool localDateTime = true)
        {
            RegisterConventions(autoGenerateID, localDateTime, null);
        }

        public static void RegisterConventions(bool autoGenerateID, bool localDateTime, IEnumerable<IConvention> additionConventions)
        {
            var conventionPack = new ConventionPack();
            conventionPack.Add(new NamedIdMemberConvention("id", "Id", "ID", "iD"));
            if (autoGenerateID)
                conventionPack.Add(new GuidIDGeneratorConvention());
            if (localDateTime)
                conventionPack.Add(new UseLocalDateTimeConvention());
            if (additionConventions != null)
                conventionPack.AddRange(additionConventions);
            
            ConventionRegistry.Register("DefaultConvention", conventionPack, t => true);
        }

        #endregion

        #region IMongoDBRepositoryContext Members

        public IMongoDBRepositoryContextSettings Settings
        {
            get { return settings; }
        }

        public MongoCollection GetCollectionForType(Type type)
        {
            lock (syncObj)
            {
                if (this.mongoCollections.ContainsKey(type))
                    return this.mongoCollections[type];
                else
                {
                    MongoCollection mongoCollection = null;
                    if (settings.MapTypeToCollectionName != null)
                        mongoCollection = this.database.GetCollection(settings.MapTypeToCollectionName(type));
                    else
                        mongoCollection = this.database.GetCollection(type.Name);
                    this.mongoCollections.Add(type, mongoCollection);
                    return mongoCollection;
                }
            }
        }
        #endregion

        #region IUnitOfWork Members

        public override void Commit()
        {
            lock (syncObj)
            {
                foreach (var newObj in this.NewCollection)
                {
                    MongoCollection collection = this.GetCollectionForType(newObj.GetType());
                    collection.Insert(newObj);
                }
                foreach (var modifiedObj in this.ModifiedCollection)
                {
                    MongoCollection collection = this.GetCollectionForType(modifiedObj.GetType());
                    collection.Save(modifiedObj);
                }
                foreach (var delObj in this.DeletedCollection)
                {
                    Type objType = delObj.GetType();
                    PropertyInfo propertyInfo = objType.GetProperty("ID", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                    if (propertyInfo == null)
                        throw new InvalidOperationException("Cannot delete an object which doesn't contain an ID property.");
                    Guid id = (Guid)propertyInfo.GetValue(delObj, null);
                    MongoCollection collection = this.GetCollectionForType(objType);
                    IMongoQuery query = Query.EQ("_id", id);
                    collection.Remove(query);
                }
                this.ClearRegistrations();
                this.Committed = true;
            }
        }

        public override void Rollback()
        {
            this.Committed = false;
        }

        #endregion
    }
}
