using System; 
using System.ComponentModel.DataAnnotations; 
using System.Linq; 
using System.Text; 
 
namespace UIShell.RbacPermissionService 
{ 
    [Serializable] 
    [MetadataType(typeof(UserInfoMD))]  
    public partial class UserInfo  
    {  
        public class UserInfoMD  
        {  
            [Display(Name = "编号")]  
            public Guid ID { get; set; }  
            [Display(Name = "真实姓名")]  
            public string TName { get; set; }  
            [Display(Name = "曾用名")]  
            public string CengYongMing { get; set; }  
            [Display(Name = "姓名全拼")]  
            public string NameQuanPin { get; set; }  
            [Display(Name = "性别")]  
            public string Sex { get; set; }  
            [Display(Name = "出生年月")]  
            public string Birthday { get; set; }  
            [Display(Name = "民族")]  
            public int NationalID { get; set; }  
            [Display(Name = "政治面貌")]  
            public int PoliticalID { get; set; }  
            [Display(Name = "籍贯")]  
            public string NativeID { get; set; }  
            [Display(Name = "照片")]  
            public string UserPhoto { get; set; }  
            [Display(Name = "身份证号")]  
            public string IdCard { get; set; }  
            [Display(Name = "电话")]  
            public string Telephone { get; set; }  
            [Display(Name = "电子邮箱")]  
            public string Email { get; set; }  
            [Display(Name = "用户QQ")]  
            public string UserQQ { get; set; }  
        }  
    }  
 
} 

