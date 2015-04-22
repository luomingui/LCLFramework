using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UIShell.RbacPermissionService;
using UIShell.RbacManagementPlugin.ViewModels;
using LCL.MvcExtensions;
using LCL;
using System.Web.Security;
using LCL.Caching;
using LCL.Repositories;
using LCL.Specifications;
using LCL.MetaModel;

namespace UIShell.RbacManagementPlugin.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        public ActionResult Login()
        {
            return View();
        }
        // Home/Index
        [HttpPost, ActionName("Login")]
        public ActionResult Login(Account account)
        {
            //if (Session["ValidateCode"].ToString() != account.VerifyCode)
            //{
            //    ModelState.AddModelError("VerifyCode", "validate code is error");
            //    return View();
            //}
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (string.IsNullOrWhiteSpace(account.Name))
            {
                ModelState.AddModelError("Name", "消息：用户名不能为空！");
                return View();
            }
            if (string.IsNullOrWhiteSpace(account.Password))
            {
                ModelState.AddModelError("Name", "消息：密码不能为空！");
                return View();
            }
            if (account.Name == "admin" && account.Password == "123456")
            {
                MemoryCacheHelper.RemoveCache(account.Name);
                #region Cookies
                //验证成功，用户名密码正确，构造用户数据（可以添加更多数据，这里只保存用户Id）
                var userData = new MyUserDataPrincipal(new LCLIdentity { Name = account.Name, IsAuthenticated = true });
                userData.UserId = Guid.Empty;
                userData.UnitName = "admin";
                userData.OrgId = Guid.Empty;
                //保存Cookie
                MyFormsAuthentication<MyUserDataPrincipal>.SetAuthCookie(account.Name, userData, account.RememberMe, Response.Cookies);
                #endregion
                return RedirectIndex();
            }
            else
            {
                #region 读库
                return RedirectIndex();
                #endregion
            }

        }
        private RedirectResult RedirectIndex()
        {
            //跳转
            string url = PageFlowService.PageNodes.FindPageNode("LayoutHome").Value;
            return Redirect("../../" + url);
        }
        public ActionResult GetValidateCode()
        {
            ValidateCode vCode = new ValidateCode();
            string code = vCode.CreateValidateCode(5);
            Session["ValidateCode"] = code;
            byte[] bytes = vCode.CreateValidateGraphic(code);
            return File(bytes, @"image/jpeg");
        }
    }
}