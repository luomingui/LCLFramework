/******************************************************* 
*  
* 作者：罗敏贵 
* 说明： 部门
* 运行环境：.NET 4.5.0 
* 版本号：1.0.0 
*  
* 历史记录： 
*    创建文件 罗敏贵 20154月22日 星期三
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

namespace UIShell.RbacManagementPlugin.Controllers
{
    public class DepartmentController : RbacController<Department>
    {
        public DepartmentController()
        {
           
        }
        public override System.Web.Mvc.ActionResult Index(int? currentPageNum, int? pageSize, System.Web.Mvc.FormCollection collection)
        {
            if (!currentPageNum.HasValue)
                currentPageNum = 1;
            if (!pageSize.HasValue)
                pageSize = PagedListViewModel<CompanyInfoPageListViewModel>.DefaultPageSize;

            string nuitId = LRequest.GetString("nuitId");

            var list = RF.Concrete<IDepartmentRepository>().GetUnitDepartment(Guid.Parse(nuitId));
            var contactLitViewModel = new PagedListViewModel<Department>(currentPageNum.Value, pageSize.Value, list);
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
                pageSize = PagedListViewModel<Department>.DefaultPageSize;
            }
            if (!id.HasValue)
            {
                Guid pid = Guid.Parse(LRequest.GetString("PID"));
                var village =RF.Concrete<IDepartmentRepository>().GetByKey(pid);
                village.HelperCode = "";
                village.Name = "";
                return View(new AddOrEditViewModel<Department>
                {
                    Added = true,
                    Entity = village,
                    CurrentPageNum = currentPageNum.Value,
                    PageSize = pageSize.Value
                });
            }
            else
            {
                var village = RF.Concrete<IDepartmentRepository>().GetByKey(id.Value);
                return View(new AddOrEditViewModel<Department>
                {
                    Added = false,
                    Entity = village,
                    CurrentPageNum = currentPageNum.Value,
                    PageSize = pageSize.Value
                });
            }
        }
        public override ActionResult Add(AddOrEditViewModel<Department> model, FormCollection collection)
        {

            if (!ModelState.IsValid)
            {
                return View("AddOrEdit", model);
            }

            int OrderBy = Convert.ToInt32(DbFactory.DBA.QueryValue("SELECT ISNULL(MAX(OrderBy),0)+1 OrderBy FROM Department  WHERE ParentId='" + model.Entity.ParentId + "'"));
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
            RF.Concrete<IDepartmentRepository>().Create(model.Entity);
            RF.Concrete<IDepartmentRepository>().Context.Commit();

            return RedirectToAction("Index", new { currentPageNum = model.CurrentPageNum, pageSize = model.PageSize });
        }
        public override ActionResult Delete(Department village, int? currentPageNum, int? pageSize, FormCollection collection)
        {
            village = RF.Concrete<IDepartmentRepository>().GetByKey(village.ID);
            DbFactory.DBA.ExecuteText("DELETE Department WHERE NodePath LIKE '" + village.NodePath + "%'");

            return RedirectToAction("Index", new { currentPageNum = currentPageNum.Value, pageSize = pageSize.Value });
        }
    }
}

