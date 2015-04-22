using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIShell.RbacPermissionService
{
    public interface IEvent
    {
        void Execute(object state);
    }
}
