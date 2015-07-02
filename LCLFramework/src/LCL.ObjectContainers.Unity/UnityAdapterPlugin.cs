
using LCL.ComponentModel;

namespace LCL.ObjectContainers.Unity
{
    /// <summary>
    /// 从 Unity 适配到 IObjectContainer 的插件。
    /// 使用此插件后，LCL 平台的 IOC 框架将由 UnityContainer 来实现。
    /// </summary>
    public class UnityAdapterPlugin : LCLPlugin
    {
        protected override int SetupLevel
        {
            get { return PluginSetupLevel.System; }
        }
        public override void Initialize(IApp app)
        {
            Logger.LogInfo("LCL.ObjectContainers.Unity Plugin Initialize......");
            LEnvironment.AppObjectContainer = new UnityObjectContainer();
        }
    }
}
