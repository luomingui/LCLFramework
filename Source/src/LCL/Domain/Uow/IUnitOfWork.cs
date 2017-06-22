using System;

namespace LCL.Domain.Uow
{
    /// <summary>
    /// 业务单元操作接口
    /// </summary>
    public interface IUnitOfWork
    {
        bool DistributedTransactionSupported { get; }
        /// <summary>
        ///  获取 当前单元操作是否已被提交  
        /// </summary>
        bool Committed { get; }
        /// <summary>
        /// 提交当前单元操作的结果  
        /// </summary>
        void Commit();
        /// <summary>
        /// 把当前单元操作回滚成未提交状态  
        /// </summary>
        void Rollback();
    }
}