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
            ddlDictionary(Guid.Empty);
        }
        public void ddlDictionary(Guid dtId)
        {
            var repo = RF.FindRepo<DictType>();
            var list = repo.FindAll();
            List<SelectListItem> selitem = new List<SelectListItem>();
            if (list.Count() > 0)
            {
                var roots = list;
                foreach (var item in roots)
                {
                    selitem.Add(new SelectListItem { Text =item.DicDes+"("+ item.Name+")", Value = item.ID.ToString() });
                }
            }
            selitem.Insert(0, new SelectListItem { Text = "==字典类型==", Value = "-1" });
            ViewData["ddlDictionary"] = selitem;
        }
        public override System.Web.Mvc.ActionResult Index(int? currentPageNum, int? pageSize, System.Web.Mvc.FormCollection collection)
        {
            if (!currentPageNum.HasValue)
            {
                currentPageNum = 1;
            }
            if (!pageSize.HasValue)
            {
                pageSize = PagedListViewModel<Dictionary>.DefaultPageSize;
            }
            int pageNum = currentPageNum.Value;

            Guid guid = Guid.Parse(LRequest.GetString("dicTypeId"));

            var list = repo.GetDictTypeList(guid);

            var contactLitViewModel = new DictionaryPagedListViewModel(pageNum, pageSize.Value, list.ToList());
            contactLitViewModel.DicTypeId = guid;

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
                pageSize = PagedListViewModel<Dictionary>.DefaultPageSize;
            }
            Guid guid = Guid.Parse(LRequest.GetString("dicTypeId"));
            if (!id.HasValue)
            {
                return View(new DictionaryAddOrEditViewModel
                {
                    Added = true,
                    Entity = null,
                    DicTypeId=guid,
                    CurrentPageNum = currentPageNum.Value,
                    PageSize = pageSize.Value
                });
            }
            else
            {
                var repo = RF.FindRepo<Dictionary>();
                var village = repo.GetByKey(id.Value);
                return View(new DictionaryAddOrEditViewModel
                {
                    Added = false,
                    Entity = village,
                    DicTypeId = guid,
                    CurrentPageNum = currentPageNum.Value,
                    PageSize = pageSize.Value
                });
            }
        }
        public ActionResult Add(DictionaryAddOrEditViewModel model, FormCollection collection)
        {
            return base.Add(model, collection);
        }
        public override ActionResult Delete(Dictionary model, int? currentPageNum, int? pageSize, FormCollection collection)
        {
            var entity = RF.Concrete<IDictionaryRepository>().GetByKey(model.ID);
            return base.Delete(entity, currentPageNum, pageSize, collection);
        }
    }
}

