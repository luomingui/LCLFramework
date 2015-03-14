using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Happy.Command
{
    /// <summary>
    /// 命令拦截器，拦截正在被执行的命令。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public abstract class CommandInterceptorAttribute : Attribute
    {
        /// <summary>
        /// 构造方法。
        /// </summary>
        /// <param name="order">指示拦截器在管道中的位置</param>
        protected CommandInterceptorAttribute(int order)
        {
            this.Order = order;
        }

        /// <summary>
        /// 拦截正在被执行的命令。
        /// </summary>
        /// <param name="context">命令执行上下文</param>
        public abstract void Intercept(ICommandExecuteContext context);

        /// <summary>
        /// 拦截器在管道中的位置。
        /// </summary>
        public int Order { get; protected set; }
    }
}
