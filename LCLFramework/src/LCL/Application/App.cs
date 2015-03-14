using LCL.DomainServices;
using LCL.ObjectContainers.TinyIoC;
using System;
using System.ComponentModel;
using LCL.Reflection;
using System.Reflection;
using LCL.OpenLicense;
using System.Web;
using LCL.Repositories;
using LCL.Bus;
using LCL.Bus.DirectBus;
using LCL.ComponentModel;

namespace LCL
{
    /// <summary>
    /// Represents the implementation of the application.
    /// </summary>
    [LicenseProvider(typeof(FileLicenseProvider))]
    public abstract class App : DisposableObject, IApp
    {
        private License license = null;
        public App()
        {
            try
            {
                license = LicenseManager.Validate(typeof(App), this);
            }
            catch
            {
                throw new Exception("LCL组件未授权,请联系程序开发商,邮箱是：minguiluo@163.com .");
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
        }
        protected virtual void InitEnvironment()
        {
            Logger.LogInfo("LCL LEnvironment InitEnvironment ....");
            ServerContext.SetCurrent(new ServerContextProvider());
            LEnvironment.AppObjectContainer = new TinyIoCObjectContainer();
            LEnvironment.InitApp(this);

            if (AssemblyExtensions.IsDebugBuild(Assembly.GetExecutingAssembly()))
            {
                Logger.SetImplementation(new TraceLogger());
            }
        }
        protected virtual void InitServiceLocator()
        {
            ServiceLocator.Instance.Register<IMessageDispatcher, MessageDispatcher>();
            ServiceLocator.Instance.Register<ICommandBus, DirectCommandBus>();
            ServiceLocator.Instance.Register<IEventBus, DirectEventBus>();

            ServiceLocator.Instance.Register<IAggregateRoot, AggregateRoot>();
            ServiceLocator.Instance.Register<ISourcedAggregateRoot, SourcedAggregateRoot>();

            ServiceLocator.Instance.Register<IPlugin, LCLPlugin>();
        }
        /// <summary>
        /// 初始化所有Plugins
        /// </summary>
        protected virtual void InitAllPlugins()
        {
            LEnvironment.StartupPlugins();
        }
        private void InitServiceMetas()
        {
            DomainServiceLocator.TryAddPluginsService();
        }

        #region IApp
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