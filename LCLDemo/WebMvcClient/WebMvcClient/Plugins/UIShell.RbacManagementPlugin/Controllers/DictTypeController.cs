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

namespace UIShell.RbacManagementPlugin.Controllers
{
    public class DictTypeController : RbacController<DictType>
    {
        public DictTypeController()
        {

        }
        [Permission("删除", "Delete")]
        [BizActivityLog("删除字典类型", "Name")]
        public override ActionResult Delete(DictType village, int? currentPageNum, int? pageSize, FormCollection collection)
        {
            DbFactory.DBA.ExecuteText("DELETE Dictionary WHERE DictType_ID='" + village.ID + "'");
            return base.Delete(village, currentPageNum, pageSize, collection);
        }

        public List<EasyUITreeModel> easyTree = new List<EasyUITreeModel>();
        [HttpPost]
        public virtual CustomJsonResult AjaxEasyUITree()
        {
            var modelList = repo.FindAll().ToList();
            string id = LRequest.GetString("id");
            Guid guid = string.IsNullOrWhiteSpace(id) ? Guid.Empty : Guid.Parse(id);
            var list = modelList.Where(p => p.ParentId == guid);
            foreach (var item in list)
            {
                EasyUITreeModel model = new EasyUITreeModel();
                model.id = item.ID.ToString();
                model.attributes = item.ID.ToString();
                model.text = item.Code;
                model.parentId = item.ParentId.ToString();
                easyTree.Add(model);
            }
            var json = new CustomJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.Data = easyTree;
            return json;
        }
    }
    public class EasyUITreeModel
    {
        public EasyUITreeModel()
        {
            state = "closed";
            Checked = false;
            children = null;
            iconCls = "icon-bullet_green";
        }
        public string id { get; set; }
        public string text { get; set; }
        public string state { get; set; }
        public string parentId { get; set; }
        public string iconCls { get; set; }
        public bool Checked { get; set; }
        public string attributes { get; set; }
        public List<EasyUITreeModel> children { get; set; }
    }
}

