
using LCL.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LCL.Domain.Services
{
    /// <summary>
    /// 表示用于Byteart Retail领域模型中的领域服务类型。
    /// </summary>
    public class DomainService : IDomainService
    {
        #region Private Fields
        private readonly IRepositoryContext repositoryContext;
        #endregion

        #region Ctor
        /// <summary>
        /// 初始化一个新的<c>DomainService</c>类型的实例。
        /// </summary>
        /// <param name="repositoryContext">仓储上下文。</param>
        public DomainService(IRepositoryContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
        }
        #endregion

        
    }
}
