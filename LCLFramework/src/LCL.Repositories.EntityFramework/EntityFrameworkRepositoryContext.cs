using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;

namespace LCL.Repositories.EntityFramework
{
    /// <summary>
    /// Represents the Entity Framework repository context.
    /// </summary>
    public class EntityFrameworkRepositoryContext : RepositoryContext, IEntityFrameworkRepositoryContext
    {
        #region Private Fields
        private readonly DbContext efContext;
        private readonly object sync = new object();
        #endregion

        #region Ctor
        /// <summary>
        /// Initializes a new instance of <c>EntityFrameworkRepositoryContext</c> class.
        /// </summary>
        /// <param name="efContext">The <see cref="DbContext"/> object that is used when initializing the <c>EntityFrameworkRepositoryContext</c> class.</param>
        public EntityFrameworkRepositoryContext(DbContext efContext)
        {
            this.efContext = efContext;
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <param name="disposing">A <see cref="System.Boolean"/> value which indicates whether
        /// the object should be disposed explicitly.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                efContext.Dispose();
            }
            base.Dispose(disposing);
        }
        protected Boolean RemoveHoldingEntityInContext<TAggregateRoot>(TAggregateRoot entity) where TAggregateRoot : class
        {//用于监测Context中的Entity是否存在，如果存在，将其Detach，防止出现问题。
            try
            {
                var objContext = ((IObjectContextAdapter)this.efContext).ObjectContext;
                var objSet = objContext.CreateObjectSet<TAggregateRoot>();
                var entityKey = objContext.CreateEntityKey(objSet.EntitySet.Name, entity);
                Object foundEntity;
                var exists = objContext.TryGetObjectByKey(entityKey, out foundEntity);
                if (exists)
                {
                    objContext.Detach(foundEntity);
                }
                return (exists);
            }
            catch (Exception ex)
            {
                Logger.LogError("改变实体状态出错：", ex);
                return false;
            }
        }
        #endregion

        #region IEntityFrameworkRepositoryContext Members
        /// <summary>
        /// Gets the <see cref="DbContext"/> instance handled by Entity Framework.
        /// </summary>
        public DbContext Context
        {
            get { return this.efContext; }
        }

        #endregion

        #region IRepositoryContext Members
        /// <summary>
        /// Registers a new object to the repository context.
        /// </summary>
        /// <param name="obj">The object to be registered.</param>
        public override void RegisterNew<TAggregateRoot>(TAggregateRoot obj)
        {
            this.efContext.Entry(obj).State = System.Data.Entity.EntityState.Added;
            Committed = false;
        }
        /// <summary>
        /// Registers a modified object to the repository context.
        /// //http://www.cnblogs.com/guomingfeng/p/mvc-ef-update.html
        /// </summary>
        /// <param name="obj">The object to be registered.</param>
        public override void RegisterModified<TAggregateRoot>(TAggregateRoot obj)
        {
            Committed = false;
            try
            {
                RemoveHoldingEntityInContext<TAggregateRoot>(obj);
                if (this.efContext.Entry(obj).State == EntityState.Detached)
                {
                    var updated = this.efContext.Set<TAggregateRoot>().Attach(obj);
                }
                this.efContext.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                //return updated;
            }
            catch (DbEntityValidationException dbex)
            {
                var msg = string.Empty;
                foreach (var validationErrors in dbex.EntityValidationErrors)
                    foreach (var validateionError in validationErrors.ValidationErrors)
                        msg += string.Format("属性:{0} 错误:{1}", validateionError.PropertyName, validateionError.ErrorMessage);
                var fail = new Exception(msg, dbex);
                throw fail;
            }
        }
        /// <summary>
        /// Registers a deleted object to the repository context.
        /// </summary>
        /// <param name="obj">The object to be registered.</param>
        public override void RegisterDeleted<TAggregateRoot>(TAggregateRoot obj)
        {
            Committed = false;
            try
            {
                obj.IsDelete = true;
                RemoveHoldingEntityInContext<TAggregateRoot>(obj);
                if (this.efContext.Entry(obj).State == EntityState.Detached)
                {
                    var updated = this.efContext.Set<TAggregateRoot>().Attach(obj);
                }
                this.efContext.Entry(obj).State = System.Data.Entity.EntityState.Modified;
            }
            catch (DbEntityValidationException dbex)
            {
                var msg = string.Empty;
                foreach (var validationErrors in dbex.EntityValidationErrors)
                    foreach (var validateionError in validationErrors.ValidationErrors)
                        msg += string.Format("属性:{0} 错误:{1}", validateionError.PropertyName, validateionError.ErrorMessage);
                var fail = new Exception(msg, dbex);
                throw fail;
            }
        }
        #endregion

        #region IUnitOfWork Members
        /// <summary>
        /// Gets a <see cref="System.Boolean"/> value which indicates
        /// whether the Unit of Work could support Microsoft Distributed
        /// Transaction Coordinator (MS-DTC).
        /// </summary>
        public override bool DTCompatible
        {
            get { return true; }
        }
        /// <summary>
        /// Commits the transaction.
        /// </summary>
        public override void Commit()
        {
            if (!Committed)
            {
                lock (sync)
                {
                    try
                    {

                        if (efContext.Database.Connection.State == System.Data.ConnectionState.Closed)
                        {
                            efContext.Database.Connection.Open();
                        }

                        efContext.SaveChanges();
                        Committed = true;
                    }
                    catch (DbUpdateException e)
                    {
                        if (e.InnerException != null && e.InnerException.InnerException is SqlException)
                        {
                            SqlException sqlEx = e.InnerException.InnerException as SqlException;
                            throw sqlEx;
                        }
                        throw;
                    }
                    catch (DbEntityValidationException dbex)
                    {
                        var msg = string.Empty;
                        foreach (var validationErrors in dbex.EntityValidationErrors)
                            foreach (var validateionError in validationErrors.ValidationErrors)
                                msg += string.Format("属性:{0} 错误:{1}", validateionError.PropertyName, validateionError.ErrorMessage);
                        var fail = new Exception(msg, dbex);
                        throw fail;
                    }
                    finally
                    {
                        if (efContext.Database.Connection.State == System.Data.ConnectionState.Open)
                        {
                            efContext.Database.Connection.Close();
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Rollback the transaction.
        /// </summary>
        public override void Rollback()
        {
            Committed = false;
        }

        #endregion
    }
}
