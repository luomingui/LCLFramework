/******************************************************* 
*  
* 作者：罗敏贵 
* 说明： 
* 运行环境：.NET 4.5.0 
* 版本号：1.0.0 
*  
* 历史记录： 
*    创建文件 罗敏贵 2015年11月29日
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

namespace UIShell.WinUI.Controllers
{
    public class WFActorController : RbacController<WFActor>
    {
        IWFActorRepository repo = RF.Concrete<IWFActorRepository>();
        public WFActorController()
        {

        }
        public override ActionResult Index(int? currentPageNum, int? pageSize, FormCollection collection)
        {
            if (!currentPageNum.HasValue) currentPageNum = 1;
            if (!pageSize.HasValue) pageSize = PagedResult<WFActor>.DefaultPageSize;
            int pageNum = currentPageNum.Value;

            var routId = Guid.Parse(LRequest.GetString("routId"));
            var pageList = new List<WFActor>();// repo.AjaxGetByRoutId(routId);
            var viewModel = new WFActorPagedListViewModel(currentPageNum.Value, pageSize.Value, pageList);
            viewModel.RoutId = routId;

            return View(viewModel);
        }
        [HttpPost]
        public CustomJsonResult AjaxGetByRoutId(int? page, int? rows, Guid routId)
        {
            if (!page.HasValue) page = 1;
            if (!rows.HasValue) rows = PagedResult<WFActor>.DefaultPageSize;
            int pageNumber = page.Value;
            int pageSize = rows.Value;

            var json = new CustomJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            try
            {
                var pageList = repo.GetByRoutId(routId);
                var viewModel = new WFActorPagedListViewModel(pageNumber, pageSize, pageList);

                var easyUIPages = new Dictionary<string, object>();
                easyUIPages.Add("total", viewModel.PageCount);
                easyUIPages.Add("rows", viewModel.PagedModels);

                json.Data = easyUIPages;
            }
            catch (Exception ex)
            {
                Logger.LogError("AjaxGetByPage:", ex);
            }
            return json;
        }
        [HttpPost]
        public override CustomJsonResult AjaxAdd(WFActor model)
        {
            return base.AjaxAdd(model);
        }
        [HttpPost]
        public CustomJsonResult AjaxAddActorUser(Guid actorId, IList<Guid> idUserList)
        {
            var result = new Result(true);
            try
            {
                repo.AddActorUser(actorId, idUserList);
            }
            catch (Exception ex)
            {
                Logger.LogError("AjaxAddActorUser", ex);
                result = new Result(false);
            }
            var json = new CustomJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.Data = result;
            return json;
        }
    }
}

