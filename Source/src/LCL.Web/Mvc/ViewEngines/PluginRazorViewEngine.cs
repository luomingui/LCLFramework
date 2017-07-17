using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;
using LCL.Plugins;
using LCL.Domain.Services;
using LCL;

namespace LCL.Web.Mvc.ViewEngines
{
    /// <summary>
    /// 
    /// </summary>
    public class PluginRazorViewEngine : RazorViewEngine, IPluginViewEngine
    {
        private List<string> _areaViewLocationFormats = new List<string>
			{
                "~/Plugins/{2}/Views/{1}/{0}.cshtml",
                "~/Plugins/{2}/Views/Shared/{0}.cshtml"
			};

        private List<string> _pluginViewLocationFormats = new List<string>
			{
                "~/Plugins/{2}/Views/{1}/{0}.cshtml",
                "~/Plugins/{2}/Views/Shared/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml",
                "~/Views/{1}/{0}.cshtml",
				"~/Views/Shared/{0}.cshtml"
			};
        public PluginRazorViewEngine(IPlugin bundle)
        {
            Bundle = bundle;
            string bundleRelativePath = "~/Plugins/" + SymbolicName;

            AreaViewLocationFormats = Utils.RedirectToBundlePath(AreaViewLocationFormats, bundleRelativePath).ToArray();
            AreaMasterLocationFormats = Utils.RedirectToBundlePath(AreaMasterLocationFormats, bundleRelativePath).ToArray();
            AreaPartialViewLocationFormats = Utils.RedirectToBundlePath(AreaPartialViewLocationFormats, bundleRelativePath).ToArray();
            ViewLocationFormats = Utils.RedirectToBundlePath(ViewLocationFormats, bundleRelativePath).ToArray();
            MasterLocationFormats = Utils.RedirectToBundlePath(MasterLocationFormats, bundleRelativePath).ToArray();
            PartialViewLocationFormats = Utils.RedirectToBundlePath(PartialViewLocationFormats, bundleRelativePath).ToArray();
        }
        public IPlugin Bundle { get; private set; }
        public string SymbolicName
        {
            get { return Bundle.PluginDescriptor.SystemName; }
        }

        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            string layoutPath = null;
            var runViewStartPages = false;
            IEnumerable<string> fileExtensions = base.FileExtensions;
            return new RazorView(controllerContext, partialPath, layoutPath, runViewStartPages, fileExtensions);
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            string layoutPath = masterPath;
            var runViewStartPages = true;
            IEnumerable<string> fileExtensions = base.FileExtensions;
            return new RazorView(controllerContext, viewPath, layoutPath, runViewStartPages, fileExtensions);
        }

        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName,
                                                  string masterName, bool useCache)
        {
            object symbolicName = controllerContext.GetPluginSymbolicName();
            if (symbolicName != null && SymbolicName.Equals(symbolicName))
            {
                this.CodeGeneration(SymbolicName);
                return base.FindView(controllerContext, viewName, masterName, useCache);
            }
            return new ViewEngineResult(new string[0]);
        }

        public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName,
                                                         bool useCache)
        {
            object symbolicName = controllerContext.GetPluginSymbolicName();
            if (symbolicName != null && SymbolicName.Equals(symbolicName))
            {
                return base.FindPartialView(controllerContext, partialViewName, useCache);
            }
            return new ViewEngineResult(new string[0]);
        }
        private void CodeGeneration(string pluginName)
        {
            //RazorBuildProvider.CodeGenerationStarted += (object sender, EventArgs e) =>
            //{
            //    RazorBuildProvider provider = (RazorBuildProvider)sender;
            //    var plugin = LEnvironment.GetPlugin(pluginName);
            //    if (plugin != null)
            //    {
            //        Debug.WriteLine(pluginName + " RazorBuildProvider.CodeGenerationStarted AddAssemblyReference...");
            //        provider.AssemblyBuilder.AddAssemblyReference(plugin.Assembly);
            //    }
            //};
        }
    }
}