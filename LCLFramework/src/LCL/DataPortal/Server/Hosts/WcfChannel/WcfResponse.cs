using System.Runtime.Serialization;


namespace LCL.DataPortal.Server.Hosts.WcfChannel
{
    /// <summary>
    /// Response message for returning
    /// the results of a data portal call.
    /// </summary>
    [DataContract]
    public class WcfResponse
    {
        [DataMember]
        private object _result;

        /// <summary>
        /// Create new instance of object.
        /// </summary>
        /// <param name="result">Result object to be returned.</param>
        public WcfResponse(object result)
        {
            this.Result = result;
        }
        public object Result
        {
            get
            {
                return InnerSerializer.DeserializeObject(this._result);
            }
            set
            {
                this._result = InnerSerializer.SerializeObject(value);
            }
        }
    }
}