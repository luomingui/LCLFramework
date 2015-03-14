using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace LCL.MetaModel
{
    /// <summary>
    /// 运行时命令
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// 是否这个命令所对应的按钮可以被执行
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        bool CanExecute(object param);

        /// <summary>
        /// 执行这个命令
        /// </summary>
        /// <param name="param"></param>
        void Execute(object param);
    }
}