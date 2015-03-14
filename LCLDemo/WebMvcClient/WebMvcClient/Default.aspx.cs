using LCL.MvcExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebMvcClient
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var pageFlowService = PageFlowService.PageNodes.FindPageNode("LayoutLogin");
            if (pageFlowService == null)
            {
                throw new Exception("没有安装PageFlowService服务。");
            }
            if (string.IsNullOrEmpty(pageFlowService.Value))
            {
                throw new Exception("没有定义第一个页面节点。");
            }
            // Redirect to first node.
            Response.Redirect(pageFlowService.Value, false);
        }
    }
}