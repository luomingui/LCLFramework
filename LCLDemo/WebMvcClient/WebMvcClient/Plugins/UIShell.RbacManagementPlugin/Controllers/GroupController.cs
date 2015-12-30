/******************************************************* 
*  
* 作者：罗敏贵 
* 说明： 用户组
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

namespace UIShell.RbacManagementPlugin.Controllers
{ 
    public class GroupController : RbacController<Group> 
    { 
        public GroupController() 
        { 
        } 
    } 
} 

