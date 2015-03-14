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
        public static IDomainRepository GetDomainRepository()
        {
            return ServiceLocator.Instance.Resolve<IDomainRepository>();
        }
        public static IRepository<TAggregateRoot> FindRepo<TAggregateRoot>() where TAggregateRoot : class, IAggregateRoot
        {
            return ServiceLocator.Instance.Resolve<IRepository<TAggregateRoot>>();
        }
        public static TRepository Concrete<TRepository>() where TRepository : class
        {
            return ServiceLocator.Instance.Resolve<TRepository>();
        }
        public static object Find(Type type)
        {
            return ServiceLocator.Instance.Resolve(type);
        }
        public static void Save<TAggregateRoot>(IRepository<TAggregateRoot> rep) where TAggregateRoot : class, IAggregateRoot
        {
            rep.Context.Commit();
        }
    }
}
