using LCL;
using System;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Routing;
using System.Linq;

namespace LCL.MvcExtensions
{
    /// <summary>
    /// Mvc Plugin 插件控制器工厂。
    /// 创建一个插件控制器工厂，来获得插件程序集中的控制器类型
    /// </summary>
    public class PluginControllerFactory : DefaultControllerFactory
    {
        public override IController CreateController(System.Web.Routing.RequestContext requestContext, string controllerName)
        {
            try
            {
                //http://localhost:1616/Blog/Index?plugin=BlogPlugin
                //未找到路径“/UIShell.AreaManagementPlugin/Entity/Add”的控制器
                var controller = base.CreateController(requestContext, controllerName);
                if (controller == null)
                {
                    return controller;
                }
                return controller;
            }
            catch (Exception)
            {
                //throw;
                return null;
            }
        }
        protected override Type GetControllerType(RequestContext requestContext, string controllerName)
        {
            var symbolicName = requestContext.GetPluginSymbolicName();
            if (symbolicName != null)
            {
                var controllerType = ControllerTypeCache.GetControllerType(symbolicName, controllerName);
                if (controllerType != null)
                {
                    return controllerType;
                }
                var controllerTypeName = controllerName + "Controller";
                var plugin = LEnvironment.GetAllPlugins().FirstOrDefault(p => p.Assembly.GetName().Name == symbolicName);
                if (plugin != null)
                {
                    foreach (var type in plugin.Assembly.GetTypes())
                    {
                        if (type.Name.Contains(controllerTypeName) && typeof(IController).IsAssignableFrom(type))
                        {
                            controllerType = type;
                            ControllerTypeCache.AddControllerType(symbolicName.ToString(), controllerName, controllerType);
                            Debug.WriteLine(string.Format("Loaded controller '{0}' from bundle '{1}' and then added to cache.", controllerName, symbolicName));
                            return controllerType;
                        }
                    }
                }
                Debug.WriteLine(string.Format("Failed to load controller '{0}' from bundle '{1}'.", controllerName, symbolicName));
            }
            try
            {
                //if exists duplicated controller type, below will throw an exception.
                return base.GetControllerType(requestContext, controllerName);
            }
            catch (Exception)
            {
                //intentionally suppress the duplication error.
            }
            requestContext.RouteData.DataTokens["pluginName"] = symbolicName;
            requestContext.RouteData.DataTokens["Namespaces"] = ControllerTypeCache.DefaultNamespaces;
            var result = base.GetControllerType(requestContext, controllerName);
            if (result != null)
            {
                return result;
            }
            return null;
        }
    }
}