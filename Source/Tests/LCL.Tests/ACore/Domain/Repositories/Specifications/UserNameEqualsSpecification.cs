using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LCL.Tests.Domain.Model;
using LCL.Domain.Specifications;

namespace LCL.Tests.Domain.Repositories.Specifications
{
    internal class UserNameEqualsSpecification : UserStringEqualsSpecification
    {
        public UserNameEqualsSpecification(string userName)
            : base(userName)
        {

        }

        public override System.Linq.Expressions.Expression<Func<User, bool>> GetExpression()
        {
            return c => c.UserName == value;
        }
    }
}
