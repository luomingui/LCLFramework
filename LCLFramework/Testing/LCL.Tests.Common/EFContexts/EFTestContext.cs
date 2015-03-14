using LCL.Repositories.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace LCL.Tests.Common
{
    public class EFTestDbContext : BaseDbContext
    {
        public EFTestDbContext()
            : base("RBAC")
        {

        }
        //RBAC
        public DbSet<Org> Org { get; set; }
        public DbSet<OrgPositionOperationDeny> OrgPositionOperationDeny { get; set; }
        public DbSet<Position> Position { get; set; }
        public DbSet<User> User { get; set; }
    }

    /// <summary>
    /// 数据库初始化操作类
    /// </summary>
    internal static class DatabaseInitializer
    {
        /// <summary>
        /// 数据库初始化
        /// </summary>
        public static void Initialize()
        {
            try
            {
                Database.SetInitializer<EFTestDbContext>(new DropCreateDatabaseIfModelChanges<EFTestDbContext>());
                Database.SetInitializer(new SampleData());
                using (var db = new EFTestDbContext())
                {
                    db.Database.Initialize(false);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("DatabaseInitializer ", ex);
            }
        }
    }

    /// <summary>
    /// 数据库初始化策略
    /// </summary>
    internal class SampleData : CreateDatabaseIfNotExists<EFTestDbContext>
    {
        protected override void Seed(EFTestDbContext context)
        {
            //RBAC
            var ppod1 = context.Set<OrgPositionOperationDeny>().Add(new OrgPositionOperationDeny
            {
                ID = Guid.NewGuid(),
                BlockKey = "小区管理",
                ModuleKey = "小区管理",
                OperationKey = "add",
            });
            var ppod2 = context.Set<OrgPositionOperationDeny>().Add(new OrgPositionOperationDeny
            {
                ID = Guid.NewGuid(),
                BlockKey = "小区管理",
                ModuleKey = "小区管理",
                OperationKey = "edit",
            });
            var ppod3 = context.Set<OrgPositionOperationDeny>().Add(new OrgPositionOperationDeny
            {
                ID = Guid.NewGuid(),
                BlockKey = "小区管理",
                ModuleKey = "小区管理",
                OperationKey = "delete",
            });
            ICollection<OrgPositionOperationDeny> ppodList = new List<OrgPositionOperationDeny>();
            ppodList.Add(ppod1);
            ppodList.Add(ppod2);
            ppodList.Add(ppod3);

            var org = context.Set<Org>().Add(new Org { ID = Guid.NewGuid(), Name = "研发部" });

            var positon2 = context.Set<Position>().Add(new Position
            {
                ID = Guid.NewGuid(),
                Name = "软件工程师",
                Org = org,
                OrgPositionOperationDeny = ppodList
            });

            var user2 = context.Set<User>().Add(new User { ID = Guid.NewGuid(), Code = "admin", Name = "管理员", Password = "123456", Position = positon2 });

            context.SaveChanges();
        }
    }
}
