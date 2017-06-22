using LCL.Domain.Repositories;
using LCL.Repositories.EntityFramework;
using LCL.Tests.Domain.Model;
using LCL.Tests.Domain.Repositories;

namespace LCL.Tests.Domain.Repositories.EntityFramework
{
    public class RoleRepository : EntityFrameworkRepository<Role>, IRoleRepository
    {
        public RoleRepository(IRepositoryContext context)
            : base(context)
        { }

    }
}
