
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCL.Caching
{
    public static class CacheExtensions
    {
        public static T Get<T>(this ICacheProvider cacheManager, string key, Func<T> acquire)
        {
            return Get(cacheManager, key, 60, acquire);
        }
        public static T Get<T>(this ICacheProvider cacheManager, string key, int cacheTime, Func<T> acquire)
        {
            if (cacheManager.Exists(key))
            {
                return cacheManager.Get<T>(key);
            }
            else
            {
                var result = acquire();
                if (cacheTime > 0)
                    cacheManager.Set(key, result, cacheTime);
                return result;
            }
        }
        public static TValue Get<TKey, TValue>(this ICacheProvider cache, TKey key, Func<TKey, TValue> factory)
        {
            return (TValue)cache.Get(key.ToString(), (k) => (object)factory(key));
        }
        public static TValue Get<TKey, TValue>(this ICacheProvider cache, TKey key, Func<TValue> factory)
        {
            return cache.Get(key, (k) => factory());
        }
        public static TValue GetOrDefault<TKey, TValue>(this ICacheProvider cache, TKey key)
        {
            var value = cache.GetOrDefault(key.ToString());
            if (value == null)
            {
                return default(TValue);
            }

            return (TValue)value;
        }
    }
}
