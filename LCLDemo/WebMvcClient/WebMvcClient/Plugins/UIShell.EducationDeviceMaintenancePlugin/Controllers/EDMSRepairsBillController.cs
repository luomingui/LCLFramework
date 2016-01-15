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

namespace UIShell.EducationDeviceMaintenancePlugin.Controllers 
{ 
    public class EDMSRepairsBillController : RbacController<EDMSRepairsBill> 
    {
        IEDMSRepairsBillRepository repo = RF.Concrete<IEDMSRepairsBillRepository>();
        public EDMSRepairsBillController() 
        { 
        }
        public override CustomJsonResult AjaxAdd(EDMSRepairsBill model)
        {
            var result = new Result(true);
            try
            {
                model.RepairsPerson = RbacPrincipal.CurrentUser.UserName;
                model.RepairsPersonPhone = RbacPrincipal.CurrentUser.TelePhone;
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
    } 
} 

