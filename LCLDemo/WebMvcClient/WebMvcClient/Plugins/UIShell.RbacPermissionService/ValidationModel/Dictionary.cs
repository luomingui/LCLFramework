using System; 
using System.ComponentModel.DataAnnotations; 
using System.Linq; 
using System.Text; 
 
namespace UIShell.RbacPermissionService 
{ 
    [Serializable] 
    [MetadataType(typeof(DictionaryMD))]  
    public partial class Dictionary  
    {  
  
        public class DictionaryMD  
        {  
            [Display(Name = "编号")]  
            public Guid ID { get; set; }  
            [Display(Name = "名称")]  
            public string Name { get; set; }  
            [Display(Name = "值")]  
            public string Value { get; set; }  
            [Display(Name = "排序")]  
            public int Order { get; set; }  
            [Display(Name = "添加时间")]  
            public DateTime AddDate { get; set; }  
            [Display(Name = "更新时间")]  
            public DateTime UpdateDate { get; set; }  
        }  
    }  
 
} 

