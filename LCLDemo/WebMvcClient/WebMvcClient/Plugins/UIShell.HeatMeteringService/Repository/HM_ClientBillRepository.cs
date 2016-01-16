/*******************************************************  
*   
* 作者：罗敏贵  
* 说明： 客户票据 
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
    public interface IHM_ClientBillRepository : IRepository<HM_ClientBill>  
    {  
  
    }  
    public class HM_ClientBillRepository : EntityFrameworkRepository<HM_ClientBill>, IHM_ClientBillRepository  
    {  
        public HM_ClientBillRepository(IRepositoryContext context)  
            : base(context)  
        {   
          
        }  
    }  
}  

