using LCL.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LCL.Repositories
{
    public interface IRepository<TAggregateRoot> where TAggregateRoot : class, IEntity
    {
        IRepositoryContext Context { get; }
        void Create(TAggregateRoot entity);
        void Update(TAggregateRoot entity);
        void Delete(TAggregateRoot entity); 
        void DeleteByKey(object keyValue);
        void Delete(Expression<Func<TAggregateRoot, bool>> predicate);
        TAggregateRoot GetByKey(object keyValue);
        #region Obsolete
        [Obsolete("The method is obsolete, use FindXXX instead.")]
        IEnumerable<TAggregateRoot> Get(Expression<Func<TAggregateRoot, bool>> predicate);
        #endregion

        #region Specification
        IQueryable<TAggregateRoot> FindAll(); 
        IQueryable<TAggregateRoot> FindAll(ISpecification<TAggregateRoot> specification);
        IQueryable<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, dynamic>> sortPredicate, LCL.SortOrder sortOrder);
        PagedResult<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, dynamic>> sortPredicate, LCL.SortOrder sortOrder, int pageNumber, int pageSize);
        IQueryable<TAggregateRoot> FindAll(params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties);
        IQueryable<TAggregateRoot> FindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, LCL.SortOrder sortOrder);
        PagedResult<TAggregateRoot> FindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, LCL.SortOrder sortOrder, int pageNumber, int pageSize);

        TAggregateRoot Find(Expression<Func<TAggregateRoot, bool>> predicate);
        TAggregateRoot Find(ISpecification<TAggregateRoot> specification);
        TAggregateRoot Find(ISpecification<TAggregateRoot> specification, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties);
        bool Exists(ISpecification<TAggregateRoot> specification);

        #endregion
    }
}
