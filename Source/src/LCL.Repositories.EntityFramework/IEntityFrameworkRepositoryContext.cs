using LCL.Domain.Repositories;
using System.Data.Entity;

namespace LCL.Repositories.EntityFramework
{
    public interface IEntityFrameworkRepositoryContext : IRepositoryContext
    {
        LclDbContext Context { get; }
    }
}
