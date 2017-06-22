namespace LCL.Plugins
{
    /// <summary>
    /// 插件的操作接口，主要有设置插件的属性信息，安装插件接口，卸载插件接口。
    /// </summary>
    public interface IPlugin
    {
        PluginDescriptor PluginDescriptor { get; set; }
        void Install();
        void Uninstall();
    }
}
