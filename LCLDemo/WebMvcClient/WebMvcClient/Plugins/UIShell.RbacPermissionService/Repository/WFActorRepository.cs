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
        List<WFActor> GetByRoutId(Guid routId);
        List<WFActorUser> GetActorUserByActorId(Guid actorId);
        void AddActorUser(Guid actorId, IList<Guid> idUserList);
    }
    public class WFActorRepository : EntityFrameworkRepository<WFActor>, IWFActorRepository
    {
        public WFActorRepository(IRepositoryContext context)
            : base(context)
        {

        }
        public List<WFActor> GetByRoutId(Guid routId)
        {
            var spec = Specification<WFActor>.Eval(p => p.Rout_ID == routId);
            var pageList = this.FindAll(spec, p => p.SortNo, LCL.SortOrder.Ascending);
            return pageList.ToList();
        }
        public void AddActorUser(Guid actorId, IList<Guid> idUserList)
        {
            var rf = RF.Concrete<IWFActorUserRepository>();
            for (int i = 0; i < idUserList.Count; i++)
            {
                rf.Create(new WFActorUser
                {
                    ID = Guid.NewGuid(),
                    User = new User { ID = idUserList[i] },
                    Actor = new WFActor { ID = actorId }
                });
                rf.Context.Commit();
            }

        }
        public List<WFActorUser> GetActorUserByActorId(Guid actorId)
        {
            var spec = Specification<WFActorUser>.Eval(p => p.Actor.ID == actorId);
            var pageList = RF.Concrete<IWFActorUserRepository>().FindAll(spec, p => p.UpdateDate, LCL.SortOrder.Ascending);
            return pageList.ToList();
        }
    }
}

