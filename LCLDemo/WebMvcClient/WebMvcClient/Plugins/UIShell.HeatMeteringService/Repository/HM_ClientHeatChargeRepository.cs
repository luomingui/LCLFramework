/*******************************************************  
*   
* 作者：罗敏贵  
* 说明： 热计量采暖费结算单 
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
    public interface IHM_ClientHeatChargeRepository : IRepository<HM_ClientHeatCharge>  
    {  
  
    }  
    public class HM_ClientHeatChargeRepository : EntityFrameworkRepository<HM_ClientHeatCharge>, IHM_ClientHeatChargeRepository  
    {  
        public HM_ClientHeatChargeRepository(IRepositoryContext context)  
            : base(context)  
        {   
          
        }  
    }  
}  

