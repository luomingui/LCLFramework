using System; 
using System.ComponentModel.DataAnnotations;

namespace UIShell.RbacPermissionService 
{ 
    [Serializable] 
    [MetadataType(typeof(UserMD))]  
    public partial class User  
    {  
        public class UserMD  
        {  
            [Display(Name = "登录名称")]  
            public string Name { get; set; }  
            [Display(Name = "登录密码")]
            [Required(ErrorMessage = "密码不能为空")]
            [StringLength(20, ErrorMessage = "密码长度不能超过20个字符")]
            [DataType(DataType.Password)]
            public string Password { get; set; }  
            [Display(Name = "是否锁定")]  
            public bool IsLockedOut { get; set; }  
        }  
    }  
 
} 

