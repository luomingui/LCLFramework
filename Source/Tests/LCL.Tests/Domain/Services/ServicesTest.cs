using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LCL.Tests.ACore.Domain.Services;

namespace LCL.Tests.Domain.Services
{
    [TestClass]
    public class ServicesTest : TestsBase
    {
        [TestMethod]
        public void TestMethod1()
        {
            var server= RF.Service<IOrderService>();
            server.Confirm(Guid.Empty);
        }
    }
}
