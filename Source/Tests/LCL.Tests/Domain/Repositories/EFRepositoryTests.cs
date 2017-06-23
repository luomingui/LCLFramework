using LCL;
using LCL.Domain.Repositories;
using LCL.Domain.Specifications;
using LCL.Infrastructure;
using LCL.Tests.ACore;
using LCL.Tests.Domain.Model;
using LCL.Tests.Domain.Repositories;
using LCL.Transactions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LCL.Tests.Repositories.EntityFrameworkRepository
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class EFRepositoryTests
    {
        private const int pageSize = 20;
        private const int pagingTotalRecords = 97;

        public EFRepositoryTests()
        {

        }
        private TestContext testContextInstance;
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            EngineContext.Initialize(false);
        }
        [TestInitialize()]
        public void MyTestInitialize()
        {
            Helper.ClearEFTestDB();
        }

        #endregion

        [TestMethod]
        public void EntityFrameworkRepositoryTests_SaveAggregateRootTest()
        {
            User customer = new User
            {
                UserName = "daxnet",
                Email = "271391233@qq.com",
                Password = "123456"
            };

            var customerRepository = RF.Service<IUserRepository>();

            customerRepository.Insert(customer);
            customerRepository.Context.Commit();
            customerRepository.Context.Dispose();

            int actual = Helper.ReadRecordCountFromEFTestDB(Helper.EF_Table_EFCustomers);

            int expected = 1;
            Assert.AreEqual(expected, actual);
        }
    }
}
