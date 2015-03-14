using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCL.MetaModel
{
    public class CommandRepository
    {
        internal void AddByAssembly(System.Reflection.Assembly assembly)
        {
            var commands = assembly.GetTypes().Where(t => typeof(ICommand) == t.BaseType);
            foreach (var type in commands)
            {
               // this.Add(type);
            }
        }
      
    }
}
