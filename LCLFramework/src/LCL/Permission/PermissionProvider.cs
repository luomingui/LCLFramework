using LCL.MetaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LCL
{
    /// <summary>
    /// 权限提供程序。
    /// </summary>
    public class PermissionProvider
    {
        protected internal PermissionProvider() { }

        /// <summary>
        /// 是否能显示某个模块
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        internal protected virtual bool CanShowModule(ModuleMeta module)
        {
            return true;
        }
        internal protected virtual bool CanShowBlock(ModuleMeta block)
        {
            return true;
        }
        /// <summary>
        /// 控制某一个块是否可以显示
        /// </summary>
        /// <param name="module"></param>
        /// <param name="block"></param>
        /// <returns></returns>
        internal protected virtual bool CanShowBlock(ModuleMeta module, string block)
        {
            return true;
        }

        /// <summary>
        /// 是否有某个操作的权限
        /// </summary>
        /// <param name="module"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        internal protected virtual bool HasOperation(ModuleMeta module, ModuleOperation operation)
        {
            return true;
        }

        /// <summary>
        /// 是否能执行某个命令
        /// </summary>
        /// <param name="module"></param>
        /// <param name="block"></param>
        /// <param name="commandName"></param>
        /// <returns></returns>
        internal protected virtual bool HasCommand(ModuleMeta module, string block, string commandName)
        {
            return true;
        }

        internal protected virtual bool HasCommand(string block, string module, string commandName)
        {
            return true;
        }
    }
}
