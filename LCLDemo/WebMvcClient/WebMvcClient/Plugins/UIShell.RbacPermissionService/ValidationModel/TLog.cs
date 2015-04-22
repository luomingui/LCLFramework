using System; 
using System.ComponentModel.DataAnnotations; 
using System.Linq; 
using System.Text; 
 
namespace UIShell.RbacPermissionService 
{ 
    [Serializable] 
    [MetadataType(typeof(TLogMD))]  
    public partial class TLog  
    {  
  
        public class TLogMD  
        {  
            [Display(Name = "编号")]  
            public Guid ID { get; set; }  
            [Display(Name = "单位")]  
            public Guid Org_Id { get; set; }  
            [Display(Name = "用户")]  
            public Guid UserId { get; set; }  
            [Display(Name = "内容")]  
            public string Content { get; set; }  
            [Display(Name = "添加时间")]  
            public DateTime AddDate { get; set; }  
            [Display(Name = "更新时间")]  
            public DateTime UpdateDate { get; set; }  
        }  
    }  
 
} 

