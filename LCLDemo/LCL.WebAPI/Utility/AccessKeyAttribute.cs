using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace LCL.WebAPI.Utility
{
    /// <summary>
    /// 添加WEB权限过滤器
    /// 在你想要的ApiController 或者是 Action 添加过滤器[AccessKey]
    /// </summary>
    public class AccessKeyAttribute:AuthorizeAttribute
    {
        /// <summary>
        /// 权限验证
        /// </summary>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var request = actionContext.Request;
            if (request.Headers.Contains("access-key")) {
                var accessKey = request.Headers.GetValues("access-key").SingleOrDefault();

                return accessKey == "luomingui";
            }
            return false;
        }
        /// <summary>
        /// 处理末授权的请求
        /// </summary>
        /// <param name="actionContext"></param>
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            //base.HandleUnauthorizeadRequest(actionContext);

            var content = JsonConvert.SerializeObject(new { State=HttpStatusCode.Unauthorized});
            actionContext.Response = new HttpResponseMessage
            {
                Content = new StringContent(content, Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.Unauthorized
            };
        }
    }
} 