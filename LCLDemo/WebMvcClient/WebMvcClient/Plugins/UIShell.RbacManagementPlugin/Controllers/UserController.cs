/******************************************************* 
*  
* 作者：罗敏贵 
* 说明： 用户
* 运行环境：.NET 4.5.0 
* 版本号：1.0.0 
*  
* 历史记录： 
*    创建文件 罗敏贵 20154月18日 星期六
*  
*******************************************************/
using LCL;
using LCL.MvcExtensions;
using LCL.Repositories;
using System.Collections.Generic;
using System.Web.Mvc;

using UIShell.RbacPermissionService;

namespace UIShell.RbacManagementPlugin.Controllers
{
    public class UserController : RbacController<User>
    {
        IUserRepository repo = RF.Concrete<IUserRepository>();
        public UserController()
        {
         
        }
        [Permission("重置密码", "ResetPassword")]
        [HttpPost]
        public CustomJsonResult ResetPassword(IList<string> idList)
        {
            var result = new Result(true);
            for (int i = 0; i < idList.Count; i++)
            {
                repo.ChangePassword(idList[i], "123456");
            }
            var json = new CustomJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.Data = result;
            return json;
        }
    }
}

