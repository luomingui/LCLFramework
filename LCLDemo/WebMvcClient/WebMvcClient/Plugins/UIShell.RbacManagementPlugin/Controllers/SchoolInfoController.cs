/******************************************************* 
*  
* 作者：罗敏贵 
* 说明： 学校信息
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
    public class SchoolInfoController : RbacController<UnitInfo>
    {
        public SchoolInfoController()
        {
          
        }
        public override System.Web.Mvc.ActionResult Index(int? currentPageNum, int? pageSize, System.Web.Mvc.FormCollection collection)
        {
            if (!currentPageNum.HasValue)
                currentPageNum = 1;
            if (!pageSize.HasValue)
                pageSize = PagedListViewModel<SchoolInfoPageListViewModel>.DefaultPageSize;

            var list = RF.Concrete<IUnitInfoRepository>().GetSchoolInfoList();
            var contactLitViewModel = new PagedListViewModel<SchoolInfoPageListViewModel>(currentPageNum.Value, pageSize.Value, list);
            return View(contactLitViewModel);
        }
        public override ActionResult AddOrEdit(int? currentPageNum, int? pageSize, Guid? id, FormCollection collection)
        {
            if (!currentPageNum.HasValue)
            {
                currentPageNum = 1;
            }
            if (!pageSize.HasValue)
            {
                pageSize = PagedListViewModel<SchoolInfoAddOrEditViewModel>.DefaultPageSize;
            }
            if (!id.HasValue)
            {
                return View(new SchoolInfoAddOrEditViewModel
                {
                   SchoolInfo=new SchoolInfo(),
                   UnitInfo=new UnitInfo(),
                });
            }
            else
            {
                return View(new SchoolInfoAddOrEditViewModel
                {
                    SchoolInfo = RF.Concrete<ISchoolInfoRepository>().Find(LCL.Specifications.Specification<SchoolInfo>.Eval(e => e.UnitInfo.ID==id.Value)),
                    UnitInfo =RF.Concrete<IUnitInfoRepository>().GetByKey(id.Value),
                });
            }
        }
    }
}

