/******************************************************* 
*  
* 作者：罗敏贵 
* 说明： 行政区域
* 运行环境：.NET 4.5.0 
* 版本号：1.0.0 
*  
* 历史记录： 
*    创建文件 罗敏贵 20154月18日 星期六
*  
*******************************************************/
using LCL.MvcExtensions;
using LCL.Repositories;
using LCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UIShell.RbacPermissionService;
using LCL.Specifications;
using System.Data;

namespace UIShell.RbacManagementPlugin.Controllers
{
    public class XzqyController : RbacController<Xzqy>
    {
        IXzqyRepository repo = RF.Concrete<IXzqyRepository>();
        public XzqyController()
        {

        }
        public override System.Web.Mvc.ActionResult Index(int? currentPageNum, int? pageSize, System.Web.Mvc.FormCollection collection)
        {
            if (!currentPageNum.HasValue)
            {
                currentPageNum = 1;
            }
            if (!pageSize.HasValue)
            {
                pageSize = PagedListViewModel<Xzqy>.DefaultPageSize;
            }
            int pageNum = currentPageNum.Value;

            var list = repo.GetFull();

            var contactLitViewModel = new PagedListViewModel<Xzqy>(pageNum, pageSize.Value, list.ToList());

            return View(contactLitViewModel);
        }
        public override ActionResult AddOrEdit(int? currentPageNum, int? pageSize, Guid? id, FormCollection collection)
        {
            if (!currentPageNum.HasValue)
            {
                currentPageNum = 1;
            }
            if (!pageSize.HasValue)
            {
                pageSize = PagedListViewModel<Xzqy>.DefaultPageSize;
            }
            if (!id.HasValue)
            {
                Guid pid = Guid.Parse(LRequest.GetString("PID"));
                var village = repo.GetByKey(pid);
                village.HelperCode = "";
                village.Name = "";
                return View(new AddOrEditViewModel<Xzqy>
                {
                    Added = true,
                    Entity = village,
                    CurrentPageNum = currentPageNum.Value,
                    PageSize = pageSize.Value
                });
            }
            else
            {
                var village = repo.GetByKey(id.Value);
                return View(new AddOrEditViewModel<Xzqy>
                {
                    Added = false,
                    Entity = village,
                    CurrentPageNum = currentPageNum.Value,
                    PageSize = pageSize.Value
                });
            }
        }
        public override ActionResult Add(AddOrEditViewModel<Xzqy> model, FormCollection collection)
        {

            if (!ModelState.IsValid)
            {
                return View("AddOrEdit", model);
            }

            int OrderBy = Convert.ToInt32(DbFactory.DBA.QueryValue("SELECT ISNULL(MAX(OrderBy),0)+1 OrderBy FROM Xzqy  WHERE ParentId='" + model.Entity.ParentId + "'"));
            model.Entity.OrderBy = OrderBy;
            if (model.Entity.ParentId != Guid.Empty)
            {
                model.Entity.NodePath = model.Entity.NodePath + "\\" + model.Entity.Name;
                model.Entity.Level = model.Entity.Level + 1;
                model.Entity.IsLast = false;
            }
            else
            {
                model.Entity.NodePath = model.Entity.Name;
            }
            model.Entity.ID = Guid.NewGuid();
            model.Entity.AddDate = DateTime.Now;
            model.Entity.UpdateDate = DateTime.Now;
            repo.Create(model.Entity);
            repo.Context.Commit();

            return RedirectToAction("Index", new { currentPageNum = model.CurrentPageNum, pageSize = model.PageSize });
        }
        public override ActionResult Delete(Xzqy village, int? currentPageNum, int? pageSize, FormCollection collection)
        {
            village= repo.GetByKey(village.ID);
            DbFactory.DBA.ExecuteText("DELETE Xzqy WHERE NodePath LIKE '" + village.NodePath + "%'");

            return RedirectToAction("Index", new { currentPageNum = currentPageNum.Value, pageSize = pageSize.Value });
        }
    }
}

