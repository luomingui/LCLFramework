using LCL.ComponentModel;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.WebPages.Razor;

namespace LCL.MvcExtensions
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

            AreaViewLocationFormats = Utility.RedirectToBundlePath(AreaViewLocationFormats, bundleRelativePath).ToArray();
            AreaMasterLocationFormats = Utility.RedirectToBundlePath(AreaMasterLocationFormats, bundleRelativePath).ToArray();
            AreaPartialViewLocationFormats = Utility.RedirectToBundlePath(AreaPartialViewLocationFormats, bundleRelativePath).ToArray();
            ViewLocationFormats = Utility.RedirectToBundlePath(ViewLocationFormats, bundleRelativePath).ToArray();
            MasterLocationFormats = Utility.RedirectToBundlePath(MasterLocationFormats, bundleRelativePath).ToArray();
            PartialViewLocationFormats = Utility.RedirectToBundlePath(PartialViewLocationFormats, bundleRelativePath).ToArray();
        }
        public IPlugin Bundle { get; private set; }
        public string SymbolicName
        {
            get { return Bundle.Assembly.GetName().Name; }
        }
        public void InitPath()
        {
            Debug.WriteLine("PluginRazorViewEngine InitPath....");
            foreach (var item in LEnvironment.GetPluginsName())
            {
                if (item.EndsWith("Plugin"))
                {
                    Debug.WriteLine("~/Plugins/" + item + "/Views/{1}/{0}.cshtml");

                    this._areaViewLocationFormats.Add("~/Plugins/" + item + "/Views/{1}/{0}.cshtml");
                    this._areaViewLocationFormats.Add("~/Plugins/" + item + "/Views/Shared/{0}.cshtml");

                    this._pluginViewLocationFormats.Add("~/Plugins/" + item + "/Views/{1}/{0}.cshtml");
                    this._pluginViewLocationFormats.Add("~/Plugins/" + item + "/Views/Shared/{0}.cshtml");
                }
            }
            this.AreaMasterLocationFormats = this._areaViewLocationFormats.ToArray();
            this.ViewLocationFormats = this._pluginViewLocationFormats.ToArray();
            this.MasterLocationFormats = this._areaViewLocationFormats.ToArray();
            this.PartialViewLocationFormats = this._pluginViewLocationFormats.ToArray();
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
            if (symbolicName != null && Bundle.Assembly.GetName().Name.Equals(symbolicName))
            {
                this.CodeGeneration(Bundle.Assembly.GetName().Name);
                return base.FindView(controllerContext, viewName, masterName, useCache);
            }
            return new ViewEngineResult(new string[0]);
        }

        public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName,
                                                         bool useCache)
        {
            object symbolicName = controllerContext.GetPluginSymbolicName();
            if (symbolicName != null && Bundle.Assembly.GetName().Name.Equals(symbolicName))
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