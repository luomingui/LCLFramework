using System;
using System.Linq.Expressions;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LCL.Specifications;
using LCL.Tests.Common;

namespace LCL.UnitTest.Domain
{
    /// <summary>
    /// Summary description for SpecificationTests
    /// </summary>
    [TestClass]
    public class SpecificationTests
    {

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateNotSpecificationNullSpecificationThrowArgumentNullExceptionTest()
        {
            //Arrange
            NotSpecification<User> notSpec;

            //Act
            notSpec = new NotSpecification<User>((ISpecification<User>)null);
        }
    }
}
