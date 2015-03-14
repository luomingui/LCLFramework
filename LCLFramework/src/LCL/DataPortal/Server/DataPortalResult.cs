using System;
using System.Collections.Specialized;
using LCL.Serialization.Mobile;

namespace LCL.DataPortal.Server
{
    /// <summary>
    /// Returns data from the server-side DataPortal to the 
    /// client-side DataPortal. Intended for internal CSLA .NET
    /// use only.
    /// </summary>
    [Serializable]
    public class DataPortalResult : MobileObject
    {
        private object _returnObject;

        private HybridDictionary _globalContext;

        /// <summary>
        /// The business object being returned from
        /// the server.
        /// </summary>
        public object ReturnObject
        {
            get { return _returnObject; }
        }

        /// <summary>
        /// The global context being returned from
        /// the server.
        /// </summary>
        public HybridDictionary GlobalContext
        {
            get { return _globalContext; }
        }

        /// <summary>
        /// Creates an instance of the object.
        /// </summary>
        public DataPortalResult( )
        {
            _globalContext = DistributionContext.GetGlobalContext();
        }

        /// <summary>
        /// Creates an instance of the object.
        /// </summary>
        /// <param name="returnObject">Object to return as part
        /// of the result.</param>
        public DataPortalResult(object returnObject)
        {
            _returnObject = returnObject;
            _globalContext = DistributionContext.GetGlobalContext();
        }
    }
}