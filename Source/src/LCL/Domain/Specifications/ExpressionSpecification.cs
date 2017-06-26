
using System;
using System.Linq.Expressions;

namespace LCL.Domain.Specifications
{
    public sealed class ExpressionSpecification<T> : Specification<T>
    {
        #region Private Fields
        private Expression<Func<T, bool>> expression;
        #endregion

        #region Ctor
        public ExpressionSpecification(Expression<Func<T, bool>> expression)
        {
            this.expression = expression;
        }
        #endregion

        #region Public Methods
        public override Expression<Func<T, bool>> GetExpression()
        {
            return this.expression;
        }
        #endregion
    }
}
