using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LCL.UnitTest
{
    //copy "$(TargetPath)" "$(SolutionDir)Testing\LCL.UnitTest\bin\Debug\Plugins\"
    [TestClass]
    public class SimpleTest
    {
        [ClassInitialize]
        public static void SimpleTest_ClassInitialize(TestContext context)
        {
            ServerTestHelper.ClassInitialize(context);
        }
    }
}
