using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LCL.Repositories;
using LCL.Repositories.EntityFramework;
using LCL.Tests.Repositories.EntityFrameworkRepository;
using LCL.Tests.Common;

namespace LCL.UnitTest.Domain
{
    [TestClass]
    public class RFUnitTest
    {
        [ClassInitialize]
        public static void SimpleTest_ClassInitialize(TestContext context)
        {
            Helper.ClassInitialize(context);
        }
        [TestMethod]
        public void TestIOCIRepositoryContext()
        {
            var repo = RF.Concrete<IRepositoryContext>();
            Assert.IsNotNull(repo);
        }
        [TestMethod]
        public void TestIOCRFIUserRepository()
        {
            var repo = RF.Concrete<IUserRepository>();
            Assert.IsNotNull(repo);
        }
        [TestMethod]
        public void TestIOCRFFindRepo()
        {
            var repo = RF.FindRepo<User>();
            Assert.IsNotNull(repo);
        }
    }
}
