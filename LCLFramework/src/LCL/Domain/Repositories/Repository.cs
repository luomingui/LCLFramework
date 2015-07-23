using LCL.Caching;
using LCL.DataPortal;
using LCL.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LCL.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract partial class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {

        ISpecification<TEntity> spec = Specification<TEntity>.Eval(p => p.IsDelete == false);

        private readonly IRepositoryContext context;
        public Repository(IRepositoryContext context)
        {
            this.context = context;
            this.DataPortalLocation = DataPortalLocation.Local;
        }
        public IRepositoryContext Context
        {
            get { return this.context; }
        }

        #region IRepositoryContext

        #region CUD
        protected abstract void DoAdd(TEntity entity);
        protected abstract void DoRemove(TEntity entity);
        protected abstract void DoRemove(object keyValue);
        protected abstract void DoRemove(Expression<Func<TEntity, bool>> predicate);
        protected abstract void DoUpdate(TEntity entity);
        protected abstract TEntity DoGetByKey(object key);
        public void Create(TEntity entity)
        {
            this.DoAdd(entity);
        }
        public void Delete(TEntity entity)
        {
            this.DoRemove(entity);
        }
        public void DeleteByKey(object keyValue)
        {
            this.DoRemove(keyValue);
        }
        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            this.DoRemove(predicate);
        }
        public void Update(TEntity entity)
        {
            this.DoUpdate(entity);
        }
        public TEntity GetByKey(object keyValue)
        {
            return this.DoGetByKey(keyValue);
        }
        #endregion

        #endregion

        #region FindAll
        protected abstract IQueryable<TEntity> DoFindAll(ISpecification<TEntity> specification, Expression<Func<TEntity, dynamic>> sortPredicate, LCL.SortOrder sortOrder);
        protected abstract IQueryable<TEntity> DoFindAll(ISpecification<TEntity> specification, Expression<Func<TEntity, dynamic>> sortPredicate, LCL.SortOrder sortOrder, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties);
        protected abstract PagedResult<TEntity> DoFindAll(ISpecification<TEntity> specification, Expression<Func<TEntity, dynamic>> sortPredicate, LCL.SortOrder sortOrder, int pageNumber, int pageSize);
        protected abstract TEntity DoFind(Expression<Func<TEntity, bool>> predicate);
        protected abstract TEntity DoFind(ISpecification<TEntity> specification);
        protected abstract TEntity DoFind(ISpecification<TEntity> specification, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties);
        protected abstract bool DoExists(ISpecification<TEntity> specification);
        protected virtual IQueryable<TEntity> DoFindAll()
        {
            return this.DoFindAll(spec, p => p.UpdateDate, SortOrder.Descending);
        }
        protected virtual IQueryable<TEntity> DoFindAll(Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder)
        {
            return this.DoFindAll(spec, sortPredicate, sortOrder);
        }
        protected virtual PagedResult<TEntity> DoFindAll(Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize)
        {
            return this.DoFindAll(spec, sortPredicate, sortOrder, pageNumber, pageSize);
        }
        protected virtual IQueryable<TEntity> DoFindAll(ISpecification<TEntity> specification)
        {
            return this.DoFindAll(specification, p => p.UpdateDate, SortOrder.Descending);
        }
        protected virtual IQueryable<TEntity> DoFindAll(params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoFindAll(spec, p => p.UpdateDate, SortOrder.Descending, eagerLoadingProperties);
        }
        public IQueryable<TEntity> FindAll()
        {
            return this.DoFindAll();
        }
        public IQueryable<TEntity> FindAll(ISpecification<TEntity> specification)
        {
            return this.DoFindAll(specification);
        }
        public IQueryable<TEntity> FindAll(Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder)
        {
            return this.DoFindAll(sortPredicate, sortOrder);
        }
        public IQueryable<TEntity> FindAll(params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoFindAll(eagerLoadingProperties);
        }
        public PagedResult<TEntity> FindAll(Expression<Func<TEntity, dynamic>> sortPredicate, LCL.SortOrder sortOrder, int pageNumber, int pageSize)
        {
            return this.DoFindAll(sortPredicate, sortOrder, pageNumber, pageSize);
        }
        public IQueryable<TEntity> FindAll(ISpecification<TEntity> specification, Expression<Func<TEntity, dynamic>> sortPredicate, LCL.SortOrder sortOrder)
        {
            return this.DoFindAll(specification, sortPredicate, sortOrder);
        }
        public PagedResult<TEntity> FindAll(ISpecification<TEntity> specification, Expression<Func<TEntity, dynamic>> sortPredicate, LCL.SortOrder sortOrder, int pageNumber, int pageSize)
        {
            return this.DoFindAll(specification, sortPredicate, sortOrder, pageNumber, pageSize);
        }
        public TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {
            return this.DoFind(predicate);
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
        protected abstract List<TEntity> DoFindByPid(Guid? pid);
        public List<TEntity> FindByPid(Guid? pid)
        {
            return this.DoFindByPid(pid);
        }
    }
}
