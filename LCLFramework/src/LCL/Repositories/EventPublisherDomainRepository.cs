using LCL.Bus;

namespace LCL.Repositories
{
    /// <summary>
    /// Represents the base class for domain repositories that would publish
    /// domain events to the event bus while saving the aggregates.
    /// </summary>
    public abstract class EventPublisherDomainRepository : DomainRepository
    {
        #region Private Fields
        private readonly IEventBus eventBus;
        #endregion

        #region Ctor
        /// <summary>
        /// Initializes a new instance of <c>EventPublisherDomainRepository</c> class.
        /// </summary>
        /// <param name="eventBus">The <see cref="Apworks.Bus.IEventBus"/> instance
        /// to which the domain events are published.</param>
        public EventPublisherDomainRepository(IEventBus eventBus)
        {
            this.eventBus = eventBus;
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <param name="disposing">A <see cref="System.Boolean"/> value which indicates whether
        /// the object should be disposed explicitly.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                eventBus.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the <see cref="Apworks.Bus.IEventBus"/> instance to which the domain events are published.
        /// </summary>
        public IEventBus EventBus
        {
            get { return this.eventBus; }
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
            get { return this.eventBus.DistributedTransactionSupported; }
        }
        /// <summary>
        /// Rollback the transaction.
        /// </summary>
        public override void Rollback()
        {
            eventBus.Rollback();
        }
        #endregion
    }
}
