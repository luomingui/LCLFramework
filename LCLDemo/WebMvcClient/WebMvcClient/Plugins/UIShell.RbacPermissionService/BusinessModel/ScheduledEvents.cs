using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UIShell.RbacPermissionService
{
    /// <summary>
    /// 计划任务
    /// </summary>
    public partial class ScheduledEvents : BaseModel
    {
        public ScheduledEvents()
        {
            Lastexecuted = Convert.ToDateTime("1999-01-01");
            Enable = false;
            hour = 1;
            minute = 1;
        }
        /// <summary>
        /// 计划任务名称
        /// </summary>
        [Required(ErrorMessage = "计划任务名称不能为空")]
        [Display(Name = "计划任务名称")]
        public string Key { get; set; }
        /// <summary>
        /// 上次执行时间
        /// </summary>
        [Display(Name = "上次执行时间")]
        public Nullable<DateTime> Lastexecuted { get; set; }
        /// <summary>
        /// 服务名称
        /// </summary>
        [Display(Name = "服务名称")]
        public string ServerName { get; set; }

        #region NotMapped
        public string ScheduleType { get; set; }
        /// <summary>
        /// 执行方式 定时执行 周期执行
        /// </summary>
        public bool ExetimeType { get; set; }
        public int hour { get; set; }
        public int minute { get; set; }
        public int timeserval { get; set; }
        /// <summary>
        /// 执行方式 定时执行 周期执行
        /// </summary>
        public string Exetime { get; set; }
        /// <summary>
        /// 状态 开启，关闭
        /// </summary>
        public bool Enable { get; set; }
        /// <summary>
        /// 级别 系统，非系统
        /// </summary>
        public bool Issystemevent { get; set; }
        #endregion
        
    }
}
