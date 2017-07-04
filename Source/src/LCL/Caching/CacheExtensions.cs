
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
    }
}
