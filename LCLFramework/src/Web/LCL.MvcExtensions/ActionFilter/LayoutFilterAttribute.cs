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
using System.Web.Mvc;

namespace LCL.MvcExtensions
{
	[Obsolete]
	public class LayoutFilterAttribute : ActionFilterAttribute
	{
		public string Layout { get; private set; }
        public LayoutFilterAttribute(string layout)
		{
			Layout = layout;
		}
		public override void OnResultExecuting(ResultExecutingContext filterContext)
		{
			base.OnResultExecuting(filterContext);
			var viewResult = filterContext.Result as ViewResult;
			if(viewResult != null)
			{
			    viewResult.MasterName = Layout;
			}
		}
	}
}