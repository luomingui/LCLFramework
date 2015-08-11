using System; 
using System.ComponentModel.DataAnnotations;

namespace UIShell.RbacPermissionService 
{ 
    [Serializable] 
    [MetadataType(typeof(DictionaryMD))]  
    public partial class Dictionary  
    {  
        public class DictionaryMD  
        {  
            [Display(Name = "Ãû³Æ")]  
            public string Name { get; set; }  
            [Display(Name = "Öµ")]  
            public string Value { get; set; }  
            [Display(Name = "ÅÅÐò")]  
            public int Order { get; set; }  
        }  
    }  
 
} 

