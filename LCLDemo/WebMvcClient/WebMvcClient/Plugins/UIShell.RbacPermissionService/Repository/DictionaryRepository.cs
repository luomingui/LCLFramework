using LCL;
/*******************************************************  
*   
* 作者：罗敏贵  
* 说明：  
* 运行环境：.NET 4.5.0  
* 版本号：1.0.0  
*   
* 历史记录：  
*    创建文件 罗敏贵 20154月18日 星期六 
*   
*******************************************************/
using LCL.Repositories;
using LCL.Repositories.EntityFramework;
using LCL.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UIShell.RbacPermissionService  
{  
    public interface IDictionaryRepository : IRepository<Dictionary>  
    {
        List<Dictionary> GetDictTypeList(Guid dicTypeId);

        string GetByName(Guid guid);
    }  
    public class DictionaryRepository : EntityFrameworkRepository<Dictionary>, IDictionaryRepository  
    {  
        public DictionaryRepository(IRepositoryContext context)  
            : base(context)  
        {   
          
        }

        public List<Dictionary> GetDictTypeList(Guid dicTypeId)
        {
            ISpecification<Dictionary> spec =
             Specification<Dictionary>.Eval(p => p.DictType.ID == dicTypeId);
            var list = this.FindAll(spec, e => e.UpdateDate, SortOrder.Descending);
            return list.ToList();
        }
        public string GetByName(Guid id)
        {
            try
            {
                var model = this.GetByKey(id);
                return model.Name;
            }
            catch (Exception)
            {
                return "保密";
            }
        }
    }  
}  

