using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LCL.Tests.Domain.Model;
using LCL.Domain.Specifications;


namespace LCL.Tests.Domain.Repositories.Specifications
{
    public class SalesOrderIDEqualsSpecification : Specification<SalesOrder>
    {
        private readonly Guid orderID;

        public SalesOrderIDEqualsSpecification(Guid orderID)
        {
            this.orderID = orderID;
        }

        public override System.Linq.Expressions.Expression<Func<SalesOrder, bool>> GetExpression()
        {
            return p => p.ID == orderID;
        }
    }
}
