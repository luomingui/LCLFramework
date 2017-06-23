using LCL;
using LCL.Domain.Repositories;
using LCL.Domain.Specifications;
using LCL.Infrastructure;
using LCL.Repositories.XML;
using LCL.Tests.ACore;
using LCL.Tests.Domain.Model;
using LCL.Tests.Domain.Repositories.XDocuments;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LCL.Tests.Repositories.XMLRepositorys
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class XMLRepositoryTests
    {

        [TestMethod]
        public void XMLRepositoryTests_SaveAggregateRootTest()
        {
            User customer = new User
            {
                Email = "minguiluo@163.com",
                Password = "123456"
            };

            var con = new XMLRepositoryContext();
            var customerRepository = new UserRepository(con);

            customerRepository.Insert(customer);
            customerRepository.Context.Commit();
            customerRepository.Context.Dispose();

        }

    }

  
}
