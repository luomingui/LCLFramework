using LCL;
using LCL.Repositories.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Text;

namespace UIShell.RbacPermissionService
{
    public class RbacDbContext : BaseDbContext
    {
        public RbacDbContext()
            : base("LCL")
        {

        }
        //public RbacDbContext(string nameOrConnectionString)
        //    : base(nameOrConnectionString)
        //{

        //}
        //RBAC
        public DbSet<Role> Role { get; set; }
        public DbSet<RoleAuthority> RoleAuthority { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<DictType> DictType { get; set; }
        public DbSet<Dictionary> Dictionary { get; set; }
        public DbSet<ScheduledEvents> ScheduledEvents { get; set; }
        public DbSet<TLog> TLog { get; set; }
        public DbSet<Xzqy> Xzqy { get; set; }
        public DbSet<Department> Department { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<IncludeMetadataConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //表前缀
            modelBuilder.Types().Configure(entity => entity.ToTable(AppConstant.Tableprefix + entity.ClrType.Name));

            // 忽略列映射 Fluent API:NotMapped
            modelBuilder.Entity<ScheduledEvents>().Ignore(p => p.ScheduleType);
            modelBuilder.Entity<ScheduledEvents>().Ignore(p => p.ExetimeType);
            modelBuilder.Entity<ScheduledEvents>().Ignore(p => p.Exetime);
            modelBuilder.Entity<ScheduledEvents>().Ignore(p => p.hour);
            modelBuilder.Entity<ScheduledEvents>().Ignore(p => p.minute);
            modelBuilder.Entity<ScheduledEvents>().Ignore(p => p.timeserval);
            modelBuilder.Entity<ScheduledEvents>().Ignore(p => p.Enable);
            modelBuilder.Entity<ScheduledEvents>().Ignore(p => p.Issystemevent);

            base.OnModelCreating(modelBuilder);

        }

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
                Database.SetInitializer<RbacDbContext>(new DropCreateDatabaseIfModelChanges<RbacDbContext>());
                Database.SetInitializer(new SampleData());
                using (var db = new RbacDbContext())
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
    internal class SampleData : CreateDatabaseIfNotExists<RbacDbContext>
    {
        protected override void Seed(RbacDbContext context)
        {
            var dep0 = context.Set<Department>().Add(new Department { ParentId = Guid.Empty, NodePath = "永新科技", Name = "永新科技", OrderBy = 0, Level = 0, IsLast = false, DepartmentType=DepartmentType.公司, OfficePhone = "0791-83881788", Address = "南昌市红谷滩江报路唐宁街B座1501室", Remark = "" });
            var dep1 = context.Set<Department>().Add(new Department { ParentId = dep0.ID, NodePath = dep0.Name + "/研发部", Name = "研发部", OrderBy = 1, Level = 1, IsLast = true, DepartmentType = DepartmentType.部门, OfficePhone = "0791-83881788", Address = "南昌市红谷滩江报路唐宁街B座1501室", Remark = "" });
            var dep2 = context.Set<Department>().Add(new Department { ParentId = dep0.ID, NodePath = dep0.Name + "/市场部", Name = "市场部", OrderBy = 2, Level = 1, IsLast = true, DepartmentType = DepartmentType.部门, OfficePhone = "0791-83881788", Address = "南昌市红谷滩江报路唐宁街B座1501室", Remark = "" });

            var role1 = context.Set<Role>().Add(new Role { Name = "系统管理员", RoleType = 1, Remark = "系统管理员" });
            var role2 = context.Set<Role>().Add(new Role { Name = "业务管理者", RoleType = 1, Remark = "业务管理者" });
            var role3 = context.Set<Role>().Add(new Role { Name = "业务操作者", RoleType = 1, Remark = "业务操作者" });

            int flgInt = 0;
            for (int i = 0; i < 10; i++)
            {
                var dep = new Department();
                var list = new List<Role>();
                switch (flgInt)
                {
                    case 0:
                        list.Add(role1);
                        dep = dep1;
                        break;
                    case 1:
                        list.Add(role2);
                        list.Add(role3);
                        dep = dep2;
                        break;
                    default:
                        flgInt = 0;
                        list.Add(role1);
                        dep = dep1;
                        break;
                }
                flgInt++;

                context.Set<User>().Add(new User
                {
                    Name = "员工" + i,
                    IsLockedOut = false,
                    Password = "123456",
                    Department = dep,
                    Role = list,
                });
            }

            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder errors = new StringBuilder();
                IEnumerable<DbEntityValidationResult> validationResult = ex.EntityValidationErrors;
                foreach (DbEntityValidationResult result in validationResult)
                {
                    errors.Append(result.Entry + ":" + result.Entry + "\r\n");
                    ICollection<DbValidationError> validationError = result.ValidationErrors;
                    foreach (DbValidationError err in validationError)
                    {
                        errors.Append(err.PropertyName + ":" + err.ErrorMessage + "\r\n");
                    }
                }
                throw new Exception(errors.ToString());
            }
        }
    }
}
