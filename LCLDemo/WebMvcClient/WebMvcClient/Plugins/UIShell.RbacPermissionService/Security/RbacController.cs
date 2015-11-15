using LCL;
using LCL.MvcExtensions;
using LCL.Repositories;
using LCL.Specifications;
using System;
using System.Collections;
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
        string title = "";
        public RbacController()
        {
            title = ViewBag.Title;
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

        #region  error 
        private Exception _serverException;
        private Exception _baseException;
        protected override void OnException(ExceptionContext filterContext)
        {
            _serverException = filterContext.Exception;
            LogErrorRecursive(_serverException);
          
            //处理Ajax请求
            if (filterContext.HttpContext.Request.IsAjaxRequest() && filterContext.Exception != null)
            {
                HandleAjaxRequestError(filterContext);
            }
            //处理一般请求
            else
            {
                // 标记异常已处理
                filterContext.ExceptionHandled = true;
                // 跳转到错误页
                filterContext.Result = new RedirectResult(Url.Action("Error"));
            }
            filterContext.ExceptionHandled = true;
            base.OnException(filterContext);
        }
        public ActionResult Error(Exception ex)
        {
            if (ex == null) ex = new Exception("未知错误.");
            return View("Error", ex);
        }
        /// <summary>  
        /// 递归输出错误日志  
        /// </summary>  
        private void LogErrorRecursive(Exception ex)
        {
            if (ex.InnerException != null)
            {
                LogErrorRecursive(ex.InnerException);
            }
            else
            {
                _baseException = ex;
            }
            Logger.LogError(title + "插件错误信息：", ex);

        }
        /// <summary>  
        /// 绑定Ajax错误 - 返回错误消息  
        /// </summary>  
        private static void HandleAjaxRequestError(ExceptionContext filterContext)
        {
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
            filterContext.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
            filterContext.Result = new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new { filterContext.Exception.Message }
            };
        }
        #endregion  

        #region override
        public override ActionResult Index(int? currentPageNum, int? pageSize, FormCollection collection)
        {
            return base.Index(currentPageNum, pageSize, collection);
        }
        public override ActionResult Add(AddOrEditViewModel<TEntity> model, FormCollection collection)
        {
            return base.Add(model, collection);
        }
        public override ActionResult Edit(AddOrEditViewModel<TEntity> model, FormCollection collection)
        {
            return base.Edit(model, collection);
        }
        public override ActionResult Delete(TEntity model, int? currentPageNum, int? pageSize, FormCollection collection)
        {
            return base.Delete(model, currentPageNum, pageSize, collection);
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
        [Permission("添加", "Add")]
        [BizActivityLog("添加", "ID,Name")]
        public override CustomJsonResult AjaxAdd(TEntity model)
        {
            return base.AjaxAdd(model);
        }
        [Permission("删除", "Delete")]
        [BizActivityLog("删除", "ID,Name")]
        public override CustomJsonResult AjaxDelete(TEntity model)
        {
            return base.AjaxDelete(model);
        }
        [Permission("修改", "Edit")]
        [BizActivityLog("修改", "ID,Name")]
        public override CustomJsonResult AjaxEdit(TEntity model)
        {
            return base.AjaxEdit(model);
        }
        [Permission("首页", "Index")]
        [BizActivityLog("首页", "rows,page")]
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

            var json = new CustomJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            try
            {
                var modelList = repo.FindAll(p => p.UpdateDate, LCL.SortOrder.Descending, pageNumber, pageSize);
                var easyUIPages = new Dictionary<string, object>();
                easyUIPages.Add("total", modelList.PageCount);
                easyUIPages.Add("rows", modelList.PagedModels);

                json.Data = easyUIPages;
            }
            catch (Exception ex)
            {
                Logger.LogError("AjaxGetByPage:", ex);
            }
            return json;
        }
        #endregion

        #region 树形
        [HttpPost]
        public virtual CustomJsonResult AjaxEasyUITree_Department()
        {
            var repo = RF.Concrete<IDepartmentRepository>();
            var modelList = repo.FindAll().ToList();
            string id = LRequest.GetString("id");
            Guid guid = string.IsNullOrWhiteSpace(id) ? Guid.Empty : Guid.Parse(id);
            var list = modelList.Where(p => p.ParentId == guid);
            List<EasyUITreeModel> easyTree = new List<EasyUITreeModel>();
            int i = 0;
            foreach (var item in list)
            {
                EasyUITreeModel model = new EasyUITreeModel();
                model.id = item.ID.ToString();
                model.text = item.Name;
                model.iconCls = (item.DepartmentType == DepartmentType.公司) || item.Name != "" ? "icon-company" : "";
                model.parentId = item.ParentId.ToString();
                model.parentName = repo.GetByName(item.ParentId);

                model.attributes.Add("Xzqy", item.Xzqy != null ? item.Xzqy.ID.ToString() : "");
                model.attributes.Add("OfficePhone", item.OfficePhone);
                model.attributes.Add("Address", item.Address);
                model.attributes.Add("Source", item.Source);
                model.attributes.Add("Remark", item.Remark);
                model.attributes.Add("DepartmentType", item.DepartmentType == DepartmentType.公司 ? (int)DepartmentType.公司 : (int)DepartmentType.部门);

                model.attributes.Add("IsLast", item.IsLast);
                model.attributes.Add("Level", item.Level);
                model.attributes.Add("NodePath", item.NodePath);
                model.attributes.Add("OrderBy", item.OrderBy);
                if (i == 0)
                    model.Checked = true;
                easyTree.Add(model);
                i++;
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
            IEnumerable<Xzqy> list = repo.FindAll(spec).ToList();
            if (pid != Guid.Empty)
            {
                list = repo.FindAll(spec1).ToList();
            }
            List<EasyUITreeModel> easyTree = new List<EasyUITreeModel>();
            int i = 0;
            foreach (var item in list)
            {
                EasyUITreeModel model = new EasyUITreeModel();
                model.id = item.ID.ToString();

                model.text = item.Name;
                model.parentId = item.ParentId.ToString();
                model.parentName = repo.GetByName(item.ParentId);

                model.attributes.Add("HelperCode", item.HelperCode != "" ? item.HelperCode : "-1");
                model.attributes.Add("Intro", item.Intro != "" ? item.Intro : "");

                model.attributes.Add("IsLast", item.IsLast);
                model.attributes.Add("Level", item.Level);
                model.attributes.Add("NodePath", item.NodePath);
                model.attributes.Add("OrderBy", item.OrderBy);
                if (i == 0)
                    model.Checked = true;

                easyTree.Add(model);
                i++;
            }
            var json = new CustomJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.Data = easyTree;
            return json;
        }
        [HttpPost]
        public virtual CustomJsonResult AjaxEasyUITree_DictType()
        {
            string id = LRequest.GetString("id");
            Guid pid = string.IsNullOrWhiteSpace(id) ? Guid.Empty : Guid.Parse(id);
            var repo = RF.Concrete<IDictTypeRepository>();
            var spec = Specification<DictType>.Eval(p => p.ParentId == Guid.Empty);
            var spec1 = Specification<DictType>.Eval(p => p.ParentId == pid);
            var list = repo.FindAll(spec).ToList();
            if (pid != Guid.Empty)
            {
                list = repo.FindAll(spec1).ToList();
            }
            List<EasyUITreeModel> easyTree = new List<EasyUITreeModel>();
            int i = 0;
            foreach (var item in list)
            {
                EasyUITreeModel model = new EasyUITreeModel();
                model.id = item.ID.ToString();
                model.text = item.Code;
                model.parentId = item.ParentId.ToString();
                model.parentName = repo.GetByName(item.ParentId);

                model.attributes.Add("IsLast", item.IsLast);
                model.attributes.Add("Level", item.Level);
                model.attributes.Add("NodePath", item.NodePath);
                model.attributes.Add("OrderBy", item.OrderBy);
                if (i == 0)
                    model.Checked = true;
                easyTree.Add(model);
                i++;
            }
            var json = new CustomJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.Data = easyTree;
            return json;
        }
        [HttpPost]
        public virtual CustomJsonResult AjaxEasyUITree_Dictionary()
        {
            string id = LRequest.GetString("id");
            Guid pid = string.IsNullOrWhiteSpace(id) ? Guid.Empty : Guid.Parse(id);
            var repo = RF.Concrete<IDictionaryRepository>();
            var spec = Specification<Dictionary>.Eval(p => p.ParentId == Guid.Empty);
            var spec1 = Specification<Dictionary>.Eval(p => p.ParentId == pid);
            var list = repo.FindAll(spec).ToList();
            if (pid != Guid.Empty)
            {
                list = repo.FindAll(spec1).ToList();
            }
            List<EasyUITreeModel> easyTree = new List<EasyUITreeModel>();
            int i = 0;
            foreach (var item in list)
            {
                EasyUITreeModel model = new EasyUITreeModel();
                model.id = item.ID.ToString();
                model.text = item.Name;
                model.parentId = item.ParentId.ToString();
                model.parentName = repo.GetByName(item.ParentId);

                model.attributes.Add("IsLast", item.IsLast);
                model.attributes.Add("Level", item.Level);
                model.attributes.Add("NodePath", item.NodePath);
                model.attributes.Add("OrderBy", item.OrderBy);
                if (i == 0)
                    model.Checked = true;
                easyTree.Add(model);
                i++;
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