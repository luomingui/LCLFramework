using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIShell.RbacPermissionService
{
    public interface IWorkContext
    {
        GeneralConfigInfo Config { get; }
    }
}
