using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using LCL.Domain.Model;
using LCL.Domain.Specifications;

namespace LCL.Domain.Repositories.Specifications
{
    public class UserRolesSpecification : Specification<UserRole>
    {
        private readonly Guid userID;

        public UserRolesSpecification(User user)
        {
            this.userID = user.ID;
        }

        public override Expression<Func<UserRole, bool>> GetExpression()
        {
            return p => p.UserID == userID;
        }
    }
}
