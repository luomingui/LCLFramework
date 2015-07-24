using LCL.DomainServices;
using LCL.ObjectContainers.TinyIoC;
using System;
using System.ComponentModel;
using LCL.Reflection;
using System.Reflection;
using LCL.OpenLicense;
using System.Web;
using LCL.Repositories;
using LCL.ComponentModel;
using System.Threading;
using System.Globalization;
using LCL.Events.Bus;
using LCL.Events;

namespace LCL
{
    /// <summary>
    /// Represents the implementation of the application.
    /// </summary>

    public abstract class App : AppLicense, IApp
    {
        protected void OnAppStartup()
        {
            Logger.LogInfo("LCL App OnAppStartup ....");
            this.InitEnvironment();
            //调用所有插件的 Initialize 方法。
            this.InitAllPlugins();
            this.OnAllPluginsIntialized();
            //菜单
            this.OnModuleOpertions();
            //服务。
            this.InitServiceMetas();
            //IOC
            this.InitServiceLocator();
            //设置多国语言
            this.SetupLanguage();
        }
        protected virtual void InitEnvironment()
        {
            Logger.LogInfo("LCL LEnvironment InitEnvironment ....");
            //如果配置了文化，则修改 UI 文化。否则使用系统默认的文化。
            var cultureName = LEnvironment.CurrentCulture;
            if (!string.IsNullOrWhiteSpace(cultureName))
            {
                try
                {
                    var culture = CultureInfo.GetCultureInfo(cultureName);
                    Thread.CurrentThread.CurrentUICulture = culture;
                }
                catch (CultureNotFoundException) { }
            }
            //如果是客户端，则所有线程使用一个身份；如果是服务端，则每个线程使用一个单独的身份。
            if (LEnvironment.Location.IsWPFUI)
            {
                AppContext.SetProvider(new StaticAppContextProvider());
            }
            else
            {
                AppContext.SetProvider(new WebOrThreadStaticAppContextProvider());
            }
            LEnvironment.AppObjectContainer = new TinyIoCObjectContainer();
            LEnvironment.InitApp(this);
        }
        protected virtual void InitAllPlugins()
        {
            LEnvironment.StartupPlugins();
        }
        private void InitServiceMetas()
        {
            DomainServiceLocator.TryAddPluginsService();
        }
        protected virtual void InitServiceLocator()
        {
            ServiceLocator.Instance.Register<IEntity, Entity>();
            ServiceLocator.Instance.Register<IEventBus, MSMQEventBus>();
            ServiceLocator.Instance.Register<IEventAggregator, EventAggregator>();
        }
        private void SetupLanguage()
        {
            //当前线程的文化名，就是 Rafy 多国语言的标识。
            var culture = Thread.CurrentThread.CurrentUICulture.Name;
            if (!Translator.IsDevCulture(culture))
            {
                var translator = LEnvironment.Provider.Translator;
                //目前，在服务端进行翻译时，只支持一种语言。所以尽量在客户端进行翻译。
                translator.CurrentCulture = culture;
                translator.Enabled = true;
            }
        }

        #region IAppEvent
        public event EventHandler<AppInitEventArgs> AllPluginsIntialized;
        protected virtual void OnAllPluginsIntialized()
        {
            var handler = this.AllPluginsIntialized;
            if (handler != null) handler(this, new AppInitEventArgs(this.ObjectContainer));
        }
        public event EventHandler<AppInitEventArgs> ModuleOperations;
        protected virtual void OnModuleOpertions()
        {
            var handler = this.ModuleOperations;
            if (handler != null) handler(this, new AppInitEventArgs(this.ObjectContainer));
        }
        #endregion

        public IObjectContainer ObjectContainer
        {
            get { return LEnvironment.AppObjectContainer; }
        }
    }
    public class AppLCL : App
    {
        public virtual void InitApp()
        {
            this.OnAppStartup();
        }
    }
}