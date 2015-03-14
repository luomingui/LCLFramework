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
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MvcExtensions
{
	/// <summary>
	/// Versions: V 1.0 版
	/// Author: 罗敏贵
	/// E-mail: minguiluo@163.com
	/// Blogs： http://www.cnblogs.com/luomingui
	/// CreateDate: 2014-9-25  星期四 14:04:37
	/// 
	/// <summary>

    /// <summary>
    /// 拒绝被IE6访问
    /// </summary>
    public class CheckExplorerAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.Browser.MajorVersion == 6)
            {
                ViewResult result = new ViewResult();
                result.ViewName = "ExplorerError";
                filterContext.Result = result;
            }
        }
    }
}
