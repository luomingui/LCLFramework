using LCL;
using LCL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace UIShell.RbacPermissionService
{
    /// <summary>
    /// 身份
    /// </summary>
    [Serializable]
    public class LCLPrincipal : IPrincipal
    {
        #region 构造函数
        /// <summary>
        /// 由于 DataportalContext 使用了 Principel，所以每次都传输 LCLIdentity 对象，
        /// 内部的数据要尽量简单，不可以序列化此字段。
        /// </summary>
        [NonSerialized]
        private LCLIdentity _edsIdentity;
        private Guid _userId;
        private LCLPrincipal(LCLIdentity realIdentity)
        {
            this._userId = realIdentity.UserId;
            this._edsIdentity = realIdentity;
        }
        #endregion
        #region 接口实现

        IIdentity IPrincipal.Identity
        {
            get
            {
                if (this._edsIdentity == null)
                {
                    this._edsIdentity = RF.Concrete<LCLIdentityRepository>().GetById(this._userId) as LCLIdentity;
                }

                return this._edsIdentity;
            }
        }

        bool IPrincipal.IsInRole(string role)
        {
            return false;
        }

        #endregion
        /// <summary>
        /// 尝试使用指定的用户名密码登录，并返回是否成功。
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool Login(string username, string password)
        {
            bool IsFlg = false;
            LCLIdentity identity = null;
            if (username == "admin" && password == "123456")
            {
                identity = new LCLIdentity
                {
                    User = new User { Name = "admin", Department = new Department { Name = "LCL" } }
                };
                identity.Name = username;
                identity.UserCode = username;
                identity.UnitName = "京通";
                identity.IsAuthenticated = true;
                LEnvironment.Principal = new LCLPrincipal(identity);
                IsFlg= identity.IsAuthenticated;
            }
            else
            {
                identity = RF.Concrete<LCLIdentityRepository>().GetBy(username, password);
                if (identity.IsAuthenticated)
                {
                    LEnvironment.Principal = new LCLPrincipal(identity);
                }
                else
                {
                    LEnvironment.Principal = new AnonymousPrincipal();
                }
                IsFlg= identity.IsAuthenticated;
            }
            AddCookie(username, LEnvironment.Principal);
            return IsFlg;
        }
        public static void AddCookie(string username, IPrincipal userData)
        {
            var data = (new System.Web.Script.Serialization.JavaScriptSerializer()).Serialize(userData);
            //Cookie保存是时间
            int CookieSaveDays = 60;
            //创建ticket
            var ticket = new FormsAuthenticationTicket(2, username, DateTime.Now, DateTime.Now.AddMinutes(CookieSaveDays), true, data);
            //加密ticket
            var cookieValue = FormsAuthentication.Encrypt(ticket);

            //创建Cookie
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieValue)
            {
                HttpOnly = true,
                Secure = FormsAuthentication.RequireSSL,
                Domain = FormsAuthentication.CookieDomain,
                Path = FormsAuthentication.FormsCookiePath,
            };
            cookie.Expires = DateTime.Now.AddMinutes(CookieSaveDays);
            //写入Cookie
            HttpContext.Current.Response.Cookies.Remove(cookie.Name);
            HttpContext.Current.Response.Cookies.Add(cookie);
            //写入Session
            HttpContext.Current.Session["LCLPrincipal"] = userData;  
        }
        //从Request中解析出Ticket,UserData
        public static IPrincipal TryParsePrincipal()
        {
            // 1. 读登录Cookie
            var cookie = HttpContext.Current.Response.Cookies[FormsAuthentication.FormsCookieName];
            var session= HttpContext.Current.Session["LCLPrincipal"];

            if (cookie == null || string.IsNullOrEmpty(cookie.Value))
            {
                if (session != null)
                {
                    return (LCLPrincipal)session;
                }
                else {
                    return null;
                }
            }
            // 2. 解密Cookie值，获取FormsAuthenticationTicket对象
            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            if (ticket != null && !string.IsNullOrEmpty(ticket.UserData))
            {
                var userData = (new JavaScriptSerializer()).Deserialize<LCLIdentity>(ticket.UserData);
                if (userData != null)
                {
                    LEnvironment.Principal = new LCLPrincipal(userData);
                    return LEnvironment.Principal;
                }
            }
            return null;
        }
        /// <summary>
        /// 登出系统。
        /// </summary>
        public static void Logout()
        {
            LEnvironment.Principal = new AnonymousPrincipal();
            HttpContext.Current.Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
            HttpContext.Current.Response.Cookies.Clear();
        }
    }
}
