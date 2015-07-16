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
    public abstract partial class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly IRepositoryContext context;
        private string className = "";
        private string cacheKey = "";
        public Repository(IRepositoryContext context)
        {
            this.context = context;

            this.DataPortalLocation = DataPortalLocation.Local;

            this.className = typeof(TEntity).Name;
            this.cacheKey = "LCL_Cache_" + className;
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
            MemoryCacheHelper.RemoveEntityCache(className);
        }
        public void Delete(TEntity entity)
        {
            this.DoRemove(entity);
            MemoryCacheHelper.RemoveEntityCache(className);
        }
        public void DeleteByKey(object keyValue)
        {
            this.DoRemove(keyValue);
            MemoryCacheHelper.RemoveEntityCache(className);
        }
        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            this.DoRemove(predicate);
            MemoryCacheHelper.RemoveEntityCache(className);
        }
        public void Update(TEntity entity)
        {
            this.DoUpdate(entity);
            MemoryCacheHelper.RemoveEntityCache(className);
        }
        public TEntity GetByKey(object keyValue)
        {
            return this.DoGetByKey(keyValue);
        }
        #endregion

        #region Query
        protected abstract IEnumerable<TEntity> DoGet(Expression<Func<TEntity, bool>> predicate);
        [Obfuscation]
        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            cacheKey = cacheKey + "_Get";
            var result = MemoryCacheHelper.GetCache(cacheKey);
            if (result == null)
            {
                if (DataPortalLocation == DataPortalLocation.Local)
                {
                    result = this.DoGet(predicate);
                }
                else
                {
                    result = this.FetchList(predicate, "DoGet");
                }
                if (result != null)
                    MemoryCacheHelper.SetCache(cacheKey, result);
            }
            return (IEnumerable<TEntity>)result;
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
            return this.DoFindAll(new AnySpecification<TEntity>(), p => p.ID, LCL.SortOrder.Descending);
        }
        protected virtual IQueryable<TEntity> DoFindAll(Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder)
        {
            return this.DoFindAll(new AnySpecification<TEntity>(), sortPredicate, sortOrder);
        }
        protected virtual PagedResult<TEntity> DoFindAll(Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize)
        {
            return this.DoFindAll(new AnySpecification<TEntity>(), sortPredicate, sortOrder, pageNumber, pageSize);
        }
        protected virtual IQueryable<TEntity> DoFindAll(ISpecification<TEntity> specification)
        {
            return this.DoFindAll(specification, null, LCL.SortOrder.Unspecified);
        }
        protected virtual IQueryable<TEntity> DoFindAll(params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoFindAll(new AnySpecification<TEntity>(), null, LCL.SortOrder.Unspecified, eagerLoadingProperties);
        }
        public IQueryable<TEntity> FindAll()
        {
            cacheKey = cacheKey + "_FindAll";
            var result = MemoryCacheHelper.GetCache(cacheKey);
            if (result == null)
            {
                result = this.DoFindAll();
                if (result != null)
                    MemoryCacheHelper.SetCache(cacheKey, result);
            }
            return (IQueryable<TEntity>)result;
        }
        public IQueryable<TEntity> FindAll(ISpecification<TEntity> specification)
        {
            cacheKey = cacheKey + "_FindAll_" + specification.ID;
            var result = MemoryCacheHelper.GetCache(cacheKey);
            if (result == null)
            {
                result = this.DoFindAll(specification);
                if (result != null)
                    MemoryCacheHelper.SetCache(cacheKey, result);
            }
            return (IQueryable<TEntity>)result;
        }
        public IQueryable<TEntity> FindAll(Expression<Func<TEntity, dynamic>> sortPredicate, LCL.SortOrder sortOrder)
        {
            cacheKey = cacheKey + "_FindAll_" + sortPredicate.Name;
            var result = MemoryCacheHelper.GetCache(cacheKey);
            if (result == null)
            {
                result = this.DoFindAll(sortPredicate, sortOrder);
                if (result != null)
                    MemoryCacheHelper.SetCache(cacheKey, result);
            }
            return (IQueryable<TEntity>)result;
        }
        public IQueryable<TEntity> FindAll(params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
        {
            cacheKey = cacheKey + "_FindAll_" + eagerLoadingProperties.ToString();
            var result = MemoryCacheHelper.GetCache(cacheKey);
            if (result == null)
            {
                result = this.DoFindAll(eagerLoadingProperties);
                if (result != null)
                    MemoryCacheHelper.SetCache(cacheKey, result);
            }
            return (IQueryable<TEntity>)result;
        }
        public PagedResult<TEntity> FindAll(Expression<Func<TEntity, dynamic>> sortPredicate, LCL.SortOrder sortOrder, int pageNumber, int pageSize)
        {
            cacheKey = cacheKey + "_FindAll_" + sortPredicate.Name;
            var result = MemoryCacheHelper.GetCache(cacheKey);
            if (result == null)
            {
                result = this.DoFindAll(sortPredicate, sortOrder, pageNumber, pageSize);
                if (result != null)
                    MemoryCacheHelper.SetCache(cacheKey, result);
            }
            return (PagedResult<TEntity>)result;
        }
        public IQueryable<TEntity> FindAll(ISpecification<TEntity> specification, Expression<Func<TEntity, dynamic>> sortPredicate, LCL.SortOrder sortOrder)
        {
            cacheKey = cacheKey + "_FindAll_" + specification.ID;
            var result = MemoryCacheHelper.GetCache(cacheKey);
            if (result == null)
            {
                result = this.DoFindAll(specification, sortPredicate, sortOrder);
                if (result != null)
                    MemoryCacheHelper.SetCache(cacheKey, result);
            }
            return (IQueryable<TEntity>)result;
        }
        public PagedResult<TEntity> FindAll(ISpecification<TEntity> specification, Expression<Func<TEntity, dynamic>> sortPredicate, LCL.SortOrder sortOrder, int pageNumber, int pageSize)
        {
            cacheKey = cacheKey + "_FindAll_" + specification.ID;
            var result = MemoryCacheHelper.GetCache(cacheKey);
            if (result == null)
            {
                result = this.DoFindAll(specification, sortPredicate, sortOrder, pageNumber, pageSize);
                if (result != null)
                    MemoryCacheHelper.SetCache(cacheKey, result);
            }
            return (PagedResult<TEntity>)result;
        }
        public TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {
            cacheKey = cacheKey + "_Find_" + predicate.Name;
            var result = MemoryCacheHelper.GetCache(cacheKey);
            if (result == null)
            {
                result = this.DoFind(predicate);
                if (result != null)
                    MemoryCacheHelper.SetCache(cacheKey, result);
            }
            return (TEntity)result;
        }
        public TEntity Find(ISpecification<TEntity> specification)
        {
            cacheKey = cacheKey + "_Find_" + specification.ID;
            var result = MemoryCacheHelper.GetCache(cacheKey);
            if (result == null)
            {
                result = this.DoFind(specification);
                if (result != null)
                    MemoryCacheHelper.SetCache(cacheKey, result);
            }
            return (TEntity)result;
        }
        public TEntity Find(ISpecification<TEntity> specification, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
        {
            cacheKey = cacheKey + "_Find_" + specification.ID;
            var result = MemoryCacheHelper.GetCache(cacheKey);
            if (result == null)
            {
                result = this.DoFind(specification, eagerLoadingProperties);
                if (result != null)
                    MemoryCacheHelper.SetCache(cacheKey, result);
            }
            return (TEntity)result;
        }
        public bool Exists(ISpecification<TEntity> specification)
        {
            cacheKey = cacheKey + "_Exists_" + specification.ID;
            var result = MemoryCacheHelper.GetCache(cacheKey);
            if (result == null)
            {
                result = this.DoExists(specification);
                if (result != null)
                    MemoryCacheHelper.SetCache(cacheKey, result);
            }
            return (bool)result;
        }
        #endregion
    }
}
