using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LCL.DomainServices
{
    /// <summary>
    /// 服务工厂简化API
    /// <remarks>
    /// 本类是线程安全的。
    /// </remarks>
    /// </summary>
    public static class DS
    {
        /// <summary>
        /// 创建一个具体的服务。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Create<T>()
            where T : IDomainService
        {
            return DomainServiceFactory.Create<T>();
        }

        /// <summary>
        /// 创建一个具体的服务。
        /// </summary>
        /// <param name="contractType">契约类型。</param>
        /// <returns></returns>
        public static IDomainService Create(Type contractType)
        {
            return DomainServiceFactory.Create(contractType);
        }

        /// <summary>
        /// 创建一个指定版本的服务。
        /// </summary>
        /// <param name="contractType">服务类型.</param>
        /// <param name="version">需要的服务的版本号.</param>
        /// <returns></returns>
        public static IDomainService Create(Type contractType, Version version)
        {
            return DomainServiceFactory.Create(contractType, version);
        }
    }
}