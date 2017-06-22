
using System;
using System.Collections.Generic;
using LCL.Domain.Repositories;
using LCL.Repositories.EntityFramework;
using LCL.Tests.Domain.Model;
using LCL.Tests.Domain.Repositories;
using LCL.Tests.Domain.Repositories.Specifications;
using LCL;
namespace LCL.Tests.Domain.Repositories.EntityFramework
{
    public class SalesOrderRepository : EntityFrameworkRepository<SalesOrder>, ISalesOrderRepository
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
