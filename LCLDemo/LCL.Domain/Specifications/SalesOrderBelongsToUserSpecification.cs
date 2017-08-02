using LCL.Domain.Specifications;
using LCL.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LCL.Domain.Repositories.Specifications
{
    public class SalesOrderBelongsToUserSpecification : Specification<SalesOrder>
    {
        private readonly User user;

        public SalesOrderBelongsToUserSpecification(User user)
        {
            this.user = user;
        }

        public override System.Linq.Expressions.Expression<Func<SalesOrder, bool>> GetExpression()
        {
            return so => so.User.ID == user.ID;
        }
    }
}
