using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

/*
 http://www.cnblogs.com/happyframework/p/3224278.html
 模板结构： 
 * Views 
 *    Front
 *       Templates
 *           Classic
 *             Home
 *               Index.cshtml
 *           Metro
 *             Home
 *               Index.cshtml
 */
namespace LCL.Web.Template
{
    /// <summary>
    /// 模板服务。
    /// </summary>
    public interface ITemplateService
    {
        /// <summary>
        /// 获取当前的模板。
        /// </summary>
        string GetTemplate(RequestContext context);

        /// <summary>
        /// 设置当前的模板。
        /// </summary>
        void SetTemplate(RequestContext context, string template);
    }
}
