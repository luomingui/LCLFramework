using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LCL.ComponentModel;
using Apworks.Tests.Buses.Commands;
using LCL.Bus;

namespace LCL.UnitTest
{
    [TestClass]
    public class BusUnitTest
    {
        [ClassInitialize]
        public static void SimpleTest_ClassInitialize(TestContext context)
        {
            ServerTestHelper.ClassInitialize(context);
        }
        [TestMethod]
        public void TestMethod1()
        {
            UserCommand cmd = new UserCommand("dddddd", "my@163.com");
            using (ICommandBus bus = LEnvironment.AppObjectContainer.Resolve<ICommandBus>())
            {
                bus.Publish(cmd);
                bus.Commit();
            }
        }
    }
}
