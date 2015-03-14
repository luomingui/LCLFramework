/*******************************************************
Author: 罗敏贵
Explain：
Versions: V 1.0 版
E-mail: minguiluo@163.com
Blogs： http://www.cnblogs.com/luomingui
History:
      CreateDate 2014-9-25  星期四 14:41:57
    
*******************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LCL.MvcExtensions
{
    /// <summary>
    /// 重定向地址错误
    /// </summary>
    public class RedirectToUrlOnErrorAttribute : RedirectOnErrorAttribute
    {
        public string Url { get; set; }
        protected override bool Validate(ActionExecutedContext filterContext)
        {
            //### the url property is always needed
            if (string.IsNullOrEmpty(Url) || Url.Trim() == string.Empty)
                throw new ArgumentNullException("RedirectToUrlOnErrorAttribute's Url property must have a value.");
            //### continue execution
            return true;
        }
        protected override void Redirect(ActionExecutedContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Redirect(Url, true);
        }
    }
}
