using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LCL.Tests.Common;
using LCL.Repositories;
using LCL.Tests.Repositories.EntityFrameworkRepository;
using LCL.Transactions;
using LCL.Events.Bus;
using LCL.Repositories.EntityFramework;

namespace LCL.UnitTest.Events
{
    [TestClass]
    public class EventsUnitTest
    {
        [ClassInitialize]
        public static void SimpleTest_ClassInitialize(TestContext context)
        {
            Helper.ClassInitialize(context);
            Helper.ClearMessageQueue(Helper.EventBus_MessageQueue);
        }
        [TestMethod]
        public void TestMethod1()
        {
            var context = RF.Concrete<IRepositoryContext>();// new EntityFrameworkRepositoryContext(new EFTestDbContext());
            var bus = new MSMQEventBus(Helper.EventBus_MessageQueue);
            var repo = RF.Concrete<IUserRepository>();
            var entity = new User { ID = Guid.NewGuid(), Name = "EventsUnitTest", Email = "123@qq.com", Password = "123456" };
            repo.Create(entity);
            repo.Context.Commit();
            using (ITransactionCoordinator coordinator = TransactionCoordinatorFactory.Create(context, bus))
            {
                var tongz = repo.GetByKey(entity.ID);
                tongz.ChangeEmail("321@qq.com");
                coordinator.Commit();
            }
        }
    }
}
