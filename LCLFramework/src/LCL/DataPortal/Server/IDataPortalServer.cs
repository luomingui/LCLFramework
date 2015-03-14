using System;

namespace LCL.DataPortal.Server
{
    public interface IDataPortalServer
    {

        DataPortalResult Action(Type objectType, string methodName, object criteria, DataPortalContext context);

        DataPortalResult Fetch(Type objectType, object criteria, DataPortalContext context);
        DataPortalResult Update(object obj, DataPortalContext context);
    }
}