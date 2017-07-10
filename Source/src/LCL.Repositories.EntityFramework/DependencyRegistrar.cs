using Autofac;
using LCL.Domain.Repositories;
using LCL.Domain.Services;
using LCL.Infrastructure;
using LCL.Infrastructure.DependencyManagement;
using LCL.LData;

namespace LCL.Repositories.EntityFramework
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            //上下文注入
            builder.RegisterType<EntityFrameworkRepositoryContext>().As<IEntityFrameworkRepositoryContext>().Named("ef", typeof(IEntityFrameworkRepositoryContext)).InstancePerLifetimeScope();
            //仓储模式注入
            builder.RegisterGeneric(typeof(EntityFrameworkRepository<>)).As(typeof(IRepository<>)).Named("ef", typeof(IRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(EntityFrameworkRepository<,>)).As(typeof(IRepository<,>)).Named("ef", typeof(IRepository<,>)).InstancePerLifetimeScope();

            Logger.LogInfo(Order + " init plugin LCL.Repositories.EntityFramework");
        }

        public int Order
        {
            get { return -1; }
        }
    }
}
