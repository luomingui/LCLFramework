using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LCL.Domain.Entities
{
    public interface IMayHaveTenant
    {
        int? TenantId { get; set; }
    }
}
