using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using LCL.DataPortal.Server.Hosts.WcfChannel;

namespace LCL.DataPortal.Server.Hosts
{
    /// <summary>
    /// 使用 WCF 实现的统一的数据门户。
    /// 
    /// 标记了 ConcurrencyMode.Multiple 来表示多线程进行
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple,
        UseSynchronizationContext = false)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class WcfPortal : IWcfPortal
    {
        public WcfResponse Action(ActionRequest request)
        {
            DataPortalFacade portal = new DataPortalFacade();
            object result;
            try
            {
                result = portal.Action(request.ObjectType, request.MethodName, request.Criteria, request.Context);
            }
            catch (Exception ex)
            {
                result = ex;
            }
            return new WcfResponse(result);
        }
        public WcfResponse Fetch(FetchRequest request)
        {
            DataPortalFacade portal = new DataPortalFacade();
            object result;
            try
            {
                result = portal.Fetch(request.ObjectType, request.Criteria, request.Context);
            }
            catch (Exception ex)
            {
                result = ex;
            }
            return new WcfResponse(result);
        }
        public WcfResponse Update(UpdateRequest request)
        {
            DataPortalFacade portal = new DataPortalFacade();
            object result;
            try
            {
                result = portal.Update(request.Object, request.Context);
            }
            catch (Exception ex)
            {
                result = ex;
            }
            return new WcfResponse(result);
        }
      
    }
}