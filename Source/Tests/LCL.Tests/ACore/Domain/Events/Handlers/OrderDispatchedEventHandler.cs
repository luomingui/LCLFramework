using LCL.Bus;
using LCL.Domain.Events;
using LCL.Tests.Domain.Model;
using LCL.Tests.Domain.Repositories;

namespace LCL.Tests.Domain.Events.Handlers
{
    public class OrderDispatchedEventHandler : IDomainEventHandler<OrderDispatchedEvent>
    {
        private readonly ISalesOrderRepository salesOrderRepository;
        private readonly IEventBus bus;

        public OrderDispatchedEventHandler(ISalesOrderRepository salesOrderRepository, IEventBus bus)
        {
            this.salesOrderRepository = salesOrderRepository;
            this.bus = bus;
        }

        public void Handle(OrderDispatchedEvent evnt)
        {
            SalesOrder salesOrder = evnt.Source as SalesOrder;
            salesOrder.DateDispatched = evnt.DispatchedDate;
            salesOrder.Status = SalesOrderStatus.Dispatched;

            bus.Publish<OrderDispatchedEvent>(evnt);
        }
    }
}
