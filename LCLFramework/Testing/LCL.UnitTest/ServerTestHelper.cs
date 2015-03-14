using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LCL.UnitTest
{
    public static class ServerTestHelper
    {
        public static void ClassInitialize(TestContext context)
        {
            new TestServerApp().Start();
        }
    }
}
