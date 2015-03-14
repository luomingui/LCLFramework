using LCL;
using LCL.MetaModel;
using LCL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace UIShell.RbacManagementPlugin
{
    /// <summary>
    /// 为底层的 LCL 提供权限实现方案
    /// </summary>
    internal class LCLPermissionProvider : PermissionProvider
    {
        protected override bool CanShowModule(ModuleMeta module)
        {
            return HasModuleOperation(module.KeyLabel, string.Empty, SystemOperationKeys.Read);
        }
        protected override bool CanShowBlock(ModuleMeta block)
        {
            return HasModuleOperation(string.Empty, block.KeyLabel, SystemOperationKeys.Read);
        }
        protected override bool CanShowBlock(ModuleMeta module, string block)
        {
            return HasModuleOperation(module.KeyLabel, block, SystemOperationKeys.Read);
        }
        protected override bool HasCommand(ModuleMeta module, string block, string commandKey)
        {
            return HasModuleOperation(module.KeyLabel, block, commandKey);
        }
        protected override bool HasOperation(ModuleMeta module, ModuleOperation operation)
        {
            return HasModuleOperation(module.KeyLabel, string.Empty, operation.Name);
        }
        protected override bool HasCommand(string block, string module, string commandName)
        {
            return HasModuleOperation(module, block, commandName);
        }
        /// <summary>
        /// 检查是否拥有某个模块下某个实体某个操作的权限。
        /// </summary>
        /// <param name="module"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        private static bool HasModuleOperation(string module, string block, string operation)
        {
            bool hasPermission = true;
            try
            {
                //string userName = LEnvironment.Principal.Identity.Name;
                //hasPermission = RF.Concrete<IPositionOperationRepository>().HasModuleOperation(userName, module, block, operation);
            }
            catch (Exception ex)
            {
                Logger.LogError("检查是否拥有某个模块下某个实体某个操作的权限", ex);
            }
            return hasPermission;
        }
    }
}