using System;
using System.Security.Principal;
using System.Threading;

namespace LCL.DataPortal.Server
{
    public class DataPortalFacade : IDataPortalServer
    {
        public DataPortalResult Action(Type objectType, string methodName, object criteria, DataPortalContext context)
        {
            try
            {
                SetContext(context);

                var portal = DataPortalFactory.Factory();
                var result = portal.Action(objectType, methodName, criteria, context);

                return result;
            }
            finally
            {
                ClearContext(context);
            }
        }
        public DataPortalResult Fetch(Type objectType, object criteria, DataPortalContext context)
        {
            try
            {
                SetContext(context);

                var portal = DataPortalFactory.Factory();
                var result = portal.Fetch(objectType, criteria, context);

                return result;
            }
            finally
            {
                ClearContext(context);
            }
        }
        public DataPortalResult Update(object obj, DataPortalContext context)
        {
            try
            {
                SetContext(context);

                var portal = DataPortalFactory.Factory();
                var result = portal.Update(obj, context);

                return result;
            }
            finally
            {
                ClearContext(context);
            }
        }
        private static void SetContext(DataPortalContext context)
        {
            DistributionContext.SetLogicalExecutionLocation(DistributionContext.LogicalExecutionLocations.Server);
            if (!context.IsRemotePortal) return;
            DistributionContext.SetExecutionLocation(DistributionContext.ExecutionLocations.Server);
            DistributionContext.SetContext(context.ClientContext, context.GlobalContext);

            System.Threading.Thread.CurrentThread.CurrentCulture =
              new System.Globalization.CultureInfo(context.ClientCulture);
            System.Threading.Thread.CurrentThread.CurrentUICulture =
              new System.Globalization.CultureInfo(context.ClientUICulture);

            if (DistributionContext.AuthenticationType == "Windows")
            {
                if (context.Principal != null)
                {
                    System.Security.SecurityException ex =
                      new System.Security.SecurityException("Resources.NoPrincipalAllowedException");
                    ex.Action = System.Security.Permissions.SecurityAction.Demand;
                    throw ex;
                }
                AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
            }
            else
            {
                if (context.Principal == null)
                {
                    System.Security.SecurityException ex =
                      new System.Security.SecurityException(
                        "Resources.BusinessPrincipalException" + " Nothing");
                    ex.Action = System.Security.Permissions.SecurityAction.Demand;
                    throw ex;
                }
                DistributionContext.User = context.Principal;
                LEnvironment.Principal = context.Principal;
            }
        }
        private static void ClearContext(DataPortalContext context)
        {
            DistributionContext.SetLogicalExecutionLocation(DistributionContext.LogicalExecutionLocations.Client);
            if (!context.IsRemotePortal) return;
            DistributionContext.Clear();
            if (DistributionContext.AuthenticationType != "Windows")
                DistributionContext.User = null;
            LEnvironment.Principal = null;
        }
    }
}