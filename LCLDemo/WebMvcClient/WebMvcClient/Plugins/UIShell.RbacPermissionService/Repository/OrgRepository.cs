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
    public interface IOrgRepository : IRepository<Org>
    {
       
    }
    public class OrgRepository : EntityFrameworkRepository<Org>, IOrgRepository
    {
        public OrgRepository(IRepositoryContext context)
            : base(context)
        {

        }
       
    }
}
