/******************************************************* 
*  
* 作者：罗敏贵 
* 说明： 系统操作日志
* 运行环境：.NET 4.5.0 
* 版本号：1.0.0 
*  
* 历史记录： 
*    创建文件 罗敏贵 20154月18日 星期六
*  
*******************************************************/
using System.Linq;
using LCL.MvcExtensions;
using LCL.Repositories;
using System.Web.Mvc;
using UIShell.RbacPermissionService;
using LCL;

namespace UIShell.RbacManagementPlugin.Controllers
{
    public class TLogController : RbacController<TLog>
    {
        public TLogController()
        {
            repo = RF.Concrete<ITLogRepository>();
        }
       
    }
}

