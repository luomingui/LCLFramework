using System;
using System.Reflection;

namespace LCL
{
    /// <summary>
    /// 表示持久化事件时出现的并发异常
    /// </summary>
    public interface IObjectContainer
    {
        T GetWrappedContainer<T>();
        /// <summary>
        /// 注册一个给定的类型及其所有实现的接口
        /// </summary>
        /// <param name="type"></param>
        void RegisterType(Type type);
        /// <summary>
        /// 注册一个给定的类型及其所有实现的接口
        /// </summary>
        /// <param name="type"></param>
        /// <param name="key"></param>
        void RegisterType(Type type, string key);
        /// <summary>
        /// 注册给定程序集中符合条件的所有类型
        /// </summary>
        /// <param name="typePredicate"></param>
        /// <param name="assemblies"></param>
        void RegisterTypes(Func<Type, bool> typePredicate, params Assembly[] assemblies);
        /// <summary>
        /// 注册给定接口的实现
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImpl"></typeparam>
        /// <param name="life"></param>
        void Register<TService, TImpl>(LifeStyle life)
            where TService : class
            where TImpl : class, TService;
        /// <summary>
        /// 注册给定接口的实现
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImpl"></typeparam>
        /// <param name="key"></param>
        /// <param name="life"></param>
        void Register<TService, TImpl>(string key, LifeStyle life = LifeStyle.Singleton)
            where TService : class
            where TImpl : class, TService;
        /// <summary>
        /// 注册给定接口的默认实现
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImpl"></typeparam>
        /// <param name="life"></param>
        void RegisterDefault<TService, TImpl>(LifeStyle life)
            where TService : class
            where TImpl : class, TService;
        /// <summary>
        /// 注册给定类型的实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="life"></param>
        void Register<T>(T instance, LifeStyle life) where T : class;
        /// <summary>
        /// 注册给定类型的实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="key"></param>
        /// <param name="life"></param>
        void Register<T>(T instance, string key, LifeStyle life) where T : class;
        /// <summary>
        /// 判断给定的类型是否已经注册
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        bool IsRegistered(Type type);
        /// <summary>
        /// 判断给定的类型是否已经注册
        /// </summary>
        /// <param name="type"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        bool IsRegistered(Type type, string key);
        /// <summary>
        /// 获取给定类型的一个实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Resolve<T>() where T : class;
        /// <summary>
        /// 获取给定类型的一个实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T Resolve<T>(string key) where T : class;
        /// <summary>
        /// 获取给定类型的一个实例
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        object Resolve(Type type);
        /// <summary>
        /// 获取给定类型的一个实例
        /// </summary>
        /// <param name="key"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        object Resolve(string key, Type type);
    }
}
