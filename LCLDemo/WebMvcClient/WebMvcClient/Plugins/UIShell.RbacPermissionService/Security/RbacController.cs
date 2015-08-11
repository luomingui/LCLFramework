using LCL;
using LCL.MvcExtensions;
using LCL.Repositories;
using LCL.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace UIShell.RbacPermissionService
{
    [ValidateInput(false)]
    [MyAuthorizeAttribute]
    public class RbacController : BaseController
    {
        string moduleId = "";
        public RbacController()
        {

        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            moduleId = filterContext.GetPluginSymbolicName();
            if (!string.IsNullOrWhiteSpace(LCL.LEnvironment.Principal.Identity.Name)
                && LCL.LEnvironment.Principal.Identity.Name.Length > 1)
            {

            }
            else
            {
                string loginUrl = FormsAuthentication.LoginUrl;
                filterContext.HttpContext.Response.Redirect(loginUrl, true);
            }
            base.OnActionExecuting(filterContext);
        }
        protected override void OnException(ExceptionContext filterContext)
        {
            Logger.LogError(filterContext.GetPluginSymbolicName() + "插件错误信息：", filterContext.Exception);

            // 标记异常已处理
            filterContext.ExceptionHandled = true;
            // 跳转到错误页
            filterContext.Result = new RedirectResult(Url.Action("Error"));
        }

        #region 行政区域
        [HttpPost]
        public JsonResult GetOrgChildList(Guid pid)
        {
            var repo = RF.Concrete<IXzqyRepository>();
            ISpecification<Xzqy> spec = Specification<Xzqy>.Eval(p => p.ParentId == Guid.Empty);
            ISpecification<Xzqy> spec1 = Specification<Xzqy>.Eval(p => p.ParentId == pid);
            IEnumerable<Xzqy> list = repo.FindAll(spec);
            if (pid != Guid.Empty)
            {
                list = repo.FindAll(spec1);
            }
            return Json(list.ToList(), JsonRequestBehavior.AllowGet);
        }
        #endregion

        public ActionResult Error()
        {
            return View("Error");
        }
        public void AddTLog(string title, string content, EnumLogType enumlogtype)
        {
            HttpBrowserCapabilitiesBase bc = HttpContext.Request.Browser;
            string Browser = "您好，您正在使用 " + bc.Browser + " v." + bc.Version + ",你的运行平台是 " + bc.Platform;
            var repo = RF.Concrete<ITLogRepository>();
            repo.Create(new TLog
            {
                ID = Guid.NewGuid(),
                UpdateDate = DateTime.Now,
                AddDate = DateTime.Now,
                UserName = LCL.LEnvironment.Identity.Name,
                LogType = enumlogtype,
                Title = ViewBag.Title,
                Content = content,
                IsActiveX = bc.ActiveXControls,
                Browser = Browser,
                ModuleName = moduleId,
                url = HttpContext.Request.Path,
                MachineName = Environment.MachineName,
                IP = HttpContext.Request.UserHostAddress,
            });
            repo.Context.Commit();
        }
    }
    [ValidateInput(false)]
    [MyAuthorizeAttribute]
    public class RbacController<TEntity> : BaseRepoController<TEntity> where TEntity : class, IEntity, new()
    {
        string moduleId = "";
        public RbacController()
        {

        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            moduleId = filterContext.GetPluginSymbolicName();
            if (!string.IsNullOrWhiteSpace(LCL.LEnvironment.Principal.Identity.Name)
                && LCL.LEnvironment.Principal.Identity.Name.Length > 1)
            {

            }
            else
            {
                string loginUrl = FormsAuthentication.LoginUrl;
                filterContext.HttpContext.Response.Redirect(loginUrl, true);
            }
            base.OnActionExecuting(filterContext);
        }
        protected override void OnException(ExceptionContext filterContext)
        {
            Logger.LogError(filterContext.GetPluginSymbolicName() + "插件错误信息：", filterContext.Exception);

            // 标记异常已处理
            filterContext.ExceptionHandled = true;
            // 跳转到错误页
            filterContext.Result = new RedirectResult(Url.Action("Error"));

        }
        public ActionResult Error(Exception ex)
        {
            return View("Error");
        }
        public override ActionResult AddOrEdit(int? currentPageNum, int? pageSize, Guid? id, FormCollection collection)
        {
            if (!currentPageNum.HasValue)
            {
                currentPageNum = 1;
            }
            if (!pageSize.HasValue)
            {
                pageSize = PagedListViewModel<TEntity>.DefaultPageSize;
            }
            if (!id.HasValue)
            {
                if (!Check("Add"))
                {
                    return NoPermissionView();
                }
                return View(new AddOrEditViewModel<TEntity>
                {
                    Added = true,
                    Entity = new TEntity(),
                    CurrentPageNum = currentPageNum.Value,
                    PageSize = pageSize.Value
                });
            }
            else
            {
                if (!Check("Edit"))
                {
                    return NoPermissionView();
                }
                var repo = RF.FindRepo<TEntity>();
                var village = repo.GetByKey(id.Value);
                return View(new AddOrEditViewModel<TEntity>
                {
                    Added = false,
                    Entity = village,
                    CurrentPageNum = currentPageNum.Value,
                    PageSize = pageSize.Value
                });
            }
        }

        #region 行政区域
        [HttpPost]
        public JsonResult GetOrgChildList(Guid pid)
        {
            var repo = RF.Concrete<IXzqyRepository>();
            ISpecification<Xzqy> spec = Specification<Xzqy>.Eval(p => p.ParentId == Guid.Empty);
            ISpecification<Xzqy> spec1 = Specification<Xzqy>.Eval(p => p.ParentId == pid);
            IEnumerable<Xzqy> list = repo.FindAll(spec);
            if (pid != Guid.Empty)
            {
                list = repo.FindAll(spec1);
            }
            return Json(list.ToList(), JsonRequestBehavior.AllowGet);
        }
        #endregion

        public void AddTLog(string title, string content, EnumLogType enumlogtype)
        {

            HttpBrowserCapabilitiesBase bc = HttpContext.Request.Browser;
            string Browser = "您好，您正在使用 " + bc.Browser + " v." + bc.Version + ",你的运行平台是 " + bc.Platform;

            var repo = RF.Concrete<ITLogRepository>();
            repo.Create(new TLog
            {
                ID = Guid.NewGuid(),
                UpdateDate = DateTime.Now,
                AddDate = DateTime.Now,
                UserName = LCL.LEnvironment.Principal.Identity.Name,
                LogType = enumlogtype,
                Title = ViewBag.Title,
                Content = content,
                IsActiveX = bc.ActiveXControls,
                Browser = Browser,
                ModuleName = moduleId,
                MachineName = Environment.MachineName,
                IP = HttpContext.Request.UserHostAddress,
            });
            repo.Context.Commit();
        }

    }
}