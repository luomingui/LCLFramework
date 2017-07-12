using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCL.Config.Startup
{
    public interface IModuleConfigurations
    {
        /// <summary>
        /// Gets the ABP configuration object.
        /// </summary>
        ILclStartupConfiguration LclConfiguration { get; }
    }
    internal class ModuleConfigurations : IModuleConfigurations
    {
        public ILclStartupConfiguration LclConfiguration { get; private set; }

        public ModuleConfigurations(ILclStartupConfiguration lclConfiguration)
        {
            LclConfiguration = lclConfiguration;
        }
    }
}
