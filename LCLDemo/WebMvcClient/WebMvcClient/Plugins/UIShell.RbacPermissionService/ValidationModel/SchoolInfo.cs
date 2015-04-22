using System; 
using System.ComponentModel.DataAnnotations; 
using System.Linq; 
using System.Text; 
 
namespace UIShell.RbacPermissionService 
{ 
    [Serializable] 
    [MetadataType(typeof(SchoolInfoMD))]  
    public partial class SchoolInfo  
    {  
  
        public class SchoolInfoMD  
        {  
            [Display(Name = "编号")]  
            public Guid ID { get; set; }  
            [Display(Name = "校长名称")]  
            public string HeadmasterName { get; set; }  
            [Display(Name = "校长手机")]  
            public string HeadmasterPhone { get; set; }  
            [Display(Name = "校庆日")]  
            public DateTime SchoolYear { get; set; }  
            [Display(Name = "土地证号")]  
            public string LandCardNo { get; set; }  
            [Display(Name = "添加时间")]  
            public DateTime AddDate { get; set; }  
            [Display(Name = "更新时间")]  
            public DateTime UpdateDate { get; set; }  
        }  
    }  
 
} 

