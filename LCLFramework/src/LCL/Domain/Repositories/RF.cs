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
            var repo = ServiceLocator.Instance.Resolve<IRepository<TEntity>>();
            if (repo == null)
            {
                throw new Exception("IOC不存在" + typeof(IRepository<TEntity>).FullName + "此类型！请将此类型加入到IOC中！");
            }
            return repo;
        }
        public static TRepository Concrete<TRepository>() where TRepository : class
        {
            var repo = ServiceLocator.Instance.Resolve<TRepository>();
            if (repo == null)
            {
                throw new Exception("IOC不存在" + typeof(TRepository).FullName + "此类型！请将此类型加入到IOC中！");
            }
            return repo;
        }
        public static object Find(Type type)
        {
            var repo = ServiceLocator.Instance.Resolve(type);
            if (repo == null)
            {
                throw new Exception("IOC不存在" + type.FullName + "此类型！请将此类型加入到IOC中！");
            }
            return repo;
        }
        public static void Save<TEntity>(IRepository<TEntity> rep) where TEntity : class, IEntity
        {
            rep.Context.Commit();
        }
    }
}
