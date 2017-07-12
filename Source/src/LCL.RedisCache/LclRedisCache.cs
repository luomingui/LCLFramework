using System;
using System.Reflection;
using LCL;
using StackExchange.Redis;
using LCL.Domain.Entities;

namespace LCL.Caching.Redis
{
    public class LclRedisCache : ICacheProvider
    {
        private readonly IDatabase _database;
        private readonly IRedisCacheSerializer _serializer;
        public LclRedisCache(IAbpRedisCacheDatabaseProvider redisCacheDatabaseProvider,IRedisCacheSerializer redisCacheSerializer)
        {
            _database = redisCacheDatabaseProvider.GetDatabase();
            _serializer = redisCacheSerializer;
        }


        public void Clear()
        {
            _database.KeyDeleteWithPrefix(GetLocalizedKey("*"));
        }

        public bool Exists(string key)
        {
            if (GetOrDefault(key)!=null)
                return true;
            else
                return false;
        }

        public T Get<T>(string key)
        {
            var objbyte = _database.StringGet(GetLocalizedKey(key));
            return objbyte.HasValue ? (T)Deserialize(objbyte) : default(T);
        }
        public object GetOrDefault(string key)
        {
            var objbyte = _database.StringGet(GetLocalizedKey(key));
            return objbyte.HasValue ? Deserialize(objbyte) : null;
        }
        public void Remove(string key)
        {
            _database.KeyDelete(GetLocalizedKey(key));
        }

        public void Set(string key, object data, int cacheTime)
        {
            var type = data.GetType();
            if (EntityHelper.IsEntity(type) && type.GetAssembly().FullName.Contains("EntityFrameworkDynamicProxies"))
            {
                type = type.GetTypeInfo().BaseType;
            }
            TimeSpan ts1 = TimeSpan.FromHours(cacheTime);
            _database.StringSet(
              GetLocalizedKey(key),
              Serialize(data, type),
              ts1
              );
        }
        protected virtual string Serialize(object value, Type type)
        {
            return _serializer.Serialize(value, type);
        }

        protected virtual object Deserialize(RedisValue objbyte)
        {
            return _serializer.Deserialize(objbyte);
        }

        protected virtual string GetLocalizedKey(string key)
        {
            return  "c:" + key;
        }

      
    }
}
