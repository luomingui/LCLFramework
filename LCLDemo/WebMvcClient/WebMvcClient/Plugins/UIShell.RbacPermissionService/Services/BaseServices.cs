using LCL;
using LCL.DomainServices;
using LCL.MvcExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIShell.RbacPermissionService.Services
{
    [Serializable]
    [Contract, ContractImpl]
    public abstract class BaseServices : DomainService
    {
        protected abstract void ExecuteCode();
        protected override void Execute()
        {
            ExecuteCode();
        }
    }
}
