using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LCL.Domain.Specifications;
using System.Threading.Tasks;
using LCL.Domain.Entities;

namespace LCL.Domain.Repositories
{
    public interface IRepository<TAggregateRoot> : IRepository<TAggregateRoot, Guid> where TAggregateRoot : class, IAggregateRoot<Guid>
    {

    }

    public interface IRepository<TAggregateRoot, TPrimaryKey> where TAggregateRoot : class, IAggregateRoot<TPrimaryKey>
    {
        IRepositoryContext Context { get; }

        #region Select/Get/Query
        IQueryable<TAggregateRoot> FindAll();
        Task<List<TAggregateRoot>> FindAllAsync();
        IQueryable<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder);
        PagedResult<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize);
        IQueryable<TAggregateRoot> FindAll(ISpecification<TAggregateRoot> specification);
        IQueryable<TAggregateRoot> FindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder);
        PagedResult<TAggregateRoot> FindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize);
        IQueryable<TAggregateRoot> FindAll(params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties);
        IQueryable<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties);
        PagedResult<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties);
        IQueryable<TAggregateRoot> FindAll(ISpecification<TAggregateRoot> specification, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties);
        IQueryable<TAggregateRoot> FindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties);
        PagedResult<TAggregateRoot> FindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties);
        TAggregateRoot Get(TPrimaryKey id);
        Task<TAggregateRoot> GetAsync(TPrimaryKey id);


        TAggregateRoot Find(TPrimaryKey id);
        Task<TAggregateRoot> FindAsync(TPrimaryKey id);
        TAggregateRoot Find(Expression<Func<TAggregateRoot, bool>> predicate);
        Task<TAggregateRoot> FindAsync(Expression<Func<TAggregateRoot, bool>> predicate);
        TAggregateRoot Find(ISpecification<TAggregateRoot> specification);
        TAggregateRoot Find(ISpecification<TAggregateRoot> specification, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties);
        bool Exists(ISpecification<TAggregateRoot> specification);

        T Query<T>(Func<IQueryable<TAggregateRoot>, T> queryMethod);

        #endregion

        #region Insert
        TAggregateRoot Insert(TAggregateRoot entity);
        Task<TAggregateRoot> InsertAsync(TAggregateRoot entity);
        TPrimaryKey InsertAndGetId(TAggregateRoot entity);
        Task<TPrimaryKey> InsertAndGetIdAsync(TAggregateRoot entity);
        #endregion

        #region Update
        TAggregateRoot Update(TAggregateRoot entity);
        Task<TAggregateRoot> UpdateAsync(TAggregateRoot entity);
        TAggregateRoot Update(TPrimaryKey id, Action<TAggregateRoot> updateAction);
        Task<TAggregateRoot> UpdateAsync(TPrimaryKey id, Func<TAggregateRoot, Task> updateAction);
        #endregion

        #region Delete
        void Delete(TAggregateRoot entity);
        Task DeleteAsync(TAggregateRoot entity);
        void Delete(TPrimaryKey id);
        Task DeleteAsync(TPrimaryKey id);
        void Delete(Expression<Func<TAggregateRoot, bool>> predicate);
        Task DeleteAsync(Expression<Func<TAggregateRoot, bool>> predicate);

        #endregion

    }
}
