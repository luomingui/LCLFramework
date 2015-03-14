using System;
namespace LCL
{
    /// <summary>
    /// Represents that the implemented classes are message handlers.
    /// </summary>
    /// <typeparam name="T">The type of the message to be handled.</typeparam>
    public interface IHandler<in T>
    {
        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message to be handled.</param>
        void Handle(T message);
    }
    /// <summary>
    /// Represents that the decorated methods are inline domain event handlers.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class HandlesAttribute : System.Attribute
    {
        #region Public Properties
        /// <summary>
        /// Gets or sets the type of the domain event that can be handled by
        /// the decorated method.
        /// </summary>
        public Type DomainEventType { get; set; }
        #endregion

        #region Ctor
        /// <summary>
        /// Initializes a new instance of <c>HandlesAttribute</c> class.
        /// </summary>
        /// <param name="domainEventType">The type of the domain event that can be handled by
        /// the decorated method.</param>
        public HandlesAttribute(Type domainEventType)
        {
            this.DomainEventType = domainEventType;
        }
        #endregion
    }
}
