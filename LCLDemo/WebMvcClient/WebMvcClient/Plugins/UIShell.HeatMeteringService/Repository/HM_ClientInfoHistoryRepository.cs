/*******************************************************  
*   
* 作者：罗敏贵  
* 说明： 采暖变更历史 
* 运行环境：.NET 4.5.0  
* 版本号：1.0.0  
*   
* 历史记录：  
*    创建文件 罗敏贵 2016年1月11日 
*   
*******************************************************/  
using LCL.Repositories;  
using LCL.Repositories.EntityFramework;  
using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Text;  
using System.Threading.Tasks;  
  
namespace UIShell.HeatMeteringService  
{  
    public interface IHM_ClientInfoHistoryRepository : IRepository<HM_ClientInfoHistory>  
    {  
  
    }  
    public class HM_ClientInfoHistoryRepository : EntityFrameworkRepository<HM_ClientInfoHistory>, IHM_ClientInfoHistoryRepository  
    {  
        public HM_ClientInfoHistoryRepository(IRepositoryContext context)  
            : base(context)  
        {   
          
        }  
    }  
}  

