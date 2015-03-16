using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LCL.MvcExtensions
{
    /// <summary>
    /// 自定义的视图
    /// 视图需要继承 IView 接口
    /// http://www.cnblogs.com/webabcd/archive/2009/05/14/1456453.html
    /// </summary>
    public class HtmlView: IView
    {
       // 视图文件的物理路径
        private string _viewPhysicalPath;
        public HtmlView(string viewPhysicalPath)
        {
            _viewPhysicalPath = viewPhysicalPath;
        }
        /// <summary>
        /// 实现 IView 接口的 Render() 方法
        /// </summary>
        public void Render(ViewContext viewContext, TextWriter writer)
        {
            // 获取视图文件的原始内容  
            string rawContents = File.ReadAllText(_viewPhysicalPath);
            // 根据自定义的规则解析原始内容  
            string parsedContents = Parse(rawContents, viewContext.ViewData);
            // 呈现出解析后的内容
            writer.Write(parsedContents);
        }
        public string Parse(string contents, ViewDataDictionary viewData)
        {
            // 对 {##} 之间的内容作解析
            return Regex.Replace
            (
                contents, 
                @"\{#(.+)#\}", 
                // 委托类型 public delegate string MatchEvaluator(Match match)
                p => GetMatch(p, viewData)
            );
        }
        protected virtual string GetMatch(Match m, ViewDataDictionary viewData)
        {
            if (m.Success)
            {
                // 获取匹配后的结果，即 ViewData 中的 key 值，并根据这个 key 值返回 ViewData 中对应的 value
                string key = m.Result("$1");
                if (viewData.ContainsKey(key))
                {
                    return viewData[key].ToString();
                }
            }
            return string.Empty;
        }
    }
    /// <summary>
    /// 自定义的视图引擎
    /// 视图引擎需要继承 IViewEngine 接口
    /// VirtualPathProviderViewEngine 继承了 IViewEngine 接口，
    /// 实现了根据指定的路径格式搜索对应的页面文件的功能（内用缓存机制）
    /// http://www.cnblogs.com/webabcd/archive/2009/05/14/1456453.html
    /// </summary>
    public class HtmlViewEngine : VirtualPathProviderViewEngine
    {
        public HtmlViewEngine()
        {
            // 自定义 View 路径格式
            base.ViewLocationFormats = new string[] 
            { 
                "~/Views/{1}/{0}.html", "~/Views/Shared/{0}.html" 
            };
        }
        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            return this.CreateView(controllerContext, partialPath, string.Empty);
        }
        /// <summary>
        /// 根据指定路径返回一个实现了 IView 接口的对象
        /// </summary>
        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            var physicalPath = controllerContext.HttpContext.Server.MapPath(viewPath);
            return new HtmlView(physicalPath);
        }
    }
    
 
}
