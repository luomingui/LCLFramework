using LCL.Caching;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace LCL.Repositories.EntityFramework
{
    /// <summary>
    /// Represents the Entity Framework repository.
    /// </summary>
    /// <typeparam name="TEntity">The type of the aggregate root.</typeparam>
    public class EntityFrameworkRepository<TEntity> : Repository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly IEntityFrameworkRepositoryContext efContext;
        /// <summary>
        /// Initializes a new instace of <c>EntityFrameworkRepository</c> class.
        /// </summary>
        /// <param name="context">The repository context.</param>
        public EntityFrameworkRepository(IRepositoryContext context)
            : base(context)
        {
            if (context is IEntityFrameworkRepositoryContext)
                this.efContext = context as IEntityFrameworkRepositoryContext;
        }
        #region Private Methods
        private MemberExpression GetMemberInfo(LambdaExpression lambda)
        {
            if (lambda == null)
                throw new ArgumentNullException("method");
            MemberExpression memberExpr = null;
            if (lambda.Body.NodeType == ExpressionType.Convert)
            {
                memberExpr = ((UnaryExpression)lambda.Body).Operand as MemberExpression;
            }
            else if (lambda.Body.NodeType == ExpressionType.MemberAccess)
            {
                memberExpr = lambda.Body as MemberExpression;
            }
            if (memberExpr == null)
                throw new ArgumentException("method");
            return memberExpr;
        }
        private string GetEagerLoadingPath(Expression<Func<TEntity, dynamic>> eagerLoadingProperty)
        {
            MemberExpression memberExpression = this.GetMemberInfo(eagerLoadingProperty);
            var parameterName = eagerLoadingProperty.Parameters.First().Name;
            var memberExpressionStr = memberExpression.ToString();
            var path = memberExpressionStr.Replace(parameterName + ".", "");
            return path;
        }
        #endregion

        #region Protected Properties
        /// <summary>
        /// Gets the instance of the <see cref="IEntityFrameworkRepositoryContext"/>.
        /// </summary>
        protected IEntityFrameworkRepositoryContext EFContext
        {
            get { return efContext; }
        }
        #endregion

        /// <summary>
        /// Does the add.
        /// </summary>
        /// <param name="entity">The entity.</param>
        protected override void DoAdd(TEntity entity)
        {
            //判断是否是树形

            efContext.RegisterNew(entity);
        }
        /// <summary>
        /// Does the remove.
        /// </summary>
        /// <param name="entity">The entity.</param>
        protected override void DoRemove(TEntity entity)
        {
            efContext.RegisterDeleted(entity);
        }
        protected override void DoRemove(object keyValue)
        {
            var entity = efContext.Context.Set<TEntity>().Where(p => p.ID == (Guid)keyValue).First();
            efContext.RegisterDeleted(entity);
        }
        /// <summary>
        /// Does the remove.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        protected override void DoRemove(Expression<Func<TEntity, bool>> predicate)
        {
            var list = DoGet(predicate);
            foreach (var entity in list)
            {
                efContext.RegisterDeleted(entity);
            }
        }
        /// <summary>
        /// Does the update.
        /// </summary>
        /// <param name="entity">The entity.</param>
        protected override void DoUpdate(TEntity entity)
        {
            efContext.RegisterModified(entity);
        }
        /// <summary>
        /// Does the get.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        protected override IEnumerable<TEntity> DoGet(Expression<Func<TEntity, bool>> predicate)
        {
            return efContext.Context.Set<TEntity>().Where(predicate).ToList();
        }
        protected override TEntity DoGetByKey(object keyValue)
        {
            return efContext.Context.Set<TEntity>().Where(p => p.ID == (Guid)keyValue).First();
        }

        #region DoXXXXXX
        protected override IQueryable<TEntity> DoFindAll(Specifications.ISpecification<TEntity> specification,
            Expression<Func<TEntity, dynamic>> sortPredicate, LCL.SortOrder sortOrder)
        {
            var query = efContext.Context.Set<TEntity>().Where(specification.GetExpression());
            if (sortPredicate != null)
            {
                switch (sortOrder)
                {
                    case SortOrder.Ascending:
                        return query.SortBy(sortPredicate);
                    case SortOrder.Descending:
                        return query.SortByDescending(sortPredicate);
                    default:
                        break;
                }
            }
            return query;
        }
        protected override IQueryable<TEntity> DoFindAll(Specifications.ISpecification<TEntity> specification,
            Expression<Func<TEntity, dynamic>> sortPredicate, LCL.SortOrder sortOrder, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
        {
            var dbset = efContext.Context.Set<TEntity>();
            IQueryable<TEntity> queryable = null;
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
                        return queryable.SortBy(sortPredicate);
                    case SortOrder.Descending:
                        return queryable.SortByDescending(sortPredicate);
                    default:
                        break;
                }
            }
            return queryable;
        }
        protected override PagedResult<TEntity> DoFindAll(Specifications.ISpecification<TEntity> specification,
            Expression<Func<TEntity, dynamic>> sortPredicate, LCL.SortOrder sortOrder, int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
                throw new ArgumentOutOfRangeException("pageNumber", pageNumber, "The pageNumber is one-based and should be larger than zero.");
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException("pageSize", pageSize, "The pageSize is one-based and should be larger than zero.");
            if (sortPredicate == null)
                throw new ArgumentNullException("sortPredicate");

            var query = efContext.Context.Set<TEntity>().Where(specification.GetExpression());
            int skip = (pageNumber - 1) * pageSize;
            int take = pageSize;

            switch (sortOrder)
            {
                case SortOrder.Ascending:
                    var pagedGroupAscending = query.SortBy(sortPredicate).Skip(skip).Take(take).GroupBy(p => new { Total = query.Count() }).FirstOrDefault();
                    if (pagedGroupAscending == null)
                        return null;
                    return new PagedResult<TEntity>(pagedGroupAscending.Key.Total, (pagedGroupAscending.Key.Total + pageSize - 1) / pageSize, pageSize, pageNumber, pagedGroupAscending.Select(p => p).ToList());
                case SortOrder.Descending:
                    var pagedGroupDescending = query.SortByDescending(sortPredicate).Skip(skip).Take(take).GroupBy(p => new { Total = query.Count() }).FirstOrDefault();
                    if (pagedGroupDescending == null)
                        return null;
                    return new PagedResult<TEntity>(pagedGroupDescending.Key.Total, (pagedGroupDescending.Key.Total + pageSize - 1) / pageSize, pageSize, pageNumber, pagedGroupDescending.Select(p => p).ToList());
                default:
                    break;
            }

            return null;
        }
        protected override TEntity DoFind(Expression<Func<TEntity, bool>> predicate)
        {
            return efContext.Context.Set<TEntity>().Where(predicate).FirstOrDefault();
        }
        protected override TEntity DoFind(Specifications.ISpecification<TEntity> specification)
        {
            return efContext.Context.Set<TEntity>().Where(specification.IsSatisfiedBy).FirstOrDefault();
        }
        protected override TEntity DoFind(Specifications.ISpecification<TEntity> specification, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
        {
            var dbset = efContext.Context.Set<TEntity>();
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
        protected override bool DoExists(Specifications.ISpecification<TEntity> specification)
        {
            var count = efContext.Context.Set<TEntity>().Count(specification.IsSatisfiedBy);
            return count != 0;
        }
        #endregion


    }
}
