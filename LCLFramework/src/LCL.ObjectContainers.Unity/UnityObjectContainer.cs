using System;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.Unity;

namespace LCL.ObjectContainers.Unity
{
    public class UnityObjectContainer : IObjectContainer
    {
        private IUnityContainer _unityContainer;
        public UnityObjectContainer()
        {
            _unityContainer = new UnityContainer();
        }
        public UnityObjectContainer(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
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
            _unityContainer.RegisterType(from, to);
        }


     
    }
}
