using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LCL.Domain.Entities
{
    public interface IMustHaveTenant
    {
        int TenantId { get; set; }
    }
}
