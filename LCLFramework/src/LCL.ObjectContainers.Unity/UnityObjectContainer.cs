using System;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.Unity;
using System.Configuration;
using Microsoft.Practices.Unity.Configuration;

namespace LCL.ObjectContainers.Unity
{
    /// <summary>
    /// https://msdn.microsoft.com/en-us/library/microsoft.practices.unity.iunitycontainer_members(v=pandp.30).aspx
    /// </summary>
    public class UnityObjectContainer : IObjectContainer
    {
        private IUnityContainer _unityContainer;
        public UnityObjectContainer()
        {
            _unityContainer = new Microsoft.Practices.Unity.UnityContainer();
        }
        public IUnityContainer UnityContainer
        {
            get { return _unityContainer; }
        }
        public T GetWrappedContainer<T>()
        {
            if (typeof(T).Equals(typeof(UnityContainer)))
                return (T)this._unityContainer;

            throw new Exception(string.Format("The wrapped container type provided by the current object container should be '{0}'.", typeof(UnityObjectContainer)));
        }
        public void RegisterType(Type type)
        {
            var life = ParseLife(type);

            if (!_unityContainer.IsRegistered(type))
            {
                if (life == LifeStyle.Singleton)
                {
                    _unityContainer.RegisterType(type, new ContainerControlledLifetimeManager());
                }
                else
                {
                    _unityContainer.RegisterType(type);
                }
            }

            foreach (var interfaceType in type.GetInterfaces())
            {
                if (!_unityContainer.IsRegistered(interfaceType))
                {
                    if (life == LifeStyle.Singleton)
                    {
                        _unityContainer.RegisterType(interfaceType, type, new ContainerControlledLifetimeManager());
                    }
                    else
                    {
                        _unityContainer.RegisterType(interfaceType, type);
                    }
                }
            }
        }
        public void RegisterType(Type type, string key)
        {
            var life = ParseLife(type);
            if (!IsRegistered(type, key))
            {
                if (life == LifeStyle.Singleton)
                {
                    _unityContainer.RegisterType(type, new ContainerControlledLifetimeManager());
                }
                else
                {
                    _unityContainer.RegisterType(type);
                }
            }
            foreach (var interfaceType in type.GetInterfaces())
            {
                if (!IsRegistered(interfaceType, key))
                {
                    if (life == LifeStyle.Singleton)
                    {
                        _unityContainer.RegisterType(interfaceType, type, new ContainerControlledLifetimeManager());
                    }
                    else
                    {
                        _unityContainer.RegisterType(interfaceType, type);
                    }
                }
            }
        }
        public void RegisterTypes(Func<Type, bool> typePredicate, params Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetExportedTypes().Where(x => typePredicate(x)))
                {
                    RegisterType(type);
                }
            }
        }
        public void Register<TService, TImpl>(LifeStyle life = LifeStyle.Singleton)
            where TService : class
            where TImpl : class, TService
        {
            if (life == LifeStyle.Singleton)
            {
                _unityContainer.RegisterType<TService, TImpl>(new ContainerControlledLifetimeManager());
            }
            else
            {
                _unityContainer.RegisterType<TService, TImpl>();
            }
        }
        public void Register<TService, TImpl>(string key, LifeStyle life = LifeStyle.Singleton)
            where TService : class
            where TImpl : class, TService
        {
            if (life == LifeStyle.Singleton)
            {
                _unityContainer.RegisterType<TService, TImpl>(key, new ContainerControlledLifetimeManager());
            }
            else
            {
                _unityContainer.RegisterType<TService, TImpl>(key);
            }
        }
        public void RegisterDefault<TService, TImpl>(LifeStyle life = LifeStyle.Singleton)
            where TService : class
            where TImpl : class, TService
        {
            if (life == LifeStyle.Singleton)
            {
                _unityContainer.RegisterType<TService, TImpl>(new ContainerControlledLifetimeManager());
            }
            else
            {
                _unityContainer.RegisterType<TService, TImpl>();
            }
        }
        public void Register<T>(T instance, LifeStyle life = LifeStyle.Singleton) where T : class
        {
            if (life == LifeStyle.Singleton)
            {
                _unityContainer.RegisterInstance<T>(instance, new ContainerControlledLifetimeManager());
            }
            else
            {
                _unityContainer.RegisterInstance<T>(instance);
            }
        }
        public void Register<T>(T instance, string key, LifeStyle life = LifeStyle.Singleton) where T : class
        {
            if (life == LifeStyle.Singleton)
            {
                _unityContainer.RegisterInstance<T>(key, instance, new ContainerControlledLifetimeManager());
            }
            else
            {
                _unityContainer.RegisterInstance<T>(key, instance);
            }
        }
        public bool IsRegistered(Type type)
        {
            return _unityContainer.IsRegistered(type);
        }
        public bool IsRegistered(Type type, string key)
        {
            return _unityContainer.IsRegistered(type, key);
        }
        public T Resolve<T>() where T : class
        {
            T resolved;
            try
            {
                resolved = _unityContainer.Resolve<T>();
            }
            catch (Exception)
            {
                resolved = default(T);
            }
            return resolved;
        }
        public T Resolve<T>(string key) where T : class
        {
            return _unityContainer.Resolve<T>(key);
        }
        public object Resolve(Type type)
        {
            return _unityContainer.Resolve(type);
        }
        public object Resolve(string key, Type type)
        {
            return _unityContainer.Resolve(type, key);
        }
        public T[] ResolveAll<T>() where T : class
        {
            return _unityContainer.ResolveAll<T>().ToArray();
        }
        private static LifeStyle ParseLife(Type type)
        {
            var componentAttributes = type.GetCustomAttributes(typeof(ComponentAttribute), false);
            return componentAttributes.Count() <= 0 ? LifeStyle.Transient : (componentAttributes[0] as ComponentAttribute).LifeStyle;
        }

        public void RegisterType(Type from, Type to)
        {
            //RegisterType(typeof(IRepository<>), typeof(Repository<>));
            _unityContainer.RegisterType(from, to, new ContainerControlledLifetimeManager());
        }


        #region 从config文件中读取配置信息
        public void GetUnityContainerList()
        {
            foreach (var item in _unityContainer.Registrations)
            {
                string msg = string.Format("ioc:Name[{0}]RegisteredType[{1}]LifetimeManager[{2}]MappedToType[{3}]"
                    , item.Name, item.RegisteredType.ToString(), item.LifetimeManager.ToString(), item.MappedToType.ToString());

            }
        }
        /// <summary>
        /// 从config文件中读取配置信息
        /// http://www.tuicool.com/articles/yiUzQj
        /// </summary>
        /// <returns></returns>
        public IUnityContainer LoadUnityConfig()
        {
            ////根据文件名获取指定config文件
            string filePath = AppDomain.CurrentDomain.BaseDirectory + @"Configs\Unity.config";
            var fileMap = new ExeConfigurationFileMap { ExeConfigFilename = filePath };
            //从config文件中读取配置信息
            Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            var unitySection = (UnityConfigurationSection)configuration.GetSection("unity");
            var container = new UnityContainer();
            foreach (var item in unitySection.Containers)
            {
                container.LoadConfiguration(unitySection, item.Name);
            }
            return container;
        }
        #endregion
    }
}
