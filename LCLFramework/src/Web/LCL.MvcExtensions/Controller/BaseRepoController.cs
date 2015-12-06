using LCL.ComponentModel;
using LCL.MetaModel;
using LCL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LCL.MvcExtensions
{
    [Authorize]
    public class BaseRepoController<TAggregateRoot> : BaseController where TAggregateRoot : class, IEntity, new()
    {
        public IRepository<TAggregateRoot> repo = RF.FindRepo<TAggregateRoot>();
        public bool Check(string permissionId)
        {
            string areaName = this.ControllerContext.RouteData.GetAreaName();
            string controllerName = this.GetType().Name.Replace("Controller", "");
            return PermissionMgr.HasCommand(areaName, controllerName, permissionId);
        }
        public ActionResult NoPermissionView()
        {
            return View(PermissionAttribute.NoPermissionView);
        }
        public virtual ActionResult List(int? currentPageNum, int? pageSize, FormCollection collection)
        {
            if (!currentPageNum.HasValue)currentPageNum = 1;
            if (!pageSize.HasValue)pageSize = PagedResult<TAggregateRoot>.DefaultPageSize;
            int pageNum = currentPageNum.Value;

            var pageList = repo.FindAll(p => p.UpdateDate, LCL.SortOrder.Descending, currentPageNum.Value, pageSize.Value);
            return View(pageList);
        }
        [Permission("首页", "Index")]
        public virtual ActionResult Index(int? currentPageNum, int? pageSize, FormCollection collection)
        {
            if (!Check("Index"))
                return NoPermissionView();
            return View();
        }
        public virtual ActionResult AddOrEdit(int? currentPageNum, int? pageSize, Guid? id, FormCollection collection)
        {
            if (!currentPageNum.HasValue) currentPageNum = 1;
            if (!pageSize.HasValue) pageSize = PagedResult<TAggregateRoot>.DefaultPageSize;
            int pageNum = currentPageNum.Value;

            if (!id.HasValue)
            {
                if (!Check("Add"))
                {
                    return NoPermissionView();
                }
                ViewBag.Action = "Add";

                return View(new AddOrEditViewModel<TAggregateRoot>
                {
                    Added = true,
                    Entity = null,
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
                ViewBag.Action = "Edit";
                var repo = RF.FindRepo<TAggregateRoot>();
                var village = repo.GetByKey(id.Value);
                return View(new AddOrEditViewModel<TAggregateRoot>
                {
                    Added = false,
                    Entity = village,
                    CurrentPageNum = currentPageNum.Value,
                    PageSize = pageSize.Value
                });
            }
        }
        [Permission("删除", "Delete")]
        public virtual ActionResult Delete(TAggregateRoot model, int? currentPageNum, int? pageSize, FormCollection collection)
        {
            if (!Check("Delete"))
            {
                return NoPermissionView();
            }
            if (!currentPageNum.HasValue)
            {
                currentPageNum = 1;
            }
            if (!pageSize.HasValue)
            {
                pageSize = PagedResult<TAggregateRoot>.DefaultPageSize;
            }
            var entity = repo.GetByKey(model.ID);
            repo.Delete(entity);
            repo.Context.Commit();

            return RedirectToAction("Index", new { currentPageNum = currentPageNum, pageSize = pageSize });
        }
        [Permission("添加", "Add")]
        [HttpPost]
        public virtual ActionResult Add(AddOrEditViewModel<TAggregateRoot> model, FormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                ModelErrorMsg();
                return View("AddOrEdit", model);
            }
            repo.Create(model.Entity);
            repo.Context.Commit();

            return RedirectToAction("Index", new { currentPageNum = model.CurrentPageNum, pageSize = model.PageSize });
        }
        [Permission("修改", "Edit")]
        [HttpPost]
        public virtual ActionResult Edit(AddOrEditViewModel<TAggregateRoot> model, FormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                ModelErrorMsg();
                return View("AddOrEdit", model);
            }
            repo.Update(model.Entity);
            repo.Context.Commit();

            return RedirectToAction("Index", new { currentPageNum = model.CurrentPageNum, pageSize = model.PageSize });
        }
        public void ModelErrorMsg()
        {
            StringBuilder sb = new StringBuilder();

            string msg = "BaseRepoController ModelErrorMsg class:{0} property:{1} error:{2}";
            //获取所有错误的Key
            List<string> Keys = ModelState.Keys.ToList();
            //获取每一个key对应的ModelStateDictionary
            foreach (var key in Keys)
            {
                var errors = ModelState[key].Errors.ToList();
                //将错误描述添加到sb中
                foreach (var error in errors)
                {
                    sb.AppendLine(string.Format(msg, typeof(TAggregateRoot).Name, key, error.ErrorMessage));
                }
            }
            Logger.LogDebug(sb.ToString());
        }

        #region Ajax
        [HttpPost]
        public virtual CustomJsonResult AjaxGetBy()
        {
            var modelList = repo.FindAll().ToList();

            var json = new CustomJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.Data = modelList;

            return json;
        }
        [HttpPost]
        public virtual CustomJsonResult AjaxGetByPage(int? page, int? rows)
        {
            /*
1、easyui-datagrid 分页接收参数
page 接受客户端的页码，对应的就是用户选择或输入的pageNumber（按照上图的例子，用户点了下一页，传到服务器端就是2）
rows 接受客户端的每页记录数，对应的就是pageSize  （用户在下拉列表选择每页显示30条记录，传到服务器就是30）
             */
            if (!page.HasValue)
            {
                page = 1;
            }
            if (!rows.HasValue)
            {
                rows = PagedResult<TAggregateRoot>.DefaultPageSize;
            }
            int pageNumber = page.Value;
            int pageSize = rows.Value;
            var modelList = repo.FindAll(p => p.UpdateDate, LCL.SortOrder.Descending, pageNumber, pageSize);

            var json = new CustomJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.Data = modelList;

            return json;
        }
        [HttpPost]
        public virtual CustomJsonResult AjaxAdd(TAggregateRoot model)
        {
            var result = new Result(true);
            try
            {
                repo.Create(model);
                repo.Context.Commit();
            }
            catch (Exception)
            {
                result = new Result(false);
            }
            var json = new CustomJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.Data = result;
            return json;
        }
        [HttpPost]
        public virtual CustomJsonResult AjaxEdit(TAggregateRoot model)
        {
            var result = new Result(true);
            try
            {
                model.UpdateDate = DateTime.Now;
                repo.Update(model);
                repo.Context.Commit();
            }
            catch (Exception)
            {
                result = new Result(false);
            }
            var json = new CustomJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.Data = result;
            return json;
        }
        [HttpPost]
        public virtual CustomJsonResult AjaxDelete(TAggregateRoot model)
        {
            var result = new Result(true);
            try
            {
                var entity = repo.GetByKey(model.ID);
                repo.Delete(entity);
                repo.Context.Commit();
            }
            catch (Exception)
            {
                result = new Result(false);
            }
            var json = new CustomJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.Data = result;
            return json;
        }
        [HttpPost]
        public virtual CustomJsonResult AjaxDeleteList(IList<Guid> idList)
        {
            var result = new Result(true);
            try
            {
                for (int i = 0; i < idList.Count; i++)
                {
                    var entity = repo.GetByKey(idList[i]);
                    repo.Delete(entity);
                }
                repo.Context.Commit();
            }
            catch (Exception)
            {
                result = new Result(false);
            }
            var json = new CustomJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.Data = result;
            return json;
        }
        [HttpPost]
        public virtual CustomJsonResult AjaxGetByModel(Guid id)
        {
            var result = new Result(true);
            try
            {
                var model = repo.GetByKey(id);
                result.DataObject = model;
            }
            catch (Exception)
            {
                result = new Result(false);
            }
            var json = new CustomJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.Data = result;
            return json;
        }
        #endregion
    }
}
