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

namespace UIShell.RbacPermissionService  
{  
    public interface IScheduledEventsRepository : IRepository<ScheduledEvents>  
    {  
  
    }  
    public class ScheduledEventsRepository : EntityFrameworkRepository<ScheduledEvents>, IScheduledEventsRepository  
    {  
        public ScheduledEventsRepository(IRepositoryContext context)  
            : base(context)  
        {   
          
        }  
    }  
}  

