using System;
using LCL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LCL.Tests;

namespace LCL.Tests.Extensions
{
    [TestClass]
    public class ObjectExtensions_Tests
    {
       [TestMethod]
        public void As_Test()
        {
            var obj = (object)new ObjectExtensions_Tests();
            obj.As<ObjectExtensions_Tests>();

            obj = null;
            obj.As<ObjectExtensions_Tests>();
        }

      [TestMethod]
        public void To_Tests()
        {
            "42".To<int>().ShouldBe(42);
            "42".To<Int32>().ShouldBe(42);

            "28173829281734".To<long>();
            "28173829281734".To<Int64>();

            "2.0".To<double>();
            "0.2".To<double>();
            (2.0).To<int>().ShouldBe(2);

            "false".To<bool>();
            "True".To<bool>();
           
        }

    
    }
}
