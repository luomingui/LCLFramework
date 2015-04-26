using LCL.MetaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace LCL.MvcExtensions
{
    public static class MvcModuleMetaExtensions
    {
        public static bool IsSelected(this ModuleMeta node, RouteData routeData)
        {
            object plugin = routeData.Values["pluginName"];
            object controller = routeData.Values["controller"];
            object action = routeData.Values["action"];
            string url = string.Format("/{0}/{1}/", plugin, controller);
            return node.CustomUI.StartsWith(url, StringComparison.InvariantCultureIgnoreCase);
        }
        public static bool HasSubMenu(this ModuleMeta node)
        {
            return node.Children.Count > 0;
        }
        public static bool IsOpened(this ModuleMeta node, RouteData routeData)
        {
            bool result;
            if (!node.HasSubMenu())
            {
                result = false;
            }
            else
            {
                foreach (ModuleMeta child in node.Children)
                {
                    if (child.IsSelected(routeData))
                    {
                        result = true;
                        return result;
                    }
                }
                result = false;
            }
            return result;
        }
    }
}