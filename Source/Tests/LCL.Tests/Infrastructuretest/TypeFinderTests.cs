using LCL.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;


namespace LCL.Tests
{
    [TestClass]
    public class TypeFinderTests
    {
        [TestMethod]
        public void TypeFinder_Benchmark_Findings()
        {
            var finder = new AppDomainTypeFinder();

            var type = finder.FindClassesOfType<ISomeInterface>();
            type.Count().ShouldEqual(1);
            typeof(ISomeInterface).IsAssignableFrom(type.FirstOrDefault()).ShouldBeTrue();
        }

        public interface ISomeInterface
        {
        }

        public class SomeClass : ISomeInterface
        {
        }
    }
}
