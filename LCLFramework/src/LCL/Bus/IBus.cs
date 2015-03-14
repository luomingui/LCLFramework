using LCL.Repositories;
using System;
using System.Collections.Generic;

namespace LCL.Bus
{
    public interface IBus : IUnitOfWork, IDisposable
    {
        void Publish<TMessage>(TMessage message);
        void Publish<TMessage>(IEnumerable<TMessage> messages);
        void Clear();
    }
}
