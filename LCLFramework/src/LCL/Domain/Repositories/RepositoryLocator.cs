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

namespace LCL.Repositories
{
    /// <summary>
    /// 服务实现的定位器
    /// 服务命名规范： 
    ///    1：服务名称Service
    ///    2：服务名称Service_V10002
    /// <threadsafety static="true" instance="true"/>
    /// </summary>
    [DebuggerDisplay("Count = {Count}")]
    internal static class RepositoryLocator
    {
        private static Dictionary<Type, Type> _allServices = new Dictionary<Type, Type>(100);
        internal static void TryAddPluginsService()
        {
            Logger.LogInfo("RepositoryLocator Initialize......");
            //TODO:组件初始化
            var plugins = LEnvironment.GetAllPluginAssembly();
            foreach (var plugin in plugins)
            {
                TryAddAssemblyService(plugin.Assembly);
                //ServiceLocator.Instance.RegisterTypes(p => p., plugin.Assembly);
            }
        }
        internal static void TryAddAssemblyService(params Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetExportedTypes())
                {
                  //  TryAddService(type);
                }
            }
        }
    }
}