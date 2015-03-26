using System;
namespace LCL.Events
{
    /// <summary>
    /// Represents the base class for event handlers.
    /// </summary>
    /// <typeparam name="TEvent">The type of the event to be handled.</typeparam>
    [Obsolete("This class is obsolete, use IEventHandler<TEvent> instead.")]
    public abstract class EventHandler<TEvent> : IEventHandler<TEvent>
        where TEvent : class, IEvent
    {
        #region Public Methods
        /// <summary>
        /// Handles the specified event.
        /// </summary>
        /// <param name="message">The event to be handled.</param>
        public abstract void Handle(TEvent message);
        #endregion
    }
}
