using System;
using System.ComponentModel.DataAnnotations;

namespace UIShell.RbacPermissionService
{
    [Serializable]
    [MetadataType(typeof(DepartmentMD))]
    public partial class Department
    {
        public class DepartmentMD
        {
            [Display(Name = "部门类型")]
            public int DepartmentType { get; set; }
            [Display(Name = "名称")]
            [Required(ErrorMessage = "名称不能为空")]
            [StringLength(20, ErrorMessage = "名称长度不能超过20个字符")]
            public string Name { get; set; }
            [Display(Name = "办公电话")]
            public string OfficePhone { get; set; }
            [Display(Name = "地址")]
            public string Address { get; set; }
            [Display(Name = "来源")]
            public string Source { get; set; }
            [Display(Name = "描述")]
            public string Remark { get; set; }
        }
    }

}

