using LCL.Core;
using LCL.Core.Domain.Services;
using LCL.Core.Plugins;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LCL.Web.Framework.ViewEngines
{
    public class PluginWebFormViewEngine : WebFormViewEngine, IPluginViewEngine
    {
        public IPlugin Bundle { get; private set; }
        public PluginWebFormViewEngine(IPlugin bundle)
        {
            Bundle = bundle;
            string bundleRelativePath = Utils.MapPathReverse(bundle.PluginDescriptor.ReferencedAssembly.Location);

            AreaViewLocationFormats = Utils.RedirectToBundlePath(AreaViewLocationFormats, bundleRelativePath).ToArray();
            AreaMasterLocationFormats = Utils.RedirectToBundlePath(AreaMasterLocationFormats, bundleRelativePath).ToArray();
            AreaPartialViewLocationFormats = Utils.RedirectToBundlePath(AreaPartialViewLocationFormats, bundleRelativePath).ToArray();
            ViewLocationFormats = Utils.RedirectToBundlePath(ViewLocationFormats, bundleRelativePath).ToArray();
            MasterLocationFormats = Utils.RedirectToBundlePath(MasterLocationFormats, bundleRelativePath).ToArray();
            PartialViewLocationFormats = Utils.RedirectToBundlePath(PartialViewLocationFormats, bundleRelativePath).ToArray();
        }
        public string SymbolicName
        {
            get { return Bundle.PluginDescriptor.SystemName; }
        }

        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName,
                                                  string masterName, bool useCache)
        {
            object symbolicName = controllerContext.GetPluginSymbolicName();
            if (symbolicName != null && SymbolicName.Equals(symbolicName))
            {
                return base.FindView(controllerContext, viewName, masterName, useCache);
            }
            else
            {
                Logger.LogDebug("FindView symbolicName:" + SymbolicName + "viewName:" + viewName);
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
            else
            {
                Logger.LogDebug("FindPartialView symbolicName:" + SymbolicName + "partialViewName:" + partialViewName);
            }
            return new ViewEngineResult(new string[0]);
        }
    }
}
