/******************************************************* 
*  
* 作者：罗敏贵 
* 说明： 角色权限
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
 
namespace UIShell.RbacManagementPlugin.Controllers 
{ 
    public class RoleAuthorityController : RbacController<RoleAuthority> 
    { 
        public RoleAuthorityController() 
        { 
       ddlRoleAuthority(Guid.Empty); 

        } 
        public void ddlRoleAuthority(Guid dtId) 
        { 
            var repo = RF.FindRepo<Role>(); 
            var list = repo.FindAll(); 
 
            List<SelectListItem> selitem = new List<SelectListItem>(); 
            if (list.Count() > 0) 
            { 
                var roots = list; 
                foreach (var item in roots) 
                { 
                    selitem.Add(new SelectListItem { Text = item.Name, Value = item.ID.ToString() }); 
                } 
            } 
            selitem.Insert(0, new SelectListItem { Text = "==所属角色==", Value = "-1" }); 
            ViewData["ddlRoleAuthority"] = selitem; 
        } 

 
    } 
} 

