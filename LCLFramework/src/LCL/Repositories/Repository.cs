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
    public abstract partial class Repository<TAggregateRoot> : IRepository<TAggregateRoot>
        where TAggregateRoot : class, IEntity
    {
        private readonly IRepositoryContext context;
        private string className = "";
        private string cacheKey = "";
        public Repository(IRepositoryContext context)
        {
            this.context = context;

            this.DataPortalLocation = DataPortalLocation.Local;

            this.className = typeof(TAggregateRoot).Name;
            this.cacheKey = "LCL_Cache_" + className;
        }
        public IRepositoryContext Context
        {
            get { return this.context; }
        }

        #region IRepositoryContext

        #region CUD
        protected abstract void DoAdd(TAggregateRoot entity);
        protected abstract void DoRemove(TAggregateRoot entity);
        protected abstract void DoRemove(object keyValue);
        protected abstract void DoRemove(Expression<Func<TAggregateRoot, bool>> predicate);
        protected abstract void DoUpdate(TAggregateRoot entity);
        protected abstract TAggregateRoot DoGetByKey(object key);
        public void Create(TAggregateRoot entity)
        {
            this.DoAdd(entity);
            MemoryCacheHelper.RemoveEntityCache(className);
        }
        public void Delete(TAggregateRoot entity)
        {
            this.DoRemove(entity);
            MemoryCacheHelper.RemoveEntityCache(className);
        }
        public void DeleteByKey(object keyValue)
        {
            this.DoRemove(keyValue);
            MemoryCacheHelper.RemoveEntityCache(className);
        }
        public void Delete(Expression<Func<TAggregateRoot, bool>> predicate)
        {
            this.DoRemove(predicate);
            MemoryCacheHelper.RemoveEntityCache(className);
        }
        public void Update(TAggregateRoot entity)
        {
            this.DoUpdate(entity);
            MemoryCacheHelper.RemoveEntityCache(className);
        }
        public TAggregateRoot GetByKey(object keyValue)
        {
            return this.DoGetByKey(keyValue);
        }
        #endregion

        #region Query
        protected abstract IEnumerable<TAggregateRoot> DoGet(Expression<Func<TAggregateRoot, bool>> predicate);
        [Obfuscation]
        public IEnumerable<TAggregateRoot> Get(Expression<Func<TAggregateRoot, bool>> predicate)
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
            return (IEnumerable<TAggregateRoot>)result;
        }
        #endregion

        #endregion

        #region FindAll
        protected abstract IQueryable<TAggregateRoot> DoFindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, LCL.SortOrder sortOrder);
        protected abstract IQueryable<TAggregateRoot> DoFindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, LCL.SortOrder sortOrder, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties);
        protected abstract PagedResult<TAggregateRoot> DoFindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, LCL.SortOrder sortOrder, int pageNumber, int pageSize);
        protected abstract TAggregateRoot DoFind(Expression<Func<TAggregateRoot, bool>> predicate);
        protected abstract TAggregateRoot DoFind(ISpecification<TAggregateRoot> specification);
        protected abstract TAggregateRoot DoFind(ISpecification<TAggregateRoot> specification, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties);
        protected abstract bool DoExists(ISpecification<TAggregateRoot> specification);
        protected virtual IQueryable<TAggregateRoot> DoFindAll()
        {
            return this.DoFindAll(new AnySpecification<TAggregateRoot>(), null, LCL.SortOrder.Descending);
        }
        protected virtual IQueryable<TAggregateRoot> DoFindAll(Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder)
        {
            return this.DoFindAll(new AnySpecification<TAggregateRoot>(), sortPredicate, sortOrder);
        }
        protected virtual PagedResult<TAggregateRoot> DoFindAll(Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize)
        {
            return this.DoFindAll(new AnySpecification<TAggregateRoot>(), sortPredicate, sortOrder, pageNumber, pageSize);
        }
        protected virtual IQueryable<TAggregateRoot> DoFindAll(ISpecification<TAggregateRoot> specification)
        {
            return this.DoFindAll(specification, null, LCL.SortOrder.Unspecified);
        }
        protected virtual IQueryable<TAggregateRoot> DoFindAll(params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoFindAll(new AnySpecification<TAggregateRoot>(), null, LCL.SortOrder.Unspecified, eagerLoadingProperties);
        }
        public IQueryable<TAggregateRoot> FindAll()
        {
            cacheKey = cacheKey + "_FindAll";
            var result = MemoryCacheHelper.GetCache(cacheKey);
            if (result == null)
            {
                result = this.DoFindAll();
                if (result != null)
                    MemoryCacheHelper.SetCache(cacheKey, result);
            }
            return (IQueryable<TAggregateRoot>)result;
        }
        public IQueryable<TAggregateRoot> FindAll(ISpecification<TAggregateRoot> specification)
        {
            cacheKey = cacheKey + "_FindAll_" + specification.ID;
            var result = MemoryCacheHelper.GetCache(cacheKey);
            if (result == null)
            {
                result = this.DoFindAll(specification);
                if (result != null)
                    MemoryCacheHelper.SetCache(cacheKey, result);
            }
            return (IQueryable<TAggregateRoot>)result;
        }
        public IQueryable<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, dynamic>> sortPredicate, LCL.SortOrder sortOrder)
        {
            cacheKey = cacheKey + "_FindAll_" + sortPredicate.Name;
            var result = MemoryCacheHelper.GetCache(cacheKey);
            if (result == null)
            {
                result = this.DoFindAll(sortPredicate, sortOrder);
                if (result != null)
                    MemoryCacheHelper.SetCache(cacheKey, result);
            }
            return (IQueryable<TAggregateRoot>)result;
        }
        public IQueryable<TAggregateRoot> FindAll(params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            cacheKey = cacheKey + "_FindAll_" + eagerLoadingProperties.ToString();
            var result = MemoryCacheHelper.GetCache(cacheKey);
            if (result == null)
            {
                result = this.DoFindAll(eagerLoadingProperties);
                if (result != null)
                    MemoryCacheHelper.SetCache(cacheKey, result);
            }
            return (IQueryable<TAggregateRoot>)result;
        }
        public PagedResult<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, dynamic>> sortPredicate, LCL.SortOrder sortOrder, int pageNumber, int pageSize)
        {
            cacheKey = cacheKey + "_FindAll_" + sortPredicate.Name;
            var result = MemoryCacheHelper.GetCache(cacheKey);
            if (result == null)
            {
                result = this.DoFindAll(sortPredicate, sortOrder, pageNumber, pageSize);
                if (result != null)
                    MemoryCacheHelper.SetCache(cacheKey, result);
            }
            return (PagedResult<TAggregateRoot>)result;
        }
        public IQueryable<TAggregateRoot> FindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, LCL.SortOrder sortOrder)
        {
            cacheKey = cacheKey + "_FindAll_" + specification.ID;
            var result = MemoryCacheHelper.GetCache(cacheKey);
            if (result == null)
            {
                result = this.DoFindAll(specification, sortPredicate, sortOrder);
                if (result != null)
                    MemoryCacheHelper.SetCache(cacheKey, result);
            }
            return (IQueryable<TAggregateRoot>)result;
        }
        public PagedResult<TAggregateRoot> FindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, LCL.SortOrder sortOrder, int pageNumber, int pageSize)
        {
            cacheKey = cacheKey + "_FindAll_" + specification.ID;
            var result = MemoryCacheHelper.GetCache(cacheKey);
            if (result == null)
            {
                result = this.DoFindAll(specification, sortPredicate, sortOrder, pageNumber, pageSize);
                if (result != null)
                    MemoryCacheHelper.SetCache(cacheKey, result);
            }
            return (PagedResult<TAggregateRoot>)result;
        }
        public TAggregateRoot Find(Expression<Func<TAggregateRoot, bool>> predicate)
        {
            cacheKey = cacheKey + "_Find_" + predicate.Name;
            var result = MemoryCacheHelper.GetCache(cacheKey);
            if (result == null)
            {
                result = this.DoFind(predicate);
                if (result != null)
                    MemoryCacheHelper.SetCache(cacheKey, result);
            }
            return (TAggregateRoot)result;
        }
        public TAggregateRoot Find(ISpecification<TAggregateRoot> specification)
        {
            cacheKey = cacheKey + "_Find_" + specification.ID;
            var result = MemoryCacheHelper.GetCache(cacheKey);
            if (result == null)
            {
                result = this.DoFind(specification);
                if (result != null)
                    MemoryCacheHelper.SetCache(cacheKey, result);
            }
            return (TAggregateRoot)result;
        }
        public TAggregateRoot Find(ISpecification<TAggregateRoot> specification, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            cacheKey = cacheKey + "_Find_" + specification.ID;
            var result = MemoryCacheHelper.GetCache(cacheKey);
            if (result == null)
            {
                result = this.DoFind(specification, eagerLoadingProperties);
                if (result != null)
                    MemoryCacheHelper.SetCache(cacheKey, result);
            }
            return (TAggregateRoot)result;
        }
        public bool Exists(ISpecification<TAggregateRoot> specification)
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
