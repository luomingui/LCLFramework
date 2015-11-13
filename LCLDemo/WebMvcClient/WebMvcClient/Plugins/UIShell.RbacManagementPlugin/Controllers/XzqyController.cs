using LCL;
/******************************************************* 
*  
* 作者：罗敏贵 
* 说明： 行政区域
* 运行环境：.NET 4.5.0 
* 版本号：1.0.0 
*  
* 历史记录： 
*    创建文件 罗敏贵 20154月18日 星期六
*  
*******************************************************/
using LCL.MvcExtensions;
using LCL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UIShell.RbacPermissionService;

namespace UIShell.RbacManagementPlugin.Controllers
{
    public class XzqyController : RbacController<Xzqy>
    {
        IXzqyRepository repo = null;
        public XzqyController()
        {
            repo = RF.Concrete<IXzqyRepository>();
        }
        //[Permission("首页", "Index")]
        public override ActionResult Index(int? currentPageNum, int? pageSize, System.Web.Mvc.FormCollection collection)
        {
            if (!currentPageNum.HasValue)
            {
                currentPageNum = 1;
            }
            if (!pageSize.HasValue)
            {
                pageSize = PagedResult<Xzqy>.DefaultPageSize;
            }
            int pageNum = currentPageNum.Value;

            var list = repo.GetFull();

            var contactLitViewModel = new PagedResult<Xzqy>(pageNum, pageSize.Value, list.ToList());

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
                pageSize = PagedResult<Xzqy>.DefaultPageSize;
            }
            if (!id.HasValue)
            {
                var village = new Xzqy();

                string _pid = LRequest.GetString("PID");
                if (_pid.Length > 3)
                {
                    Guid pid = Guid.Parse(LRequest.GetString("PID"));
                    var _village = repo.GetByKey(pid);

                    if (_village != null)
                    {
                        village.ParentId = _village.ID;
                        village.NodePath = _village.NodePath;
                        village.Level = _village.Level + 1;
                        village.IsLast = false;
                    }
                }
                return View(new AddOrEditViewModel<Xzqy>
                {
                    Added = true,
                    Entity = village,
                    CurrentPageNum = currentPageNum.Value,
                    PageSize = pageSize.Value
                });
            }
            else
            {
                var village = repo.GetByKey(id.Value);
                return View(new AddOrEditViewModel<Xzqy>
                {
                    Added = false,
                    Entity = village,
                    CurrentPageNum = currentPageNum.Value,
                    PageSize = pageSize.Value
                });
            }
        }
        [Permission("添加", "Add")]
        [BizActivityLog("添加行政区域", "Name")]
        public override ActionResult Add(AddOrEditViewModel<Xzqy> model, FormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return View("AddOrEdit", model);
            }
            int OrderBy = Convert.ToInt32(DbFactory.DBA.QueryValue("SELECT ISNULL(MAX(OrderBy),0)+1 OrderBy FROM Xzqy WHERE ParentId='" + model.Entity.ParentId + "'"));
            model.Entity.OrderBy = OrderBy;
            if (model.Entity.ParentId != Guid.Empty)
            {
                model.Entity.NodePath = model.Entity.NodePath + "\\" + model.Entity.Name;
                model.Entity.Level = model.Entity.Level + 1;
                model.Entity.IsLast = false;
            }
            else
            {
                model.Entity.NodePath = model.Entity.Name;
            }
            model.Entity.AddDate = DateTime.Now;
            model.Entity.UpdateDate = DateTime.Now;

            RF.Concrete<IXzqyRepository>().Create(model.Entity);
            RF.Concrete<IXzqyRepository>().Context.Commit();

            return RedirectToAction("Index", new { currentPageNum = model.CurrentPageNum, pageSize = model.PageSize });
        }
        public override CustomJsonResult AjaxDelete(Xzqy model)
        {
            model = repo.GetByKey(model.ID);
            DbFactory.DBA.ExecuteText("DELETE Xzqy WHERE NodePath LIKE '" + model.NodePath + "%'");
            DbFactory.DBA.ExecuteText("DELETE Xzqy WHERE ID= '" + model.ID + "'");

            var result = new Result(true);

            var json = new CustomJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.Data = result;

            return json;
        }
        public override CustomJsonResult AjaxGetByPage(int? page, int? rows)
        {
            /*
1、easyui-datagrid 分页接收参数
page 接受客户端的页码，对应的就是用户选择或输入的pageNumber（按照上图的例子，用户点了下一页，传到服务器端就是2）
rows 接受客户端的每页记录数，对应的就是pageSize  （用户在下拉列表选择每页显示30条记录，传到服务器就是30）

返回到前台的值必须按照如下的格式包括 total and rows 
 */
            if (!page.HasValue)
            {
                page = 1;
            }
            if (!rows.HasValue)
            {
                rows = PagedResult<Xzqy>.DefaultPageSize;
            }
            int pageNumber = page.Value;
            int pageSize = rows.Value;
            var modelList = repo.FindAll(p => p.NodePath, LCL.SortOrder.Ascending, pageNumber, pageSize);
            //	{"id":2,"name":"Designing","begin":"3/4/2010","end":"3/10/2010","progress":100,"_parentId":1,"state":"closed"},
            var easyUIPages = new Dictionary<string, object>();
            easyUIPages.Add("total", modelList.PageCount);
            easyUIPages.Add("rows", modelList.PagedModels);

            var json = new CustomJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.Data = easyUIPages;
            return json;
        }

        [HttpPost]
        public CustomJsonResult GetByName(Guid id)
        {
            string name = repo.GetByName(id);
            var json = new CustomJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.Data = name;
            return json;
        }
    }
}

