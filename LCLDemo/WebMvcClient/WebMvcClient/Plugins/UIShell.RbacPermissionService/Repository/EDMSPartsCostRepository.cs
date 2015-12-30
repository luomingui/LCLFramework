/*******************************************************  
*   
* 作者：罗敏贵  
* 说明： 配件/服务费 
* 运行环境：.NET 4.5.0  
* 版本号：1.0.0  
*   
* 历史记录：  
*    创建文件 罗敏贵 2015年12月23日 
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
    public interface IEDMSPartsCostRepository : IRepository<EDMSPartsCost>  
    {  
  
    }  
    public class EDMSPartsCostRepository : EntityFrameworkRepository<EDMSPartsCost>, IEDMSPartsCostRepository  
    {  
        public EDMSPartsCostRepository(IRepositoryContext context)  
            : base(context)  
        {   
          
        }  
    }  
}  

