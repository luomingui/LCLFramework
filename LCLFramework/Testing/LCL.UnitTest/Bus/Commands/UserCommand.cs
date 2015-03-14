using LCL.Commands;
using System;

namespace Apworks.Tests.Buses.Commands
{
    public class UserCommand : Command
    {
        public string Code { set; get; }
        public string Name { set; get; }

        public UserCommand(string Name, string Code)
        {
            this.Name = Name;
            this.Code = Code;
        }
    }
}
