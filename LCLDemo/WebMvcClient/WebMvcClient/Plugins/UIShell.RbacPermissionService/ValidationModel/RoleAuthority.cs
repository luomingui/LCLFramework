using System; 
using System.ComponentModel.DataAnnotations; 
using System.Linq; 
using System.Text; 
 
namespace UIShell.RbacPermissionService 
{ 
    [Serializable] 
    [MetadataType(typeof(RoleAuthorityMD))]  
    public partial class RoleAuthority  
    {  
  
        public class RoleAuthorityMD  
        {  
            [Display(Name = "编号")]  
            public Guid ID { get; set; }  
            [Display(Name = "块")]  
            public string BlockKey { get; set; }  
            [Display(Name = "模块")]  
            public string ModuleKey { get; set; }  
            [Display(Name = "功能")]  
            public string OperationKey { get; set; }  
            [Display(Name = "类型")]  
            public int AuthorityType { get; set; }  
            [Display(Name = "添加时间")]  
            public DateTime AddDate { get; set; }  
            [Display(Name = "更新时间")]  
            public DateTime UpdateDate { get; set; }  
        }  
    }  
 
} 

