using System;

namespace LCL.Events
{
    /// <summary>
    /// Represents that the implemented classes are events.
    /// </summary>
    public interface IEvent : IEntity
    {
        /// <summary>
        /// Gets or sets the date and time on which the event was produced.
        /// </summary>
        /// <remarks>The format of this date/time value could be various between different
        /// systems. Apworks recommend system designer or architect uses the standard
        /// UTC date/time format.</remarks>
        DateTime Timestamp { get; set; }
        /// <summary>
        /// Gets or sets the assembly qualified type name of the event.
        /// </summary>
        string AssemblyQualifiedEventType { get; set; }
    }
}
