using System; 
using System.ComponentModel.DataAnnotations;

namespace UIShell.RbacPermissionService 
{ 
    [Serializable] 
    [MetadataType(typeof(DictTypeMD))]  
    public partial class DictType  
    {  
        public class DictTypeMD  
        {  
            [Display(Name = "Ãû³Æ")]  
            public string Name { get; set; }  
            [Display(Name = "ÃèÊö")]  
            public string DicDes { get; set; }  
        }  
    }  
 
} 

