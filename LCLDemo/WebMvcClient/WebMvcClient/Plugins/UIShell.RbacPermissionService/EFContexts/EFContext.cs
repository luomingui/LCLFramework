using LCL;
using LCL.MvcExtensions;
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
            modelBuilder.Types().Configure(entity => entity.ToTable("Edms_" + entity.ClrType.Name));
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
            //单位信息
            var org = context.Set<Org>().Add(new Org
            {
                ID = Guid.NewGuid(),
                Name = "学苑传媒集团",
                HelperCode = "01",
                NameShort = "xueyuan",
                OrgType = OrgType.集团,
                Linkman = "陈元虎",
                Phone = "110",
                Fax = "120",
                IsValid = true,
                Describe = ""
            });
            var org1 = context.Set<Org>().Add(new Org
            {
                ID = Guid.NewGuid(),
                ParentId = org.ID,
                Name = "江西学苑传媒有限公司",
                HelperCode = "0101",
                NameShort = "xueyuan",
                OrgType = OrgType.公司,
                Linkman = "陈元虎",
                Phone = "110",
                Fax = "120",
                IsValid = true,
                Describe = ""
            });
            var org2 = context.Set<Org>().Add(new Org
            {
                ID = Guid.NewGuid(),
                ParentId = org1.ID,
                Name = "研发部",
                HelperCode = "010101",
                NameShort = "xueyuan",
                OrgType = OrgType.部门,
                Linkman = "陈元虎",
                Phone = "110",
                Fax = "120",
                IsValid = true,
                Describe = ""
            });
            var org3 = context.Set<Org>().Add(new Org
            {
                ID = Guid.NewGuid(),
                ParentId = org2.ID,
                Name = "工程师",
                HelperCode = "01010101",
                NameShort = "xueyuan",
                OrgType = OrgType.部门,
                Linkman = "陈元虎",
                Phone = "110",
                Fax = "120",
                IsValid = true,
                Describe = ""
            });

            //角色
            var role = context.Set<Role>().Add(new Role
            {
                ID = Guid.NewGuid(),
                Name = "系统管理员",
                IsValid = true,
                HelperCode = "01",
                Describe = ""
            });
            var user = new User
            {
                ID = Guid.NewGuid(),
                Name = "管理员",
                NameShort = "administrator",
                HelperCode = "admin",
                Password = "123456",
                Org = org3,
                Phone = "13026209315",
            };
            user.Role.Add(role);

            context.Set<User>().Add(user);
           
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
