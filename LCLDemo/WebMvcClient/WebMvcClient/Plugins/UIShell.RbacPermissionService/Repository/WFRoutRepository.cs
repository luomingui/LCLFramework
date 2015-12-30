/*******************************************************  
*   
* 作者：罗敏贵  
* 说明： 流程 
* 运行环境：.NET 4.5.0  
* 版本号：1.0.0  
*   
* 历史记录：  
*    创建文件 罗敏贵 2015年11月30日 
*   
*******************************************************/  
using LCL.Repositories;  
using LCL.Repositories.EntityFramework;  
using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Text;  
using System.Threading.Tasks;

namespace UIShell.RbacPermissionService
{  
    public interface IWFRoutRepository : IRepository<WFRout>  
    {
        WFRout GetRoutName(string routName);
    }  
    public class WFRoutRepository : EntityFrameworkRepository<WFRout>, IWFRoutRepository  
    {  
        public WFRoutRepository(IRepositoryContext context)  
            : base(context)  
        {   
          
        }
        public WFRout GetRoutName(string routName)
        {
            var entity=Find(new KeyWFRoutSpecification(routName, "Name"));
            if (entity == null)
                entity = new WFRout { ID = Guid.Empty };
            return entity;
        }
    }  
}  

