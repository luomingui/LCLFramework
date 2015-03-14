using LCL.ObjectContainers.TinyIoC;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LCL
{
    [DebuggerDisplay("ObjectContainer = {_container}")]
    public class ServiceLocator
    {
        private readonly IObjectContainer _container = LEnvironment.AppObjectContainer;
        public static readonly ServiceLocator Instance = new ServiceLocator();
        private ServiceLocator()
        {

        }
        /// <summary>
        /// 注册一个给定的类型及其所有实现的接口
        /// </summary>
        /// <param name="type"></param>
        public void RegisterType(Type type)
        {
            _container.RegisterType(type);
        }
        /// <summary>
        /// 注册一个给定的类型及其所有实现的接口
        /// </summary>
        /// <param name="type"></param>
        /// <param name="key"></param>
        public void RegisterType(Type type, string key)
        {
            _container.RegisterType(type, key);
        }
        public void RegisterType(Type type, Type Impl)
        {
            //_container.RegisterType()
        }
        /// <summary>
        /// 注册给定程序集中符合条件的所有类型
        /// </summary>
        /// <param name="typePredicate"></param>
        /// <param name="assemblies"></param>
        public void RegisterTypes(Func<Type, bool> typePredicate, params Assembly[] assemblies)
        {
            _container.RegisterTypes(typePredicate, assemblies);
        }
        /// <summary>
        /// 注册给定接口的实现
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImpl"></typeparam>
        /// <param name="life"></param>
        public void Register<TService, TImpl>(LifeStyle life = LifeStyle.Singleton)
            where TService : class
            where TImpl : class, TService
        {
            _container.Register<TService, TImpl>(life);
        }
        /// <summary>
        /// 注册给定接口的实现
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImpl"></typeparam>
        /// <param name="key"></param>
        /// <param name="life"></param>
        public void Register<TService, TImpl>(string key, LifeStyle life = LifeStyle.Singleton)
            where TService : class
            where TImpl : class, TService
        {
            _container.Register<TService, TImpl>(key, life);
        }
        /// <summary>
        /// 注册给定接口的默认实现
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImpl"></typeparam>
        /// <param name="life"></param>
        public void RegisterDefault<TService, TImpl>(LifeStyle life = LifeStyle.Singleton)
            where TService : class
            where TImpl : class, TService
        {
            _container.RegisterDefault<TService, TImpl>(life);
        }
        /// <summary>
        /// 注册给定类型的实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="life"></param>
        public void Register<T>(T instance, LifeStyle life = LifeStyle.Singleton) where T : class
        {
            _container.Register<T>(instance, life);
        }
        /// <summary>
        /// 注册给定类型的实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="key"></param>
        /// <param name="life"></param>
        public void Register<T>(T instance, string key, LifeStyle life = LifeStyle.Singleton) where T : class
        {
            _container.Register<T>(instance, key, life);
        }
        /// <summary>
        /// 判断给定的类型是否已经注册
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool IsRegistered(Type type)
        {
            return _container.IsRegistered(type);
        }
        /// <summary>
        /// 判断给定的类型是否已经注册
        /// </summary>
        /// <param name="type"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsRegistered(Type type, string key)
        {
            return _container.IsRegistered(type, key);
        }
        /// <summary>
        /// 获取给定类型的一个实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Resolve<T>() where T : class
        {
            return _container.Resolve<T>();
        }
        /// <summary>
        /// 获取给定类型的一个实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Resolve<T>(string key) where T : class
        {
            return _container.Resolve<T>(key);
        }
        /// <summary>
        /// 获取给定类型的一个实例
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public object Resolve(Type type)
        {
            return _container.Resolve(type);
        }
        /// <summary>
        /// 获取给定类型的一个实例
        /// </summary>
        /// <param name="key"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public object Resolve(string key, Type type)
        {
            return _container.Resolve(key, type);
        }
    }
}
