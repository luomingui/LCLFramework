
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Web;

namespace LCL.MvcExtensions
{
    /// <summary>
    /// WebApp 启动环境
    /// </summary>
    public class WebApp : App
    {
        public void Startup()
        {
            try
            {
                this.OnAppStartup();
            }
            catch (Exception ex)
            {
                Logger.LogError("Web 项目启动时发生异常", ex);
                throw;
            }
        }
        protected override void InitEnvironment()
        {
            //如果在初始化时发生异常，则会引发再次启动。这时应该保证之前的所有的初始化工作归零。
            var isWcfServer = ConfigurationHelper.GetAppSettingOrDefault("LCL_IsWCFServer", false);
            LEnvironment.Location.IsWebUI = !isWcfServer;
            LEnvironment.Location.IsWPFUI = false;
            LEnvironment.Location.DataPortalMode = DataPortalMode.ConnectDirectly;
            base.InitEnvironment();
            //LEnvironment.AppObjectContainer = new UnityObjectContainer();
        }
    }
}