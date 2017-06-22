using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LCL;
using LCL.Infrastructure;

namespace LCL.Tests
{
    [TestClass]
    public class TestsBase
    {
        [TestInitialize]
        public virtual void SetUp()
        {
            //initialize engine context
            EngineContext.Initialize(false);
        }
    }
}
