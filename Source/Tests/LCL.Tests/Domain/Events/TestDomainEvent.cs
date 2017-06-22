
using LCL;
using LCL.Bus;
using LCL.Domain.Events;
using LCL.Tests.ACore;
using LCL.Tests.Domain.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCL.Tests.Events
{
    [TestClass]
    public class TestDomainEvent : TestsBase
    {
        [TestMethod]
        public void TestBusMethod()
        {
            var domainevent = new OrderConfirmedEvent();
            DomainEvent.Publish<OrderConfirmedEvent>(domainevent);
        }
        [TestMethod]
        public void MyTestMethod()
        {
            var domainevent = new OrderDispatchedEvent();
            DomainEvent.Publish<OrderDispatchedEvent>(domainevent);
        }

    }
}
