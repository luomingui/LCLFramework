using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCL.Domain.Model.ValidationModel
{
    [Serializable]
    [MetadataType(typeof(RoleMD))]
    public partial class Role
    {
        public class RoleMD
        {
            [Display(Name = "名称")]
            public string Name { get; set; }
            [Display(Name = "描述")]
            public string Description { get; set; }
        }
    }
}
