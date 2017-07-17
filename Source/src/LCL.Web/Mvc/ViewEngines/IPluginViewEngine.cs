using LCL.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace LCL.Web.Mvc.ViewEngines
{
    public interface IPluginViewEngine : IViewEngine
    {
        IPlugin Bundle { get; }
        string SymbolicName { get; }
    }
}
