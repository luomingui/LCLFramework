using LCL.Data;
using LCL.DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCL.Repositories
{
    public static class RF
    {
        public static IRepository<TEntity> FindRepo<TEntity>() where TEntity : class, IEntity
        {
            return ServiceLocator.Instance.Resolve<IRepository<TEntity>>();
        }
        public static TRepository Concrete<TRepository>() where TRepository : class
        {
            return ServiceLocator.Instance.Resolve<TRepository>();
        }
        public static object Find(Type type)
        {
            return ServiceLocator.Instance.Resolve(type);
        }
        public static void Save<TEntity>(IRepository<TEntity> rep) where TEntity : class, IEntity
        {
            rep.Context.Commit();
        }
    }
}
