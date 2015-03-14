using LCL.Events;
using System.Collections.Generic;

namespace LCL
{
    /// <summary>
    /// Represents that the implemented classes are aggregate roots that
    /// support event sourcing mechanism.
    /// </summary>
    public interface ISourcedAggregateRoot : IAggregateRoot
    {
        /// <summary>
        /// Builds the aggreate from the historial events.
        /// </summary>
        /// <param name="historicalEvents">The historical events from which the aggregate is built.</param>
        void BuildFromHistory(IEnumerable<IDomainEvent> historicalEvents);
        /// <summary>
        /// Gets all the uncommitted events.
        /// </summary>
        IEnumerable<IDomainEvent> UncommittedEvents { get; }
        /// <summary>
        /// Gets the version of the aggregate.
        /// </summary>
        long Version { get; }
        /// <summary>
        /// Gets the branch on which the aggregate exists.
        /// </summary>
        long Branch { get; }
    }
}
