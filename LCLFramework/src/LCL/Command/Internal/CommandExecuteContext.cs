using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Happy.Infrastructure.ExtentionMethods.Reflection;

namespace Happy.Command.Internal
{
    internal sealed class CommandExecuteContext : ICommandExecuteContext
    {
        private CommandInterceptorChain _commandInterceptorChain;

        internal CommandExecuteContext(ICommandService commandService, ICommand command, Action commandExecutor)
        {
            this.CommandService = commandService;
            this.Command = command;
            _commandInterceptorChain = new CommandInterceptorChain(
                this,
                command.GetType().GetAttributes<CommandInterceptorAttribute>(),
                commandExecutor);
        }


        public ICommandService CommandService
        {
            get;
            private set;
        }

        public ICommand Command { get; private set; }

        public void ExecuteNext()
        {
            _commandInterceptorChain.ExecuteNext();
        }
    }
}
