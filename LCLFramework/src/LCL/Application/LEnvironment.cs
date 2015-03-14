using System;
using System.IO;
using System.Runtime;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Windows;

namespace LCL
{
    /// <summary>
    /// LCL 的上下文环境
    /// </summary>
    public static partial class LEnvironment
    {
        #region Provider

        private static EnvironmentProvider _provider;
        public static EnvironmentProvider Provider
        {
            get
            {
                if (_provider == null)
                {
                    _provider = new EnvironmentProvider();
                }
                return _provider;
            }
            set
            {
                _provider = value;
            }
        }

        #endregion

        #region 身份

        /// <summary>
        /// 返回当前用户。
        /// 
        /// 本属性不会为 null，请使用 IsAuthenticated 属性来判断是否已经登录。
        /// </summary>
        public static ILCLIdentity Identity
        {
            get
            {
                var principal = Principal;
                if (principal != null)
                {
                    var user = principal.Identity as ILCLIdentity;
                    if (user != null) return user;
                }
                return new AnonymousIdentity();
            }
        }

        /// <summary>
        /// 返回当前身份。
        /// 
        /// 可能返回 null。如果不想判断 null，请使用 Identity 属性。
        /// </summary>
        public static IPrincipal Principal
        {
            get
            {
                IPrincipal current;
                if (HttpContext.Current != null)
                {
                    current = HttpContext.Current.User;
                    if (current == null)
                    {
                        if (string.IsNullOrWhiteSpace(current.Identity.Name))
                        {
                            current = HttpContext.Current.Cache.Get("LCLUser") as IPrincipal;
                        }
                        else
                        {
                            current = new AnonymousPrincipal();
                            HttpContext.Current.User = current;
                        }
                    }
                }
                else if (Application.Current != null)
                {
                    if (_principal == null)
                    {
                        _principal = new AnonymousPrincipal();
                        //如果想启用 windows 验证，可以使用以下代码。
                        //_principal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
                    }
                    current = _principal;
                }
                else
                {
                    current = Thread.CurrentPrincipal;
                    if (current == null)
                    {
                        current = new AnonymousPrincipal();
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
                    HttpContext.Current.Cache.Insert("LCLUser", value);
                }
                else if (Application.Current != null)
                {
                    _principal = value;
                }
                else
                {
                    Thread.CurrentPrincipal = value;
                }
            }
        }

        [ThreadStatic]
        private static IPrincipal __principalThreadSafe;
        private static IPrincipal __principal;

        /// <summary>
        /// 当前线程使用的身份。
        /// 如果是客户端，则所有线程使用一个身份；如果是服务端，则每个线程使用一个单独的身份。
        /// </summary>
        private static IPrincipal _principal
        {
            get
            {
                return LEnvironment.Location.IsWPFUI ? __principal : __principalThreadSafe;
            }
            set
            {
                if (LEnvironment.Location.IsWPFUI)
                {
                    __principal = value;
                }
                else
                {
                    __principalThreadSafe = value;
                }
            }
        }

        #endregion

        #region IApp AppCore
        public static IObjectContainer AppObjectContainer { get; set; }

        private static IApp _appCore;

        public static IApp AppCore
        {
            get { return _appCore; }
        }

        internal static void InitApp(IApp appCore)
        {
            if (_appCore != null) throw new InvalidOperationException();

            _appCore = appCore;
        }

        #endregion

        #region Location

        [ThreadStatic]
        private static int _threadPortalCount;

        private static LocationInformation _location = new LocationInformation();

        /// <summary>
        /// 当前应用程序的位置信息。
        /// 
        /// 对应的位置：
        /// 单机版：IsWPFUI = true, DataPortalMode = DirectConnect；
        /// Web 服务器：IsWebUI = true, DataPortalMode = DirectConnect；
        /// C/S 客户端：IsWPFUI = true, DataPortalMode = ThroughService；
        /// C/S 服务端（默认值）：IsWPFUI = flase, IsWebUI = flase, DataPortalMode = DirectConnect；
        /// </summary>
        public static LocationInformation Location
        {
            get { return _location; }
        }

        /// <summary>
        /// 使用这个方法后，Location 会被重置，这样可以再次对该属性进行设置。
        /// </summary>
        private static void ResetLocation()
        {
            _location.IsWebUI = false;
            _location.IsWPFUI = false;
            _location.DataPortalMode = DataPortalMode.ConnectDirectly;
        }

        /// <summary>
        /// 获取当前线程目前已经进入的数据门户层数。
        /// 
        /// Set 方法为 LCL 框架内部调用，外部请不要设置，否则会引起未知的异常。
        /// </summary>
        public static int ThreadPortalCount
        {
            get { return _threadPortalCount; }
            internal set { _threadPortalCount = value; }
        }

        /// <summary>
        /// 判断是否在服务端。
        /// 
        /// 如果是单机版，则当进入至少一次数据门户后，才能算作服务端，返回true。
        /// </summary>
        /// <returns></returns>
        public static bool IsOnServer()
        {
            //当在服务端、或者是单机版模拟服务端时，默认值为直接在服务端运行。
            if (_location.ConnectDataDirectly)
            {
                if (_location.IsWPFUI) return _threadPortalCount > 0;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 判断是否在客户端
        /// 单机版，如果还没有进入数据门户中，则同样返回 true。
        /// </summary>
        /// <returns></returns>
        public static bool IsOnClient()
        {
            return !_location.ConnectDataDirectly ||
                (_location.IsWPFUI && _threadPortalCount == 0);
        }
        #endregion

    }

    /// <summary>
    /// 数据访问层执行的地点
    /// </summary>
    public enum DataPortalLocation
    {
        /// <summary>
        /// 根据 LCLEnvironment.Location 而判断是否在远程服务端执行。
        /// 
        /// 此种状态下，目前只有 LCLLocation.WPFClient 的位置时，才会选择在远程服务器执行。20130118
        /// </summary>
        Dynamic,
        /// <summary>
        /// 将在当前机器执行。
        /// </summary>
        Local,
    }
}
