
using LCL.Repositories;
namespace LCL.Commands
{
    /// <summary>
    /// Represents the base class for command handlers.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command to be handled.</typeparam>
    public abstract class CommandHandler<TCommand> : ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        #region Ctor
        /// <summary>
        /// Initializes a new instance of <c>CommandHandler&lt;TCommand&gt;</c> class.
        /// </summary>
        public CommandHandler()
        {
            
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Handles the specified command.
        /// </summary>
        /// <param name="command">The command instance to be handled.</param>
        public abstract void Handle(TCommand command);
        #endregion

        #region Protected Properties
        /// <summary>
        /// Gets the instance of the domain repository that could be used
        /// by the current command handler to perform repository operations.
        /// </summary>
        protected virtual IDomainRepository DomainRepository
        {
            get { return RF.GetDomainRepository(); }
        }
        #endregion

    }
}
