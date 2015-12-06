/*******************************************************  
*   
* 作者：罗敏贵  
* 说明： 任务历史记录 
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
    public interface IWFTaskHistoryRepository : IRepository<WFTaskHistory>  
    {  
  
    }  
    public class WFTaskHistoryRepository : EntityFrameworkRepository<WFTaskHistory>, IWFTaskHistoryRepository  
    {  
        public WFTaskHistoryRepository(IRepositoryContext context)  
            : base(context)  
        {   
          
        }  
    }  
}  

