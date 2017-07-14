using Autofac;
using Autofac.Core.Activators.Reflection;
using LCL.Domain.Repositories;
using LCL.Domain.Services;
using LCL.Infrastructure;
using LCL.Infrastructure.DependencyManagement;
using System.Reflection;

namespace LCL.Repositories.EntityFramework
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            //上下文注入
            builder.RegisterType<EntityFrameworkRepositoryContext>().As<IEntityFrameworkRepositoryContext>()
                .FindConstructorsWith(new DefaultConstructorFinder(type => type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)))
                .InstancePerLifetimeScope();
            //仓储模式注入
            builder.RegisterGeneric(typeof(EntityFrameworkRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(EntityFrameworkRepository<,>)).As(typeof(IRepository<,>)).InstancePerLifetimeScope();

            Logger.LogInfo(Order + " init plugin LCL.Repositories.EntityFramework");

        }

        public int Order
        {
            get { return 1; }
        }
    }
}
