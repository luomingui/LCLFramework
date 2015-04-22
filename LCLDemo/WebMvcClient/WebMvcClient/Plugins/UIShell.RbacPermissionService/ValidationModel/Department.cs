using System; 
using System.ComponentModel.DataAnnotations; 
using System.Linq; 
using System.Text; 
 
namespace UIShell.RbacPermissionService 
{ 
    [Serializable] 
    [MetadataType(typeof(DepartmentMD))]  
    public partial class Department  
    {  
        public class DepartmentMD  
        {  
            [Display(Name = "编号")]  
            public Guid ID { get; set; }  
            [Display(Name = "代码")]  
            public string HelperCode { get; set; }  
            [Display(Name = "名称")]  
            public string Name { get; set; }  
            [Display(Name = "是否最后一级")]  
            public bool IsLast { get; set; }  
            [Display(Name = "树形深度")]  
            public int Level { get; set; }  
            [Display(Name = "树形路径")]  
            public string NodePath { get; set; }  
            [Display(Name = "排序")]  
            public int OrderBy { get; set; }  
            [Display(Name = "上一级")]  
            public Guid ParentId { get; set; }  
            [Display(Name = "添加时间")]  
            public DateTime AddDate { get; set; }  
            [Display(Name = "更新时间")]  
            public DateTime UpdateDate { get; set; }  
        }  
    }  
 
} 

