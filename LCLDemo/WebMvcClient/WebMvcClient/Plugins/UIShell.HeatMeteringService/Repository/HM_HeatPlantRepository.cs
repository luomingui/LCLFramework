/*******************************************************  
*   
* 作者：罗敏贵  
* 说明： 供热站 
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
    public interface IHM_HeatPlantRepository : IRepository<HM_HeatPlant>  
    {  
  
    }  
    public class HM_HeatPlantRepository : EntityFrameworkRepository<HM_HeatPlant>, IHM_HeatPlantRepository  
    {  
        public HM_HeatPlantRepository(IRepositoryContext context)  
            : base(context)  
        {   
          
        }  
    }  
}  

