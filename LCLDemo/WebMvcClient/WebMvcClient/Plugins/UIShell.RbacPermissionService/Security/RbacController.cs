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
            if (ex == null) ex = new Exception("未知错误.");
            return View("Error",ex);
        }
        public override ActionResult AddOrEdit(int? currentPageNum, int? pageSize, Guid? id, FormCollection collection)
        {
            if (!currentPageNum.HasValue)
            {
                currentPageNum = 1;
            }
            if (!pageSize.HasValue)
            {
                pageSize = PagedResult<TEntity>.DefaultPageSize;
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
        public override CustomJsonResult AjaxGetByPage(int? page, int? rows)
        {
            /*
1、easyui-datagrid 分页接收参数
page 接受客户端的页码，对应的就是用户选择或输入的pageNumber（按照上图的例子，用户点了下一页，传到服务器端就是2）
rows 接受客户端的每页记录数，对应的就是pageSize  （用户在下拉列表选择每页显示30条记录，传到服务器就是30）

返回到前台的值必须按照如下的格式包括 total and rows 
 */
            if (!page.HasValue)
            {
                page = 1;
            }
            if (!rows.HasValue)
            {
                rows = PagedResult<TEntity>.DefaultPageSize;
            }
            int pageNumber = page.Value;
            int pageSize = rows.Value;
            var modelList = repo.FindAll(p => p.UpdateDate, LCL.SortOrder.Descending, pageNumber, pageSize);

            var easyUIPages = new Dictionary<string, object>();
            easyUIPages.Add("total", modelList.PageCount);
            easyUIPages.Add("rows", modelList.PagedModels);
 
            var json = new CustomJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.Data = easyUIPages;
            return json;
        }
        #region 树形
        public List<EasyUITreeModel> easyTree = new List<EasyUITreeModel>();
        [HttpPost]
        public virtual CustomJsonResult AjaxEasyUITree_Department()
        {
            var repo = RF.Concrete<IDepartmentRepository>();
            var modelList = repo.FindAll().ToList();
            string id = LRequest.GetString("id");
            Guid guid = string.IsNullOrWhiteSpace(id) ? Guid.Empty : Guid.Parse(id);
            var list = modelList.Where(p => p.ParentId == guid);
            foreach (var item in list)
            {
                EasyUITreeModel model = new EasyUITreeModel();
                model.id = item.ID.ToString();
                model.attributes = item.ID.ToString();
                model.text = item.Name;
                model.parentId = item.ParentId.ToString();
                model.children = new List<EasyUITreeModel>();
                easyTree.Add(model);
            }
            var json = new CustomJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.Data = easyTree;
            return json;
        }
        [HttpPost]
        public CustomJsonResult AjaxEasyUITree_Xzqy()
        {
            string id = LRequest.GetString("id");
            Guid pid = string.IsNullOrWhiteSpace(id) ? Guid.Empty : Guid.Parse(id);
            var repo = RF.Concrete<IXzqyRepository>();
            ISpecification<Xzqy> spec = Specification<Xzqy>.Eval(p => p.ParentId == Guid.Empty);
            ISpecification<Xzqy> spec1 = Specification<Xzqy>.Eval(p => p.ParentId == pid);
            IEnumerable<Xzqy> list = repo.FindAll(spec);
            if (pid != Guid.Empty)
            {
                list = repo.FindAll(spec1);
            }
            easyTree = new List<EasyUITreeModel>();
            foreach (var item in list)
            {
                EasyUITreeModel model = new EasyUITreeModel();
                model.id = item.ID.ToString();
                model.attributes = item.ID.ToString();
                model.text = item.Name;
                model.parentId = item.ParentId.ToString();
                model.children = new List<EasyUITreeModel>();
                easyTree.Add(model);
            }
            var json = new CustomJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.Data = easyTree;
            return json;
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
        public void alert(string msg)
        {
            Response.Write("<script>$.messager.alert('消息','" + msg + "');</script>");
        }

    }
}