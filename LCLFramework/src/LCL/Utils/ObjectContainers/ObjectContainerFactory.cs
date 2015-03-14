
using LCL.ObjectContainers.TinyIoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCL
{
    /// <summary>
    /// IOC 容器工厂。
    /// </summary>
    public interface IObjectContainerFactory
    {
        /// <summary>
        /// 创建一个独立的的 IOC 容器
        /// </summary>
        /// <returns></returns>
        IObjectContainer CreateContainer();
    }
    /// <summary>
    /// IOC 容器工厂。
    /// </summary>
    public static class ObjectContainerFactory
    {
        private static IObjectContainerFactory _provider = new DefaultObjectContainerFactory();

        /// <summary>
        /// 创建一个独立的的 IOC 容器
        /// </summary>
        /// <returns></returns>
        public static IObjectContainer CreateContainer()
        {
            return _provider.CreateContainer();
        }

        /// <summary>
        /// 指定具体的提供程序
        /// </summary>
        /// <param name="value"></param>
        public static void SetProvider(IObjectContainerFactory value)
        {
            if (value == null) throw new ArgumentNullException("value");
            _provider = value;
        }

        private class DefaultObjectContainerFactory : IObjectContainerFactory
        {
            public IObjectContainer CreateContainer()
            {
                return new TinyIoCObjectContainer();
            }
        }
    }
}
