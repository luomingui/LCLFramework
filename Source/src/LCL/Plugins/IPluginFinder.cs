using System.Collections.Generic;

namespace LCL.Plugins
{
    /// <summary>
    /// Plugin finder
    /// <remarks>
    /// 获取插件的信息接口，在ioc里的LCL.Web.Framework.DependencyRegistrar注册此接口。
    /// 系统启动的时候会加载到内存里。  
    /// </remarks>
    /// <code>
    /// //plugins
    /// builder.RegisterType<PluginFinder>().As<IPluginFinder>().InstancePerLifetimeScope();
    /// </code>
    /// </summary>
    public interface IPluginFinder
    {
        bool AuthenticateStore(PluginDescriptor pluginDescriptor, int storeId);
        IEnumerable<T> GetPlugins<T>(bool installedOnly = true, int storeId = 0) where T : class, IPlugin;
        IEnumerable<PluginDescriptor> GetPluginDescriptors(bool installedOnly = true, int storeId = 0);
        IEnumerable<PluginDescriptor> GetPluginDescriptors<T>(bool installedOnly = true, int storeId = 0) where T : class, IPlugin;
        PluginDescriptor GetPluginDescriptorBySystemName(string systemName, bool installedOnly = true);
        PluginDescriptor GetPluginDescriptorBySystemName<T>(string systemName, bool installedOnly = true) where T : class, IPlugin;
        void ReloadPlugins();
    }
}
