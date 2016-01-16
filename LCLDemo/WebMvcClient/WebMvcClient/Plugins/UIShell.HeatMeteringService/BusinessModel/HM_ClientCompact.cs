using System;
using UIShell.RbacPermissionService;

namespace UIShell.HeatMeteringService
{
    /// <summary>
    /// 供热合同
    /// </summary>
    [Serializable]
    public partial class HM_ClientCompact : BaseModel
    {
        /// <summary>
        /// 客户
        /// </summary>
        public HM_ClientInfo ClientInfo { set; get; }
        /// <summary>
        /// 用户
        /// </summary>
        public Guid User_ID { set; get; }
        /// <summary>
        /// 合同文件
        /// </summary>
        public string FilePath { set; get; }

    }
}
