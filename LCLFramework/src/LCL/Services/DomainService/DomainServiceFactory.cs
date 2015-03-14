using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LCL.DomainServices
{
    /// <summary>
    /// 服务工厂
    /// <remarks>
    /// 本类是线程安全的。
    /// </remarks>
    /// </summary>
    public static class DomainServiceFactory
    {
        /// <summary>
        /// 创建一个具体的服务。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Create<T>()
            where T : IDomainService
        {
            return (T)Create(typeof(T));
        }

        /// <summary>
        /// 创建一个具体的服务。
        /// </summary>
        /// <param name="contractType">契约类型。</param>
        /// <returns></returns>
        public static IDomainService Create(Type contractType)
        {
            IDomainService res = null;

            var impl = DomainServiceLocator.FindImpl(contractType);
            if (impl != null)
            {
                res = Activator.CreateInstance(impl.ServiceType) as IDomainService;
                if (res == null)
                {
                    throw new InvalidProgramException(string.Format("{0} 类型必须实现 LCL.Domain.IService 接口。", impl.ServiceType));
                }
            }
            if (res == null)
            {
                throw new InvalidProgramException(string.Format("没有注册实现 {0} 契约类型的服务，请在相应的服务类型上标记 ContractImplAttribute。", contractType));
            }

            return res;
        }

        /// <summary>
        /// 创建一个指定版本的服务。
        /// </summary>
        /// <param name="contractType">服务类型.</param>
        /// <param name="version">需要的服务的版本号.</param>
        /// <returns></returns>
        public static IDomainService Create(Type contractType, Version version)
        {
            var impl = DomainServiceLocator.FindImpl(contractType, version);
            if (impl != null)
            {
                return Activator.CreateInstance(impl.ServiceType) as IDomainService;
            }

            return null;
        }
    }
}