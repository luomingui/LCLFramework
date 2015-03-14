
//using LCL.Reflection;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace LCL.UnitTest.Domain
//{
//    [TestClass]
//    public class ReflectionTest
//    {
//        [ClassInitialize]
//        public static void ET_ClassInitialize(TestContext context)
//        {
//            ServerTestHelper.ClassInitialize(context);
//        }

//        private int TestArguments(int a, int? b)
//        {
//            return 1;
//        }
//        private int TestArguments(int a, string c)
//        {
//            return 2;
//        }
//        private int TestArguments(int a, PagingInfo d)
//        {
//            return 3;
//        }
//        private int TestArguments(int? a, int? b)
//        {
//            return 5;
//        }
//        private int TestArguments(int a, int? b, string c, PagingInfo d)
//        {
//            return 4;
//        }

//        [TestMethod]
//        public void Hxy_Reflection_MethodCaller_ArgumentMatch1()
//        {
//            try
//            {
//                MethodCaller.CallMethod(this, "TestArguments", null, "");
//                //Assert.IsFalse(true, "应该无法找到对应的方法。");
//            }
//            catch (InvalidProgramException) { }
//        }

//        [TestMethod]
//        public void Hxy_Reflection_MethodCaller_ArgumentMatch2()
//        {
//            try
//            {
//                var res = (int)MethodCaller.CallMethod(this, "TestArguments", 1, null);
//                //Assert.IsFalse(true, "应该找到过多的方法。");
//            }
//            catch (InvalidProgramException) { }
//        }
//        [TestMethod]
//        public void Hxy_Reflection_MethodCaller_ArgumentMatch3()
//        {
//            var res = (int)MethodCaller.CallMethod(this, "TestArguments", 1, new MethodCaller.NullParameter { ParameterType = typeof(string) });
//            Assert.IsTrue(res == 2);
//        }

//        [TestMethod]
//        public void Hxy_Reflection_MethodCaller_ArgumentMatch4()
//        {
//            var res = (int)MethodCaller.CallMethod(this, "TestArguments", 1, PagingInfo.Empty);
//            Assert.IsTrue(res == 3);
//        }

//        [TestMethod]
//        public void Hxy_Reflection_MethodCaller_ArgumentMatch5()
//        {
//            var res = (int)MethodCaller.CallMethod(this, "TestArguments", 1, "SDF");
//            Assert.IsTrue(res == 2);
//        }

//        [TestMethod]
//        public void Hxy_Reflection_MethodCaller_ArgumentMatch6()
//        {
//            var res = (int)MethodCaller.CallMethod(this, "TestArguments", 1, 1);
//            Assert.IsTrue(res == 1);
//        }

//        [TestMethod]
//        public void Hxy_Reflection_MethodCaller_ArgumentMatch7()
//        {
//            var res = (int)MethodCaller.CallMethod(this, "TestArguments", null, 1);
//            Assert.IsTrue(res == 5);
//        }

//        [TestMethod]
//        public void Hxy_Reflection_MethodCaller_ArgumentMatch8()
//        {
//            var res = (int)MethodCaller.CallMethod(this, "TestArguments", 1, 1, "", null);
//            Assert.IsTrue(res == 4);
//        }

//        [TestMethod]
//        public void Hxy_Reflection_MethodCaller_ArgumentMatch9()
//        {
//            var res = (int)MethodCaller.CallMethod(this, "TestArguments", 1, null, "", null);
//            Assert.IsTrue(res == 4);
//        }

//    }
//}
