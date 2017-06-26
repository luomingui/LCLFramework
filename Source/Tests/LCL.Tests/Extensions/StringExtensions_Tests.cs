using System;
using System.Globalization;
using System.Linq;
using LCL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LCL.Tests.Extensions
{
    [TestClass]
    public class StringExtensions_Tests
    {
      [TestMethod]
        public void Right_Test()
        {
            const string str = "This is a test string";
            str.Right(3);
            str.Right(0);
            str.Right(str.Length);
        }

       [TestMethod]
        public void Left_Test()
        {
            const string str = "This is a test string";
            str.Left(3);
            str.Left(0);
            str.Left(str.Length);
        }

       [TestMethod]
        public void NormalizeLineEndings_Test()
        {
            const string str = "This\r\n is a\r test \n string";
            var normalized = str.NormalizeLineEndings();
            var lines = normalized.SplitToLines();
            lines.Length.ShouldBe(4);
        }

       [TestMethod]
        public void NthIndexOf_Test()
        {
            const string str = "This is a test string";

            str.NthIndexOf('i', 0).ShouldBe(-1);
            str.NthIndexOf('i', 1).ShouldBe(2);
            str.NthIndexOf('i', 2).ShouldBe(5);
            str.NthIndexOf('i', 3).ShouldBe(18);
            str.NthIndexOf('i', 4).ShouldBe(-1);
        }

        [TestMethod]
        public void Truncate_Test()
        {
            const string str = "This is a test string";
            const string nullValue = null;

            str.Truncate(7);
            str.Truncate(0);
            str.Truncate(100);

            nullValue.Truncate(5);
        }

       [TestMethod]
        public void TruncateWithPostFix_Test()
        {
            const string str = "This is a test string";
            const string nullValue = null;

            str.TruncateWithPostfix(3);
            str.TruncateWithPostfix(12);
            str.TruncateWithPostfix(0);
            str.TruncateWithPostfix(100);

            nullValue.Truncate(5);

            str.TruncateWithPostfix(3, "~");
            str.TruncateWithPostfix(12, "~");
            str.TruncateWithPostfix(0, "~");
            str.TruncateWithPostfix(100, "~");

            nullValue.TruncateWithPostfix(5, "~");
        }

       [TestMethod]
        public void RemovePostFix_Tests()
        {
            //null case
            (null as string).RemovePreFix("Test").ShouldBeNull();

            //Simple case
            "MyTestAppService".RemovePreFix("AppService");
            "MyTestAppService".RemovePreFix("Service");

            //Multiple postfix (orders of postfixes are important)
            "MyTestAppService".RemovePreFix("AppService", "Service");
            "MyTestAppService".RemovePreFix("Service", "AppService");

            //Unmatched case
            "MyTestAppService".RemovePreFix("Unmatched");
        }

        [TestMethod]
        public void RemovePreFix_Tests()
        {
            "Home.Index".RemovePreFix("NotMatchedPostfix");
            "Home.About".RemovePreFix("Home.");
        }

        [TestMethod]
        public void ToEnum_Test()
        {
            "MyValue1".ToEnum<MyEnum>();
            "MyValue2".ToEnum<MyEnum>();
        }

        private enum MyEnum
        {
            MyValue1,
            MyValue2
        }
    }
}