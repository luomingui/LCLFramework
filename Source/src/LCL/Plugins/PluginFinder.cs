using System;
using System.Collections.Generic;
using System.Linq;

namespace LCL.Plugins
{
    /// <summary>
    /// Plugin finder
    /// 加载所有的插件，并获取它们的信息.
    /// </summary>
    public class PluginFinder : IPluginFinder
    {
        #region Fields

        private IList<PluginDescriptor> _plugins;

        private bool _arePluginsLoaded = false;

        #endregion

        #region Utilities
        protected virtual void EnsurePluginsAreLoaded()
        {
            if (!_arePluginsLoaded)
            {
                var foundPlugins = PluginManager.ReferencedPlugins.ToList();
                foundPlugins.Sort(); //sort
                _plugins = foundPlugins.ToList();

                _arePluginsLoaded = true;
            }
        }

        #endregion
        
        #region Methods
        public virtual bool AuthenticateStore(PluginDescriptor pluginDescriptor, int storeId)
        {
            if (pluginDescriptor == null)
                throw new ArgumentNullException("pluginDescriptor");

            //no validation required
            if (storeId == 0)
                return true;

            if (pluginDescriptor.LimitedToStores.Count == 0)
                return true;

            return pluginDescriptor.LimitedToStores.Contains(storeId);
        }
        public virtual IEnumerable<T> GetPlugins<T>(bool installedOnly = true, int storeId = 0) where T : class, IPlugin
        {
            EnsurePluginsAreLoaded();

            foreach (var plugin in _plugins)
                if (typeof(T).IsAssignableFrom(plugin.PluginType))
                    if (!installedOnly || plugin.Installed)
                        if (AuthenticateStore(plugin, storeId))
                            yield return plugin.Instance<T>();
        }
        public virtual IEnumerable<PluginDescriptor> GetPluginDescriptors(bool installedOnly = true, int storeId = 0)
        {
            EnsurePluginsAreLoaded();

            foreach (var plugin in _plugins)
                if (!installedOnly || plugin.Installed)
                    if (AuthenticateStore(plugin, storeId))
                        yield return plugin;
        }
        public virtual IEnumerable<PluginDescriptor> GetPluginDescriptors<T>(bool installedOnly = true, int storeId = 0) 
            where T : class, IPlugin
        {
            EnsurePluginsAreLoaded();

            foreach (var plugin in _plugins)
                if (typeof(T).IsAssignableFrom(plugin.PluginType))
                    if (!installedOnly || plugin.Installed)
                        if (AuthenticateStore(plugin, storeId))
                            yield return plugin;
        }
        public virtual PluginDescriptor GetPluginDescriptorBySystemName(string systemName, bool installedOnly = true)
        {
            return GetPluginDescriptors(installedOnly)
                .SingleOrDefault(p => p.SystemName.Equals(systemName, StringComparison.InvariantCultureIgnoreCase));
        }
        public virtual PluginDescriptor GetPluginDescriptorBySystemName<T>(string systemName, bool installedOnly = true) where T : class, IPlugin
        {
            return GetPluginDescriptors<T>(installedOnly)
                .SingleOrDefault(p => p.SystemName.Equals(systemName, StringComparison.InvariantCultureIgnoreCase));
        }
        public virtual void ReloadPlugins()
        {
            _arePluginsLoaded = false;
            EnsurePluginsAreLoaded();
        }

        #endregion
    }
}
