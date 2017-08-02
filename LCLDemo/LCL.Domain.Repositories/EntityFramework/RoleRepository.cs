using LCL.Domain.Repositories;
using LCL.Repositories.EntityFramework;
using LCL.Domain.Model;
using LCL;

namespace LCL.Domain.Repositories.EntityFramework
{
    public class RoleRepository : EntityFrameworkRepository<Role>, IRoleRepository
    {
        public RoleRepository(IRepositoryContext context)
            : base(context)
        { }

    }
}
