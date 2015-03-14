
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Routing;
using System.Linq;
using System.Web.Compilation;
using System.Reflection;

[assembly: System.Web.PreApplicationStartMethod(typeof(LCL.MvcExtensions.Bootstrapper), "Initialize")]
namespace LCL.MvcExtensions
{
    /// <summary>
    /// 引导程序。
    /// </summary>
    public static class Bootstrapper
    {
        /// <summary>
        /// 初始化。
        /// </summary>
        public static void Initialize()
        {
            Logger.LogInfo("LCL Bootstrapper Initialize ....");
            LEnvironment.Provider.IsLibrary = false;
            new WebApp().Startup();
            AddPluginReferencedAssemblies();
        }
        public static void AddPluginReferencedAssemblies()
        {
            var assemlbies = LEnvironment.GetAllPluginAssemblys();
            assemlbies.ForEach(AddReferencedAssembly);

            assemlbies.ForEach(DefaultControllerConfig.Register);
        }
        public static void AddReferencedAssembly(Assembly assembly)
        {
            //todo:use reflection to add assembly to Build Manager if the app_start is finished.
            BuildManager.AddReferencedAssembly(assembly);
            Logger.LogInfo(string.Format("LCL Bootstrapper Add assembly '{0} to top level referenced assemblies.'", assembly.FullName));
        }
    }
}