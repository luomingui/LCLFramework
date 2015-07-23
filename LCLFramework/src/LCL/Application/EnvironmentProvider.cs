using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;

namespace LCL
{
    /// <summary>
    /// 环境变量值提供器。
    /// private const string PluginsPath = "~/Plugins";
    /// private const string ShadowCopyPath = "~/Plugins/bin";
    /// </summary>
    public class EnvironmentProvider
    {
        public EnvironmentProvider()
        {
            this.PluginsDirectoryName = "Plugins";
            this.RootDirectory = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            var httpContext = HttpContext.Current;
            if (httpContext != null)
            {
                this.DllRootDirectory = httpContext.Server.MapPath("Bin");
                this.PluginsDirectory = HostingEnvironment.MapPath(@"~\" + this.PluginsDirectoryName + "");
                this.PluginsShadowCopyDirectory = this.PluginsDirectory + @"\Bin";
                this.IsDebuggingEnabled = httpContext.IsDebuggingEnabled;
                this.IsLibrary = false;
            }
            else
            {
                this.DllRootDirectory = this.RootDirectory;
                this.PluginsDirectory = this.RootDirectory + @"\" + this.PluginsDirectoryName;
                this.PluginsShadowCopyDirectory = this.PluginsDirectory + @"\Bin";
                this.IsDebuggingEnabled = ConfigurationHelper.GetAppSettingOrDefault<bool>("IsDebuggingEnabled", false);
                this.IsLibrary = true;
            }
        }
        /// <summary>
        /// 整个应用程序的根目录
        /// </summary>
        public string RootDirectory { get; set; }
        //插件目录名称
        public string PluginsDirectoryName { get; set; }
        ///~/Plugins
        public string PluginsDirectory { get; set; }
        ///~/Plugins/bin
        public string PluginsShadowCopyDirectory { get; set; }
        public bool IsLibrary { get; set; }
        public string DllRootDirectory { get; set; }
        /// <summary>
        /// 在程序启动时，设置本属性以指示当前程序是否处于调试状态。
        /// </summary>
        public bool IsDebuggingEnabled { get; set; }

        private Translator _translator;

        /// <summary>
        /// 当前使用的翻译器
        /// </summary>
        public Translator Translator
        {
            get
            {
                if (this._translator == null)
                {
                    this._translator = new EmptyTranslator();
                }
                return _translator;
            }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                _translator = value;
            }
        }
        /// <summary>
        /// 相对路径转换为绝对路径。
        /// </summary>
        /// <param name="appRootRelative"></param>
        /// <returns></returns>
        public string ToAbsolute(string appRootRelative)
        {
            return Path.Combine(RootDirectory, appRootRelative);
        }
    }
}
