using System;
using System.IO;
using System.Runtime;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Security;
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

        #region Language

        /// <summary>
        /// 把程序中编写的字符串翻译为当前语言。
        /// 
        /// 直接扩展在字符串上的翻译方法，方便使用
        /// </summary>
        /// <param name="embadedValue"></param>
        /// <returns></returns>
        public static string Translate(this string embadedValue)
        {
            if (embadedValue == null)
                return "";
            return _provider.Translator.Translate(embadedValue);
        }

        /// <summary>
        /// 把当前语言翻译为程序中编写的字符串。
        /// 
        /// 直接扩展在字符串上的翻译方法，方便使用
        /// </summary>
        /// <param name="translatedValue"></param>
        /// <returns></returns>
        public static string TranslateReverse(this string translatedValue)
        {
            return _provider.Translator.TranslateReverse(translatedValue);
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
                var current = AppContext.CurrentPrincipal;
                if (current == null)
                {
                    if (HttpContext.Current.Items["__LCLPrincipal"] == null)
                        current = new AnonymousPrincipal();
                    else
                        current = (IPrincipal)HttpContext.Current.Items["__LCLPrincipal"];

                    ////如果想启用 windows 验证，可以使用以下代码。
                    //current = new WindowsPrincipal(WindowsIdentity.GetCurrent());
                    AppContext.CurrentPrincipal = current;
                }
                return current;
            }
            set
            {
                AppContext.CurrentPrincipal = value;
                HttpContext.Current.Items["__LCLPrincipal"] = value;
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


        public static string CurrentCulture
        {
            get
            {
                return ConfigurationHelper.GetAppSettingOrDefault("currentCulture", "zh-CN");
            }
        }
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
