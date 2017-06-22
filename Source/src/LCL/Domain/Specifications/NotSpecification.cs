using System;
using System.Linq.Expressions;

namespace LCL.Domain.Specifications
{
    public class NotSpecification<T> : Specification<T>
    {
        #region Private Fields
        private ISpecification<T> spec;
        #endregion

        #region Ctor
        public NotSpecification(ISpecification<T> specification)
        {
            this.spec = specification;
        }
        #endregion

        #region Public Methods
        public override Expression<Func<T, bool>> GetExpression()
        {
            var body = Expression.Not(this.spec.GetExpression().Body);
            return Expression.Lambda<Func<T, bool>>(body, this.spec.GetExpression().Parameters);
        }
        #endregion
    }
}
