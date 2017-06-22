using System;
using LCL.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LCL.Tests
{
   [TestClass]
    public class SingletonTests
    {
       [TestMethod]
        public void Singleton_IsNullByDefault()
        {
            var instance = Singleton<SingletonTests>.Instance;
        }

         [TestMethod]
        public void Singletons_ShareSame_SingletonsDictionary()
        {
            Singleton<int>.Instance = 1;
            Singleton<double>.Instance = 2.0;

            //Singleton<int>.AllSingletons
            //Singleton.AllSingletons[typeof(int)]
            //Singleton.AllSingletons[typeof(double)]
        }

        [TestMethod]
        public void SingletonDictionary_IsCreatedByDefault()
        {
            var instance = SingletonDictionary<SingletonTests, object>.Instance;
        }

         [TestMethod]
        public void SingletonDictionary_CanStoreStuff()
        {
            var instance = SingletonDictionary<Type, SingletonTests>.Instance;
            instance[typeof(SingletonTests)] = this;
        }

         [TestMethod]
        public void SingletonList_IsCreatedByDefault()
        {
            var instance = SingletonList<SingletonTests>.Instance;
        }

        [TestMethod]
        public void SingletonList_CanStoreItems()
        {
            var instance = SingletonList<SingletonTests>.Instance;
            instance.Insert(0, this);
        }
    }
}
