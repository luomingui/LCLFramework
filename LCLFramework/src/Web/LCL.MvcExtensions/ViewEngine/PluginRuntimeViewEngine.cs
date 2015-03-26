using LCL.ComponentModel;
using System.Collections.Concurrent;
using System.Web.Mvc;

namespace LCL.MvcExtensions
{
    public class PluginRuntimeViewEngine : IViewEngine
    {
        private readonly ConcurrentDictionary<string, IPluginViewEngine> _viewEngines =
            new ConcurrentDictionary<string, IPluginViewEngine>();

        //TODO: 创建插件视图
        public PluginRuntimeViewEngine(IPluginViewEngineFactory viewEngineFactory)
        {
            this.BundleViewEngineFactory = viewEngineFactory;
            LEnvironment.GetAllPlugins().ForEach(AddViewEngine);
        }

        public ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName,
                                                bool useCache)
        {
            var viewEngine = GetViewEngine(controllerContext);
            if (viewEngine != null)
            {
                return viewEngine.FindPartialView(controllerContext, partialViewName, useCache);
            }
            return new ViewEngineResult(new string[0]);
        }

        public ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName,
                                         bool useCache)
        {
            var viewEngine = GetViewEngine(controllerContext);
            if (viewEngine != null)
            {
                return viewEngine.FindView(controllerContext, viewName, masterName, useCache);
            }
            return new ViewEngineResult(new string[0]);
        }

        public void ReleaseView(ControllerContext controllerContext, IView view)
        {
            var viewEngine = GetViewEngine(controllerContext);
            if (viewEngine != null)
            {
                viewEngine.ReleaseView(controllerContext, view);
            }
        }

        public IPluginViewEngineFactory BundleViewEngineFactory { get; private set; }

        private void AddViewEngine(IPlugin bundle)
        {
            _viewEngines.TryAdd(bundle.Assembly.GetName().Name, BundleViewEngineFactory.CreateViewEngine(bundle));
        }

        private void RemoveViewEngine(IPlugin bundle)
        {
            IPluginViewEngine tmp;
            _viewEngines.TryRemove(bundle.Assembly.GetName().Name, out tmp);
        }

        private IViewEngine GetViewEngine(ControllerContext controllerContext)
        {
            IPluginViewEngine tmp;
            object symbolicName = controllerContext.GetPluginSymbolicName();
            if (symbolicName == null)
            {
                Logger.LogDebug("GetViewEngine symbolicName:" + symbolicName.ToString() + "isIViewEngine:" + _viewEngines.TryGetValue(symbolicName.ToString(), out tmp));
                return null;
            }
            if (_viewEngines.TryGetValue(symbolicName.ToString(), out tmp))
            {
                return tmp;
            }
            else
            {
                Logger.LogDebug("GetViewEngine symbolicName:" + symbolicName.ToString() + "isIViewEngine:" + _viewEngines.TryGetValue(symbolicName.ToString(), out tmp));
            }
            return null;
        }
    }
}