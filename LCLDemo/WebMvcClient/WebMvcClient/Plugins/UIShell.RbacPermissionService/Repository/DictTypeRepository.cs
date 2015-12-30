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
using System;
using System.Collections.Generic;

namespace UIShell.RbacPermissionService
{
    public interface IDictTypeRepository : IRepository<DictType>
    {
        string GetByName(Guid guid);
    }
    public class DictTypeRepository : EntityFrameworkRepository<DictType>, IDictTypeRepository
    {
        private string className = "";
        private string cacheKey = "";
        public DictTypeRepository(IRepositoryContext context)
            : base(context)
        {
            this.className = typeof(DictType).Name;
            this.cacheKey = "LCL_Cache_" + className;
        }
        protected override void DoRemove(object keyValue)
        {
            DbFactory.DBA.ExecuteText("delete from Dictionary where DictType_ID='" + keyValue + "'");
            base.DoRemove(keyValue);
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

