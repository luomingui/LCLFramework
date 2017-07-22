using Autofac;
using LCL.Domain.Services;
using LCL.Infrastructure;
using LCL.Infrastructure.DependencyManagement;

namespace LCL.Caching.Redis
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            //cache manager
            builder.RegisterType<LclRedisCache>().As<ICacheProvider>().Named<ICacheProvider>("lcl_cache_redis").SingleInstance();
            Logger.LogInfo(Order + " init plugin LCL.Caching.Redis");
        }

        public int Order
        {
            get { return -1; }
        }
    }
}
