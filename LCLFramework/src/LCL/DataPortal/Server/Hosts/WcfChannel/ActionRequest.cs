using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace LCL.DataPortal.Server.Hosts.WcfChannel
{
    /// <summary>
    /// Request message for retrieving
    /// an existing business object.
    /// </summary>
    [DataContract]
    public class ActionRequest
    {
        [DataMember]
        private Type _objectType;
        [DataMember]
        private object _criteria;
        [DataMember]
        private Server.DataPortalContext _context;
        private string _methodName;
        public ActionRequest(Type objectType, string methodName, object criteria, DataPortalContext context)
        {
            // TODO: Complete member initialization
            this.ObjectType = objectType;
            this.MethodName = methodName;
            this.Criteria = criteria;
            this.Context = context;
        }
        public string MethodName
        {
            get { return _methodName; }
            set { _methodName = value; }
        }

        /// <summary>
        /// The type of the business object
        /// to be retrieved.
        /// </summary>
        public Type ObjectType
        {
            get { return _objectType; }
            set { _objectType = value; }
        }

        /// <summary>
        /// Criteria object describing business object.
        /// </summary>
        public object Criteria
        {
            get
            {
                return InnerSerializer.DeserializeObject(_criteria);
            }
            set
            {
                _criteria = InnerSerializer.SerializeObject(value);
            }
        }

        /// <summary>
        /// Data portal context from client.
        /// </summary>
        public Server.DataPortalContext Context
        {
            get { return _context; }
            set { _context = value; }
        }
    }
}