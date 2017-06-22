using LCL.Domain.Uow;
using System;

namespace LCL.Domain.Repositories
{
    /// <summary>
    /// 仓储上下文
    /// </summary>
    public interface IRepositoryContext : IUnitOfWork, IDisposable
    {
        Guid ID { get; }
        void RegisterNew(object obj);
        void RegisterModified(object obj);
        void RegisterDeleted(object obj);
    }
}
