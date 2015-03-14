/*******************************************************
Author: 罗敏贵
Explain：
Versions: V 1.0 版
E-mail: minguiluo@163.com
Blogs： http://www.cnblogs.com/luomingui
History:
      CreateDate 2014-9-25  星期四 14:41:57
    
*******************************************************/
using System.Net;
using System.Web.Mvc;

namespace LCL.MvcExtensions
{
	public class HeadResult : ActionResult
	{
		public HttpStatusCode StatusCode { get; private set; }

		public HeadResult(HttpStatusCode statusCode)
		{
			StatusCode = statusCode;
		}

		public override void ExecuteResult(ControllerContext context)
		{
			var response = context.RequestContext.HttpContext.Response;
			response.StatusCode = (int)StatusCode;
		}
	}
}