using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCL.Web.Template
{
    /// <summary>
    /// 获取或访问<see cref="ITemplateService"/>实例的唯一入口。
    /// </summary>
    public static class TemplateService
    {
        private static readonly MemoryTemplateService _DefaultCommandService = new MemoryTemplateService();

        private static TemplateServiceProvider currentProvider = () => _DefaultCommandService;

        static TemplateService()
        {
            DefaultTemplateName = "";// "Default";
            TemplatePathFormat = "~/Views/{0}/{1}/{2}.cshtml";
        }

        /// <summary>
        /// 获取当前应用程序正在使用的模板服务。
        /// </summary>
        public static ITemplateService Current
        {
            get { return currentProvider(); }
        }

        /// <summary>
        /// 设置当前应用程序正在使用的模板服务提供者。
        /// </summary>
        public static void SetProvider(TemplateServiceProvider provider)
        {
            if(provider==null)
                throw new ArgumentNullException("TemplateServiceProvider");

            currentProvider = provider;
        }

        /// <summary>
        /// 默认模板。
        /// </summary>
        public static string DefaultTemplateName { get; set; }

        /// <summary>
        /// 模板路径格式。
        /// </summary>
        public static string TemplatePathFormat { get; set; }
    }
}
