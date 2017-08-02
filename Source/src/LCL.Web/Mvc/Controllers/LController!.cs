using LCL.Domain.Entities;
using LCL.Domain.Services;
using LCL.Web.Mvc.ViewEngines;
using LCL.Web.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace LCL.Web.Mvc.Controllers
{

    public class LController<TAggregateRoot> : LController where TAggregateRoot : class,IAggregateRoot
    {
        #region Ajax CURD
        public virtual LJsonResult GetByAjax()
        {
            var modelList = this.Repository<TAggregateRoot>().FindAll().ToList();

            var json = new LJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.Data = modelList;

            return json;
        }
        public virtual LJsonResult GetByIdAjax(Guid id)
        {
            var result = new LResult(true);
            try
            {
                var model = this.Repository<TAggregateRoot>().Get(id);
                result.Data = model;
            }
            catch (Exception)
            {
                result = new LResult(false);
            }
            var json = new LJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.Data = result;
            return json;
        }
        public virtual LJsonResult GetPageEasyUIAjax(int? page, int? rows)
        {
            /*
   1、easyui-datagrid 分页接收参数
   page 接受客户端的页码，对应的就是用户选择或输入的pageNumber（按照上图的例子，用户点了下一页，传到服务器端就是2）
   rows 接受客户端的每页记录数，对应的就是pageSize  （用户在下拉列表选择每页显示30条记录，传到服务器就是30）
                */
            if (!page.HasValue) page = 1;
            if (!rows.HasValue) rows =10;
            int pageNumber = page.Value;
            int pageSize = rows.Value;

            var modelList = this.Repository<TAggregateRoot>().FindAll(p => p.ID, SortOrder.Descending, pageNumber, pageSize);

            var gridModel = new EasyUIGridModel<TAggregateRoot>();
            if (modelList != null)
            {
                gridModel.total = modelList.Count;
                gridModel.rows = modelList.Data.ToList<TAggregateRoot>();
            }
            var json = new LJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.Data = gridModel;

            return json;
        }
        public virtual LJsonResult GetByPageAjax(int? page, int? rows)
        {
            if (!page.HasValue) page = 1;
            if (!rows.HasValue) rows = 10;
            int pageNumber = page.Value;
            int pageSize = rows.Value;
            var modelList = this.Repository<TAggregateRoot>().FindAll(p => p.ID, SortOrder.Descending, pageNumber, pageSize);
            var json = new LJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.Data = modelList;

            return json;
        }
        [HttpPost]
        public virtual LJsonResult AddAjax(TAggregateRoot model)
        {
            var result = new LResult(true);
            try
            {
                var customerRepository = this.Repository<TAggregateRoot>();
                customerRepository.Insert(model);
                customerRepository.Context.Commit();
                customerRepository.Context.Dispose();
            }
            catch (Exception)
            {
                result = new LResult(false);
            }

            var json = new LJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.Data = result;

            return json;
        }
        [HttpPost]
        public virtual LJsonResult DeleteAjax(TAggregateRoot model)
        {
            var result = new LResult(true);
            try
            {
                var customerRepository = this.Repository<TAggregateRoot>();

                var entity = customerRepository.Get(model.ID);
                if (entity != null)
                    customerRepository.Delete(entity);
                customerRepository.Context.Commit();
                customerRepository.Context.Dispose();
            }
            catch (Exception)
            {
                result = new LResult(false);
            }
            var json = new LJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.Data = result;
            return json;
        }
        [HttpPost]
        public virtual LJsonResult EditAjax(TAggregateRoot model)
        {
            var result = new LResult(true);
            try
            {
                this.Repository<TAggregateRoot>().Update(model);
            }
            catch (Exception)
            {
                result = new LResult(false);
            }
            var json = new LJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.Data = result;
            return json;
        }
        [HttpPost]
        public virtual LJsonResult DeleteListAjax(IList<Guid> idList)
        {
            var result = new LResult(true);
            try
            {
                for (int i = 0; i < idList.Count; i++)
                {
                    var entity = this.Repository<TAggregateRoot>().Get(idList[i]);
                    this.Repository<TAggregateRoot>().Delete(entity);
                }
            }
            catch (Exception)
            {
                result = new LResult(false);
            }
            var json = new LJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.Data = result;
            return json;
        }
  
        #endregion

        #region CURD
        public virtual ActionResult List(int? currentPageNum, int? pageSize, FormCollection collection)
        {
            if (!currentPageNum.HasValue) currentPageNum = 1;
            if (!pageSize.HasValue) pageSize = 10;
            int pageNum = currentPageNum.Value;

            var pageList = this.Repository<TAggregateRoot>().FindAll(p => p.ID, LCL.SortOrder.Descending, currentPageNum.Value, pageSize.Value);
            return View(pageList);
        }
        [Permission("首页", "Index")]
        public virtual ActionResult Index(int? currentPageNum, int? pageSize, FormCollection collection)
        {
            return View();
        }
        public virtual ActionResult AddOrEdit(int? currentPageNum, int? pageSize, Guid? id, FormCollection collection)
        {
            if (!currentPageNum.HasValue) currentPageNum = 1;
            if (!pageSize.HasValue) pageSize =10;
            int pageNum = currentPageNum.Value;

            if (!id.HasValue)
            {
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
                ViewBag.Action = "Edit";
                var repo =this.Repository<TAggregateRoot>();
                var village = repo.Get(id.Value);
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
        [HttpPost]
        public virtual ActionResult Delete(TAggregateRoot model, int? currentPageNum, int? pageSize, FormCollection collection)
        {
            if (!currentPageNum.HasValue)
            {
                currentPageNum = 1;
            }
            if (!pageSize.HasValue)
            {
                pageSize = 10;
            }
            var repo = this.Repository<TAggregateRoot>();
            var entity = repo.Get(model.ID);
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
            this.Repository<TAggregateRoot>().Insert(model.Entity);
            this.Repository<TAggregateRoot>().Context.Commit();

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
            this.Repository<TAggregateRoot>().Update(model.Entity);
            this.Repository<TAggregateRoot>().Context.Commit();

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
        public ActionResult NoPermissionView()
        {
            return View(PermissionAttribute.NoPermissionView);
        }
        #endregion
    }
}
