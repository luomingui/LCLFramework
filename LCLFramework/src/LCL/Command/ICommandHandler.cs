using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Happy.Command
{
    /// <summary>
    /// 命令处理器接口，一个命令只能有一个命令处理器。
    /// </summary>
    public interface ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        /// <summary>
        /// 处理命令。
        /// </summary>
        void Handle(TCommand command);
    }
}
