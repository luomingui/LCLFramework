using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;

using LCL.Infrastructure.DependencyManagement;
using LCL.Domain.Services;
using LCL.Domain.Repositories;
using LCL.Plugins;
using LCL.Caching;
using LCL.Bus;
using LCL.Domain.Events;
using LCL.Config;
using LCL.Caching.Memory;
using LCL.Config.Startup;
using LCL.LData;
using System.Web.Mvc;
using Autofac.Integration.Mvc;
using Autofac.Core.Activators.Reflection;
using LCL.Domain.Entities;
using System.Reflection;

namespace LCL.Infrastructure
{
    /// <summary>
    ///Autofac  Engine
    /// </summary>
    public class LEngine : IEngine
    {
        #region Fields

        private ContainerManager _containerManager;

        #endregion

        #region Utilities
        protected virtual void RunStartupTasks()
        {
            var typeFinder = _containerManager.Resolve<ITypeFinder>();
            var startUpTaskTypes = typeFinder.FindClassesOfType<IStartupTask>();
            var startUpTasks = new List<IStartupTask>();
            foreach (var startUpTaskType in startUpTaskTypes)
                startUpTasks.Add((IStartupTask)Activator.CreateInstance(startUpTaskType));
            //sort
            startUpTasks = startUpTasks.AsQueryable().OrderBy(st => st.Order).ToList();
            foreach (var startUpTask in startUpTasks)
                startUpTask.Execute();
        }
        protected virtual void RegisterDependencies(LConfig config)
        {
            var builder = new ContainerBuilder();
            var container = builder.Build();

            //dependencies
            var typeFinder = new WebAppTypeFinder(config);
            builder = new ContainerBuilder();
            builder.RegisterInstance(config).As<LConfig>().SingleInstance();
            builder.RegisterInstance(this).As<IEngine>().SingleInstance();
            builder.RegisterInstance(typeFinder).As<ITypeFinder>().SingleInstance();

            // DDD
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Repository<,>)).As(typeof(IRepository<,>)).InstancePerLifetimeScope();
            builder.RegisterType<RepositoryContext>().As<IRepositoryContext>().InstancePerLifetimeScope();

            builder.RegisterType<EventBus>().As<IEventBus>().InstancePerLifetimeScope();
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().InstancePerLifetimeScope();
            builder.RegisterType<DomainEvent>().As<IDomainEvent>().InstancePerLifetimeScope();

            //plugins
            builder.RegisterType<PluginFinder>().As<IPluginFinder>().InstancePerLifetimeScope();

            //cache manager
            builder.RegisterType<MemoryCacheProvider>().As<ICacheProvider>().Named<ICacheProvider>("lcl_cache_static").SingleInstance();
            //config
            builder.RegisterType<DictionaryBasedConfig>().As<IDictionaryBasedConfig>().InstancePerLifetimeScope();
            builder.RegisterType<ModuleConfigurations>().As<IModuleConfigurations>().InstancePerLifetimeScope();
            builder.RegisterType<LclStartupConfiguration>().As<ILclStartupConfiguration>().InstancePerLifetimeScope();

            builder.RegisterType<LocalizationService>().As<ILocalizationService>().SingleInstance();
            builder.RegisterType<SettingService>().As<ISettingService>().SingleInstance();
            builder.RegisterType<SequentialGuidGenerator>().As<IGuidGenerator>().InstancePerLifetimeScope();

            builder.Update(container);

            //register dependencies provided by other assemblies
            builder = new ContainerBuilder();
            var drTypes = typeFinder.FindClassesOfType<IDependencyRegistrar>();
            var drInstances = new List<IDependencyRegistrar>();
            foreach (var drType in drTypes)
                drInstances.Add((IDependencyRegistrar)Activator.CreateInstance(drType));
            //sort
            drInstances = drInstances.AsQueryable().OrderBy(t => t.Order).ToList();
            foreach (var dependencyRegistrar in drInstances)
                dependencyRegistrar.Register(builder, typeFinder);
            builder.Update(container);

            this._containerManager = new ContainerManager(container);

            //设置依赖项解析器
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        #endregion

        #region Methods
        public void Initialize(LConfig config)
        {
            Logger.LogInfo("LCL Initialize components and plugins in the nop environment. ");
            //register dependencies
            RegisterDependencies(config);

            //startup tasks
            if (!config.IgnoreStartupTasks)
            {
                RunStartupTasks();
            }

        }
        public T Resolve<T>() where T : class
        {
            return ContainerManager.Resolve<T>();
        }
        public object Resolve(Type type)
        {
            return ContainerManager.Resolve(type);
        }
        public T[] ResolveAll<T>()
        {
            return ContainerManager.ResolveAll<T>();
        }

        #endregion

        #region Properties
        public ContainerManager ContainerManager
        {
            get { return _containerManager; }
        }

        #endregion
    }
}
