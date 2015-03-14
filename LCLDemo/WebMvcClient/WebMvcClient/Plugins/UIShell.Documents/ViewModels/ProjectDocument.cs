using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UIShell.Documents.Model
{
    [Serializable]
    [MetadataType(typeof(ProjectDocumentMD))]
    public partial class ProjectDocument
    {
        public class ProjectDocumentMD
        {
            [Display(Name = "项目名称")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "项目名称不能为空")]
            public string ProjectName { get; set; }
            [Display(Name = "标题")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "标题不能为空")]
            public string Title { get; set; }
            [Display(Name = "类型名称")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "类型名称不能为空")]
            public string TypeName { get; set; }
            [Display(Name = "版本号")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "版本号不能为空")]
            public string Version { get; set; }
            [Display(Name = "内容")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "内容不能为空")]
            public string Content { get; set; }
        }
    }
}