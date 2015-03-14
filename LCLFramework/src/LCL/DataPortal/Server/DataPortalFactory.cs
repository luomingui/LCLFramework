using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCL.DataPortal.Server
{
    internal class DataPortalFactory
    {
        internal static IDataPortalServer Factory()
        {
            IDataPortalServer dps = null;

            DataPortalType dpt = DataPortalType.LCL;
            switch (dpt)
            {
                case DataPortalType.Final:
                    dps = new FinalDataPortal();
                    break;
                case DataPortalType.LCL:
                    dps = new LCLDataPortal();
                    break;
                default:
                    break;
            }
            return dps;
        }
    }
    internal enum DataPortalType
    {
        Final,
        LCL
    }
}
