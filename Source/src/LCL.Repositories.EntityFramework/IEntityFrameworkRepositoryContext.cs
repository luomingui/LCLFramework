using LCL.Domain.Repositories;
using System.Data.Entity;

namespace LCL.Repositories.EntityFramework
{
    public interface IEntityFrameworkRepositoryContext : IRepositoryContext
    {
        BaseDbContext Context { get; }
    }
}
