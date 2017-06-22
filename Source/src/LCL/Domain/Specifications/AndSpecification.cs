using System;
using System.Linq.Expressions;

namespace LCL.Domain.Specifications
{
    public class AndSpecification<T> : CompositeSpecification<T>
    {
        #region Ctor
        public AndSpecification(ISpecification<T> left, ISpecification<T> right) : base(left, right) { }
        #endregion

        #region Public Methods
        public override Expression<Func<T, bool>> GetExpression()
        {
            return Left.GetExpression().And(Right.GetExpression());
        }
        #endregion
    }

}
