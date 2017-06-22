
namespace LCL.Domain.Events
{
    /// <summary>
    /// 事件处理
    /// </summary>
    /// <typeparam name="TEvent">要处理的事件类型</typeparam>
    public interface IEventHandler<in TEvent> : IHandler<TEvent> where TEvent : class, IEvent
    {
    }
}
