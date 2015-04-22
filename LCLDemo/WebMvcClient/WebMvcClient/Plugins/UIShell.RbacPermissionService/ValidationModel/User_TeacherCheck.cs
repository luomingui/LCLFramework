using System; 
using System.ComponentModel.DataAnnotations; 
using System.Linq; 
using System.Text; 
 
namespace UIShell.RbacPermissionService 
{ 
    [Serializable] 
    [MetadataType(typeof(User_TeacherCheckMD))]  
    public partial class User_TeacherCheck  
    {  
        public class User_TeacherCheckMD  
        {  
            [Display(Name = "编号")]  
            public Guid ID { get; set; }  
            [Display(Name = "学校审核通过")]  
            public bool IsPassSchool { get; set; }  
            [Display(Name = "学校审核者")]  
            public Guid SchoolPassBy { get; set; }  
            [Display(Name = "学校审核时间")]  
            public DateTime SchoolPassTime { get; set; }  
            [Display(Name = "学校审核意见")]  
            public string SchoolPassComments { get; set; }  
            [Display(Name = "县局审核通过")]  
            public bool IsPassCounty { get; set; }  
            [Display(Name = "县局审核者")]  
            public Guid CountyPassBy { get; set; }  
            [Display(Name = "县局审核时间")]  
            public DateTime CountyPassTime { get; set; }  
            [Display(Name = "县局审核意见")]  
            public string CountyPassComments { get; set; }  
            [Display(Name = "市局审核通过")]  
            public bool IsPassCity { get; set; }  
            [Display(Name = "市局审核时间")]  
            public DateTime CitylPassTime { get; set; }  
            [Display(Name = "市局审核者")]  
            public Guid CityPassBy { get; set; }  
            [Display(Name = "市局审核意见")]  
            public string CityPassComments { get; set; }  
            [Display(Name = "省局审核通过")]  
            public bool IsPassProvince { get; set; }  
            [Display(Name = "省局审核者")]  
            public Guid ProvincePassBy { get; set; }  
            [Display(Name = "省局审核时间")]  
            public DateTime ProvincePassTime { get; set; }  
            [Display(Name = "省局审核意见")]  
            public string ProvincePassComments { get; set; }  
            [Display(Name = "添加时间")]  
            public DateTime AddDate { get; set; }  
            [Display(Name = "更新时间")]  
            public DateTime UpdateDate { get; set; }  
        }  
    }  
} 

