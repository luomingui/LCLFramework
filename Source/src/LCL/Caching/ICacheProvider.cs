using System;

namespace LCL.Caching
{
    /// <summary>
    /// 表示实现该接口的类型是能够为应用程序提供缓存机制的类型。
    /// </summary>
    public interface ICacheProvider
    {
        #region Methods
        object GetOrDefault(string key);
        T Get<T>(string key);
        void Set(string key, object data, int cacheTime);
        bool Exists(string key);
        void Remove(string key);
        void Clear();
        #endregion
    }
}
