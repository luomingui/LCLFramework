using LCL;
using LCL.Repositories.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using UIShell.RbacPermissionService;

namespace UIShell.RbacPermissionService
{
    public class RbacDbContext : BaseDbContext
    {
        public RbacDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }
        //RBAC
        public DbSet<Org> Org { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<RoleAuthority> RoleAuthority { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Dictionary> Dictionary { get; set; }
        
    }
}
