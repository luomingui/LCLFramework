using LCL.Repositories;
using LCL.Caching;
using LCL.Repositories.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace UIShell.RbacPermissionService
{
    public interface IUserRepository : IRepository<User>
    {
       
    }
    public class UserRepository : EntityFrameworkRepository<User>, IUserRepository
    {
        public UserRepository(IRepositoryContext context)
            : base(context)
        {

        }
       
    }
}
