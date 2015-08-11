/******************************************************* 
*  
* 作者：罗敏贵 
* 说明： 系统操作日志
* 运行环境：.NET 4.5.0 
* 版本号：1.0.0 
*  
* 历史记录： 
*    创建文件 罗敏贵 20154月18日 星期六
*  
*******************************************************/
using System.Linq;
using LCL.MvcExtensions;
using LCL.Repositories;
using System.Web.Mvc;
using UIShell.RbacPermissionService;

namespace UIShell.RbacManagementPlugin.Controllers
{
    public class TLogController : RbacController<TLog>
    {
        ITLogRepository repo = RF.Concrete<ITLogRepository>();
        public TLogController()
        {

        }
        [Permission("首页", "Index")]
        public override System.Web.Mvc.ActionResult Index(int? currentPageNum, int? pageSize, System.Web.Mvc.FormCollection collection)
        {
            if (!currentPageNum.HasValue)
            {
                currentPageNum = 1;
            }
            if (!pageSize.HasValue)
            {
                pageSize = PagedListViewModel<TLog>.DefaultPageSize;
            }
            int pageNum = currentPageNum.Value;

            var list = repo.FindAll(p => p.UpdateDate, LCL.SortOrder.Descending);

            var contactLitViewModel = new PagedListViewModel<TLog>(pageNum, pageSize.Value, list.ToList());

            return View(contactLitViewModel);
        }
        public override ActionResult Add(AddOrEditViewModel<TLog> model, FormCollection collection)
        {
            return base.Add(model, collection);
        }
        public override ActionResult Edit(AddOrEditViewModel<TLog> model, FormCollection collection)
        {
            return base.Edit(model, collection);
        }
        public override ActionResult Delete(TLog model, int? currentPageNum, int? pageSize, FormCollection collection)
        {
            return base.Delete(model, currentPageNum, pageSize, collection);
        }
    }
}

