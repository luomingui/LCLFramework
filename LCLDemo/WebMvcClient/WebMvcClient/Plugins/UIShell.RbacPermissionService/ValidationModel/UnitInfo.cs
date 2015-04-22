using System; 
using System.ComponentModel.DataAnnotations; 
using System.Linq; 
using System.Text; 
 
namespace UIShell.RbacPermissionService 
{ 
    [Serializable] 
    [MetadataType(typeof(UnitInfoMD))]  
    public partial class UnitInfo  
    {  
  
        public class UnitInfoMD  
        {  
            [Display(Name = "编号")]  
            public Guid ID { get; set; }  
            [Display(Name = "组织机构代码")]  
            public string HelperCode { get; set; }  
            [Display(Name = "名称")]  
            public string Name { get; set; }  
            [Display(Name = "全称")]  
            public string NameFull { get; set; }  
            [Display(Name = "外文名称")]  
            public string NameEN { get; set; }  
            [Display(Name = "电子邮箱")]  
            public string Email { get; set; }  
            [Display(Name = "办公电话")]  
            public string OfficePhone { get; set; }  
            [Display(Name = "传真电话")]  
            public string FaxPhone { get; set; }  
            [Display(Name = "地址")]  
            public string Address { get; set; }  
            [Display(Name = "邮政编码")]  
            public string ZipCode { get; set; }  
            [Display(Name = "单位网址")]  
            public string HomePage { get; set; }  
            [Display(Name = "描述")]  
            public string Remark { get; set; }  
            [Display(Name = "单位类型")]
            public UnitType UnitType { get; set; }  
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

