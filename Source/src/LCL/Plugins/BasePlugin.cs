using LCL.Domain.Services;
namespace LCL.Plugins
{
    /// <summary>
    /// 实现IPlugin.cs的方法。
    /// </summary>
    public abstract class BasePlugin : IPlugin
    {
        protected BasePlugin()
        {
        }
        
        public virtual PluginDescriptor PluginDescriptor { get; set; }
        public virtual void Install() 
        {
            PluginManager.MarkPluginAsInstalled(this.PluginDescriptor.SystemName);
            Logger.LogInfo("initialize " + this.PluginDescriptor.SystemName+" plugin ");
        }
        public virtual void Uninstall() 
        {
            PluginManager.MarkPluginAsUninstalled(this.PluginDescriptor.SystemName);
            Logger.LogInfo("Uninstall " + this.PluginDescriptor.SystemName + " plugin ");
        }

    }
}
