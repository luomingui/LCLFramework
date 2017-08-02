
using LCL.Domain.Entities;
using LCL.Domain.Events;
using LCL.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCL.Domain.Events
{
    [Serializable]
    public class GetUserOrdersEvent : DomainEvent
    {
        public GetUserOrdersEvent(IEntity source) : base(source) { }

        public IEnumerable<SalesOrder> SalesOrders { get; set; }


    }
}
