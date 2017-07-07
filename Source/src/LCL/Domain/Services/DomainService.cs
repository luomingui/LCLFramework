
using LCL.Application;
using LCL.Bus;
using LCL.Domain.Repositories;


namespace LCL.Domain.Services
{
    /// <summary>
    /// 表示用于Byteart Retail领域模型中的领域服务类型。
    /// </summary>
    public class DomainService : ApplicationService, IDomainService
    {

        #region Ctor
        /// <summary>
        /// 初始化一个新的<c>DomainService</c>类型的实例。
        /// </summary>
        /// <param name="repositoryContext">仓储上下文。</param>
        public DomainService(IRepositoryContext context, IEventBus bus):base(context,bus)
        {
          
        }
        #endregion

        
    }
}
