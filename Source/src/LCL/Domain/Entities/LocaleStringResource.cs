using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCL.Domain.Entities
{
    public partial class LocaleStringResource : Entity
    {
        public string ResourceName { get; set; }
        public string ResourceValue { get; set; }
    }
}
