using System; 
using System.ComponentModel.DataAnnotations;

namespace UIShell.RbacPermissionService 
{ 
    [Serializable] 
    [MetadataType(typeof(RoleAuthorityMD))]  
    public partial class RoleAuthority  
    {  
        public class RoleAuthorityMD  
        {  
            [Display(Name = "块")]  
            public string BlockKey { get; set; }  
            [Display(Name = "模块")]  
            public string ModuleKey { get; set; }  
            [Display(Name = "功能")]  
            public string OperationKey { get; set; }  
            [Display(Name = "Url")]  
            public string Url { get; set; }  
            [Display(Name = "类型")]  
            public int AuthorityType { get; set; }  
        }  
    }  
 
} 

