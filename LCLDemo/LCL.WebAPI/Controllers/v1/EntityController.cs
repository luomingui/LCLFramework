using LCL.Domain.Entities;
using LCL.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LCL.Domain.Specifications;
using System.Linq.Expressions;
using LCL.WebAPI.Utility;

namespace LCL.WebAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    [VersionedRoute("api/version",1)]
    public class EntityController<TEntity> : ApiController where TEntity : class, IEntity<Guid>
    {
        private readonly IRepository<TEntity> repository;
        private readonly IRepositoryContext unitOfWork;
        string Name;
        // GET api/实体名
        public EntityController()
        {
            this.repository = RF.Service<IRepository<TEntity>>();
            this.unitOfWork = RF.Service<IRepositoryContext>();
            this.Name = typeof(TEntity).Name;
        }
        public IEnumerable<string> Get()
        {
            var arrlist=new List<string>();
            var list= this.GetType().GetMethods().ToList();
            for (int i = 0; i < list.Count; i++)
            {
                arrlist.Add(list[i].Name);
            }
            return arrlist;
        }
        protected override void Dispose(bool disposing)
        {
            if (unitOfWork != null) {
                this.unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
        [HttpDelete, Route("Delete")]
        public void Delete(TEntity entity)
        {
            repository.Delete(entity);
        }
        [HttpDelete, Route("Delete")]
        public void Delete(List<Guid> ids)
        {
            repository.Delete(ids);
        }
        [HttpDelete, Route("Delete")]
        public void Delete(Guid id)
        {
            repository.Delete(id);
        }
        [HttpDelete, Route("Delete")]
        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            repository.Delete(predicate);
        }
       
        [HttpGet, Route("Exists")]
        public bool Exists(ISpecification<TEntity> specification)
        {
            return repository.Exists(specification);
        }
        [HttpGet, Route("Find/{id}")]
        public TEntity Find(Guid id)
        {
            return repository.Find(id);
        }
        [HttpGet]
        public TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {
            return repository.Find(predicate);
        }
        [HttpGet]
        public TEntity Find(ISpecification<TEntity> specification)
        {
            return repository.Find(specification);
        }
        [HttpGet]
        public TEntity Find(ISpecification<TEntity> specification, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
        {
            return repository.Find(specification, eagerLoadingProperties);
        }
        [HttpGet, Route("FindAll")]
        public IQueryable<TEntity> FindAll()
        {
            return repository.FindAll();
        }
        [HttpGet]
        public IQueryable<TEntity> FindAll(Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder)
        {
            return repository.FindAll(sortPredicate, sortOrder);
        }
        [HttpGet]
        public PagedResult<TEntity> FindAll(Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize)
        {
            return repository.FindAll(sortPredicate, sortOrder, pageNumber, pageSize);
        }
        [HttpGet]
        public IQueryable<TEntity> FindAll(ISpecification<TEntity> specification)
        {
            return repository.FindAll(specification);
        }
        [HttpGet]
        public IQueryable<TEntity> FindAll(ISpecification<TEntity> specification, Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder)
        {
            return repository.FindAll(specification, sortPredicate, sortOrder);
        }
        [HttpGet]
        public PagedResult<TEntity> FindAll(ISpecification<TEntity> specification, Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize)
        {
            return repository.FindAll(specification, sortPredicate, sortOrder, pageNumber, pageSize);
        }
        [HttpGet]
        public IQueryable<TEntity> FindAll(params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
        {
            return repository.FindAll(eagerLoadingProperties);
        }
        [HttpGet]
        public IQueryable<TEntity> FindAll(Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
        {
            return repository.FindAll(sortPredicate, sortOrder, eagerLoadingProperties);
        }
        [HttpGet]
        public PagedResult<TEntity> FindAll(Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
        {
            return repository.FindAll(sortPredicate, sortOrder, pageNumber, pageSize, eagerLoadingProperties);
        }
        [HttpGet]
        public IQueryable<TEntity> FindAll(ISpecification<TEntity> specification, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
        {
            return repository.FindAll(specification, eagerLoadingProperties);
        }
        [HttpGet]
        public IQueryable<TEntity> FindAll(ISpecification<TEntity> specification, Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
        {
            return repository.FindAll(specification, sortPredicate, sortOrder, eagerLoadingProperties);
        }
        [HttpGet]
        public PagedResult<TEntity> FindAll(ISpecification<TEntity> specification, Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
        {
            return repository.FindAll(specification, sortPredicate, sortOrder, pageNumber, pageSize, eagerLoadingProperties);
        }
        [HttpGet]
        public TEntity Get(Guid id)
        {
            return repository.Get(id);
        }
        [HttpPost]
        public TEntity Insert(TEntity entity)
        {
            return repository.Insert(entity);
        }
        [HttpPost]
        public void Insert(List<TEntity> entitys)
        {
            repository.Insert(entitys);
        }
        [HttpPost]
        public Guid InsertAndGetId(TEntity entity)
        {
            return repository.InsertAndGetId(entity);
        }
        [HttpGet]
        public T Query<T>(Func<IQueryable<TEntity>, T> queryMethod)
        {
            return repository.Query(queryMethod);
        }
        [HttpPut]
        public TEntity Update(TEntity entity)
        {
            return repository.Update(entity);
        }
        [HttpPut]
        public void Update(List<TEntity> entitys)
        {
            repository.Update(entitys);
        }
        [HttpPut]
        public TEntity Update(Guid id, Action<TEntity> updateAction)
        {
            return repository.Update(id, updateAction);
        }
    }
}
