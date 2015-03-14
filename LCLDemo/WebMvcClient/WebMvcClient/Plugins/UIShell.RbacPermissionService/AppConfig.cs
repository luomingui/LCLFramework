using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace UIShell.RbacPermissionService
{
    public class AppConfig
    {
        public static string AppName = "UIShell.RbacPermissionService";
        public static string CompanyName = "UIShell";
        public static string AppLogo = "Title.png";
        public static bool NavHidden
        {
            //取消点击折叠按钮重载页面用SESSION存储折叠状态，改为COOKIES【By Vin 2014/05/23】
            get
            {
                if (HttpContext.Current.Request.Cookies.AllKeys.Contains("NavHiddern"))
                {
                    var cookie = HttpContext.Current.Request.Cookies["NavHiddern"];
                    return cookie.Value.ToLower().Equals("true")?true:false;
                }
                return false;
            }
            set
            {
                var cookie = new HttpCookie("NavHiddern", value.ToString());
                HttpContext.Current.Response.Cookies.Set(cookie);
            }
        }

        static AppConfig()
        {
            var app = WebConfigurationManager.AppSettings["AppName"];
            if(!string.IsNullOrEmpty(app))
            {
                AppName = app;
            }
            var company = WebConfigurationManager.AppSettings["CompanyName"];
            if (!string.IsNullOrEmpty(company))
            {
                CompanyName = company;
            }
            var appLogo = WebConfigurationManager.AppSettings["AppLogo"];
            if (!string.IsNullOrEmpty(appLogo))
            {
                AppLogo = appLogo;
            }
        }
    }
}