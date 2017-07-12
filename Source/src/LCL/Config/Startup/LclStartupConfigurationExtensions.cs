using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCL.Config.Startup
{
    public static class LclStartupConfigurationExtensions
    {
        public static void ReplaceService<TType>(this ILclStartupConfiguration configuration, Action replaceAction)
          where TType : class
        {
            configuration.ReplaceService(typeof(TType), replaceAction);
        }
    }
}
