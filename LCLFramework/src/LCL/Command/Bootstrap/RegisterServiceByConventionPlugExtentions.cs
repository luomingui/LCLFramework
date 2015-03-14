using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Happy.Bootstrap;
using Happy.Bootstrap.RegistyByConvention;
using Happy.Command.Bootstrap.Internal;

namespace Happy.Command.Bootstrap
{
    /// <summary>
    /// 扩展<see cref="RegisterServiceByConventionPlugin"/>。
    /// </summary>
    public static class RegisterServiceByConventionPlugExtentions
    {
        /// <summary>
        /// 按照约定自动注册命令处理器。
        /// </summary>
        public static RegisterServiceByConventionPlugin UseCommandRegister(this RegisterServiceByConventionPlugin that)
        {
            return that.AddConvertion(new CommandRegister());
        }
    }
}
