/******************************************************* 
*  
* 作者：罗敏贵 
* 说明： 部门
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
    public class DepartmentController : RbacController<Department>
    {
        public DepartmentController()
        {
            //ddlDepartment(Guid.Empty);

        }
        public void ddlDepartment(Guid dtId)
        {
            var repo = RF.FindRepo<UnitInfo>();
            var list = repo.FindAll();

            List<SelectListItem> selitem = new List<SelectListItem>();
            if (list.Count() > 0)
            {
                var roots = list.Where(e => e.ParentId == Guid.Empty);
                foreach (var item in roots)
                {
                    selitem.Add(new SelectListItem { Text = item.Name, Value = item.ID.ToString() });
                    var child = list.Where(p => p.ParentId == item.ID);
                    foreach (var item1 in child)
                    {
                        if (dtId == item1.ID)
                        {
                            selitem.Add(new SelectListItem { Text = "----" + item1.Name, Value = item1.ID.ToString(), Selected = true });
                            this.ViewData["selected"] = item1.ID.ToString();
                        }
                        else
                        {
                            selitem.Add(new SelectListItem { Text = "----" + item1.Name, Value = item1.ID.ToString() });
                        }
                    }
                }
            }
            selitem.Insert(0, new SelectListItem { Text = "==部门==", Value = "-1" });
            ViewData["ddlDepartment"] = selitem;
        }


    }
}

