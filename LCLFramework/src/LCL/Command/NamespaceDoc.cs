using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Happy.Command
{
    /// <summary>
    /// 命令模式的变体，将命令和处理方法分离的版本。
    /// </summary>
    /// <remarks>
    /// <see cref="CommandService"/>执行<see cref="ICommand"/>的过程如下：
    /// <list type="number">
    ///     <item>同步执行由<see cref="CommandInterceptorAttribute"/>组成的管道，执行发布前的预处理。</item>
    ///     <item>同步执行<see cref="ICommandHandler{TCommand}"/>。</item>
    ///     <item>同步执行由<see cref="CommandInterceptorAttribute"/>组成的管道，执行发布后的后续处理。</item>
    /// </list>
    /// </remarks>
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    class NamespaceDoc
    {
    }
}
