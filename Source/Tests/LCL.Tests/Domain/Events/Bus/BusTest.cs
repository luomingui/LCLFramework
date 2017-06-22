using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using LCL.Tests;
using LCL;
using LCL.Bus;
using LCL.Domain.Services;
using LCL.Tests.ACore;
using LCL.Tests.Domain.Events;

namespace LCL.Tests.Events.Bus
{
    [TestClass]
    public class BusTest : TestsBase
    {
      
        [TestMethod]
        public void Should_Call_Action_On_Event_With_Correct_Source()
        {

            RF.Service<EventBus>().Publish<OrderDispatchedEvent>(new OrderDispatchedEvent());

            RF.Service<EventBus>().Commit();

        }
      
    }

}
