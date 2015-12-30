using LCL.DomainServices;
/*******************************************************  
*   
* 作者：罗敏贵  
* 说明：  
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
using UIShell.RbacPermissionService.Services;

namespace UIShell.RbacPermissionService
{  
    public interface IEDMSRepairsBillRepository : IRepository<EDMSRepairsBill>  
    {  
  
    }  
    public class EDMSRepairsBillRepository : EntityFrameworkRepository<EDMSRepairsBill>, IEDMSRepairsBillRepository  
    {  
        public EDMSRepairsBillRepository(IRepositoryContext context)  
            : base(context)  
        {   
          
        }
        protected override void DoAdd(EDMSRepairsBill entity)
        {
             //启动流程
            var flow = DomainServiceFactory.Create<FlowServices>();
            flow.Arguments.Attributes = new System.Collections.Hashtable();
            flow.Arguments.Attributes.Add("itemId",entity.ID);
            flow.Arguments.Attributes.Add("userId",entity.RepairsPerson);
            flow.FlowAction = FlowAction.申请工作;
            flow.Invoke();

            entity.WFItem = (WFItem)flow.Result.DataObject;
            base.DoAdd(entity);
        }
    }  
}  

