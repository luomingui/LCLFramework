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
    public abstract class Repository<TEntity> : Repository<TEntity, Guid>, IRepository<TEntity>
      where TEntity : class, IEntity
    {
        #region Ctor
        public Repository(IRepositoryContext context)
            : base(context)
        {

        }
        #endregion
    }

    public abstract class Repository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
      where TEntity : class, IEntity<TPrimaryKey>
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
        protected abstract IQueryable<TEntity> DoFindAll(ISpecification<TEntity> specification, Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder);
        protected abstract PagedResult<TEntity> DoFindAll(ISpecification<TEntity> specification, Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize);
        protected abstract IQueryable<TEntity> DoFindAll(ISpecification<TEntity> specification, Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties);
        protected abstract PagedResult<TEntity> DoFindAll(ISpecification<TEntity> specification, Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties);
        protected abstract TEntity DoFind(ISpecification<TEntity> specification);
        protected abstract TEntity DoFind(ISpecification<TEntity> specification, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties);
        protected abstract bool DoExists(ISpecification<TEntity> specification);

        protected virtual IQueryable<TEntity> DoFindAll()
        {
            return DoFindAll(new AnySpecification<TEntity>(), null, SortOrder.Unspecified);
        }
        protected virtual IQueryable<TEntity> DoFindAll(Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder)
        {
            return DoFindAll(new AnySpecification<TEntity>(), sortPredicate, sortOrder);
        }
        protected virtual PagedResult<TEntity> DoFindAll(Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize)
        {
            return DoFindAll(new AnySpecification<TEntity>(), sortPredicate, sortOrder, pageNumber, pageSize);
        }
        protected virtual IQueryable<TEntity> DoFindAll(ISpecification<TEntity> specification)
        {
            return DoFindAll(specification, null, SortOrder.Unspecified);
        }
        protected virtual IQueryable<TEntity> DoFindAll(params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
        {
            return DoFindAll(new AnySpecification<TEntity>(), null, SortOrder.Unspecified, eagerLoadingProperties);
        }
        protected virtual IQueryable<TEntity> DoFindAll(Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
        {
            return DoFindAll(new AnySpecification<TEntity>(), sortPredicate, sortOrder, eagerLoadingProperties);
        }
        protected virtual PagedResult<TEntity> DoFindAll(Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
        {
            return DoFindAll(new AnySpecification<TEntity>(), sortPredicate, sortOrder, pageNumber, pageSize, eagerLoadingProperties);
        }
        protected virtual IQueryable<TEntity> DoFindAll(ISpecification<TEntity> specification, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
        {
            return DoFindAll(new AnySpecification<TEntity>(), null, SortOrder.Unspecified, eagerLoadingProperties);
        }

        #endregion

        #region IRepository<TAggregateRoot> Members
        public IRepositoryContext Context
        {
            get { return this.context; }
        }

        #region Insert
        protected abstract TEntity DoInsert(TEntity entity);

        public virtual TEntity Insert(TEntity entity)
        {
            return this.DoInsert(entity);
        }
        public void Insert(List<TEntity> entitys)
        {
            foreach (var item in entitys)
            {
                this.DoInsert(item);
            }
        }
        public virtual Task<TEntity> InsertAsync(TEntity entity)
        {
            return Task.FromResult(Insert(entity));
        }

        public virtual TPrimaryKey InsertAndGetId(TEntity entity)
        {
            return Insert(entity).ID;
        }

        public virtual Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity)
        {
            return Task.FromResult(InsertAndGetId(entity));
        }
        public virtual TEntity InsertOrUpdate(TEntity entity)
        {
            return entity.IsTransient()
                ? Insert(entity)
                : Update(entity);
        }
        #endregion   

        #region Update
        protected abstract TEntity DoUpdate(TEntity entity);
        public virtual TEntity Update(TEntity entity)
        {
            return this.DoUpdate(entity);
        }
        public void Update(List<TEntity> entitys)
        {
            foreach (var item in entitys)
            {
                this.DoUpdate(item);
            }
        }
        public virtual Task<TEntity> UpdateAsync(TEntity entity)
        {
            return Task.FromResult(Update(entity));
        }

        public virtual TEntity Update(TPrimaryKey id, Action<TEntity> updateAction)
        {
            var entity = Get(id);
            updateAction(entity);
            return entity;
        }

        public virtual async Task<TEntity> UpdateAsync(TPrimaryKey id, Func<TEntity, Task> updateAction)
        {
            var entity = await GetAsync(id);
            await updateAction(entity);
            return entity;
        }

        #endregion

        #region delete

        protected abstract void DoDelete(TEntity entity);
        public virtual void Delete(TEntity entity)
        {
            this.DoDelete(entity);
        }
        public void Delete(List<TPrimaryKey> ids)
        {
            for (int i = 0; i < ids.Count; i++)
            {
                this.DoDelete(ids[i]);
            }
        }
        public virtual Task DeleteAsync(TEntity entity)
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

        public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            foreach (var entity in FindAll().Where(predicate).ToList())
            {
                Delete(entity);
            }
        }

        public virtual Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            Delete(predicate);
            return Task.FromResult(0);
        }
        #endregion

        #region Select/Get/Query  
        public TEntity Get(TPrimaryKey id)
        {
            var entity = Find(id);
            if (entity == null)
            {
                throw new LException(typeof(TEntity), id);
            }
            return entity;
        }
        public virtual async Task<TEntity> GetAsync(TPrimaryKey id)
        {
            var entity = await FindAsync(id);
            if (entity == null)
            {
                throw new LException(typeof(TEntity), id);
            }

            return entity;
        }
        public IQueryable<TEntity> FindAll()
        {
            return this.DoFindAll();
        }
        public virtual Task<List<TEntity>> FindAllAsync()
        {
            return Task.FromResult(FindAll().ToList());
        }
        public IQueryable<TEntity> FindAll(Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder)
        {
            return this.DoFindAll(sortPredicate, sortOrder);
        }
        public IQueryable<TEntity> FindAll(ISpecification<TEntity> specification)
        {
            return this.DoFindAll(specification);
        }
        public IQueryable<TEntity> FindAll(ISpecification<TEntity> specification, Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder)
        {
            return this.DoFindAll(specification, sortPredicate, sortOrder);
        }
        public PagedResult<TEntity> FindAll(Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize)
        {
            return this.DoFindAll(sortPredicate, sortOrder, pageNumber, pageSize);
        }
        public PagedResult<TEntity> FindAll(ISpecification<TEntity> specification, Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize)
        {
            return this.DoFindAll(specification, sortPredicate, sortOrder, pageNumber, pageSize);
        }
        public IQueryable<TEntity> FindAll(params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoFindAll(eagerLoadingProperties);
        }
        public IQueryable<TEntity> FindAll(Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoFindAll(sortPredicate, sortOrder, eagerLoadingProperties);
        }
        public PagedResult<TEntity> FindAll(Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoFindAll(sortPredicate, sortOrder, pageNumber, pageSize, eagerLoadingProperties);
        }
        public IQueryable<TEntity> FindAll(ISpecification<TEntity> specification, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoFindAll(specification, eagerLoadingProperties);
        }
        public IQueryable<TEntity> FindAll(ISpecification<TEntity> specification, Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoFindAll(specification, sortPredicate, sortOrder, eagerLoadingProperties);
        }
        public PagedResult<TEntity> FindAll(ISpecification<TEntity> specification, Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoFindAll(specification, sortPredicate, sortOrder, pageNumber, pageSize, eagerLoadingProperties);
        }
        public virtual T Query<T>(Func<IQueryable<TEntity>, T> queryMethod)
        {
            return queryMethod(FindAll());
        }

        public TEntity Find(ISpecification<TEntity> specification)
        {
            return this.DoFind(specification);
        }
        public TEntity Find(ISpecification<TEntity> specification, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoFind(specification, eagerLoadingProperties);
        }
        public bool Exists(ISpecification<TEntity> specification)
        {
            return this.DoExists(specification);
        }

        #endregion


        #region Find
        public virtual TEntity Find(TPrimaryKey id)
        {
            return FindAll().FirstOrDefault(CreateEqualityExpressionForId(id));
        }
        public virtual Task<TEntity> FindAsync(TPrimaryKey id)
        {
            return Task.FromResult(Find(id));
        }

        public virtual TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {
            return FindAll().FirstOrDefault(predicate);
        }

        public virtual Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(Find(predicate));
        }

        #endregion

        protected virtual IQueryable<TEntity> ApplyFilters(IQueryable<TEntity> query)
        {
            // query = ApplyMultiTenancyFilter(query);
            query = ApplySoftDeleteFilter(query);
            return query;
        }
        private IQueryable<TEntity> ApplySoftDeleteFilter(IQueryable<TEntity> query)
        {
            if (typeof(ISoftDelete).GetTypeInfo().IsAssignableFrom(typeof(TEntity)))
            {
                query = query.Where(e => !((ISoftDelete)e).IsDeleted);
            }
            return query;
        }
        protected static Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TPrimaryKey id)
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));

            var lambdaBody = Expression.Equal(
                Expression.PropertyOrField(lambdaParam, "ID"),
                Expression.Constant(id, typeof(TPrimaryKey))
                );

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }



        #endregion



    }
}
