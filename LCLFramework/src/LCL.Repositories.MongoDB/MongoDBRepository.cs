using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using LCL.Specifications;

namespace LCL.Repositories.MongoDB
{
    /// <summary>
    /// Represents the MongoDB repository.
    /// </summary>
    /// <typeparam name="TAggregateRoot">The type of the aggregate root.</typeparam>
    public class MongoDBRepository<TAggregateRoot> : Repository<TAggregateRoot>
        where TAggregateRoot : class, IEntity
    {
        #region Private Fields
        private readonly IMongoDBRepositoryContext mongoDBRepositoryContext;
        #endregion

        #region Ctor
        /// <summary>
        /// Initializes a new instance of <c>MongoDBRepository[TAggregateRoot]</c> class.
        /// </summary>
        /// <param name="context">The <see cref="IRepositoryContext"/> object for initializing the current repository.</param>
        public MongoDBRepository(IRepositoryContext context) : base(context)
        {
            if (context is IMongoDBRepositoryContext)
                mongoDBRepositoryContext = context as MongoDBRepositoryContext;
            else
                throw new InvalidOperationException("Invalid repository context type.");
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Adds an aggregate root to the repository.
        /// </summary>
        /// <param name="aggregateRoot">The aggregate root to be added to the repository.</param>
        protected override void DoAdd(TAggregateRoot entity)
        {
            mongoDBRepositoryContext.RegisterNew(entity);
        }
        /// <summary>
        /// Removes the aggregate root from current repository.
        /// </summary>
        /// <param name="entity">The aggregate root to be removed.</param>
        protected override void DoRemove(TAggregateRoot entity)
        {
            mongoDBRepositoryContext.RegisterDeleted(entity);
        }
        /// <summary>
        /// Updates the aggregate root in the current repository.
        /// </summary>
        /// <param name="aggregateRoot">The aggregate root to be updated.</param>
        protected override void DoUpdate(TAggregateRoot entity)
        {
            mongoDBRepositoryContext.RegisterModified(entity);
        }
        protected override TAggregateRoot DoGetByKey(object keyValue)
        {
            MongoCollection collection = mongoDBRepositoryContext.GetCollectionForType(typeof(TAggregateRoot));
            Guid id = (Guid)keyValue;
            return collection.AsQueryable<TAggregateRoot>().Where(p => p.ID == id).First();
        }
        protected override void DoRemove(object keyValue)
        {
            var entity = DoGetByKey(keyValue);
            DoRemove(entity);
        }
        protected override void DoRemove(Expression<Func<TAggregateRoot, bool>> predicate)
        {
            MongoCollection collection = mongoDBRepositoryContext.GetCollectionForType(typeof(TAggregateRoot));

            var list = collection.AsQueryable<TAggregateRoot>().Where(predicate);

            foreach (var entity in list)
            {
                mongoDBRepositoryContext.RegisterDeleted(entity);
            }
        }
        protected override List<TAggregateRoot> DoFindByPid(Guid? pid)
        {
            try
            {
                if (pid == null) pid = Guid.Empty;

                var et = typeof(TAggregateRoot) as IEntityTree;

                MongoCollection collection = mongoDBRepositoryContext.GetCollectionForType(typeof(TAggregateRoot));
                var list = collection.AsQueryable<TAggregateRoot>().OfType<IEntityTree>().Where(p => p.ParentId == Guid.Empty);
                if (pid != Guid.Empty)
                {
                    list = collection.AsQueryable<TAggregateRoot>().OfType<IEntityTree>().Where(p => p.ParentId == pid);
                }
                return list.ToList() as List<TAggregateRoot>;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region DoXXXXXX
        protected override bool DoExists(ISpecification<TAggregateRoot> specification)
        {
            return this.DoFind(specification) != null;
        }
        protected override TAggregateRoot DoFind(Expression<Func<TAggregateRoot, bool>> predicate)
        {
            var collection = this.mongoDBRepositoryContext.GetCollectionForType(typeof(TAggregateRoot));
            return collection.AsQueryable<TAggregateRoot>().Where(predicate).FirstOrDefault();
        }
        protected override TAggregateRoot DoFind(ISpecification<TAggregateRoot> specification, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoFind(specification);
        }
        protected override TAggregateRoot DoFind(ISpecification<TAggregateRoot> specification)
        {
            var collection = this.mongoDBRepositoryContext.GetCollectionForType(typeof(TAggregateRoot));
            return collection.AsQueryable<TAggregateRoot>().Where(specification.GetExpression()).FirstOrDefault();
        }
        protected override PagedResult<TAggregateRoot> DoFindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
                throw new ArgumentOutOfRangeException("pageNumber", pageNumber, "The pageNumber is one-based and should be larger than zero.");
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException("pageSize", pageSize, "The pageSize is one-based and should be larger than zero.");
            if (sortPredicate == null)
                throw new ArgumentNullException("sortPredicate");

            var collection = this.mongoDBRepositoryContext.GetCollectionForType(typeof(TAggregateRoot));
            var query = collection.AsQueryable<TAggregateRoot>().Where(specification.GetExpression());
            int skip = (pageNumber - 1) * pageSize;
            int take = pageSize;
            int totalCount = query.Count();
            int totalPages = (totalCount + pageSize - 1) / pageSize;
            if (sortPredicate != null)
            {
                switch (sortOrder)
                {
                    case SortOrder.Ascending:
                        var pagedCollectionAscending = query.OrderBy(sortPredicate).Skip(skip).Take(take).ToList();
                        return new PagedResult<TAggregateRoot>(totalCount, totalPages, pageSize, pageNumber, pagedCollectionAscending);
                    case SortOrder.Descending:
                        var pagedCollectionDescending = query.OrderByDescending(sortPredicate).Skip(skip).Take(take).ToList();
                        return new PagedResult<TAggregateRoot>(totalCount, totalPages, pageSize, pageNumber, pagedCollectionDescending);
                    default:
                        break;
                }
            }
            return null;
        }
        protected override IQueryable<TAggregateRoot> DoFindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoFindAll(specification, sortPredicate, sortOrder);
        }
        protected override IQueryable<TAggregateRoot> DoFindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder)
        {
            var collection = this.mongoDBRepositoryContext.GetCollectionForType(typeof(TAggregateRoot));
            var query = collection.AsQueryable<TAggregateRoot>().Where(specification.GetExpression());
            if (sortPredicate != null)
            {
                switch (sortOrder)
                {
                    case SortOrder.Ascending:
                        return query.OrderBy(sortPredicate);
                    case SortOrder.Descending:
                        return query.OrderByDescending(sortPredicate);
                    default:
                        break;
                }
            }
            return query;
        }
        #endregion

       
    }
}
