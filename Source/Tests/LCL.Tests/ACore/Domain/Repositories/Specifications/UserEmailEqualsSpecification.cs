using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LCL.Tests.Domain.Model;
using LCL.Domain.Specifications;

namespace LCL.Tests.Domain.Repositories.Specifications
{
    internal class UserEmailEqualsSpecification : UserStringEqualsSpecification
    {
        public UserEmailEqualsSpecification(string email)
            : base(email)
        { }

        public override System.Linq.Expressions.Expression<Func<User, bool>> GetExpression()
        {
            return c => c.Email == value;
        }
    }
}
