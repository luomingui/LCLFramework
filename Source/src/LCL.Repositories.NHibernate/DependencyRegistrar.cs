using Autofac;
using LCL;
using LCL.Domain.Repositories;
using LCL.Domain.Services;
using LCL.Infrastructure;
using LCL.Infrastructure.DependencyManagement;
using LCL.LData;

namespace LCL.Repositories.NHibernate
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            //上下文注入
            builder.RegisterType<NHibernateContext>().As<INHibernateContext>().Named("NHibernate", typeof(INHibernateContext)).InstancePerLifetimeScope();
            //仓储模式注入
            builder.RegisterGeneric(typeof(NHibernateRepository<>)).As(typeof(IRepository<>)).Named("NHibernate", typeof(IRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(NHibernateRepository<,>)).As(typeof(IRepository<,>)).Named("NHibernate", typeof(IRepository<,>)).InstancePerLifetimeScope();

            Logger.LogInfo(Order + " init plugin LCL.Repositories.NHibernate");
        }

        public int Order
        {
            get { return -2; }
        }
    }
}
