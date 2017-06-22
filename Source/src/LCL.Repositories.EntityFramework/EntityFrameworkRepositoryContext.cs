
using LCL.Domain.Repositories;
using System;
using System.Data.Entity;

namespace LCL.Repositories.EntityFramework
{
    public class EntityFrameworkRepositoryContext : RepositoryContext, IEntityFrameworkRepositoryContext
    {
        #region Private Fields
        private readonly BaseDbContext efContext;
        private readonly object sync = new object();
        #endregion

        #region Ctor
        public EntityFrameworkRepositoryContext(BaseDbContext efContext)
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
        public BaseDbContext Context
        {
            get { return this.efContext; }
        }

        #endregion

        #region IRepositoryContext Members
        public override void RegisterNew(object obj)
        {
            this.efContext.Entry(obj).State = System.Data.Entity.EntityState.Added;
            Committed = false;
        }
        public override void RegisterModified(object obj)
        {
            this.efContext.Entry(obj).State = System.Data.Entity.EntityState.Modified;
            Committed = false;
        }
        public override void RegisterDeleted(object obj)
        {
            this.efContext.Entry(obj).State = System.Data.Entity.EntityState.Deleted;
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
            if (!Committed)
            {
                lock (sync)
                {
                    efContext.SaveChanges();
                }
                Committed = true;
            }
        }
        public override void Rollback()
        {
            Committed = false;
        }

        #endregion
    }
}
