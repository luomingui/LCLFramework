using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace LCL.MvcExtensions
{
    //身份验证类
    public class MyFormsAuthentication<TUserData> where TUserData : class,IPrincipal, new()
    {
        //Cookie保存是时间
        private const int CookieSaveDays = 1;

        //用户登录成功时设置Cookie
        public static void SetAuthCookie(string username, TUserData userData, bool rememberMe, HttpCookieCollection Cookies)
        {
            if (userData == null)
                throw new ArgumentNullException("userData");

            var data = (new JavaScriptSerializer()).Serialize(userData);
            LCL.LEnvironment.Principal = userData;
            //创建ticket
            var ticket = new FormsAuthenticationTicket(
                2, username, DateTime.Now, DateTime.Now.AddDays(CookieSaveDays), rememberMe, data);

            //加密ticket
            var cookieValue = FormsAuthentication.Encrypt(ticket);

            //创建Cookie
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieValue);
            if (rememberMe)
                cookie.Expires = DateTime.Now.AddDays(CookieSaveDays);

            //写入Cookie
            Cookies.Remove(cookie.Name);
            Cookies.Add(cookie);
        }

        //从Request中解析出Ticket,UserData
        public static MyFormsPrincipal<TUserData> TryParsePrincipal(HttpRequestBase request)
        {
            if (request == null)
                throw new ArgumentNullException("request");

            // 1. 读登录Cookie
            var cookie = request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie == null || string.IsNullOrEmpty(cookie.Value)) return null;

            try
            {
                // 2. 解密Cookie值，获取FormsAuthenticationTicket对象
                var ticket = FormsAuthentication.Decrypt(cookie.Value);
                if (ticket != null && !string.IsNullOrEmpty(ticket.UserData))
                {
                    var userData = (new JavaScriptSerializer()).Deserialize<TUserData>(ticket.UserData);
                    if (userData != null)
                    {
                        return new MyFormsPrincipal<TUserData>(ticket, userData);
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                /* 有异常也不要抛出，防止攻击者试探。 */
                return null;
            }
        }
        public static MyFormsPrincipal<TUserData> TryParsePrincipal(HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request");

            // 1. 读登录Cookie
            var cookie = request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie == null || string.IsNullOrEmpty(cookie.Value)) return null;

            try
            {
                // 2. 解密Cookie值，获取FormsAuthenticationTicket对象
                var ticket = FormsAuthentication.Decrypt(cookie.Value);
                if (ticket != null && !string.IsNullOrEmpty(ticket.UserData))
                {
                    var userData = (new JavaScriptSerializer()).Deserialize<TUserData>(ticket.UserData);
                    if (userData != null)
                    {
                        return new MyFormsPrincipal<TUserData>(ticket, userData);
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                /* 有异常也不要抛出，防止攻击者试探。 */
                return null;
            }
        }
    }
}
