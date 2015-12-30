/******************************************************* 
*  
* 作者：罗敏贵 
* 说明： 任务列表
* 运行环境：.NET 4.5.0 
* 版本号：1.0.0 
*  
* 历史记录： 
*    创建文件 罗敏贵 2015年12月6日
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
using LCL.DomainServices;
using UIShell.RbacPermissionService.Services;

namespace UIShell.RbacManagementPlugin.Controllers
{
    public class WFTaskListController : RbacController<WFTaskList>
    {
        public WFTaskListController()
        {
        }
        [Permission("首页", "Index")]
        [BizActivityLog("首页", "rows,page")]
        public override CustomJsonResult AjaxGetByPage(int? page, int? rows)
        {
            if (!page.HasValue) page = 1;
            if (!rows.HasValue) rows = PagedResult<WFTaskList>.DefaultPageSize;
            int pageNumber = page.Value;
            int pageSize = rows.Value;

            var json = new CustomJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            try
            {
                var service = DomainServiceFactory.Create<FlowServices>();
                service.FlowAction = FlowAction.我的任务;
                service.Invoke();
                var modelList = service.Result.DataObject as EasyUIGridModel<MyTaskListViewModel>;
                json.Data = modelList;
            }
            catch (Exception ex)
            {
                Logger.LogError("AjaxGetByPage:", ex);
            }
            return json;
        }
        /// <summary>
        /// 完成任务
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public ActionResult frmTask(Guid taskId)
        {
            var ser = DomainServiceFactory.Create<FlowServices>();
            ser.Arguments.Attributes.Add("taskId", taskId);
            ser.FlowAction = FlowAction.任务地址;
            ser.Invoke();
            string taskUrl = ser.Result.Attributes["taskUrl"].ToString();
            if (string.IsNullOrWhiteSpace(taskUrl)) taskUrl = "frmTask";
            return  Json(taskUrl,JsonRequestBehavior.AllowGet);
        }
    }
}

