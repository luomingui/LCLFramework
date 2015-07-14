//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace LCL
//{
//    /// <summary>
//    /// Represents the base class for object containers.
//    /// </summary>
//    public abstract class ObjectContainer : IObjectContainer
//    {
//        #region Ctor
//        /// <summary>
//        /// Initializes a new instance of <c>ObjectContainer</c> class.
//        /// </summary>
//        public ObjectContainer()
//        {

//        }
//        private object GetProxyObject(Type targetType, object targetObject)
//        {
//            return targetObject;
//        }
//        #endregion

//        #region Protected Methods
//        /// <summary>
//        /// Gets the service object of the specified type.
//        /// </summary>
//        /// <typeparam name="T">The type of the service object.</typeparam>
//        /// <returns>The instance of the service object.</returns>
//        protected virtual T DoGetService<T>()
//            where T : class
//        {
//            return this.DoGetService(typeof(T)) as T;
//        }
//        /// <summary>
//        /// Gets the service object of the specified type, with overrided
//        /// arguments provided.
//        /// </summary>
//        /// <typeparam name="T">The type of the service object.</typeparam>
//        /// <param name="overridedArguments">The overrided arguments to be used when getting the service.</param>
//        /// <returns>The instance of the service object.</returns>
//        protected virtual T DoGetService<T>(object overridedArguments) where T : class
//        {
//            return this.DoGetService(typeof(T), overridedArguments) as T;
//        }
//        /// <summary>
//        /// Gets the service object of the specified type.
//        /// </summary>
//        /// <param name="serviceType">An object that specifies the type of service object to get.</param>
//        /// <returns>A service object of type serviceType.-or- null if there is no service object
//        /// of type serviceType.</returns>
//        protected abstract object DoGetService(Type serviceType);
//        /// <summary>
//        /// Gets the service object of the specified type, with overrided
//        /// arguments provided.
//        /// </summary>
//        /// <param name="serviceType">The type of the service to get.</param>
//        /// <param name="overridedArguments">The overrided arguments to be used when getting the service.</param>
//        /// <returns>The instance of the service object.</returns>
//        protected abstract object DoGetService(Type serviceType, object overridedArguments);
//        /// <summary>
//        /// Resolves all the objects from the specified type.
//        /// </summary>
//        /// <param name="serviceType">The type of the objects to be resolved.</param>
//        /// <returns>A <see cref="System.Array"/> object which contains all the objects resolved.</returns>
//        protected abstract Array DoResolveAll(Type serviceType);
//        /// <summary>
//        /// Resolves all the objects from the specified type.
//        /// </summary>
//        /// <typeparam name="T">The type of the objects to be resolved.</typeparam>
//        /// <returns>A <see cref="System.Array"/> object which contains all the objects resolved.</returns>
//        protected virtual T[] DoResolveAll<T>()
//            where T : class
//        {
//            return this.DoResolveAll(typeof(T)) as T[];
//        }
//        #endregion

//        #region IObjectContainer Members
//        /// <summary>
//        /// Initializes the object container by using the application/web config file.
//        /// </summary>
//        /// <param name="configSectionName">The name of the ConfigurationSection in the application/web config file
//        /// which is used for initializing the object container.</param>
//        public abstract void InitializeFromConfigFile(string configSectionName);
//        /// <summary>
//        /// Gets the wrapped container instance.
//        /// </summary>
//        /// <typeparam name="T">The type of the wrapped container.</typeparam>
//        /// <returns>The instance of the wrapped container.</returns>
//        public abstract T GetWrappedContainer<T>();
//        /// <summary>
//        /// Gets the service object of the specified type.
//        /// </summary>
//        /// <typeparam name="T">The type of the service object.</typeparam>
//        /// <returns>The instance of the service object.</returns>
//        public T GetService<T>() where T : class
//        {
//            T serviceImpl = this.DoGetService<T>();
//            return (T)GetProxyObject(typeof(T), serviceImpl);
//        }
//        /// <summary>
//        /// Gets the service object of the specified type, with overrided
//        /// arguments provided.
//        /// </summary>
//        /// <typeparam name="T">The type of the service object.</typeparam>
//        /// <param name="overridedArguments">The overrided arguments to be used when getting the service.</param>
//        /// <returns>The instance of the service object.</returns>
//        public T GetService<T>(object overridedArguments) where T : class
//        {
//            T serviceImpl = this.DoGetService<T>(overridedArguments);
//            return (T)GetProxyObject(typeof(T), serviceImpl);
//        }
//        /// <summary>
//        /// Gets the service object of the specified type, with overrided
//        /// arguments provided.
//        /// </summary>
//        /// <param name="serviceType">The type of the service to get.</param>
//        /// <param name="overridedArguments">The overrided arguments to be used when getting the service.</param>
//        /// <returns>The instance of the service object.</returns>
//        public object GetService(Type serviceType, object overridedArguments)
//        {
//            object serviceImpl = this.DoGetService(serviceType, overridedArguments);
//            return this.GetProxyObject(serviceType, serviceImpl);
//        }
//        /// <summary>
//        /// Resolves all the objects from the specified type.
//        /// </summary>
//        /// <param name="serviceType">The type of the objects to be resolved.</param>
//        /// <returns>A <see cref="System.Array"/> object which contains all the objects resolved.</returns>
//        public Array ResolveAll(Type serviceType)
//        {
//            var serviceImpls = this.DoResolveAll(serviceType);
//            List<object> proxiedServiceImpls = new List<object>();
//            foreach (var serviceImpl in serviceImpls)
//                proxiedServiceImpls.Add(GetProxyObject(serviceType, serviceImpl));
//            return proxiedServiceImpls.ToArray();
//        }
//        /// <summary>
//        /// Resolves all the objects from the specified type.
//        /// </summary>
//        /// <typeparam name="T">The type of the objects to be resolved.</typeparam>
//        /// <returns>A <see cref="System.Array"/> object which contains all the objects resolved.</returns>
//        public T[] ResolveAll<T>() where T : class
//        {
//            var serviceImpls = this.DoResolveAll<T>();
//            List<T> proxiedServiceImpls = new List<T>();
//            foreach (var serviceImpl in serviceImpls)
//                proxiedServiceImpls.Add((T)GetProxyObject(typeof(T), serviceImpl));
//            return proxiedServiceImpls.ToArray();
//        }
//        #endregion

//        #region IServiceProvider Members
//        /// <summary>
//        /// Gets the service object of the specified type.
//        /// </summary>
//        /// <param name="serviceType">An object that specifies the type of service object to get.</param>
//        /// <returns>A service object of type serviceType.-or- null if there is no service object
//        /// of type serviceType.</returns>
//        public object GetService(Type serviceType)
//        {
//            object serviceImpl = this.DoGetService(serviceType);
//            return GetProxyObject(serviceType, serviceImpl);
//        }

//        #endregion

//    }
//}
