using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace LCL.Caching
{
    /// <summary>
    /// 基于MemoryCache的缓存辅助类
    /// </summary>
    public static class MemoryCacheHelper
    {
        private static readonly Object _locker = new object();
        private static readonly ObjectCache _cache = MemoryCache.Default;
        private static readonly List<string> _cachekeylist = new List<string>();
        public static object GetCache(string CacheKey, object objObject = null)
        {
            if (objObject != null)
                SetCache(CacheKey, objObject);

            return _cache.Get(CacheKey);
        }
        public static void SetCache(string CacheKey, object objObject)
        {
            var item = new CacheItem(CacheKey, objObject);
            var policy = CreatePolicy(TimeSpan.FromMinutes(30), DateTime.Now.AddMinutes(40));
            _cache.Add(item, policy);
            _cachekeylist.Add(CacheKey);
        }
        public static void RemoveCache(string CacheKey)
        {
            try
            {
                _cache.Remove(CacheKey);
            }
            catch (Exception ex)
            {
                Logger.LogError("删除缓存" + CacheKey, ex);
            }
        }
        public static void RemoveEntityCache(string EntityName)
        {
            try
            {
                foreach (var item in _cachekeylist)
                {
                    if (item.StartsWith("LCL_Cache_" + EntityName))
                        _cache.Remove(item);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("删除缓存" + EntityName, ex);
            }
        }
        public static void RemoveCacheAll()
        {
            try
            {
                foreach (var item in _cachekeylist)
                {
                    _cache.Remove(item);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("删除缓存", ex);
            }
        }
        public static bool IsCache(string CacheKey)
        {
            object obj = GetCache(CacheKey);
            if (obj == null) return false;
            else return true;
        }
        public static T GetCacheItem<T>(String CacheKey, Func<T> cachePopulate,
            TimeSpan? slidingExpiration = null, DateTime? absoluteExpiration = null)
        {
            if (String.IsNullOrWhiteSpace(CacheKey)) throw new ArgumentException("Invalid cache key");
            if (cachePopulate == null) throw new ArgumentNullException("cachePopulate");
            if (slidingExpiration == null && absoluteExpiration == null) throw new ArgumentException("Either a sliding expiration or absolute must be provided");

            if (MemoryCache.Default[CacheKey] == null)
            {
                lock (_locker)
                {
                    if (MemoryCache.Default[CacheKey] == null)
                    {
                        var item = new CacheItem(CacheKey, cachePopulate());
                        var policy = CreatePolicy(slidingExpiration, absoluteExpiration);

                        MemoryCache.Default.Add(item, policy);
                        _cachekeylist.Add(CacheKey);
                    }
                }
            }
            return (T)MemoryCache.Default[CacheKey];
        }
        private static CacheItemPolicy CreatePolicy(TimeSpan? slidingExpiration, DateTime? absoluteExpiration)
        {
            var policy = new CacheItemPolicy();

            if (absoluteExpiration.HasValue)
            {
                policy.AbsoluteExpiration = absoluteExpiration.Value;
            }
            else if (slidingExpiration.HasValue)
            {
                policy.SlidingExpiration = slidingExpiration.Value;
            }
            policy.Priority = CacheItemPriority.Default;
            return policy;
        }
    }
}
