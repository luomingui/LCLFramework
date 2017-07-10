using Autofac;
using LCL.ObjectMapping;
using LCL.Domain.Services;
using LCL.Infrastructure;
using LCL.Infrastructure.DependencyManagement;
using System;
using AutoMapper;
using System.Reflection;
using System.ComponentModel;

namespace LCL.AutoMapper
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.RegisterType<AutoMapperObjectMapper>().As<ObjectMapping.IObjectMapper>().InstancePerLifetimeScope();

            CreateMappings(builder, typeFinder);


            Logger.LogInfo(Order + " init plugin LCL.Repositories.EntityFramework");
        }

        private static readonly object SyncObj = new object();

        private void CreateMappings(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            lock (SyncObj)
            {
                Action<IMapperConfigurationExpression> configurer = configuration =>
                {
                    FindAndAutoMapTypes(configuration, typeFinder);
                };
                Mapper.Initialize(configurer);
                builder.RegisterType<Mapper>().As<IMapper>().InstancePerLifetimeScope();

            }
        }
        private void FindAndAutoMapTypes(IMapperConfigurationExpression configuration, ITypeFinder typeFinder)
        {
            var types = typeFinder.Find(type =>
            {
                var typeInfo = type.GetTypeInfo();
                return typeInfo.IsDefined(typeof(AutoMapAttribute)) ||
                       typeInfo.IsDefined(typeof(AutoMapFromAttribute)) ||
                       typeInfo.IsDefined(typeof(AutoMapToAttribute));
            }
            );

            Logger.LogDebug(string.Format("Found {0} classes define auto mapping attributes", types.Length));

            foreach (var type in types)
            {
                Logger.LogDebug(type.FullName);
                configuration.CreateAutoAttributeMaps(type);
            }
        }
        public int Order
        {
            get { return -1; }
        }
    }
};
