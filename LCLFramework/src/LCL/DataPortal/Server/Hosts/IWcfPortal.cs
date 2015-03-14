using System.ServiceModel;
using LCL.DataPortal.Server.Hosts.WcfChannel;

namespace LCL.DataPortal.Server.Hosts
{
    [ServiceContract(Namespace = "http://ws.lhotka.net/WcfDataPortal")]
    public interface IWcfPortal
    {
        [OperationContract]
        [UseNetDataContract]
        WcfResponse Action(ActionRequest request);

        [OperationContract]
        [UseNetDataContract]
        WcfResponse Fetch(FetchRequest request);

        [OperationContract]
        [UseNetDataContract]
        WcfResponse Update(UpdateRequest request);
    }
}
