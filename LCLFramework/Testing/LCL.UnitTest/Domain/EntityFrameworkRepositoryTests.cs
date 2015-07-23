//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using LCL.Repositories;
//using LCL.Tests.Repositories.EntityFrameworkRepository;
//using LCL.Tests.Common;
//using LCL.Specifications;
//using System.Linq;
//using System.Collections.Generic;
//namespace LCL.UnitTest.Domain
//{
//    [TestClass]
//    public class EntityFrameworkRepositoryTests
//    {
//        [ClassInitialize]
//        public static void SimpleTest_ClassInitialize(TestContext context)
//        {
//            Helper.ClassInitialize(context);
//        }

//        #region EntityFrameworkRepositoryTests
//        private const int pageSize = 20;
//        private const int pagingTotalRecords = 97;
//        [TestMethod]
//        public void EntityFrameworkRepositoryTests_SaveAggregateRootTest()
//        {
//            User customer = new User
//            {
//                ID = Guid.NewGuid(),
//                Password = "123456",
//                Name = "admin",
//            };
//            var repository = RF.Concrete<IUserRepository>();
//            repository.Create(customer);
//            repository.Context.Commit();
//            repository.Context.Dispose();
//            Assert.AreNotEqual(0, customer.ID);
//        }
//        [TestMethod]
//        public void EntityFrameworkRepositoryTests_SaveAndLoadAggregateRootTest()
//        {
//            User customer = new User
//            {
//                ID = Guid.NewGuid(),
//                Password = "123456",
//                Name = "admin1",
//            };
//            var repository = RF.Concrete<IUserRepository>();
//            repository.Create(customer);
//            repository.Context.Commit();

//            var key = customer.ID;
//            var customer2 = repository.GetByKey(key);

//            repository.Context.Dispose();
//            Assert.AreEqual(customer.Name, customer2.Name);
//            Assert.AreEqual(customer.Password, customer2.Password);
//        }
//        [TestMethod]
//        public void EntityFrameworkRepositoryTests_RetrieveByAndSpecificationTest()
//        {
//            ISpecification<User> spec = Specification<User>.Eval(p => p.Name.StartsWith("a")).And(Specification<User>.Eval(p => p.Password != "12"));
//            var repository = RF.Concrete<IUserRepository>();
//            var c = repository.FindAll(spec).Count();
//            repository.Context.Dispose();
//            Assert.IsNotNull(c);
//            Assert.AreEqual(1, c);
//        }
//        [TestMethod]
//        public void EntityFrameworkRepositoryTests_Paging_NormalTest()
//        {
//            int pageNumber = 3;
//            List<User> customers = new List<User>();
//            for (int i = 1; i <= pagingTotalRecords; i++)
//                customers.Add(new User
//                {
//                    ID = Guid.NewGuid(),
//                    Password = i.ToString(),
//                    Name = "name" + i,
//                });
//            var repository = RF.Concrete<IUserRepository>();
//            foreach (var cust in customers)
//                repository.Create(cust);
//            repository.Context.Commit();

//            ISpecification<User> spec = Specification<User>.Eval(c => c.Name.StartsWith("name"));

//            var result = repository.FindAll(spec, p => p.Name, SortOrder.Ascending, pageNumber, pageSize);
//            Assert.AreEqual<int>(pageSize, result.PageSize);
//            repository.Context.Dispose();
//        }

//        #endregion
//    }
//}
