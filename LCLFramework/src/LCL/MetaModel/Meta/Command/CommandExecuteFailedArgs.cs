using System;
using System.ComponentModel;

namespace LCL.MetaModel
{
    /// <summary>
    /// 命令执行失败时的命令参数
    /// </summary>
    public class CommandExecuteFailedArgs : CancelEventArgs
    {
        public CommandExecuteFailedArgs(Exception exception, object param)
        {
            this.Exception = exception;

            this.Parameter = param;
        }

        /// <summary>
        /// 执行命令时，发生了这个异常
        /// </summary>
        public Exception Exception { get; private set; }

        /// <summary>
        /// 发生错误时的参数
        /// </summary>
        public object Parameter { get; private set; }
    }
}
