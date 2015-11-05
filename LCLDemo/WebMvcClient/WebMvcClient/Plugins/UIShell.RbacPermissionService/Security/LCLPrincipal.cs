using LCL;
using LCL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;
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
            AddCookie(username);
            if (username == "admin" && password == "123456")
            {
                var identity = new LCLIdentity
                {
                    User = new User { Name = "admin", Department = new Department { Name = "京通" } }
                };
                identity.Name = username;
                identity.UserCode = username;
                identity.UnitName = "京通";
                identity.IsAuthenticated = true;

                LEnvironment.Principal = new LCLPrincipal(identity);
                
                return identity.IsAuthenticated;
            }
            else
            {
                var identity = RF.Concrete<LCLIdentityRepository>().GetBy(username, password);
                if (identity.IsAuthenticated)
                {
                    LEnvironment.Principal = new LCLPrincipal(identity);
                }
                else
                {
                    LEnvironment.Principal = new AnonymousPrincipal();
                }
                return identity.IsAuthenticated;
            }
        }
        public static void AddCookie(string username)
        {
            //Cookie保存是时间
            int CookieSaveDays = 7;
            //创建ticket
            var ticket = new FormsAuthenticationTicket(2, username, DateTime.Now, DateTime.Now.AddDays(CookieSaveDays), true, username);
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
            cookie.Expires = DateTime.Now.AddDays(CookieSaveDays);
            //写入Cookie
            HttpContext.Current.Response.Cookies.Remove(cookie.Name);
            HttpContext.Current.Response.Cookies.Add(cookie);
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
