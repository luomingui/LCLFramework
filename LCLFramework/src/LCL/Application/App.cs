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
    [LicenseProvider(typeof(FileLicenseProvider))]
    public abstract class App : DisposableObject, IApp
    {
        #region License
        private License license = null;
        public App()
        {
            try
            {
                license = LicenseManager.Validate(typeof(App), this);
                if (license == null)
                {
                    throw new Exception("LCL组件授权失败,请检查应用程序是否有LCL.lic文件或者联系程序开发商,邮箱是：minguiluo@163.com .");
                }
                if (license != null && license.LicenseKey.Length > 5)
                    throw new Exception("LCL组件授权失败," + license.LicenseKey + ",请联系程序开发商,邮箱是：minguiluo@163.com .");
            }
            catch
            {
                throw;
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (license != null)
                {
                    license.Dispose();
                    license = null;
                }
            }
        }
        #endregion
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
            //组合所有模块的 IOC、事件、
            this.RaiseComposeOperations();
            this.InitServiceLocator();
            this.OnComposed();
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
        protected virtual void InitServiceLocator()
        {
            ServiceLocator.Instance.Register<IEntity, Entity>();
            ServiceLocator.Instance.Register<IEventBus, MSMQEventBus>();
            ServiceLocator.Instance.Register<IEventAggregator, EventAggregator>();
        }
        protected virtual void InitAllPlugins()
        {
            LEnvironment.StartupPlugins();
        }
        private void InitServiceMetas()
        {
            DomainServiceLocator.TryAddPluginsService();

            ServiceLocator.Instance.RegisterType(typeof(IRepository<>), typeof(Repository<>));
        }
        /// <summary>
        /// 设置当前语言
        /// 
        /// 需要在所有 Translator 依赖注入完成后调用。
        /// </summary>
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
        public event EventHandler AllPluginsIntialized;
        protected virtual void OnAllPluginsIntialized()
        {
            var handler = this.AllPluginsIntialized;
            if (handler != null) handler(this, EventArgs.Empty);
        }
        public event EventHandler ModuleOperations;
        protected virtual void OnModuleOpertions()
        {
            var handler = this.ModuleOperations;
            if (handler != null) handler(this, EventArgs.Empty);
        }
        /// <summary>
        /// 组件的组合操作。
        /// 组合可以在此事件中添加自己的组合逻辑，例如 A 订阅 B 的某个事件。
        /// </summary>
        public event EventHandler ComposeOperations;
        /// <summary>
        /// 触发 ComposeOperations 事件。
        /// </summary>
        protected virtual void RaiseComposeOperations()
        {
            var handler = this.ComposeOperations;
            if (handler != null) handler(this, EventArgs.Empty);
        }
        /// <summary>
        /// 所有组件组合完毕。
        /// </summary>
        public event EventHandler Composed;
        /// <summary>
        /// 触发 Composed 事件。
        /// </summary>
        protected virtual void OnComposed()
        {
            var handler = this.Composed;
            if (handler != null) handler(this, EventArgs.Empty);
        }
        #endregion


    }
    public class AppLCL : App
    {
        public virtual void InitApp()
        {
            this.OnAppStartup();
        }
    }
}