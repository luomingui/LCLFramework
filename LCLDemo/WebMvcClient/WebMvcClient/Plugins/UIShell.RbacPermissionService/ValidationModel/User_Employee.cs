using System; 
using System.ComponentModel.DataAnnotations; 
using System.Linq; 
using System.Text; 
 
namespace UIShell.RbacPermissionService 
{ 
    [Serializable] 
    [MetadataType(typeof(User_EmployeeMD))]  
    public partial class User_Employee  
    {  
        public class User_EmployeeMD  
        {  
            [Display(Name = "编号")]  
            public Guid ID { get; set; }  
            [Display(Name = "工号")]  
            public string HelperCode { get; set; }  
            [Display(Name = "添加时间")]  
            public DateTime AddDate { get; set; }  
            [Display(Name = "更新时间")]  
            public DateTime UpdateDate { get; set; }  
        }  
    }  
 
} 

