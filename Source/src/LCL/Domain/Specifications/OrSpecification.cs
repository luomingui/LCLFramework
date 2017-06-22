using System;
using System.Linq.Expressions;

namespace LCL.Domain.Specifications
{
    public class OrSpecification<T> : CompositeSpecification<T>
    {
        #region Ctor
        public OrSpecification(ISpecification<T> left, ISpecification<T> right) : base(left, right) { }
        #endregion

        #region Public Methods
        public override Expression<Func<T, bool>> GetExpression()
        {
            return Left.GetExpression().Or(Right.GetExpression());
        }
        #endregion
    }
}
