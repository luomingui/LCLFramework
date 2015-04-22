using System; 
using System.ComponentModel.DataAnnotations; 
using System.Linq; 
using System.Text; 
 
namespace UIShell.RbacPermissionService 
{ 
    [Serializable] 
    [MetadataType(typeof(User_TeacherMD))]  
    public partial class User_Teacher  
    {  
        public class User_TeacherMD  
        {  
            [Display(Name = "编号")]  
            public Guid ID { get; set; }  
            [Display(Name = "是否取得教师资格证书")]  
            public bool IsGetZGZ { get; set; }  
            [Display(Name = "取得教师资格证书书时间")]  
            public DateTime GetZGZTime { get; set; }  
            [Display(Name = "教师资格证书号")]  
            public string ZGZNo { get; set; }  
            [Display(Name = "计算机等级")]  
            public string ComputerRank { get; set; }  
            [Display(Name = "最高学位")]  
            public int DegreeID { get; set; }  
            [Display(Name = "添加时间")]  
            public DateTime AddDate { get; set; }  
            [Display(Name = "更新时间")]  
            public DateTime UpdateDate { get; set; }  
        }  
    }  
 
} 

