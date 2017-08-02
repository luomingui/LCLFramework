using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LCL.Domain.Model;
using LCL.Domain.Specifications;

namespace LCL.Domain.Repositories.Specifications
{
    public class UserEmailEqualsSpecification : UserStringEqualsSpecification
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
