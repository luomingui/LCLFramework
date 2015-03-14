using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Happy.Command.Internal
{
    internal sealed class CommandInterceptorChain
    {
        private ICommandExecuteContext _commandExecuteContext;
        private CommandInterceptorAttribute[] _commandInterceptors;
        private Action _commandExecutor;
        private int _currentCommandInterceptorIndex = -1;

        internal CommandInterceptorChain(
            ICommandExecuteContext commandExecuteContext,
            CommandInterceptorAttribute[] commandInterceptors,
            Action commandExecutor)
        {
            _commandExecuteContext = commandExecuteContext;
            _commandInterceptors = commandInterceptors.OrderBy(x => x.Order).ToArray();
            _commandExecutor = commandExecutor;
        }

        private CommandInterceptorAttribute CurrentCommandInterceptor
        {
            get
            {
                return _commandInterceptors[_currentCommandInterceptorIndex];
            }
        }

        internal void ExecuteNext()
        {
            _currentCommandInterceptorIndex++;

            if (_currentCommandInterceptorIndex < _commandInterceptors.Length)
            {
                this.CurrentCommandInterceptor.Intercept(_commandExecuteContext );
            }
            else
            {
                _commandExecutor();
            }
        }
    }
}
