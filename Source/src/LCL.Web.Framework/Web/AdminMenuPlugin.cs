using LCL.Plugins;
using LCL.Web.Framework.Menu;

namespace LCL.Web.Framework.Web
{
    public interface IAdminMenuPlugin : IPlugin
    {
        /// <summary>
        /// Authenticate a user (can he see this plugin menu item?)
        /// </summary>
        /// <returns></returns>
        bool Authenticate();

        /// <summary>
        /// Build menu item
        /// </summary>
        /// <returns>Site map item</returns>
        SiteMapNode BuildMenuItem();
    }
}
