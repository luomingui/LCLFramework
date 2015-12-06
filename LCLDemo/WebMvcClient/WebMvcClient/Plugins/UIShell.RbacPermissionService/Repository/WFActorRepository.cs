/*******************************************************  
*   
* 作者：罗敏贵  
* 说明： 步骤 
* 运行环境：.NET 4.5.0  
* 版本号：1.0.0  
*   
* 历史记录：  
*    创建文件 罗敏贵 2015年11月30日 
*   
*******************************************************/
using LCL.Repositories;
using LCL.Repositories.EntityFramework;
using LCL.Specifications;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIShell.RbacPermissionService
{
    public interface IWFActorRepository : IRepository<WFActor>
    {
        List<WFActor> AjaxGetByRoutId(Guid routId);
    }
    public class WFActorRepository : EntityFrameworkRepository<WFActor>, IWFActorRepository
    {
        public WFActorRepository(IRepositoryContext context)
            : base(context)
        {

        }
        public List<WFActor> AjaxGetByRoutId(Guid routId)
        {
            var spec = Specification<WFActor>.Eval(p => p.Rout.ID == routId);
            var pageList = this.FindAll(spec, p => p.SortNo, LCL.SortOrder.Ascending);
            return pageList.ToList();
        }
    }
}

