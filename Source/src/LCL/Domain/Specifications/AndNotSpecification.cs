using System;
using System.Linq.Expressions;

namespace LCL.Domain.Specifications
{
    public class AndNotSpecification<T> : CompositeSpecification<T>
    {
        #region Ctor
        public AndNotSpecification(ISpecification<T> left, ISpecification<T> right) : base(left, right) { }
        #endregion

        #region Public Methods
        public override Expression<Func<T, bool>> GetExpression()
        {
            var bodyNot = Expression.Not(Right.GetExpression().Body);
            var bodyNotExpression = Expression.Lambda<Func<T, bool>>(bodyNot, Right.GetExpression().Parameters);

            return Left.GetExpression().And(bodyNotExpression);
        }
        #endregion
    }
}
