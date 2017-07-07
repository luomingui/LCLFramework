using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LCL.Caching;
using LCL.Caching.Memory;

namespace LCL.Tests
{
    [TestClass]
    public class MemoryCacheManagerTests : TestsBase
    {
        [TestMethod]
        public void Can_set_and_get_object_from_cache()
        {
            var cacheManager = new MemoryCacheProvider();
            cacheManager.Set("some_key_1", 3, int.MaxValue);

            cacheManager.Get<int>("some_key_1").ShouldEqual(3);
        }

        [TestMethod]
        public void Can_validate_whetherobject_is_cached()
        {
            var cacheManager = new MemoryCacheProvider();
            cacheManager.Set("some_key_1", 3, int.MaxValue);
            cacheManager.Set("some_key_2", 4, int.MaxValue);

            cacheManager.Exists("some_key_1").ShouldEqual(true);
            cacheManager.Exists("some_key_3").ShouldEqual(false);
        }

        [TestMethod]
        public void Can_clear_cache()
        {
            var cacheManager = new MemoryCacheProvider();
            cacheManager.Set("some_key_1", 3, int.MaxValue);

            cacheManager.Clear();

            cacheManager.Exists("some_key_1").ShouldEqual(false);
        }
    }
}
