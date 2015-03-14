using LCL.ComponentModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.WebPages.Razor;

namespace LCL.MvcExtensions
{
    public class PluginWebFormViewEngine : WebFormViewEngine, IPluginViewEngine
    {
        public IPlugin Bundle { get; private set; }
        public PluginWebFormViewEngine(IPlugin bundle)
        {
            Bundle = bundle;
            string bundleRelativePath = Utility.MapPathReverse(bundle.Assembly.Location);

            AreaViewLocationFormats = Utility.RedirectToBundlePath(AreaViewLocationFormats, bundleRelativePath).ToArray();
            AreaMasterLocationFormats = Utility.RedirectToBundlePath(AreaMasterLocationFormats, bundleRelativePath).ToArray();
            AreaPartialViewLocationFormats = Utility.RedirectToBundlePath(AreaPartialViewLocationFormats, bundleRelativePath).ToArray();
            ViewLocationFormats = Utility.RedirectToBundlePath(ViewLocationFormats, bundleRelativePath).ToArray();
            MasterLocationFormats = Utility.RedirectToBundlePath(MasterLocationFormats, bundleRelativePath).ToArray();
            PartialViewLocationFormats = Utility.RedirectToBundlePath(PartialViewLocationFormats, bundleRelativePath).ToArray();
        }
        public string SymbolicName
        {
            get { return Bundle.Assembly.GetName().Name; }
        }

        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName,
                                                  string masterName, bool useCache)
        {
            object symbolicName = controllerContext.GetPluginSymbolicName();
            if (symbolicName != null && Bundle.Assembly.GetName().Name.Equals(symbolicName))
            {
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
    }
}
