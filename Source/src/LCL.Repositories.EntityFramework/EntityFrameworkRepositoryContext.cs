using System.Linq;
using LCL.Domain.Services;
using LCL.Domain.Repositories;
using System.Data.Entity.Validation;
using System.Data.Entity;

namespace LCL.Repositories.EntityFramework
{
    public class EntityFrameworkRepositoryContext : RepositoryContext, IEntityFrameworkRepositoryContext
    {
        #region Private Fields
        private readonly DbContext efContext;
        private readonly object sync = new object();
        #endregion

        #region Ctor
        public EntityFrameworkRepositoryContext(DbContext efContext)
        {
            this.efContext = efContext;
        }
        #endregion

        #region Protected Methods
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                efContext.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion

        #region IEntityFrameworkRepositoryContext Members
        public DbContext Context
        {
            get { return this.efContext; }
        }

        #endregion

        #region IRepositoryContext Members
        public override void RegisterNew(object obj)
        {
            this.efContext.Entry(obj).State = EntityState.Added;
            Committed = false;
        }
        public override void RegisterModified(object obj)
        {
            this.efContext.Entry(obj).State = EntityState.Modified;
            Committed = false;
        }
        public override void RegisterDeleted(object obj)
        {
            this.efContext.Entry(obj).State = EntityState.Deleted;
            Committed = false;
        }
        #endregion

        #region IUnitOfWork Members
        public override bool DistributedTransactionSupported
        {
            get { return true; }
        }
        public override void Commit()
        {
            try
            {
                if (!Committed)
                {
                    lock (sync)
                    {
                        this.efContext.SaveChanges();
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
        public override void Rollback()
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
    }
}
