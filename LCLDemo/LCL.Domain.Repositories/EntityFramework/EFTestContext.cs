using System;
using System.Reflection;
using System.Data.Entity;
using LCL.Repositories.EntityFramework;
using LCL.Domain.Services;
using LCL.Domain.Model;
using LCL.Infrastructure.Config;

namespace LCL.Domain.Repositories.EntityFramework
{
    public class EFTestContext : LclDbContext
    {
        #region Ctor
        public EFTestContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            EntityTypeConfigurations = Assembly.GetExecutingAssembly().GetTypes();
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets a set of <c>User</c>s.
        /// </summary>
        public DbSet<User> Users
        {
            get { return Set<User>(); }
        }

        /// <summary>
        /// Gets a set of <c>SalesOrder</c>s.
        /// </summary>
        public DbSet<SalesOrder> SalesOrders
        {
            get { return Set<SalesOrder>(); }
        }

        /// <summary>
        /// Gets a set of <c>Laptop</c>s.
        /// </summary>
        public DbSet<Product> Products
        {
            get { return Set<Product>(); }
        }

        public DbSet<Category> Categories
        {
            get { return Set<Category>(); }
        }

        public DbSet<Categorization> Categorizations
        {
            get { return Set<Categorization>(); }
        }
        /// <summary>
        /// Gets a set of <c>ShoppingCart</c>s.
        /// </summary>
        public DbSet<ShoppingCart> ShoppingCarts
        {
            get { return Set<ShoppingCart>(); }
        }

        public DbSet<UserRole> UserRoles
        {
            get { return Set<UserRole>(); }
        }

        public DbSet<Role> Roles
        {
            get { return Set<Role>(); }
        }
        #endregion

        #region Protected Methods  
        public override void LModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Types().Configure(entity => entity.ToTable("uc_" + entity.ClrType.Name));
        }
        #endregion
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
                Database.SetInitializer<EFTestContext>(new DropCreateDatabaseIfModelChanges<EFTestContext>());
                Database.SetInitializer(new SampleData());
                using (var db = new EFTestContext(AppConfig.DbSetting.Name))
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
    internal class SampleData : CreateDatabaseIfNotExists<EFTestContext>
    {
        protected override void Seed(EFTestContext context)
        {

            //context.Set<EFCustomer>().Add(new EFCustomer
            //{
            //    UserName = "admin",
            //    Password = "123456",
            //    Sequence = 0,
            //    Email = "123@163.com",
            //    Address = new EFAddress { Country = "国家", City = "城市", Street = "街道", State = "状态", Zip = "邮政编码" }
            //});
            //context.SaveChanges();
        }
    }
}
