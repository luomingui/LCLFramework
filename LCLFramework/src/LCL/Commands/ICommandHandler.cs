
using LCL.Bus;
namespace LCL.Commands
{
    /// <summary>
    /// Represents that the implemented classes are command handlers.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command to be handled.</typeparam>
    [RegisterDispatch]
    public interface ICommandHandler<TCommand> : IHandler<TCommand>
        where TCommand : ICommand
    {

    }
}
