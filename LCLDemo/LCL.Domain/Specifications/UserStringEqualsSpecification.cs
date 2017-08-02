using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LCL.Domain.Model;
using LCL.Domain.Specifications;

namespace LCL.Domain.Repositories.Specifications
{
    public abstract class UserStringEqualsSpecification : Specification<User>
    {
        protected readonly string value;

        public UserStringEqualsSpecification(string value)
        {
            this.value = value;
        }
    }
}
