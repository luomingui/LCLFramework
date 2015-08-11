using System;
using System.ComponentModel.DataAnnotations;

namespace UIShell.RbacPermissionService
{
    [Serializable]
    [MetadataType(typeof(TLogMD))]
    public partial class TLog
    {
        public class TLogMD
        {
            [Display(Name = "标题")]
            public string Title { get; set; }
            [Display(Name = "内容")]
            public string Content { get; set; }
            [Display(Name = "用户名")]
            public string UserName { get; set; }
            [Display(Name = "机器名")]
            public string MachineName { get; set; }
            [Display(Name = "模块名")]
            public string ModuleName { get; set; }
            [Display(Name = "日志类型")]
            public EnumLogType LogType { get; set; }
            [Display(Name = "IP地址")]
            public string IP { get; set; }
            [Display(Name = "网址")]
            public string url { get; set; }
            [Display(Name = "浏览器")]
            public string Browser { get; set; }
            [Display(Name = "支持ActiveX")]
            public bool IsActiveX { get; set; }
        }
    }
}

