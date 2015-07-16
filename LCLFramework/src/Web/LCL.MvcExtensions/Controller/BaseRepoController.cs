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
        IRepository<TAggregateRoot> repo = RF.FindRepo<TAggregateRoot>();
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
            if (!currentPageNum.HasValue)
            {
                currentPageNum = 1;
            }
            if (!pageSize.HasValue)
            {
                pageSize = PagedListViewModel<TAggregateRoot>.DefaultPageSize;
            }
            int pageNum = currentPageNum.Value;
            var modelList = repo.FindAll().ToList();
            var pageList = new PagedListViewModel<TAggregateRoot>(pageNum, pageSize.Value, modelList.ToList());
            return View(pageList);
        }
        [Permission("首页", "Index")]
        public virtual ActionResult Index(int? currentPageNum, int? pageSize, FormCollection collection)
        {
            if (!Check("Index"))
            {
                return NoPermissionView();
            }
            return List(currentPageNum, pageSize, collection);
        }
        public virtual ActionResult AddOrEdit(int? currentPageNum, int? pageSize, Guid? id, FormCollection collection)
        {
            if (!currentPageNum.HasValue)
            {
                currentPageNum = 1;
            }
            if (!pageSize.HasValue)
            {
                pageSize = PagedListViewModel<TAggregateRoot>.DefaultPageSize;
            }
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
                    Entity=null,
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
                pageSize = PagedListViewModel<TAggregateRoot>.DefaultPageSize;
            }
            repo.Delete(model);
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
    }
}
