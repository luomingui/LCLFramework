using LCL.DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LCL
{
    public class AppLocator
    {
        public static void DomainLocattor()
        {
            DomainServiceLocator.TryAddPluginsService();

        }
    }
}
