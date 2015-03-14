/*******************************************************
Author: 罗敏贵
Explain：
Versions: V 1.0 版
E-mail: minguiluo@163.com
Blogs： http://www.cnblogs.com/luomingui
History:
      CreateDate 2014-9-25  星期四 14:41:57
    
*******************************************************/
using System.Web.Mvc;

namespace LCL.MvcExtensions
{
	public class TempDataToViewDataAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuted(ActionExecutedContext filterContext) 
		{
			if(! (filterContext.Result is ViewResult)) return;
			var tempData = filterContext.Controller.TempData;
			var viewData = filterContext.Controller.ViewData;
			foreach(var pair in tempData)
			{
				if(!viewData.ContainsKey(pair.Key))
				{
					viewData[pair.Key] = pair.Value;
				}
			}
		}
	}
}