
using LCL;
using LCL.Domain.Entities;
using LCL.Domain.Repositories;
using LCL.Domain.Specifications;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LCL.Repositories.NHibernate
{
    /// <summary>
    /// Represents the repository which supports the NHibernate implementation.
    /// </summary>
    /// <typeparam name="TEntity">The type of the aggregate root.</typeparam>
    public class NHibernateRepository<TEntity> : NHibernateRepository<TEntity, Guid>
        where TEntity : class, IEntity
    {
        #region Ctor
        public NHibernateRepository(IRepositoryContext context)
            : base(context)
        {

        }
        #endregion
    }

    public class NHibernateRepository<TEntity, TPrimaryKey> : Repository<TEntity, TPrimaryKey>
    where TEntity : class, IEntity<TPrimaryKey>
    {
        #region Private Fields
        //private readonly ISession session = null;
        private readonly INHibernateContext nhContext = null;
        #endregion

        #region Ctor
        /// <summary>
        /// Initializes a new instance of <c>NHibernateRepository&lt;TAggregateRoot&gt;</c> class.
        /// </summary>
        /// <param name="context">The instance of the repository context.</param>
        public NHibernateRepository(IRepositoryContext context)
            : base(context)
        {
            nhContext = context as INHibernateContext;
            if (nhContext == null)
                throw new LException("The provided context type is invalid. NHibernateRepository requires an instance of NHibernateContext to be initialized.");
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Adds an entity to the repository.
        /// </summary>
        /// <param name="entity">The entity object to be added.</param>
        protected override TEntity DoInsert(TEntity entity)
        {
            this.Context.RegisterNew(entity);
            return entity;
        }
        /// <summary>
        /// Removes the entity from the repository.
        /// </summary>
        /// <param name="entity">The entity to be removed.</param>
        protected override void DoDelete(TEntity entity)
        {
            this.Context.RegisterDeleted(entity);
        }
        /// <summary>
        /// Updates the entity in the repository.
        /// </summary>
        /// <param name="entity">The entity to be updated.</param>
        protected override TEntity DoUpdate(TEntity entity)
        {
            this.Context.RegisterModified(entity);
            return entity;
        }
        public override void DoDelete(TPrimaryKey id)
        {
            var entity = this.FindAll().FirstOrDefault<TEntity>(ent => EqualityComparer<TPrimaryKey>.Default.Equals(ent.ID, id));
            Delete(entity);
        }
        /// <summary>
        /// Checkes whether the aggregate root which matches the given specification exists.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate root should match.</param>
        /// <returns>True if the aggregate root exists, otherwise false.</returns>
        protected override bool DoExists(ISpecification<TEntity> specification)
        {
            return this.DoFind(specification) != null;
        }

        /// <summary>
        /// Finds all the aggregate roots from repository.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate roots should match.</param>
        /// <param name="sortPredicate">The sort predicate which is used for sorting.</param>
        /// <param name="sortOrder">The <see cref="Apworks.Storage.SortOrder"/> enumeration which specifies the sort order.</param>
        /// <param name="eagerLoadingProperties">The properties for the aggregated objects that need to be loaded.</param>
        /// <returns>The aggregate root.</returns>
        protected override IQueryable<TEntity> DoFindAll(ISpecification<TEntity> specification, Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoFindAll(specification, sortPredicate, sortOrder);
        }
        /// <summary>
        /// Finds all the aggregate roots from repository.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate roots should match.</param>
        /// <param name="sortPredicate">The sort predicate which is used for sorting.</param>
        /// <param name="sortOrder">The <see cref="Apworks.Storage.SortOrder"/> enumeration which specifies the sort order.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The number of objects per page.</param>
        /// <param name="eagerLoadingProperties">The properties for the aggregated objects that need to be loaded.</param>
        /// <returns>The aggregate root.</returns>
        protected override PagedResult<TEntity> DoFindAll(ISpecification<TEntity> specification, Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoFindAll(specification, sortPredicate, sortOrder, pageNumber, pageSize);
        }

        /// <summary>
        /// Finds a single aggregate root from the repository.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate root should match.</param>
        /// <param name="eagerLoadingProperties">The properties for the aggregated objects that need to be loaded.</param>
        /// <returns>The aggregate root.</returns>
        protected override TEntity DoFind(ISpecification<TEntity> specification, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoFind(specification, eagerLoadingProperties);
        }

        /// <summary>
        /// Finds all the aggregate roots that match the given specification, and sorts the aggregate roots
        /// by using the provided sort predicate and the specified sort order.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate roots should match.</param>
        /// <param name="sortPredicate">The sort predicate which is used for sorting.</param>
        /// <param name="sortOrder">The <see cref="Apworks.Storage.SortOrder"/> enum which specifies the sort order.</param>
        /// <returns>All the aggregate roots that match the given specification and were sorted by using the given sort predicate and the sort order.</returns>
        protected override IQueryable<TEntity> DoFindAll(ISpecification<TEntity> specification, Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder)
        {
            return nhContext.FindAll<TEntity, TPrimaryKey>(specification, sortPredicate, sortOrder);
        }
        /// <summary>
        /// Finds all the aggregate roots that match the given specification with paging enabled, and sorts the aggregate roots
        /// by using the provided sort predicate and the specified sort order.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate roots should match.</param>
        /// <param name="sortPredicate">The sort predicate which is used for sorting.</param>
        /// <param name="sortOrder">The <see cref="Apworks.Storage.SortOrder"/> enumeration which specifies the sort order.</param>
        /// <param name="pageNumber">The number of objects per page.</param>
        /// <param name="pageSize">The number of objects per page.</param>
        /// <returns>All the aggregate roots that match the given specification and were sorted by using the given sort predicate and the sort order.</returns>
        protected override PagedResult<TEntity> DoFindAll(ISpecification<TEntity> specification, Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize)
        {
            return nhContext.FindAll<TEntity, TPrimaryKey>(specification, sortPredicate, sortOrder, pageNumber, pageSize);
        }

        /// <summary>
        /// Finds a single aggregate root that matches the given specification.
        /// </summary>
        /// <param name="specification">The specification with which the aggregate root should match.</param>
        /// <returns>The instance of the aggregate root.</returns>
        protected override TEntity DoFind(ISpecification<TEntity> specification)
        {
            return nhContext.Find<TEntity, TPrimaryKey>(specification);
        }
        #endregion

    }
}
