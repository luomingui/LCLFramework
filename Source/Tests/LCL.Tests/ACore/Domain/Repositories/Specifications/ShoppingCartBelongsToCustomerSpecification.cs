using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LCL.Tests.Domain.Model;
using LCL.Domain.Specifications;

namespace LCL.Tests.Domain.Repositories.Specifications
{
    internal class ShoppingCartBelongsToCustomerSpecification : Specification<ShoppingCart>
    {
        private readonly User user;


        public ShoppingCartBelongsToCustomerSpecification(User user)
        {
            this.user = user;
        }

        public override System.Linq.Expressions.Expression<Func<ShoppingCart, bool>> GetExpression()
        {
            return c => c.User.ID == user.ID;
        }
    }
}
