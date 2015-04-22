using System; 
using System.ComponentModel.DataAnnotations; 
using System.Linq; 
using System.Text; 
 
namespace UIShell.RbacPermissionService 
{ 
    [Serializable] 
    [MetadataType(typeof(ScheduledEventsMD))]  
    public partial class ScheduledEvents  
    {  
  
        public class ScheduledEventsMD  
        {  
            [Display(Name = "编号")]  
            public Guid ID { get; set; }  
            [Display(Name = "计划任务名称")]  
            public string Key { get; set; }  
            [Display(Name = "上次执行时间")]  
            public DateTime Lastexecuted { get; set; }  
            [Display(Name = "服务名称")]  
            public string ServerName { get; set; }  
            [Display(Name = "添加时间")]  
            public DateTime AddDate { get; set; }  
            [Display(Name = "更新时间")]  
            public DateTime UpdateDate { get; set; }  
        }  
    }  
 
} 

