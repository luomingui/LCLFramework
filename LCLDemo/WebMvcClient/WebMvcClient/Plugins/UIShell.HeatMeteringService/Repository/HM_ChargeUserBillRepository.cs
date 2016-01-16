/*******************************************************  
*   
* 作者：罗敏贵  
* 说明： 收费员领用票据 
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
    public interface IHM_ChargeUserBillRepository : IRepository<HM_ChargeUserBill>  
    {  
  
    }  
    public class HM_ChargeUserBillRepository : EntityFrameworkRepository<HM_ChargeUserBill>, IHM_ChargeUserBillRepository  
    {  
        public HM_ChargeUserBillRepository(IRepositoryContext context)  
            : base(context)  
        {   
          
        }  
    }  
}  

