using LCL;
using LCL.Caching;
using LCL.MvcExtensions;
using LCL.Repositories;
using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using UIShell.RbacPermissionService;

namespace UIShell.RbacManagementPlugin.Controllers
{
    public class AccountController : Controller
    {
        #region 登录
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost, ActionName("Login")]
        [BizActivityLog("登录系统", "Name")]
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
            MemoryCacheHelper.RemoveCacheAll();
            if (!LCLPrincipal.Login(account.Name, account.Password))
            {
                ModelState.AddModelError("Name", "消息：用户或密码错误，请重新输入！");
                return View();
            }
            else
            {
                return RedirectIndex();
            }
        }
        public JsonResult AjaxLogin(string name, string password)
        {
            var result = new Result(false);

            if (string.IsNullOrWhiteSpace(name))
            {
                result.Message = "消息：用户名不能为空！";
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                result.Message = "消息：密码不能为空！";
            }
            MemoryCacheHelper.RemoveCacheAll();
            if (!LCLPrincipal.Login(name, password))
            {
                result.Message = "消息：用户或密码错误，请重新输入！！";
            }
            else
            {
                result = new Result(true);
              
                var identity = LCL.LEnvironment.Principal.Identity as LCLIdentity;
                if (identity != null)
                {
                    if (identity.DepUserType == 2)
                    {
                        result.Message = "../../" + LCL.MvcExtensions.PageFlowService.PageNodes.FindPageNode("LayoutHome1").Value;
                    }
                    else
                    {
                        result.Message = "../../" + LCL.MvcExtensions.PageFlowService.PageNodes.FindPageNode("LayoutHome").Value;
                    }
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        private RedirectResult RedirectIndex()
        {
            //跳转
            string url = LCL.MvcExtensions.PageFlowService.PageNodes.FindPageNode("LayoutHome").Value;
            return Redirect("../../" + url);
        }
        #endregion

        #region 验证码
        public ActionResult GetValidateCode()
        {
            ValidateCode vCode = new ValidateCode();
            string code = vCode.CreateValidateCode(5);
            Session["ValidateCode"] = code;
            byte[] bytes = vCode.CreateValidateGraphic(code);
            return File(bytes, @"image/jpeg");
        }
        #endregion

        #region 退出
        public ActionResult LogOut()
        {
            try
            {
                Request.Cookies.Clear();
                LCLPrincipal.Logout();
            }
            catch (Exception ex)
            {
                Logger.LogError("退出系统", ex);
            }
            FormsAuthentication.SignOut();
            return Redirect(FormsAuthentication.LoginUrl);
        }
        #endregion

        #region 修改密码
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        [UIShell.RbacPermissionService.MyAuthorize]
        public ActionResult ChangePassword()
        {
            return View("ChangePassword");
        }
        [HttpPost, ActionName("ChangePassword")]
        [UIShell.RbacPermissionService.MyAuthorize]
        public ActionResult ChangePassword(UserChangePassword userChangePassword)
        {
            RF.Concrete<IUserRepository>().ChangeLoginPassword(userChangePassword);
            return View("ChangePassword");
        }
        #endregion

        #region 环境检测
        public JsonResult GetOrgChildList(Guid pid)
        {
            StringBuilder listMst = new StringBuilder();
            int clrv = Request.Browser.ClrVersion.Major;
            if (clrv <= 0)
            {
                listMst.AppendLine("没有安装.NETFramework2.0");
            }
            bool isAxc = Request.Browser.ActiveXControls;
            if (!isAxc)
            {
                listMst.AppendLine("不支持ActiveX控件请开启");
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}