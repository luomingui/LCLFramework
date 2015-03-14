using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Happy.Command
{
    /// <summary>
    /// 命令服务接口，负责执行命令。
    /// </summary>
    public interface ICommandService
    {
        /// <summary>
        /// 执行命令。
        /// </summary>
        void Execute<TCommand>(TCommand command)
            where TCommand : ICommand;

        /// <summary>
        /// 注册依赖的服务实例。
        /// </summary>
        ICommandService AddService<T>(T service);

        /// <summary>
        /// 获取依赖的服务实例。
        /// </summary>
        T GetService<T>();
    }
}
