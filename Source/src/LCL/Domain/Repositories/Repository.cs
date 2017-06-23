using LCL.Domain.Entities;
using LCL.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace LCL.Domain.Repositories
{
    public abstract class Repository<TAggregateRoot> : Repository<TAggregateRoot, Guid>, IRepository<TAggregateRoot>
      where TAggregateRoot : class, IAggregateRoot
    {
        #region Ctor
        public Repository(IRepositoryContext context)
            : base(context)
        {

        }
        #endregion
    }

    public abstract class Repository<TAggregateRoot, TPrimaryKey> : IRepository<TAggregateRoot, TPrimaryKey>
      where TAggregateRoot : class, IAggregateRoot<TPrimaryKey>
    {
        #region Private Fields
        private readonly IRepositoryContext context;
        #endregion

        #region Ctor
        public Repository(IRepositoryContext context)
        {
            this.context = context;
        }
        #endregion

        #region Protected Methods
        protected abstract IQueryable<TAggregateRoot> DoFindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder);
        protected abstract PagedResult<TAggregateRoot> DoFindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize);
        protected abstract IQueryable<TAggregateRoot> DoFindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties);
        protected abstract PagedResult<TAggregateRoot> DoFindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties);
        protected abstract TAggregateRoot DoFind(ISpecification<TAggregateRoot> specification);
        protected abstract TAggregateRoot DoFind(ISpecification<TAggregateRoot> specification, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties);
        protected abstract bool DoExists(ISpecification<TAggregateRoot> specification);
  
        protected virtual IQueryable<TAggregateRoot> DoFindAll()
        {
            return DoFindAll(new AnySpecification<TAggregateRoot>(), null, SortOrder.Unspecified);
        }
        protected virtual IQueryable<TAggregateRoot> DoFindAll(Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder)
        {
            return DoFindAll(new AnySpecification<TAggregateRoot>(), sortPredicate, sortOrder);
        }
        protected virtual PagedResult<TAggregateRoot> DoFindAll(Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize)
        {
            return DoFindAll(new AnySpecification<TAggregateRoot>(), sortPredicate, sortOrder, pageNumber, pageSize);
        }
        protected virtual IQueryable<TAggregateRoot> DoFindAll(ISpecification<TAggregateRoot> specification)
        {
            return DoFindAll(specification, null, SortOrder.Unspecified);
        }
        protected virtual IQueryable<TAggregateRoot> DoFindAll(params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            return DoFindAll(new AnySpecification<TAggregateRoot>(), null, SortOrder.Unspecified, eagerLoadingProperties);
        }
        protected virtual IQueryable<TAggregateRoot> DoFindAll(Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            return DoFindAll(new AnySpecification<TAggregateRoot>(), sortPredicate, sortOrder, eagerLoadingProperties);
        }
        protected virtual PagedResult<TAggregateRoot> DoFindAll(Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            return DoFindAll(new AnySpecification<TAggregateRoot>(), sortPredicate, sortOrder, pageNumber, pageSize, eagerLoadingProperties);
        }
        protected virtual IQueryable<TAggregateRoot> DoFindAll(ISpecification<TAggregateRoot> specification, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            return DoFindAll(new AnySpecification<TAggregateRoot>(), null, SortOrder.Unspecified, eagerLoadingProperties);
        }

        #endregion

        #region IRepository<TAggregateRoot> Members
        public IRepositoryContext Context
        {
            get { return this.context; }
        }

        #region Insert
        protected abstract TAggregateRoot DoInsert(TAggregateRoot entity);

        public virtual TAggregateRoot Insert(TAggregateRoot entity)
        {
            return this.DoInsert(entity);
        }
        public virtual Task<TAggregateRoot> InsertAsync(TAggregateRoot entity)
        {
            return Task.FromResult(Insert(entity));
        }

        public virtual TPrimaryKey InsertAndGetId(TAggregateRoot entity)
        {
            return Insert(entity).ID;
        }

        public virtual Task<TPrimaryKey> InsertAndGetIdAsync(TAggregateRoot entity)
        {
            return Task.FromResult(InsertAndGetId(entity));
        }
        public virtual TAggregateRoot InsertOrUpdate(TAggregateRoot entity)
        {
            return entity.IsTransient()
                ? Insert(entity)
                : Update(entity);
        }
        #endregion   

        #region Update
        protected abstract TAggregateRoot DoUpdate(TAggregateRoot entity);
        public virtual TAggregateRoot Update(TAggregateRoot entity)
        {
            return this.DoUpdate(entity);
        }
        public virtual Task<TAggregateRoot> UpdateAsync(TAggregateRoot entity)
        {
            return Task.FromResult(Update(entity));
        }

        public virtual TAggregateRoot Update(TPrimaryKey id, Action<TAggregateRoot> updateAction)
        {
            var entity = Get(id);
            updateAction(entity);
            return entity;
        }

        public virtual async Task<TAggregateRoot> UpdateAsync(TPrimaryKey id, Func<TAggregateRoot, Task> updateAction)
        {
            var entity = await GetAsync(id);
            await updateAction(entity);
            return entity;
        }

        #endregion

        #region delete

        protected abstract void DoDelete(TAggregateRoot entity);
        public virtual void Delete(TAggregateRoot entity)
        {
            this.DoDelete(entity);
        }
        public virtual Task DeleteAsync(TAggregateRoot entity)
        {
            Delete(entity);
            return Task.FromResult(0);
        }

        public abstract void DoDelete(TPrimaryKey id);
        public void Delete(TPrimaryKey id)
        {
            this.DoDelete(id);
        }
        public virtual Task DeleteAsync(TPrimaryKey id)
        {
            Delete(id);
            return Task.FromResult(0);
        }

        public virtual void Delete(Expression<Func<TAggregateRoot, bool>> predicate)
        {
            foreach (var entity in FindAll().Where(predicate).ToList())
            {
                Delete(entity);
            }
        }

        public virtual Task DeleteAsync(Expression<Func<TAggregateRoot, bool>> predicate)
        {
            Delete(predicate);
            return Task.FromResult(0);
        }
        #endregion

        #region Select/Get/Query  
        public TAggregateRoot Get(TPrimaryKey id)
        {
            var entity = Find(id);
            if (entity == null)
            {
                throw new LException(typeof(TAggregateRoot), id);
            }
            return entity;
        }
        public virtual async Task<TAggregateRoot> GetAsync(TPrimaryKey id)
        {
            var entity = await FindAsync(id);
            if (entity == null)
            {
                throw new LException(typeof(TAggregateRoot), id);
            }

            return entity;
        }
        public IQueryable<TAggregateRoot> FindAll()
        {
            return this.DoFindAll();
        }
        public virtual Task<List<TAggregateRoot>> FindAllAsync()
        {
            return Task.FromResult(FindAll().ToList());
        }
        public IQueryable<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder)
        {
            return this.DoFindAll(sortPredicate, sortOrder);
        }
        public IQueryable<TAggregateRoot> FindAll(ISpecification<TAggregateRoot> specification)
        {
            return this.DoFindAll(specification);
        }
        public IQueryable<TAggregateRoot> FindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder)
        {
            return this.DoFindAll(specification, sortPredicate, sortOrder);
        }
        public PagedResult<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize)
        {
            return this.DoFindAll(sortPredicate, sortOrder, pageNumber, pageSize);
        }
        public PagedResult<TAggregateRoot> FindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize)
        {
            return this.DoFindAll(specification, sortPredicate, sortOrder, pageNumber, pageSize);
        }
        public IQueryable<TAggregateRoot> FindAll(params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoFindAll(eagerLoadingProperties);
        }
        public IQueryable<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoFindAll(sortPredicate, sortOrder, eagerLoadingProperties);
        }
        public PagedResult<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoFindAll(sortPredicate, sortOrder, pageNumber, pageSize, eagerLoadingProperties);
        }
        public IQueryable<TAggregateRoot> FindAll(ISpecification<TAggregateRoot> specification, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoFindAll(specification, eagerLoadingProperties);
        }
        public IQueryable<TAggregateRoot> FindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoFindAll(specification, sortPredicate, sortOrder, eagerLoadingProperties);
        }
        public PagedResult<TAggregateRoot> FindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoFindAll(specification, sortPredicate, sortOrder, pageNumber, pageSize, eagerLoadingProperties);
        }
        public virtual T Query<T>(Func<IQueryable<TAggregateRoot>, T> queryMethod)
        {
            return queryMethod(FindAll());
        }
        
        public TAggregateRoot Find(ISpecification<TAggregateRoot> specification)
        {
            return this.DoFind(specification);
        }
        public TAggregateRoot Find(ISpecification<TAggregateRoot> specification, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoFind(specification, eagerLoadingProperties);
        }
        public bool Exists(ISpecification<TAggregateRoot> specification)
        {
            return this.DoExists(specification);
        }

        #endregion


        #region Find
        public virtual TAggregateRoot Find(TPrimaryKey id)
        {
            return FindAll().FirstOrDefault(CreateEqualityExpressionForId(id));
        }
        public virtual Task<TAggregateRoot> FindAsync(TPrimaryKey id)
        {
            return Task.FromResult(Find(id));
        }

        public virtual TAggregateRoot Find(Expression<Func<TAggregateRoot, bool>> predicate)
        {
            return FindAll().FirstOrDefault(predicate);
        }

        public virtual Task<TAggregateRoot> FindAsync(Expression<Func<TAggregateRoot, bool>> predicate)
        {
            return Task.FromResult(Find(predicate));
        }

        #endregion

        protected virtual IQueryable<TAggregateRoot> ApplyFilters(IQueryable<TAggregateRoot> query)
        {
           // query = ApplyMultiTenancyFilter(query);
            query = ApplySoftDeleteFilter(query);
            return query;
        }
        private IQueryable<TAggregateRoot> ApplySoftDeleteFilter(IQueryable<TAggregateRoot> query)
        {
            if (typeof(ISoftDelete).GetTypeInfo().IsAssignableFrom(typeof(TAggregateRoot)))
            {
                query = query.Where(e => !((ISoftDelete)e).IsDeleted);
            }
            return query;
        }
        protected static Expression<Func<TAggregateRoot, bool>> CreateEqualityExpressionForId(TPrimaryKey id)
        {
            var lambdaParam = Expression.Parameter(typeof(TAggregateRoot));

            var lambdaBody = Expression.Equal(
                Expression.PropertyOrField(lambdaParam, "ID"),
                Expression.Constant(id, typeof(TPrimaryKey))
                );

            return Expression.Lambda<Func<TAggregateRoot, bool>>(lambdaBody, lambdaParam);
        }
        #endregion








    
    }
}
