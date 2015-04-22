using System; 
using System.ComponentModel.DataAnnotations; 
using System.Linq; 
using System.Text; 
 
namespace UIShell.RbacPermissionService 
{ 
    [Serializable] 
    [MetadataType(typeof(UserMD))]  
    public partial class User  
    {  
  
        public class UserMD  
        {  
            [Display(Name = "编号")]  
            public Guid ID { get; set; }  
            [Display(Name = "登录名称")]  
            public string Name { get; set; }
            [Display(Name = "登录密码")]
            public string Password { get; set; }  
            [Display(Name = "是否锁定")]  
            public bool IsLockedOut { get; set; }  
            [Display(Name = "添加时间")]  
            public DateTime AddDate { get; set; }  
            [Display(Name = "更新时间")]  
            public DateTime UpdateDate { get; set; }  
        }  
    }  
 
} 

