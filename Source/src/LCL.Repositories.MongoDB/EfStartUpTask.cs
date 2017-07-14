using LCL;
using LCL.Infrastructure;
using LCL.LData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCL.Repositories.MongoDB
{
    public class EfStartUpTask : IStartupTask
    {
        public void Execute()
        {
        
        }

        public int Order
        {
            //ensure that this task is run first 
            get { return -1000; }
        }
    }
}
