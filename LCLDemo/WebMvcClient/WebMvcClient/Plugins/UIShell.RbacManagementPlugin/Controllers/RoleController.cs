using LCL.Repositories;
using System.Web.Mvc;
/******************************************************* 
*  
* 作者：罗敏贵 
* 说明： 角色
* 运行环境：.NET 4.5.0 
* 版本号：1.0.0 
*  
* 历史记录： 
*    创建文件 罗敏贵 20154月18日 星期六
*  
*******************************************************/
using UIShell.RbacPermissionService;

namespace UIShell.RbacManagementPlugin.Controllers
{
    public class RoleController : RbacController<Role>
    {
        IRoleRepository repo = RF.Concrete<IRoleRepository>();
        public RoleController()
        {

        }

        public JsonResult CheckRoleName(string RoleName)
        {
            bool isValidate = repo.CheckRoleName(RoleName);
            return Json(isValidate, JsonRequestBehavior.AllowGet);
        }
    }
}

