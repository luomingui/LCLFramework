using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using Common.Logging;
using Microsoft.Practices.ServiceLocation;

using Happy.Infrastructure.ExtentionMethods;

namespace Happy.Command.Internal
{
    internal sealed class DefaultCommandService : ICommandService
    {
        private readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

        public void Execute<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            command.MustNotNull("command");

            var context = this.CreateCommandExecuteContext(command);

            context.ExecuteNext();
        }

        public ICommandService AddService<T>(T service)
        {
            _services[typeof(T)] = service;

            return this;
        }

        public T GetService<T>()
        {
            return (T)_services[typeof(T)];
        }

        private CommandExecuteContext CreateCommandExecuteContext<TCommand>(TCommand command)
            where TCommand : ICommand
        {

            return new CommandExecuteContext(this, command, () =>
            {
                this.ExecuteCommandHandler(command);
            });
        }

        private void ExecuteCommandHandler<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            ServiceLocator.Current.MustNotNull("ServiceLocator.Current");

            var commandHandler = ServiceLocator
                .Current
                .GetInstance<ICommandHandler<TCommand>>();

            commandHandler.Handle(command);
        }
    }
}
