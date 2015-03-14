namespace LCL.Bus.DirectBus
{
    /// <summary>
    /// Represents the event bus that will dispatch the events immediately once
    /// the bus is committed.
    /// </summary>
    public sealed class DirectEventBus : DirectBus, IEventBus
    {
        #region Ctor
        /// <summary>
        /// Initializes a new instance of <c>DirectEventBus</c> class.
        /// </summary>
        /// <param name="dispatcher">The <see cref="Apworks.Bus.IMessageDispatcher"/> which
        /// dispatches messages in the bus.</param>
        public DirectEventBus(IMessageDispatcher dispatcher) : base(dispatcher) { }
        #endregion
    }
}
