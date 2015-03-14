using System.Data.Entity;

namespace LCL.Repositories.EntityFramework
{
    /// <summary>
    /// Represents that the implemented classes are repository contexts that utilize
    /// the functionality provided by Entity Framework.
    /// </summary>
    public interface IEntityFrameworkRepositoryContext : IRepositoryContext
    {
        /// <summary>
        /// Gets the <see cref="DbContext"/> instance handled by Entity Framework.
        /// </summary>
        DbContext Context { get; }
    }
}
