
using LCL;
using LCL.Domain.Repositories;
using LCL.Repositories.MongoDB;
using LCL.Tests.Domain.Model;
using LCL.Tests.Domain.Repositories.Specifications;
using System;
using System.Collections.Generic;

namespace LCL.Tests.Domain.Repositories.MongoDB
{
    public class SalesOrderRepository : MongoDBRepository<SalesOrder>, ISalesOrderRepository
    {
        public SalesOrderRepository(IRepositoryContext context)
            : base(context)
        { }

        #region ISalesOrderRepository Members

        public IEnumerable<SalesOrder> FindSalesOrdersByUser(User user)
        {
            return FindAll(new SalesOrderBelongsToUserSpecification(user), sp => sp.DateCreated, SortOrder.Descending);
        }

        public SalesOrder GetSalesOrderByID(Guid orderID)
        {
            return Find(new SalesOrderIDEqualsSpecification(orderID), elp => elp.SalesLines);
        }

        #endregion
    }
}
