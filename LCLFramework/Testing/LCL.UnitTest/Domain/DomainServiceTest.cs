//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using LCL.UnitTestLibrary;
//using LCL.DomainServices;

//namespace LCL.UnitTest.Domain
//{
//    [TestClass]
//    public class DomainServiceTest
//    {
//        [ClassInitialize]
//        public static void SimpleTest_ClassInitialize(TestContext context)
//        {
//            ServerTestHelper.ClassInitialize(context);
//        }

//        [TestMethod]
//        public void DT_Service_InterfaceContract()
//        {
//            var service = DomainServiceFactory.Create<ITestAddService>();
//            service.A = 10;
//            service.B = 20;
//            service.Invoke();
//            Assert.IsTrue(service.Result == 30);
//        }

//        /// <summary>
//        /// 本方法用于测试：在接口的属性上标记的输入与输出，不需要再在服务上进行标记。
//        /// </summary>
//        [TestMethod]
//        public void DT_Service_InterfaceContract_MarkPropertyOnInterface()
//        {
//            var service = new TestAddService();
//            service.A = 10;
//            service.B = 20;
//            service.Invoke();
//            Assert.IsTrue(service.Result == 30);
//        }

//        [TestMethod]
//        public void DT_Service_Override()
//        {
//            var service = DomainServiceFactory.Create<AddBookService>();
//            service.Invoke();
//            Assert.IsTrue(service.Result == 3);
//        }

//        [TestMethod]
//        public void DT_Service_Override_VersionSpecific()
//        {
//            var service = DomainServiceFactory.Create(typeof(AddBookService), new Version("1.0.0.2")) as AddBookService;
//            service.Invoke();
//            Assert.IsTrue(service.Result == 2);
//        }
//    }
//}
