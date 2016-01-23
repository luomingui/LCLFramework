/******************************************************* 
*  
* 作者：罗敏贵 
* 说明： 小区
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
using LCL.Specifications; 
 
namespace UIShell.HeatMeteringService.Controllers 
{ 
    public class HM_VillageController : RbacController<HM_Village> 
    { 
        public HM_VillageController() 
        { 
        }
        [HttpPost]
        public CustomJsonResult AjaxEasyUITree_HM_Village()
        {
            string id = LRequest.GetString("id");
            Guid pid = string.IsNullOrWhiteSpace(id) ? Guid.Empty : Guid.Parse(id);
            var repo = RF.Concrete<IHM_VillageRepository>();
            ISpecification<HM_Village> spec = Specification<HM_Village>.Eval(p => p.ParentId == Guid.Empty);
            ISpecification<HM_Village> spec1 = Specification<HM_Village>.Eval(p => p.ParentId == pid);
            IEnumerable<HM_Village> list = repo.FindAll(spec).ToList();
            if (pid != Guid.Empty)
            {
                list = repo.FindAll(spec1).ToList();
            }
            List<EasyUITreeModel> easyTree = new List<EasyUITreeModel>();
            int i = 0;
            foreach (var item in list)
            {
                EasyUITreeModel model = new EasyUITreeModel();
                if (i == 0)
                {
                    model.selected = true;
                    model.Checked = true;
                }
                model.id = item.ID.ToString();
                model.text = item.Name;
                model.parentId = item.ParentId.ToString();
                model.parentName = repo.GetByName(item.ParentId);

                model.attributes.Add("ID", item.ID);
                model.attributes.Add("Name", item.Name);
                model.attributes.Add("Pinyi", item.Pinyi);
                model.attributes.Add("Type", item.Type);
                model.attributes.Add("EnName", item.EnName);
                model.attributes.Add("Alias", item.Alias);
                model.attributes.Add("Population", item.Population);
                model.attributes.Add("TotalArea", item.TotalArea);
                model.attributes.Add("Office", item.Office);
                model.attributes.Add("Summary", item.Summary);
                model.attributes.Add("Address", item.Address);
                model.attributes.Add("IsLast", item.IsLast);
                model.attributes.Add("Level", item.Level);
                model.attributes.Add("NodePath", item.NodePath);
                model.attributes.Add("OrderBy", item.OrderBy);
                model.attributes.Add("ParentId", item.ParentId);
                model.attributes.Add("IsDelete", item.IsDelete);
                model.attributes.Add("AddDate", item.AddDate);
                model.attributes.Add("UpdateDate", item.UpdateDate);

                easyTree.Add(model);
                i++;
            }
            var json = new CustomJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.Data = easyTree;
            return json;
        } 
    } 
} 

