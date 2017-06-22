using LCL.Domain.Events;
using LCL.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCL.Bus
{
    public interface IBus : IUnitOfWork, IDisposable
    {
        Guid ID { get; }
        void Publish<TMessage>(TMessage message) where TMessage : class, IEvent;
        void Publish<TMessage>(IEnumerable<TMessage> messages) where TMessage : class, IEvent;
        void Clear();
    }
}
