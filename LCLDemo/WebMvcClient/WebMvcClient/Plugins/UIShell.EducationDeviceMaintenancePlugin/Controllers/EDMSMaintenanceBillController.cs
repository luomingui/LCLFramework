/******************************************************* 
*  
* 作者：罗敏贵 
* 说明： 
* 运行环境：.NET 4.5.0 
* 版本号：1.0.0 
*  
* 历史记录： 
*    创建文件 罗敏贵 2015年12月22日
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

namespace UIShell.EducationDeviceMaintenancePlugin.Controllers
{
    public class EDMSMaintenanceBillController : RbacController<EDMSMaintenanceBill>
    {
        IEDMSMaintenanceBillRepository repo = RF.Concrete<IEDMSMaintenanceBillRepository>();
        public EDMSMaintenanceBillController()
        {
        }
        public override ActionResult Index(int? currentPageNum, int? pageSize, System.Web.Mvc.FormCollection collection)
        {
            if (!currentPageNum.HasValue) currentPageNum = 1;
            if (!pageSize.HasValue) pageSize = PagedResult<Department>.DefaultPageSize;
            var contactLitViewModel = new EDMSMaintenanceBillViewModel();
            contactLitViewModel.pboId = Guid.Parse(LRequest.GetString("pboId"));
            contactLitViewModel.taskId = Guid.Parse(LRequest.GetString("taskId"));
            return View(contactLitViewModel);
        }
        public override CustomJsonResult AjaxAdd(EDMSMaintenanceBill model)
        {
            Guid pboId = Guid.Parse(LRequest.GetString("pboId"));
            Guid taskId = Guid.Parse(LRequest.GetString("taskId"));

            model.MaintainPerson = RbacPrincipal.CurrentUser.UserName;
            model.MaintainPersonPhone = RbacPrincipal.CurrentUser.TelePhone;
            model.RepairUnit = RbacPrincipal.CurrentUser.DepName;
            model.FulfillDate = DateTime.Now;
            model.EDMSMaintenanceBill_ID = pboId;
            //处理流程
            var flow = DomainServiceFactory.Create<FlowServices>();
            flow.Arguments.Attributes = new System.Collections.Hashtable();
            flow.Arguments.Attributes.Add("taskId", taskId);
            flow.Arguments.Attributes.Add("isExamine", true);
            flow.Arguments.Attributes.Add("State", 2);
            flow.Arguments.Attributes.Add("Memo", "处理报修单");
            flow.FlowAction = FlowAction.处理任务;
            flow.Invoke();
            var result = flow.Result;
            if (result.Success)
                return base.AjaxAdd(model);
            else
            {
                var json = new CustomJsonResult();
                json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                json.Data = result;
                return json;
            }
        }
    }
}

