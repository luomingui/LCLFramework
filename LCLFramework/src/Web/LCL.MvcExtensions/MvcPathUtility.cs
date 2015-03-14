using LCL.ComponentModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace LCL.MvcExtensions
{
    public class PluginMvcPathUtility
    {
        public static string RedirectToBundlePath(string locationFormat, string bundleRelativePath)
        {
            return locationFormat.Replace("~", bundleRelativePath);
        }
        public static IEnumerable<string> RedirectToBundlePath(IEnumerable<string> locationFormats, string bundleRelativePath)
        {
            return locationFormats.Select(item => RedirectToBundlePath(item, bundleRelativePath));
        }
        public static string MapPathReverse(string fullServerPath)
        {
            return @"~\" + fullServerPath.Replace(HostingEnvironment.ApplicationPhysicalPath, String.Empty);
        }
    }
    public static class UrlHelperExtensions
    {
        public static string BundleAllContent(string url)
        {
            string _bundlerootUrl = HttpContext.Current.Request.Url.Host;
            string path = "http://" + _bundlerootUrl + @"/" + url;
            return path;
        }
        public static string BundleContent(this UrlHelper helper, IPlugin bundle, string url)
        {
            string _bundlerootUrl = System.Web.HttpContext.Current.Request.Url.Authority.ToString() + @"/" + LEnvironment.Provider.PluginsDirectoryName;
            string pluginName = bundle.Assembly.GetName().Name;
            string path = "http://" + _bundlerootUrl + @"/" + pluginName + @"/" + url;
            return path;
        }
        public static string BundleContent(this UrlHelper helper, string symbolicName, string url)
        {
            string _bundlerootUrl = System.Web.HttpContext.Current.Request.Url.Authority.ToString() + @"/" + LEnvironment.Provider.PluginsDirectoryName;
            string path = "http://" + _bundlerootUrl + @"/" + symbolicName + @"/" + url;
            return path;
        }
        public static string BundleContent(this UrlHelper helper, string url)
        {
            string str = System.Environment.CurrentDirectory;
            string path =str+url;
            return path;
        }
    }
    public static class BundleUrlHelper
    {
        private static string _webRootPath;
        static BundleUrlHelper()
        {
            _webRootPath = HostingEnvironment.MapPath("~");
        }
        public static string Content(IPlugin bundle, string url)
        {
            while (url.StartsWith("~") || url.StartsWith("/") || url.StartsWith("\\"))
            {
                url = url.Remove(0, 1);
            }
            return Path.Combine(bundle.Assembly.Location.Replace(_webRootPath, @"~\"), url);
        }
        public static string Content(string symbolicName, string url)
        {
            var bundle = LEnvironment.GetPlugin(symbolicName);
            if (bundle == null)
            {
                throw new Exception(string.Format("Bundle {0} doesn't exists.", symbolicName));
            }
            return Content(bundle.Instance, url);
        }
    }
}
