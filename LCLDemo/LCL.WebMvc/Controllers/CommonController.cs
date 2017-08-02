using LCL.Domain.Entities;
using LCL.Web.Mvc.Controllers;
using System;
using System.Web.Mvc;
using System.Web.Security;

namespace LCL.WebMvc.Controllers
{
    public class CommonController : LController
    {
        //footer
        [ChildActionOnly]
        public ActionResult Footer()
        {
            return PartialView();
        }
        //sitemap page
        public ActionResult Sitemap()
        {
            return View();
        }


        #region Protected Properties
        /// <summary>
        /// 获取当前登录用户的ID值。
        /// </summary>
        protected Guid UserID
        {
            get
            {
                if (Session["UserID"] != null)
                    return new Guid(Session["UserID"].ToString());
                else
                {
                    var user = Membership.GetUser(User.Identity.Name) as LCLMembershipUser;
                    
                    Session["UserID"] = user.ID;
                    return new Guid(user.ID.ToString());
                }
            }
        }
        #endregion
    }

    public class CommonController<TAggregateRoot>:LController<TAggregateRoot> 
        where TAggregateRoot : class, IAggregateRoot

    {
        #region Protected Properties
        /// <summary>
        /// 获取当前登录用户的ID值。
        /// </summary>
        protected string UserID
        {
            get
            {
                if (Session["UserID"] != null)
                    return Session["UserID"].ToString();
                else
                {
                    var user = Membership.GetUser(User.Identity.Name) as LCLMembershipUser;

                    Session["UserID"] = user.ID;
                    return user.ID.ToString();
                }
            }
        }
        #endregion
    }
}