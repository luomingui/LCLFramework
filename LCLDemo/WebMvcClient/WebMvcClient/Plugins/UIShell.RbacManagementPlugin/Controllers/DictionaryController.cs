using LCL;
/******************************************************* 
*  
* 作者：罗敏贵 
* 说明： 字典管理
* 运行环境：.NET 4.5.0 
* 版本号：1.0.0 
*  
* 历史记录： 
*    创建文件 罗敏贵 20154月18日 星期六
*  
*******************************************************/
using LCL.MvcExtensions;
using LCL.Repositories;
using LCL.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UIShell.RbacPermissionService;

namespace UIShell.RbacManagementPlugin.Controllers
{
    public class DictionaryController : RbacController<Dictionary>
    {
        IDictionaryRepository repo = RF.Concrete<IDictionaryRepository>();

        public DictionaryController()
        {

        }
        [HttpPost]
        public JsonResult GetDicChildList(Guid? dictId)
        {
            if (!dictId.HasValue)
            {
                dictId = Guid.Empty;
            }
            var spec = Specification<Dictionary>.Eval(p => p.DictType.ID == dictId);
            var list = repo.FindAll(spec).ToList();
            return Json(list.ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}

