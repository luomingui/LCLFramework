using StackExchange.Redis;

namespace LCL.Caching.Redis
{
    public interface IAbpRedisCacheDatabaseProvider
    {
        IDatabase GetDatabase();
    }
}