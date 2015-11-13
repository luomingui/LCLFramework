/******************************************************* 
*  
* 作者：罗敏贵 
* 说明： 字典类型
* 运行环境：.NET 4.5.0 
* 版本号：1.0.0 
*  
* 历史记录： 
*    创建文件 罗敏贵 20154月18日 星期六
*  
*******************************************************/
using LCL.MvcExtensions;
using System.Web.Mvc;
using System.Linq;
using UIShell.RbacPermissionService;
using System.Collections.Generic;
using System;
using LCL.Repositories;

namespace UIShell.RbacManagementPlugin.Controllers
{
    public class DictTypeController : RbacController<DictType>
    {
        IDictTypeRepository repo = RF.Concrete<IDictTypeRepository>();
        public DictTypeController()
        {
           
        }
      
    }
}

