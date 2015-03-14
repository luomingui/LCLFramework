using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using LCL;
using LCL.Reflection;
using System.Reflection;
using System.Diagnostics;
using LCL.ComponentModel;

namespace LCL.DomainServices
{
    /// <summary>
    /// 服务实现的定位器
    /// 服务命名规范： 
    ///    1：服务名称Service
    ///    2：服务名称Service_V10002
    /// <threadsafety static="true" instance="true"/>
    /// </summary>
    [DebuggerDisplay("Count = {Count}")]
    internal static class DomainServiceLocator
    {
        private static Dictionary<Type, ContractImplList> _allServices = new Dictionary<Type, ContractImplList>(100);
        internal static void TryAddPluginsService()
        {
            Debug.WriteLine("DomainServiceLocator Initialize......");
            //TODO:组件初始化
            var plugins = LEnvironment.GetAllPluginAssembly();
            foreach (var plugin in plugins)
            {
                TryAddAssemblyService(plugin.Assembly);
            }
        }
        internal static void TryAddAssemblyService(params Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetExportedTypes())
                {
                    TryAddService(type);
                }
            }
        }
        internal static void TryAddService(Type serviceType)
        {
            //找到在 serviceType 上标记的所有 ContractImplAttribute，并构造对应的 ContractImpl 对象，加入到列表中。
            var attriList = serviceType.GetCustomAttributes(typeof(ContractImplAttribute), false);
            foreach (ContractImplAttribute attri in attriList)
            {
                var impl = new ContractImpl
                {
                    ServiceType = serviceType,
                    ContractType = attri.ContractType ?? serviceType,
                    Version = new Version(attri.Version),
                };
                if (!impl.ContractType.HasMarked<ContractAttribute>())
                {
                    throw new InvalidProgramException(string.Format(
                        "{0} 类型实现了契约类型 {1} ，需要为这个契约添加 ContractAttribute 标记。", serviceType, impl.ContractType
                        ));
                }
                //找到对应的列表，如果不存在，则添加一个新的列表。
                ContractImplList list = null;
                if (!_allServices.TryGetValue(impl.ContractType, out list))
                {
                    list = new ContractImplList(impl.ContractType);
                    _allServices.Add(impl.ContractType, list);
                }

                list.Add(impl);
            }
        }
        internal static ContractImpl FindImpl(Type contractType)
        {
            ContractImplList list = null;
            if (_allServices.TryGetValue(contractType, out list))
            {
                return list.Default();
            }
            return null;
        }
        internal static ContractImpl FindImpl(Type contractType, Version version)
        {
            ContractImplList list = null;
            if (_allServices.TryGetValue(contractType, out list))
            {
                var res = list.Find(version);
                if (res != null) { return res; }
            }
            return null;
        }
        internal static int Count
        {
            get { return _allServices.Count; }
        }

        #region private class ContractImplList

        private class ContractImplList
        {
            private List<ContractImpl> _list = new List<ContractImpl>();

            /// <summary>
            /// 对应这个契约类型
            /// </summary>
            private Type _contractType;

            public ContractImplList(Type contractType)
            {
                _contractType = contractType;
            }

            public ContractImpl Default()
            {
                if (_list.Count > 0)
                {
                    return _list[_list.Count - 1];
                }
                return null;
            }

            /// <summary>
            /// 添加一个契约实现到服务中。
            /// </summary>
            /// <param name="impl"></param>
            public void Add(ContractImpl impl)
            {
                var exists = this.Find(impl.Version);
                if (exists != null)
                {
                    //以防止重入的方式来添加服务的实现，防止多次启动应用程序而重复添加同一服务。
                    if (exists.ServiceType == impl.ServiceType) { return; }

                    throw new InvalidProgramException(string.Format(
                        "契约 {0} 不能同时注册相同版本号({1})的两个服务实现：{2}、{3}。",
                        _contractType,
                        impl.Version,
                        exists.ServiceType,
                        impl.ServiceType
                        ));
                }

                _list.Add(impl);
                _list.Sort();
            }

            public void Remove(ContractImpl impl)
            {
                _list.Remove(impl);
            }

            /// <summary>
            /// 通过版本号来查找对应的契约实现。
            /// </summary>
            /// <param name="version"></param>
            /// <returns></returns>
            public ContractImpl Find(Version version)
            {
                int min = 0, max = _list.Count - 1;
                while (min <= max)
                {
                    var i = min + (max - min >> 1);//(min + max) / 2
                    var item = _list[i];
                    var cResult = item.Version.CompareTo(version);
                    if (cResult > 0)
                    {
                        max = i - 1;
                    }
                    else if (cResult < 0)
                    {
                        min = i + 1;
                    }
                    else
                    {
                        return item;
                    }
                }

                return null;
            }
        }

        #endregion

        #region private class ContractImpl

        internal class ContractImpl : IComparable<ContractImpl>
        {
            public Type ServiceType;
            public Type ContractType;
            public Version Version;

            public int CompareTo(ContractImpl other)
            {
                return this.Version.CompareTo(other.Version);
            }
        }

        #endregion
    }
}