using System;
using System.Xml.Serialization;
namespace LCL.Events
{
    /// <summary>
    /// Represents that the implemented classes are domain events.
    /// </summary>
    /// <remarks>Domain events are the events raised by domain model.</remarks>
    public interface IDomainEvent : IEvent
    {
        /// <summary>
        /// Gets or sets the source entity from which the domain event was generated.
        /// </summary>
        IEntity Source { get; set; }
        /// <summary>
        /// Gets or sets the version of the domain event.
        /// </summary>
        long Version { get; set; }
        /// <summary>
        /// Gets or sets the branch on which the current domain event exists.
        /// </summary>
        long Branch { get; set; }
    }
}
