using System.Collections.Generic;
using System.Linq;
using LCL.Specifications;
using System;

namespace LCL.Repositories
{
    /// <summary>
    /// Represents the domain repository that uses the <see cref="Apworks.Repositories.IRepositoryContext"/>
    /// and <see cref="Apworks.Repositories.IRepository&lt;TAggregateRoot&gt;"/> instances to perform aggregate
    /// operations.
    /// </summary>
    public class RegularDomainRepository : DomainRepository
    {
        #region Private Fields
        private readonly IRepositoryContext context;
        private readonly HashSet<ISourcedAggregateRoot> dirtyHash = new HashSet<ISourcedAggregateRoot>();
        #endregion

        #region Ctor
        /// <summary>
        /// Initializes a new instance of <c>RegularDomainRepository</c> class.
        /// </summary>
        /// <param name="context">The <see cref="Apworks.Repositories.IRepositoryContext"/> instance to which the 
        /// <c>RegularDomainRepository</c> forwards the repository operations.</param>
        public RegularDomainRepository(IRepositoryContext context)
        {
            this.context = context;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the <see cref="Apworks.Repositories.IRepositoryContext"/> instance.
        /// </summary>
        public IRepositoryContext Context
        {
            get { return this.context; }
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Commits the changes registered in the domain repository.
        /// </summary>
        protected override void DoCommit()
        {
            foreach (var aggregateRootObj in this.SaveHash)
            {
                this.context.RegisterNew(aggregateRootObj);
            }
            foreach (var aggregateRootObj in this.dirtyHash)
            {
                this.context.RegisterModified(aggregateRootObj);
            }

            this.context.Commit();

            this.dirtyHash.ToList().ForEach(this.DelegatedUpdateAndClearAggregateRoot);
            this.dirtyHash.Clear();
        }
        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <param name="disposing">A <see cref="System.Boolean"/> value which indicates whether
        /// the object should be disposed explicitly.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!this.Committed)
                {
                    try
                    {
                        this.Commit();
                    }
                    catch
                    {
                        this.Rollback();
                        throw;
                    }
                }
                this.context.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion

        #region IDomainRepository Members
        /// <summary>
        /// Gets the instance of the aggregate root with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier of the aggregate root.</param>
        /// <returns>The instance of the aggregate root with the specified identifier.</returns>
        public override TAggregateRoot Get<TAggregateRoot>(Guid id)
        {
            var querySaveHash = from p in this.SaveHash
                                where p.ID.Equals(id)
                                select p;
            var queryDirtyHash = from p in this.dirtyHash
                                 where p.ID.Equals(id)
                                 select p;
            if (querySaveHash != null && querySaveHash.Count() > 0)
                return querySaveHash.FirstOrDefault() as TAggregateRoot;
            if (queryDirtyHash != null && queryDirtyHash.Count() > 0)
                return queryDirtyHash.FirstOrDefault() as TAggregateRoot;

            IRepository<TAggregateRoot> repository =RF.Concrete<IRepository<TAggregateRoot>>();
            ISpecification<TAggregateRoot> spec = Specification<TAggregateRoot>.Eval(ar => ar.ID.Equals(id));
            var result = repository.Find(spec);
            // Clears the aggregate root since version info is not needed in regular repositories.
            this.DelegatedUpdateAndClearAggregateRoot(result);
            return result;
        }
        /// <summary>
        /// Saves the aggregate represented by the specified aggregate root to the repository.
        /// </summary>
        /// <param name="aggregateRoot">The aggregate root that is going to be saved.</param>
        public override void Save<TAggregateRoot>(TAggregateRoot aggregateRoot)
        {
            IRepository<TAggregateRoot> repository = RF.Concrete<IRepository<TAggregateRoot>>();
            ISpecification<TAggregateRoot> spec = Specification<TAggregateRoot>.Eval(ar => ar.ID.Equals(aggregateRoot.ID));
            if (repository.Exists(spec))
            {
                if (!this.dirtyHash.Contains(aggregateRoot))
                    this.dirtyHash.Add(aggregateRoot);
                this.Committed = false;
            }
            else
            {
                base.Save<TAggregateRoot>(aggregateRoot);
            }

        }
        #endregion

        #region IUnitOfWork Members
        /// <summary>
        /// Gets a <see cref="System.Boolean"/> value which indicates
        /// whether the Unit of Work could support Microsoft Distributed
        /// Transaction Coordinator (MS-DTC).
        /// </summary>
        public override bool DistributedTransactionSupported
        {
            get { return context.DistributedTransactionSupported; }
        }
        /// <summary>
        /// Rollback the transaction.
        /// </summary>
        public override void Rollback()
        {
            this.context.Rollback();
        }
        #endregion

    }
}
