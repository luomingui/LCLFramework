namespace LCL.Domain.Events
{
    /// <summary>
    /// 表示领域事件的事件处理程序。
    /// </summary>
    /// <typeparam name="TDomainEvent">由当前处理程序处理的域事件的类型。</typeparam>
    public interface IDomainEventHandler<in TDomainEvent> : IEventHandler<TDomainEvent>
        where TDomainEvent : class, IDomainEvent
    {

    }
}
