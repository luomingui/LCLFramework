using LCL.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LCL.Repositories
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        IRepositoryContext Context { get; }
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity); 
        void DeleteByKey(object keyValue);
        void Delete(Expression<Func<TEntity, bool>> predicate);
        TEntity GetByKey(object keyValue);


        #region Specification
        IQueryable<TEntity> FindAll(); 
        IQueryable<TEntity> FindAll(ISpecification<TEntity> specification);
        IQueryable<TEntity> FindAll(Expression<Func<TEntity, dynamic>> sortPredicate, LCL.SortOrder sortOrder);
        PagedResult<TEntity> FindAll(Expression<Func<TEntity, dynamic>> sortPredicate, LCL.SortOrder sortOrder, int pageNumber, int pageSize);
        IQueryable<TEntity> FindAll(params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties);
        IQueryable<TEntity> FindAll(ISpecification<TEntity> specification, Expression<Func<TEntity, dynamic>> sortPredicate, LCL.SortOrder sortOrder);
        PagedResult<TEntity> FindAll(ISpecification<TEntity> specification, Expression<Func<TEntity, dynamic>> sortPredicate, LCL.SortOrder sortOrder, int pageNumber, int pageSize);

        TEntity Find(Expression<Func<TEntity, bool>> predicate);
        TEntity Find(ISpecification<TEntity> specification);
        TEntity Find(ISpecification<TEntity> specification, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties);
        bool Exists(ISpecification<TEntity> specification);

        #endregion


        List<TEntity> FindByPid(Guid? pid);
    }
}
