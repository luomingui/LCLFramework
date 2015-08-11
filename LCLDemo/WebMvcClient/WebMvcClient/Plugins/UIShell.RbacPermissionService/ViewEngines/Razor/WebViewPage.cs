#region Using...

using System;
using System.IO;
using System.Web.Mvc;
using LCL.Repositories;
using UIShell.RbacPermissionService;

#endregion

namespace LCL.MvcExtensions
{
    public abstract class WebViewPage<TModel> : System.Web.Mvc.WebViewPage<TModel>
    {
        private object _display;
        private object _layout;
        public dynamic Display { get { return _display; } }
        public new dynamic Layout { get { return _layout; } }
        public bool HasText(object thing)
        {
            return !string.IsNullOrWhiteSpace(Convert.ToString(thing));
        }
    }
    public abstract class WebViewPage : WebViewPage<dynamic>
    {
    }
}