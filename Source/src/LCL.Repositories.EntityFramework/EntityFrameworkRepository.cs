using System;
using System.Collections.Generic;

using LCL;
using LCL.Domain.Entities;
using LCL.Domain.Repositories;
using LCL.Domain.Specifications;
using System.Linq.Expressions;
using System.Linq;
using System.Data.Entity;

namespace LCL.Repositories.EntityFramework
{
    public class EntityFrameworkRepository<TAggregateRoot> : EntityFrameworkRepository<TAggregateRoot,Guid>
        where TAggregateRoot : class, IAggregateRoot
    {
       #region Ctor
        public EntityFrameworkRepository(IRepositoryContext context)
            : base(context)
        {
          
        }
        #endregion
    }

    public class EntityFrameworkRepository<TAggregateRoot, TPrimaryKey> : Repository<TAggregateRoot, TPrimaryKey>
        where TAggregateRoot : class, IAggregateRoot<TPrimaryKey>
    {
        #region Private Fields
        private readonly IEntityFrameworkRepositoryContext efContext;
        #endregion

        #region Ctor
        public EntityFrameworkRepository(IRepositoryContext context)
            : base(context)
        {
            if (context is IEntityFrameworkRepositoryContext)
                this.efContext = context as IEntityFrameworkRepositoryContext;
        }
        #endregion

        #region Private Methods
        private MemberExpression GetMemberInfo(LambdaExpression lambda)
        {
            if (lambda == null)
                throw new ArgumentNullException("method");

            MemberExpression memberExpr = null;

            if (lambda.Body.NodeType == ExpressionType.Convert)
            {
                memberExpr =
                    ((UnaryExpression)lambda.Body).Operand as MemberExpression;
            }
            else if (lambda.Body.NodeType == ExpressionType.MemberAccess)
            {
                memberExpr = lambda.Body as MemberExpression;
            }

            if (memberExpr == null)
                throw new ArgumentException("method");

            return memberExpr;
        }

        private string GetEagerLoadingPath(Expression<Func<TAggregateRoot, dynamic>> eagerLoadingProperty)
        {
            MemberExpression memberExpression = this.GetMemberInfo(eagerLoadingProperty);
            var parameterName = eagerLoadingProperty.Parameters.First().Name;
            var memberExpressionStr = memberExpression.ToString();
            var path = memberExpressionStr.Replace(parameterName + ".", "");
            return path;
        }
        #endregion

        #region Protected Properties
        protected IEntityFrameworkRepositoryContext EFContext
        {
            get { return efContext; }
        }
        public virtual DbSet<TAggregateRoot> Table { get { return efContext.Context.Set<TAggregateRoot>(); } }
        #endregion

        #region Protected Methods
        protected override TAggregateRoot DoInsert(TAggregateRoot aggregateRoot)
        {
            efContext.RegisterNew(aggregateRoot);
            return aggregateRoot;
        }
        protected override IQueryable<TAggregateRoot> DoFindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder)
        {
            var query = efContext.Context.Set<TAggregateRoot>()
                .Where(specification.GetExpression());
            if (sortPredicate != null)
            {
                switch (sortOrder)
                {
                    case SortOrder.Ascending:
                        return query.SortBy<TAggregateRoot, TPrimaryKey>(sortPredicate);
                    case SortOrder.Descending:
                        return query.SortByDescending<TAggregateRoot, TPrimaryKey>(sortPredicate);
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


            var query = efContext.Context.Set<TAggregateRoot>()
                .Where(specification.GetExpression());
            int skip = (pageNumber - 1) * pageSize;
            int take = pageSize;

            switch (sortOrder)
            {
                case SortOrder.Ascending:
                    var pagedGroupAscending = query.SortBy<TAggregateRoot, TPrimaryKey>(sortPredicate).Skip(skip).Take(take).GroupBy(p => new { Total = query.Count() }).FirstOrDefault();
                    if (pagedGroupAscending == null)
                        return null;
                    return new PagedResult<TAggregateRoot>(pagedGroupAscending.Key.Total, (pagedGroupAscending.Key.Total + pageSize - 1) / pageSize, pageSize, pageNumber, pagedGroupAscending.Select(p => p).ToList());
                case SortOrder.Descending:
                    var pagedGroupDescending = query.SortByDescending<TAggregateRoot, TPrimaryKey>(sortPredicate).Skip(skip).Take(take).GroupBy(p => new { Total = query.Count() }).FirstOrDefault();
                    if (pagedGroupDescending == null)
                        return null;
                    return new PagedResult<TAggregateRoot>(pagedGroupDescending.Key.Total, (pagedGroupDescending.Key.Total + pageSize - 1) / pageSize, pageSize, pageNumber, pagedGroupDescending.Select(p => p).ToList());
                default:
                    break;
            }

            return null;
        }
        protected override IQueryable<TAggregateRoot> DoFindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            var dbset = efContext.Context.Set<TAggregateRoot>();
            IQueryable<TAggregateRoot> queryable = null;
            if (eagerLoadingProperties != null &&
                eagerLoadingProperties.Length > 0)
            {
                var eagerLoadingProperty = eagerLoadingProperties[0];
                var eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                var dbquery = dbset.Include(eagerLoadingPath);
                for (int i = 1; i < eagerLoadingProperties.Length; i++)
                {
                    eagerLoadingProperty = eagerLoadingProperties[i];
                    eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                    dbquery = dbquery.Include(eagerLoadingPath);
                }
                queryable = dbquery.Where(specification.GetExpression());
            }
            else
                queryable = dbset.Where(specification.GetExpression());

            if (sortPredicate != null)
            {
                switch (sortOrder)
                {
                    case SortOrder.Ascending:
                        return queryable.SortBy<TAggregateRoot, TPrimaryKey>(sortPredicate);
                    case SortOrder.Descending:
                        return queryable.SortByDescending<TAggregateRoot, TPrimaryKey>(sortPredicate);
                    default:
                        break;
                }
            }
            return queryable;
        }

        protected override PagedResult<TAggregateRoot> DoFindAll(ISpecification<TAggregateRoot> specification, Expression<Func<TAggregateRoot, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            if (pageNumber <= 0)
                throw new ArgumentOutOfRangeException("pageNumber", pageNumber, "The pageNumber is one-based and should be larger than zero.");
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException("pageSize", pageSize, "The pageSize is one-based and should be larger than zero.");
            if (sortPredicate == null)
                throw new ArgumentNullException("sortPredicate");

            int skip = (pageNumber - 1) * pageSize;
            int take = pageSize;

            var dbset = efContext.Context.Set<TAggregateRoot>();
            IQueryable<TAggregateRoot> queryable = null;
            if (eagerLoadingProperties != null &&
                eagerLoadingProperties.Length > 0)
            {
                var eagerLoadingProperty = eagerLoadingProperties[0];
                var eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                var dbquery = dbset.Include(eagerLoadingPath);
                for (int i = 1; i < eagerLoadingProperties.Length; i++)
                {
                    eagerLoadingProperty = eagerLoadingProperties[i];
                    eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                    dbquery = dbquery.Include(eagerLoadingPath);
                }
                queryable = dbquery.Where(specification.GetExpression());
            }
            else
                queryable = dbset.Where(specification.GetExpression());


            switch (sortOrder)
            {
                case SortOrder.Ascending:
                    var pagedGroupAscending = queryable.SortBy<TAggregateRoot, TPrimaryKey>(sortPredicate).Skip(skip).Take(take).GroupBy(p => new { Total = queryable.Count() }).FirstOrDefault();
                    if (pagedGroupAscending == null)
                        return null;
                    return new PagedResult<TAggregateRoot>(pagedGroupAscending.Key.Total, (pagedGroupAscending.Key.Total + pageSize - 1) / pageSize, pageSize, pageNumber, pagedGroupAscending.Select(p => p).ToList());
                case SortOrder.Descending:
                    var pagedGroupDescending = queryable.SortByDescending<TAggregateRoot, TPrimaryKey>(sortPredicate).Skip(skip).Take(take).GroupBy(p => new { Total = queryable.Count() }).FirstOrDefault();
                    if (pagedGroupDescending == null)
                        return null;
                    return new PagedResult<TAggregateRoot>(pagedGroupDescending.Key.Total, (pagedGroupDescending.Key.Total + pageSize - 1) / pageSize, pageSize, pageNumber, pagedGroupDescending.Select(p => p).ToList());
                default:
                    break;
            }

            return null;
        }

        protected override TAggregateRoot DoFind(ISpecification<TAggregateRoot> specification)
        {
            return efContext.Context.Set<TAggregateRoot>().Where(specification.IsSatisfiedBy).FirstOrDefault();
        }

        protected override TAggregateRoot DoFind(ISpecification<TAggregateRoot> specification, params Expression<Func<TAggregateRoot, dynamic>>[] eagerLoadingProperties)
        {
            var dbset = efContext.Context.Set<TAggregateRoot>();
            if (eagerLoadingProperties != null &&
                eagerLoadingProperties.Length > 0)
            {
                var eagerLoadingProperty = eagerLoadingProperties[0];
                var eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                var dbquery = dbset.Include(eagerLoadingPath);
                for (int i = 1; i < eagerLoadingProperties.Length; i++)
                {
                    eagerLoadingProperty = eagerLoadingProperties[i];
                    eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                    dbquery = dbquery.Include(eagerLoadingPath);
                }
                return dbquery.Where(specification.GetExpression()).FirstOrDefault();
            }
            else
                return dbset.Where(specification.GetExpression()).FirstOrDefault();
        }
        protected override bool DoExists(ISpecification<TAggregateRoot> specification)
        {
            var count = efContext.Context.Set<TAggregateRoot>().Count(specification.IsSatisfiedBy);
            return count != 0;
        }

        protected override void DoDelete(TAggregateRoot aggregateRoot)
        {
            efContext.RegisterDeleted(aggregateRoot);
        }

        protected override TAggregateRoot DoUpdate(TAggregateRoot aggregateRoot)
        {
            efContext.RegisterModified(aggregateRoot);
            return aggregateRoot;
        }
        #endregion

        public override void DoDelete(TPrimaryKey id)
        {
            var entity = Table.Local.FirstOrDefault(ent => EqualityComparer<TPrimaryKey>.Default.Equals(ent.ID, id));
            if (entity == null)
            {
                entity = Find(id);
                if (entity == null)
                {
                    return;
                }
            }

            Delete(entity);
        }
    }

}
