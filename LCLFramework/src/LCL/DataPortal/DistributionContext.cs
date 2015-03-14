
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Windows;

namespace LCL.DataPortal
{
    public static class DistributionContext
    {
        #region User

        //改成这样后，计算过程还是会报错。
        [ThreadStatic]
        private static IPrincipal __principalThreadSafe;
        private static IPrincipal __principal;
        private static IPrincipal _principal
        {
            get
            {
                return _executionLocation == ExecutionLocations.Client ? __principal : __principalThreadSafe;
            }
            set
            {
                if (_executionLocation == ExecutionLocations.Client)
                {
                    __principal = value;
                }
                else
                {
                    __principalThreadSafe = value;
                }
            }
        }
        //private static IPrincipal _principal;

        /// <summary>
        /// Get or set the current <see cref="IPrincipal" />
        /// object representing the user's identity.
        /// </summary>
        /// <remarks>
        /// This is discussed in Chapter 5. When running
        /// under IIS the HttpContext.Current.User value
        /// is used, otherwise the current Thread.CurrentPrincipal
        /// value is used.
        /// </remarks>
        public static IPrincipal User
        {
            get
            {
                IPrincipal current;
                if (HttpContext.Current != null)
                {
                    current = HttpContext.Current.User;
                    if (current == null)
                    {
                        current = new GenericPrincipal(new AnonymousIdentity(), null);
                        HttpContext.Current.User = current;
                    }
                }
                else if (Application.Current != null)
                {
                    if (_principal == null)
                    {
                        if (DistributionContext.AuthenticationType != "Windows")
                        {
                            _principal = new GenericPrincipal(new AnonymousIdentity(), null);
                        }
                        else
                        {
                            _principal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
                        }
                    }
                    current = _principal;
                }
                else
                {
                    current = Thread.CurrentPrincipal;
                    if (current == null)
                    {
                        current = new GenericPrincipal(new AnonymousIdentity(), null);
                        Thread.CurrentPrincipal = current;
                    }
                }

                return current;
            }
            set
            {
                if (HttpContext.Current != null)
                {
                    HttpContext.Current.User = value;
                }
                else if (System.Windows.Application.Current != null)
                {
                    _principal = value;
                }
                else
                {
                    Thread.CurrentPrincipal = value;
                }
            }
        }

        #endregion

        #region Client/Global Context
        private static object _syncClientContext = new object();
        private const string _clientContextName = "SimpleCsla.ClientContext";
        private const string _globalContextName = "SimpleCsla.GlobalContext";
        public static HybridDictionary ClientContext
        {
            get
            {
                lock (_syncClientContext)
                {
                    HybridDictionary ctx = GetClientContext();
                    if (ctx == null)
                    {
                        ctx = new HybridDictionary();
                        SetClientContext(ctx);
                    }
                    return ctx;
                }
            }
        }
        public static HybridDictionary GlobalContext
        {
            get
            {
                HybridDictionary ctx = GetGlobalContext();
                if (ctx == null)
                {
                    ctx = new HybridDictionary();
                    SetGlobalContext(ctx);
                }
                return ctx;
            }
        }
        public static HybridDictionary GetClientContext( )
        {
            if (HttpContext.Current == null)
            {
                if (DistributionContext.ExecutionLocation == ExecutionLocations.Client)
                    lock (_syncClientContext)
                        return (HybridDictionary)AppDomain.CurrentDomain.GetData(_clientContextName);
                else
                {
                    LocalDataStoreSlot slot =
                      Thread.GetNamedDataSlot(_clientContextName);
                    return (HybridDictionary)Thread.GetData(slot);
                }
            }
            else
                return (HybridDictionary)
                  HttpContext.Current.Items[_clientContextName];
        }
        public static HybridDictionary GetGlobalContext( )
        {
            if (HttpContext.Current == null)
            {
                LocalDataStoreSlot slot = Thread.GetNamedDataSlot(_globalContextName);
                return (HybridDictionary)Thread.GetData(slot);
            }
            else
                return (HybridDictionary)HttpContext.Current.Items[_globalContextName];
        }
        private static void SetClientContext(HybridDictionary clientContext)
        {
            if (HttpContext.Current == null)
            {
                if (DistributionContext.ExecutionLocation == ExecutionLocations.Client)
                    lock (_syncClientContext)
                        AppDomain.CurrentDomain.SetData(_clientContextName, clientContext);
                else
                {
                    LocalDataStoreSlot slot = Thread.GetNamedDataSlot(_clientContextName);
                    Thread.SetData(slot, clientContext);
                }
            }
            else
                HttpContext.Current.Items[_clientContextName] = clientContext;
        }
        public static void SetGlobalContext(HybridDictionary globalContext)
        {
            if (HttpContext.Current == null)
            {
                LocalDataStoreSlot slot = Thread.GetNamedDataSlot(_globalContextName);
                Thread.SetData(slot, globalContext);
            }
            else
                HttpContext.Current.Items[_globalContextName] = globalContext;
        }
        public static void SetContext(
          HybridDictionary clientContext,
          HybridDictionary globalContext)
        {
            SetClientContext(clientContext);
            SetGlobalContext(globalContext);
        }
        public static void Clear( )
        {
            SetContext(null, null);
            ServerContext.Clear();
        }

        #endregion

        #region Config Settings
        private static string _authenticationType;
        public static string AuthenticationType
        {
            get
            {
                if (_authenticationType == null)
                {
                    _authenticationType = ConfigurationManager.AppSettings["CslaAuthentication"];
                    _authenticationType = _authenticationType ?? "SimpleCsla";
                }
                return _authenticationType;
            }
        }
        public static string DataPortalProxy
        {
            get { return ConfigurationHelper.GetAppSettingOrDefault("DataPortalProxy", "Local"); }
        }
        public static Uri DataPortalUrl
        {
            get { return new Uri(ConfigurationManager.AppSettings["CslaDataPortalUrl"]); }
        }
        public static string IsInRoleProvider
        {
            get
            {
                string result = ConfigurationManager.AppSettings["CslaIsInRoleProvider"];
                if (string.IsNullOrEmpty(result))
                    result = string.Empty;
                return result;
            }
        }
        public static SerializationFormatters SerializationFormatter
        {
            get
            {
                string tmp = ConfigurationManager.AppSettings["CslaSerializationFormatter"];
                if (string.IsNullOrEmpty(tmp))
                    tmp = "BinaryFormatter";
                return (SerializationFormatters)
                  Enum.Parse(typeof(SerializationFormatters), tmp);
            }
        }
        private static PropertyChangedModes _propertyChangedMode;
        private static bool _propertyChangedModeSet;
        public static PropertyChangedModes PropertyChangedMode
        {
            get
            {
                if (!_propertyChangedModeSet)
                {
                    string tmp = ConfigurationManager.AppSettings["CslaPropertyChangedMode"];
                    if (string.IsNullOrEmpty(tmp))
                        tmp = "Windows";
                    _propertyChangedMode = (PropertyChangedModes)
                      Enum.Parse(typeof(PropertyChangedModes), tmp);
                    _propertyChangedModeSet = true;
                }
                return _propertyChangedMode;
            }
            set
            {
                _propertyChangedMode = value;
                _propertyChangedModeSet = true;
            }
        }
        public enum SerializationFormatters
        {
            /// <summary>
            /// Use the standard Microsoft .NET
            /// <see cref="BinaryFormatter"/>.
            /// </summary>
            BinaryFormatter,
            /// <summary>
            /// Use the Microsoft .NET 3.0
            /// <see cref="System.Runtime.Serialization.NetDataContractSerializer">
            /// NetDataContractSerializer</see> provided as part of WCF.
            /// </summary>
            NetDataContractSerializer
        }

        /// <summary>
        /// Enum representing the locations code can execute.
        /// </summary>
        public enum ExecutionLocations
        {
            /// <summary>
            /// The code is executing on the client.
            /// </summary>
            Client,
            /// <summary>
            /// The code is executing on the application server.
            /// </summary>
            Server,
            /// <summary>
            /// The code is executing on the Silverlight client.
            /// </summary>
            Silverlight
        }

        /// <summary>
        /// Enum representing the way in which CSLA .NET
        /// should raise PropertyChanged events.
        /// </summary>
        public enum PropertyChangedModes
        {
            /// <summary>
            /// Raise PropertyChanged events as required
            /// by Windows Forms data binding.
            /// </summary>
            Windows,
            /// <summary>
            /// Raise PropertyChanged events as required
            /// by XAML data binding in WPF.
            /// </summary>
            Xaml
        }

        #endregion

        #region In-Memory Settings

        private static ExecutionLocations _executionLocation =
          ExecutionLocations.Client;

        /// <summary>
        /// Returns a value indicating whether the application code
        /// is currently executing on the client or server.
        /// </summary>
        public static ExecutionLocations ExecutionLocation
        {
            get { return _executionLocation; }
        }

        public static void SetExecutionLocation(ExecutionLocations location)
        {
            _executionLocation = location;
        }

        #endregion

        #region Logical Execution Location

        /// <summary>
        /// Enum representing the logical execution location
        /// The setting is set to server when server is execting
        /// a CRUD opertion, otherwise the setting is always client
        /// </summary>
        public enum LogicalExecutionLocations
        {
            /// <summary>
            /// The code is executing on the client.
            /// </summary>
            Client,
            /// <summary>
            /// The code is executing on the server.  This inlcudes
            /// Local mode execution
            /// </summary>
            Server
        }

        [ThreadStatic]
        private static LogicalExecutionLocations _logicalExecutionLocation =
         LogicalExecutionLocations.Client;

        /// <summary>
        /// Gets a value indicating the logical execution location
        /// of the currently executing code.
        /// </summary>
        public static LogicalExecutionLocations LogicalExecutionLocation
        {
            get { return _logicalExecutionLocation; }
        }

        public static void SetLogicalExecutionLocation(LogicalExecutionLocations location)
        {
            _logicalExecutionLocation = location;
        }

        #endregion
    }
   
}