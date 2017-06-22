

using LCL.Domain.Repositories;
using LCL.Repositories.MongoDB;
using LCL.Tests.Domain.Model;
namespace LCL.Tests.Domain.Repositories.MongoDB
{
    public class RoleRepository : MongoDBRepository<Role>, IRoleRepository
    {
        public RoleRepository(IRepositoryContext context)
            : base(context)
        { }

    }
}
