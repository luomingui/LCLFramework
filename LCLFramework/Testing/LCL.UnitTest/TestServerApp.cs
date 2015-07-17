
namespace LCL.UnitTest
{
    public class TestServerApp : App
    {
        internal void Start()
        {
            //Plugins

            this.OnAppStartup();
        }
        protected override void InitEnvironment()
        {
            base.InitEnvironment();
            //如果在初始化时发生异常，则会引发再次启动。这时应该保证之前的所有的初始化工作归零。
            var isWcfServer = ConfigurationHelper.GetAppSettingOrDefault("LCL_IsWCFServer", false);
            LEnvironment.Location.IsWebUI = !isWcfServer;
            LEnvironment.Location.IsWPFUI = true;
            LEnvironment.Location.DataPortalMode = DataPortalMode.ConnectDirectly;
        }
    }
}