using System;
using System.Linq.Expressions;

namespace LCL.Domain.Specifications
{
    public interface ISpecification<T>
    {
        Guid ID { get;  }
        bool IsSatisfiedBy(T obj);       
        ISpecification<T> And(ISpecification<T> other);
        ISpecification<T> Or(ISpecification<T> other);
        ISpecification<T> AndNot(ISpecification<T> other);
        ISpecification<T> Not();
        Expression<Func<T, bool>> GetExpression();
    }
}
