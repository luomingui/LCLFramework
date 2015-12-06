/*******************************************************  
*   
* 作者：罗敏贵  
* 说明： 项目 
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
    public interface IWFItemRepository : IRepository<WFItem>  
    {  
  
    }  
    public class WFItemRepository : EntityFrameworkRepository<WFItem>, IWFItemRepository  
    {  
        public WFItemRepository(IRepositoryContext context)  
            : base(context)  
        {   
          
        }  
    }  
}  

