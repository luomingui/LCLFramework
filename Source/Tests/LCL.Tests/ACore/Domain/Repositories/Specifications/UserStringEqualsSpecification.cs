using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LCL.Tests.Domain.Model;
using LCL.Domain.Specifications;

namespace LCL.Tests.Domain.Repositories.Specifications
{
    internal abstract class UserStringEqualsSpecification : Specification<User>
    {
        protected readonly string value;

        public UserStringEqualsSpecification(string value)
        {
            this.value = value;
        }
    }
}
