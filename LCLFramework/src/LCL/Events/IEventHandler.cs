
using LCL.Bus;

namespace LCL.Events
{
    /// <summary>
    /// Represents that the implemented classes are event handlers.
    /// </summary>
    /// <typeparam name="TEvent">The type of the event to be handled.</typeparam>
    [RegisterDispatch]
    public interface IEventHandler<in TEvent> : IHandler<TEvent>
        where TEvent : class, IEvent
    {
    }
}
