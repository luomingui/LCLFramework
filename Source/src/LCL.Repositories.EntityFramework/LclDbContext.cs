using LCL.Domain.Entities;
using LCL.Domain.Repositories;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using EntityFramework.DynamicFilters;
using System.Data.Entity.Core.Objects;
using System.ComponentModel.DataAnnotations.Schema;
using LCL.Reflection;
using LCL.Domain.Uow;
using LCL.Domain.Services;
using System.Data.Entity.Validation;
using System.Threading;

namespace LCL.Repositories.EntityFramework
{
    public class LclDbContext : DbContext, IEntityFrameworkRepositoryContext
    {
        #region Ctor
        private readonly object sync = new object();
        public LclDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            InitializeDbContext();
        }
        public IGuidGenerator GuidGenerator { get; set; }
        private void InitializeDbContext()
        {
            GuidGenerator = SequentialGuidGenerator.Instance;
        }
        #endregion

        public Type[] EntityTypeConfigurations { get; set; }
        public bool SuppressAutoSetTenantId { get; set; }

        #region Utilities

        protected virtual void CheckAndSetMustHaveTenantIdProperty(object entityAsObj)
        {
            if (SuppressAutoSetTenantId)
            {
                return;
            }

            //Only set IMustHaveTenant entities
            if (!(entityAsObj is IMustHaveTenant))
            {
                return;
            }

            var entity = entityAsObj.As<IMustHaveTenant>();

            //Don't set if it's already set
            if (entity.TenantId != 0)
            {
                return;
            }
        }

        protected virtual void CheckAndSetId(object entityAsObj)
        {
            //Set GUID Ids
            var entity = entityAsObj as IEntity<Guid>;
            if (entity != null && entity.ID == Guid.Empty)
            {
                var entityType = ObjectContext.GetObjectType(entityAsObj.GetType());
                var idProperty = entityType.GetProperty("ID");
                var dbGeneratedAttr = ReflectionUtil.GetSingleAttributeOrDefault<DatabaseGeneratedAttribute>(idProperty);
                if (dbGeneratedAttr == null || dbGeneratedAttr.DatabaseGeneratedOption == DatabaseGeneratedOption.None)
                {
                    entity.ID = GuidGenerator.Create();
                }
            }
        }
        public virtual void Initialize()
        {
            Database.Initialize(false);
            this.SetFilterScopedParameterValue(LclDataFilters.MustHaveTenant, LclDataFilters.Parameters.TenantId);
            this.SetFilterScopedParameterValue(LclDataFilters.MayHaveTenant, LclDataFilters.Parameters.TenantId);
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            LoadAssemblyEntityTypeConfiguration(modelBuilder);
            LoadAppAssemblyEntityTypeConfiguration(modelBuilder);
            LModelCreating(modelBuilder);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Filter(LclDataFilters.SoftDelete, (ISoftDelete d) => d.IsDeleted, false);
            modelBuilder.Filter(LclDataFilters.MustHaveTenant, (IMustHaveTenant t, int tenantId) => t.TenantId == tenantId, 0); //While "(int?)t.TenantId == null" seems wrong, it's needed. See https://github.com/jcachat/EntityFramework.DynamicFilters/issues/62#issuecomment-208198058
            modelBuilder.Filter(LclDataFilters.MayHaveTenant, (IMayHaveTenant t, int? tenantId) => t.TenantId == tenantId, 0);

        }
        public virtual void LModelCreating(DbModelBuilder modelBuilder) { }
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
            this.Entry(obj).State = EntityState.Added;
            CheckAndSetId(obj);
            CheckAndSetMustHaveTenantIdProperty(obj);
            Committed = false;
        }

        public void RegisterModified(object obj)
        {
            this.Entry(obj).State = EntityState.Modified;
            Committed = false;
        }
        public void RegisterDeleted(object obj)
        {
            var entry = this.Entry(obj);
            var softDeleteEntry = entry.Cast<ISoftDelete>();
            if (softDeleteEntry != null)
            {
                softDeleteEntry.Reload();
                softDeleteEntry.State = EntityState.Modified;
                softDeleteEntry.Entity.IsDeleted = true;
            }
            else
            {
                this.Entry(obj).State = EntityState.Deleted;
            }
            Committed = false;
        }
        public bool DistributedTransactionSupported
        {
            get { return true; }
        }
        private readonly ThreadLocal<bool> localCommitted = new ThreadLocal<bool>(() => true);
        public bool Committed
        {
            get { return localCommitted.Value; }
            protected set { localCommitted.Value = value; }
        }
        public void Commit()
        {
            try
            {
                if (!Committed)
                {
                    lock (sync)
                    {
                        base.SaveChanges();
                    }
                    Committed = true;
                }
            }
            catch (DbEntityValidationException ex)
            {
                LogDbEntityValidationException(ex);
                throw;
            }
        }
        public void Rollback()
        {
            Committed = false;
        }

        protected virtual void LogDbEntityValidationException(DbEntityValidationException exception)
        {
            Logger.LogDebug("There are some validation errors while saving changes in EntityFramework:");
            foreach (var ve in exception.EntityValidationErrors.SelectMany(eve => eve.ValidationErrors))
            {
                Logger.LogDebug(" - " + ve.PropertyName + ": " + ve.ErrorMessage);
            }
        }
        #endregion

        public DbContext Context
        {
            get { return this; }
        }


        /// <summary>
        /// 解决provider不能自动加载的问题
        /// （不需要调用，放在这里即可）
        /// </summary>
        private static void FixProvidersNotAutoLoadProblem()
        {
            //var _ = typeof(System.Data.SQLite.EF6.SQLiteProviderFactory);
            var __ = typeof(System.Data.Entity.SqlServer.SqlProviderServices);
            var ___ = typeof(System.Data.Entity.SqlServerCompact.SqlCeProviderServices);
        }
    }
}
