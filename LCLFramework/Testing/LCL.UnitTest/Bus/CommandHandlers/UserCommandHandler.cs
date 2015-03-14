using Apworks.Tests.Buses.Commands;
using LCL.Commands;
using LCL.Repositories;
using System.Diagnostics;

namespace Apworks.Tests.Buses.CommandHandlers
{
    public class UserCommandHandler : CommandHandler<UserCommand>
    {
        public UserCommandHandler()
        {

        }

        public override void Handle(UserCommand command)
        {
            Debug.WriteLine("UserCommandHandler  Name=" + command.Name + " Code=" + command.Code + "");
        }
    }
}
