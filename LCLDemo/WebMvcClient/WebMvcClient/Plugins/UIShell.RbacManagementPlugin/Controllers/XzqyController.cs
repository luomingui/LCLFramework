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
using System;
using System.Linq;
using System.Web.Mvc;
using UIShell.RbacPermissionService;

namespace UIShell.RbacManagementPlugin.Controllers
{
    public class XzqyController : RbacController<Xzqy>
    {
        IXzqyRepository repo =null;
        public XzqyController()
        {
            repo = RF.Concrete<IXzqyRepository>();
        }
        [Permission("首页", "Index")]
        public override ActionResult Index(int? currentPageNum, int? pageSize, System.Web.Mvc.FormCollection collection)
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
                var village = new Xzqy();

                string _pid = LRequest.GetString("PID");
                if (_pid.Length > 3)
                {
                    Guid pid = Guid.Parse(LRequest.GetString("PID"));
                    var _village = repo.GetByKey(pid);

                    if (_village != null)
                    {
                        village.ParentId = _village.ID;
                        village.NodePath = _village.NodePath;
                        village.Level = _village.Level + 1;
                        village.IsLast = false;
                    }
                }
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
        [Permission("添加", "Add")]
        [BizActivityLog("添加行政区域", "Name")]
        public override ActionResult Add(AddOrEditViewModel<Xzqy> model, FormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return View("AddOrEdit", model);
            }
            int OrderBy = Convert.ToInt32(DbFactory.DBA.QueryValue("SELECT ISNULL(MAX(OrderBy),0)+1 OrderBy FROM Xzqy WHERE ParentId='" + model.Entity.ParentId + "'"));
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
            model.Entity.AddDate = DateTime.Now;
            model.Entity.UpdateDate = DateTime.Now;

            RF.Concrete<IXzqyRepository>().Create(model.Entity);
            RF.Concrete<IXzqyRepository>().Context.Commit();

            return RedirectToAction("Index", new { currentPageNum = model.CurrentPageNum, pageSize = model.PageSize });
        }
        [Permission("删除", "Delete")]
        [BizActivityLog("删除行政区域", "Name")]
        public override ActionResult Delete(Xzqy village, int? currentPageNum, int? pageSize, FormCollection collection)
        {
            village = repo.GetByKey(village.ID);
            DbFactory.DBA.ExecuteText("DELETE Xzqy WHERE NodePath LIKE '" + village.NodePath + "%'");
            DbFactory.DBA.ExecuteText("DELETE Xzqy WHERE ID= '" + village.ID + "'");

            return RedirectToAction("Index", new { currentPageNum = currentPageNum.Value, pageSize = pageSize.Value });
        }
    }
}

