using System;
using System.Linq.Expressions;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LCL.Specifications;
using LCL.Tests.Common;
using LCL.Repositories;
using LCL.Tests.Repositories.EntityFrameworkRepository;

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
        public void CreateNotSpeNullSpecThrowArgumentNullExceptionTest()
        {
            //Arrange
            NotSpecification<User> notSpec;
            //Act
            notSpec = new NotSpecification<User>((ISpecification<User>)null);
        }
        [TestMethod()]
        public void SpecEvalTest()
        {
            ISpecification<User> spec = Specification<User>.Eval(p => p.Name == "" && p.IsDelete == true);
        }
        [TestMethod()]
        public void SpecClassTest()
        {
            var spec = new KeyMaintenanceSpecification("username1", "Name")
              .And(new KeyMaintenanceSpecification("admin", "Name"))
              .And(new DateMaintenanceSpecification(DateTime.Now, DateTime.Now.AddMonths(1), "AddDate"));
        }
    }
}
