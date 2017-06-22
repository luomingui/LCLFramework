using System;
using System.Linq.Expressions;

namespace LCL.Domain.Specifications
{
    public sealed class AnySpecification<T> : Specification<T>
    {
        #region Public Methods
        public override Expression<Func<T, bool>> GetExpression()
        {
            return o => true;
        }
        #endregion
    }
}
