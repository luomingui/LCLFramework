using LCL;
using LCL.Repositories.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Text;

namespace UIShell.RbacPermissionService
{
    public partial class EFContext : RbacDbContext
    {
        public EFContext()
            : base("LCL_Rbac")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
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
    public static class DatabaseInitializer
    {
        /// <summary>
        /// 数据库初始化
        /// </summary>
        public static void Initialize()
        {
            try
            {
                Database.SetInitializer<EFContext>(new DropCreateDatabaseIfModelChanges<EFContext>());
                Database.SetInitializer(new SampleData());
                using (var db = new EFContext())
                {
                    db.Database.Initialize(false);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
    }
    /// <summary>
    /// 数据库初始化策略
    /// </summary>
    internal class SampleData : CreateDatabaseIfNotExists<EFContext>
    {
        protected override void Seed(EFContext context)
        {
            context.Set<TLog>().Add(
                new TLog
                {
                    ID = Guid.NewGuid(),
                    UpdateDate = DateTime.Now,
                    AddDate = DateTime.Now,
                    UserId = Guid.Empty,
                    Org_Id = Guid.Empty,
                    Content = "测试日志"
                }
                );

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
