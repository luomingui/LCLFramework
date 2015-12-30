using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIShell.RbacPermissionService
{
    /// <summary>
    /// 报修状态
    /// </summary>
    public enum RepairStatus
    {
        未响应 = 0,
        已响应 = 1
    }
    /// <summary>
    /// 维修状态
    /// </summary>
    public enum MaintenanceStatus
    {
        等待企业响应 = 0,
        企业维修中 = 1,
        等待学校响应 = 2,
        学校自修中 = 3,
        审批不通过=4,
        撤销响应 = 5,
        学校审批中=6,
        学校验收中=7,
        企业退回=8,
        维修完成=9,
        已评价=10,
        验收不通过=11
    }
    /// <summary>
    /// 结算状态
    /// </summary>
    public enum AccountsStatus
    {
        未申请 = 0,
        待审核 = 1,
        待结算 = 2,
        已结算 = 3,
        已评价 = 4,
        审核不通过 = 5,
        
    }
}
