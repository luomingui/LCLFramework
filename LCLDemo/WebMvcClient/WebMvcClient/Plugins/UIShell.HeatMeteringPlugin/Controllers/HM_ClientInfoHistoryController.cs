/******************************************************* 
*  
* 作者：罗敏贵 
* 说明： 采暖变更历史
* 运行环境：.NET 4.5.0 
* 版本号：1.0.0 
*  
* 历史记录： 
*    创建文件 罗敏贵 2016年1月12日
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
using UIShell.HeatMeteringService;
using UIShell.RbacPermissionService; 
 
namespace UIShell.HeatMeteringService.Controllers 
{ 
    public class HM_ClientInfoHistoryController : RbacController<HM_ClientInfoHistory> 
    { 
        public HM_ClientInfoHistoryController() 
        { 
        } 
    } 
} 

