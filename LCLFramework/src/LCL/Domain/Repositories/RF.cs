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
            var repo= ServiceLocator.Instance.Resolve<IRepository<TEntity>>();
            if (repo == null)
            {
                throw new Exception("IOC不存在此类型！请将此类型加入到IOC中！");
            }
            return repo;
        }
        public static TRepository Concrete<TRepository>() where TRepository : class
        {
            try
            {
                return ServiceLocator.Instance.Resolve<TRepository>();
            }
            catch (Exception e)
            {
                if (e.InnerException != null && e.InnerException.InnerException != null)
                {
                    Exception sqlEx = e.InnerException.InnerException as Exception;
                    throw sqlEx;
                }
                throw new Exception("IOC不存在此类型！请将此类型加入到IOC中！");
            }
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
