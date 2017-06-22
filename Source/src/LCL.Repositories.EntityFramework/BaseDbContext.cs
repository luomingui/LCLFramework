using LCL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.Validation;
using System.Data.Objects;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using LCL.Infrastructure;
using LCL.Domain.Entities;
using LCL.Domain.Repositories;
using LCL.LData;
//using EntityFramework.DynamicFilters;
namespace LCL.Repositories.EntityFramework
{
    public class BaseDbContext : DbContext, IEntityFrameworkRepositoryContext, IRepositoryContext
    {
        #region Ctor
        private readonly object sync = new object();
        //public BaseDbContext()
        //    : base("LCL")
        //{
        //    var efDataProviderManager = new EfDataProviderManager(DbSetting.FindOrCreate("LCL"));
        //    var dataProvider = efDataProviderManager.LoadDataProvider();
        //    dataProvider.InitConnectionFactory();
        //}
        public BaseDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
          
        }
        private void InitConnectionFactory() { 
          
        }
        #endregion

        public Type[] EntityTypeConfigurations { get; set; }

        #region Utilities
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            LoadAssemblyEntityTypeConfiguration(modelBuilder);
            LoadAppAssemblyEntityTypeConfiguration(modelBuilder);
            LModelCreating(modelBuilder);
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Filter("PersonFilter", (IHasPerson entity, int personId) => entity.PersonId == personId, 0);
        }
        public virtual void LModelCreating(DbModelBuilder modelBuilder){}
        private void LoadAppAssemblyEntityTypeConfiguration(DbModelBuilder modelBuilder)
        {
            //dynamically load all configuration
            if (EntityTypeConfigurations == null)
                EntityTypeConfigurations = Assembly.GetExecutingAssembly().GetTypes();

            var typesToRegister = EntityTypeConfigurations.Where(type => !String.IsNullOrEmpty(type.Namespace))
              .Where(type => type.BaseType != null && type.BaseType.IsGenericType &&
                  type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }
        }
        private void LoadAssemblyEntityTypeConfiguration(DbModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes().Where(type => !String.IsNullOrEmpty(type.Namespace))
              .Where(type => type.BaseType != null && type.BaseType.IsGenericType &&
                  type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }
        }
        #endregion
        protected virtual TAggregateRoot AttachEntityToContext<TAggregateRoot>(TAggregateRoot entity)
            where TAggregateRoot : class, IAggregateRoot, new()
        {
            var alreadyAttached = Set<TAggregateRoot>().Local.FirstOrDefault(x => x.ID == entity.ID);
            if (alreadyAttached == null)
            {
                Set<TAggregateRoot>().Attach(entity);
                return entity;
            }
            else
            {
                return alreadyAttached;
            }
        }

        #region IRepositoryContext Methods
        public Guid ID
        {
            get { return Guid.NewGuid(); }
        }

        public void RegisterNew(object obj)
        {
            this.Entry(obj).State = System.Data.Entity.EntityState.Added;
        }

        public void RegisterModified(object obj)
        {
            this.Entry(obj).State = System.Data.Entity.EntityState.Modified;
        }

        public void RegisterDeleted(object obj)
        {
            this.Entry(obj).State = System.Data.Entity.EntityState.Deleted;
        }

        public bool DistributedTransactionSupported
        {
            get { return true; }
        }

        public bool Committed
        {
            get { throw new NotImplementedException(); }
        }

        public void Commit()
        {
            lock (sync)
            {
                this.SaveChanges();
            }
        }

        public void Rollback()
        {
            throw new NotImplementedException(); 
        }
        #endregion

        public BaseDbContext Context
        {
            get { return this; }
        }
    }
}
