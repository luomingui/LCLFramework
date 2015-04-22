/******************************************************* 
*  
* 作者：罗敏贵 
* 说明： 教师信息审核
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
    public class User_TeacherCheckController : RbacController<User_TeacherCheck> 
    { 
        public User_TeacherCheckController() 
        { 
       //ddlUser_TeacherCheck(Guid.Empty); 

        } 
        public void ddlUser_TeacherCheck(Guid dtId) 
        { 
            var repo = RF.FindRepo<User_Teacher>(); 
            var list = repo.FindAll(); 
 
            List<SelectListItem> selitem = new List<SelectListItem>(); 
            if (list.Count() > 0) 
            { 
                var roots = list; 
                foreach (var item in roots) 
                { 
                    selitem.Add(new SelectListItem { Text = item.User.Name, Value = item.ID.ToString() }); 
                } 
            } 
            selitem.Insert(0, new SelectListItem { Text = "==教师信息审核==", Value = "-1" }); 
            ViewData["ddlUser_TeacherCheck"] = selitem; 
        } 

 
    } 
} 

