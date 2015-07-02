using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;

namespace LCL
{
    /// <summary>
    /// 一个模拟 Web 环境的服务器端上下文提供器。
    /// 每次请求使用一个单独的数据上下文
    /// </summary>
    internal class WebOrThreadStaticAppContextProvider : ThreadStaticAppContextProvider
    {
        protected const string LocalContextName = "LCL.WebThreadContextProvider";

        public override IPrincipal CurrentPrincipal
        {
            get
            {
                var webContext = HttpContext.Current;
                if (webContext != null)
                {
                    return webContext.User;
                }

                return base.CurrentPrincipal;
            }
            set
            {
                var webContext = HttpContext.Current;
                if (webContext != null)
                {
                    webContext.User = value;
                }

                base.CurrentPrincipal = value;
            }
        }

        public override IDictionary<string, object> DataContainer
        {
            get
            {
                var webContext = HttpContext.Current;
                if (webContext != null)
                {
                    return webContext.Items[LocalContextName] as IDictionary<string, object>;
                }

                return base.DataContainer;
            }
            set
            {
                var webContext = HttpContext.Current;
                if (webContext != null)
                {
                    webContext.Items[LocalContextName] = value;
                }

                base.DataContainer = value;
            }
        }
    }
}
