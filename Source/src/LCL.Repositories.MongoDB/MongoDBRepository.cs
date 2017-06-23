
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using LCL.Domain.Entities;
using LCL.Domain.Repositories;
using LCL.Domain.Specifications;
using LCL;

namespace LCL.Repositories.MongoDB
{
    public class MongoDBRepository<TAggregateRoot> : MongoDBRepository<TAggregateRoot, Guid>
        where TAggregateRoot : class, IAggregateRoot
    {
         #region Ctor
        public MongoDBRepository(IRepositoryContext context)
            : base(context)
        {
          
        }
        #endregion
    }

    public class MongoDBRepository<TAggregateRoot, TPrimaryKey> : Repository<TAggregateRoot, TPrimaryKey>
    where TAggregateRoot : class, IAggregateRoot<TPrimaryKey>
    {
        #region Private Fields
        private readonly IMongoDBRepositoryContext mongoDBRepositoryContext;
        #endregion

        #region Ctor
        public MongoDBRepository(IRepositoryContext context)
            : base(context)
        {
            if (context is IMongoDBRepositoryContext)
                mongoDBRepositoryContext = context as MongoDBRepositoryContext;
            else
                throw new InvalidOperationException("Invalid repository context type.");
        }
        #endregion

        #region Protected Methods
        protected override TAggregateRoot DoInsert(TAggregateRoot aggregateRoot)
        {
            mongoDBRepositoryContext.RegisterNew(aggregateRoot);
            return aggregateRoot;
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

        protected override PagedResult<TAggregateRoot> DoFindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoFindAll(specification, sortPredicate, sortOrder, pageNumber, pageSize);
        }

        protected override TAggregateRoot DoFind(ISpecification<TAggregateRoot> specification)
        {
            var collection = this.mongoDBRepositoryContext.GetCollectionForType(typeof(TAggregateRoot));
            return collection.AsQueryable<TAggregateRoot>().Where(specification.GetExpression()).FirstOrDefault();
        }

        protected override TAggregateRoot DoFind(ISpecification<TAggregateRoot> specification, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoFind(specification);
        }

        protected override bool DoExists(ISpecification<TAggregateRoot> specification)
        {
            return this.DoFind(specification) != null;
        }

        protected override void DoDelete(TAggregateRoot aggregateRoot)
        {
            mongoDBRepositoryContext.RegisterDeleted(aggregateRoot);
        }

        protected override TAggregateRoot DoUpdate(TAggregateRoot aggregateRoot)
        {
            mongoDBRepositoryContext.RegisterModified(aggregateRoot);
            return aggregateRoot;
        }
        public override void DoDelete(TPrimaryKey id)
        {    
            var entity= this.FindAll().FirstOrDefault<TAggregateRoot>(ent => EqualityComparer<TPrimaryKey>.Default.Equals(ent.ID, id));
            Delete(entity);
        }
        #endregion
    }
}
