using System; 
using System.ComponentModel.DataAnnotations;

namespace UIShell.RbacPermissionService 
{ 
    [Serializable] 
    [MetadataType(typeof(RoleMD))]  
    public partial class Role  
    {  
        public class RoleMD  
        {  
            [Display(Name = "Ãû³Æ")]  
            public string Name { get; set; }  
            [Display(Name = "ÃèÊö")]  
            public string Remark { get; set; }  
        }  
    }  
 
} 

