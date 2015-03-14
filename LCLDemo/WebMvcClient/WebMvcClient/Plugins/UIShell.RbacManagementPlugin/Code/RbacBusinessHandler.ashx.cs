using LCL.MetaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UIShell.RbacManagementPlugin
{
    /// <summary>
    /// RbacBusinessHandler 的摘要说明
    /// </summary>
    public class RbacBusinessHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");

            // var meta= CommonModel.Modules["ss"];
            
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}