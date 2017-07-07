using Autofac;
using LCL.ObjectMapping;
using LCL.Domain.Services;
using LCL.Infrastructure;
using LCL.Infrastructure.DependencyManagement;

namespace LCL.AutoMapper
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {

            builder.RegisterType<AutoMapperObjectMapper>().As<IObjectMapper>().InstancePerLifetimeScope();

            Logger.LogInfo(Order + " init plugin LCL.Repositories.EntityFramework");
        }

        public int Order
        {
            get { return -1; }
        }
    }
};
