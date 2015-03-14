namespace LCL.Bus.DirectBus
{
    /// <summary>
    /// Represents the command bus that will dispatch the commands immediately once
    /// the bus is committed.
    /// </summary>
    public sealed class DirectCommandBus : DirectBus, ICommandBus
    {
        #region Ctor
        /// <summary>
        /// Initializes a new instance of <c>DirectCommandBus</c> class.
        /// </summary>
        /// <param name="dispatcher">The <see cref="Apworks.Bus.IMessageDispatcher"/> which
        /// dispatches messages in the bus.</param>
        public DirectCommandBus(IMessageDispatcher dispatcher) : base(dispatcher) { }
        #endregion
    }
}
