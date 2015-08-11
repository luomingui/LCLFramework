using System;
using System.ComponentModel.DataAnnotations;

namespace UIShell.RbacPermissionService
{
    [Serializable]
    [MetadataType(typeof(XzqyMD))]
    public partial class Xzqy
    {
        public class XzqyMD
        {
            [Display(Name = "区划代码")]
            public string HelperCode { get; set; }
            [Display(Name = "区划名称")]
            public string Name { get; set; }
            [Display(Name = "区划介绍")]
            public string Intro { get; set; }
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
        }
    }
}

