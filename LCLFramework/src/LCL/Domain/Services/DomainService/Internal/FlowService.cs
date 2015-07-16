using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LCL.DomainServices
{
    /// <summary>
    /// 一种过程化服务的基类
    /// 
    /// 过程化简单地指：进行一系列操作，返回是否成功以及相应的提示消息。
    /// </summary>
    [Serializable]
    public abstract class FlowDomainService : DomainService, IFlowService
    {
        [ServiceOutput]
        public Result Result { get; set; }

        protected override sealed void Execute()
        {
            this.Result = this.ExecuteCore();
        }

        protected abstract Result ExecuteCore();
    }
}