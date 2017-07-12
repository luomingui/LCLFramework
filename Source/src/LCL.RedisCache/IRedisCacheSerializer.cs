using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace LCL.Caching.Redis
{
    public interface IRedisCacheSerializer
    {
        object Deserialize(RedisValue objbyte);

        string Serialize(object value, Type type);
    }
}
