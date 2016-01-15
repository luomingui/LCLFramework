using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIShell.RbacPermissionService
{
    /// <summary>
    /// 维修单
    /// </summary>
    public partial class EDMSMaintenanceBill : BaseModel
    {
        public EDMSMaintenanceBill()
        {
            MaintenanceStatus = MaintenanceStatus.等待学校响应;
            FulfillDate = Convert.ToDateTime("1999-01-01");
            ResponseTime = Convert.ToDateTime("1999-01-01");
            PartsCosts = new HashSet<EDMSPartsCost>();
            VisitCost = 0;
        }
        #region 维修单
        /// <summary>
        /// 所属报修单
        /// </summary>
        public Guid EDMSMaintenanceBill_ID { get; set; }
        /// <summary>
        /// 维修人
        /// </summary>
        public string MaintainPerson { get; set; }
        /// <summary>
        /// 维修人电话
        /// </summary>
        public string MaintainPersonPhone { get; set; }
        /// <summary>
        /// 维修单位
        /// </summary>
        public string RepairUnit { get; set; }
        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime FulfillDate { get; set; }
        /// <summary>
        /// 响应时间
        /// </summary>
        public DateTime ResponseTime { get; set; }
        /// <summary>
        /// 故障现象
        /// </summary>
        public string FaultPhenomenon { get; set; }
        /// <summary>
        /// 故障判断
        /// </summary>
        public string FaultJudge { get; set; }
        /// <summary>
        /// 解决技巧
        /// </summary>
        public string SolvingSkills { get; set; }
        /// <summary>
        /// 维修记录
        /// </summary>
        public string Record { get; set; }
        /// <summary>
        /// 配件/服务费
        /// </summary>
        public ICollection<EDMSPartsCost> PartsCosts { get; set; }
        /// <summary>
        /// 上门费
        /// </summary>
        public int VisitCost { get; set; }
        /// <summary>
        /// 维修状态
        /// </summary>
        public MaintenanceStatus MaintenanceStatus { get; set; }
        #endregion
    }
}

