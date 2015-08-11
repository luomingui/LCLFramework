using LCL;
using System;
using System.IO;

namespace UIShell.RbacPermissionService
{
    /// <summary>
    /// 基本设置类
    /// </summary>
    public class GeneralConfigs
    {
        private static object lockHelper = new object();
        private static GeneralConfigInfo m_configinfo;
        /// <summary>
        /// 静态构造函数初始化相应实例和定时器
        /// </summary>
        static GeneralConfigs()
        {
            m_configinfo = GeneralConfigFileManager.LoadConfig();
        }

        /// <summary>
        /// 重设配置类实例
        /// </summary>
        public static void ResetConfig()
        {
            m_configinfo = GeneralConfigFileManager.LoadConfig();
        }

        public static GeneralConfigInfo GetConfig()
        {
            //if (m_configinfo == null)
            ResetConfig();
            return m_configinfo;
        }

        /// <summary>
        /// 保存配置类实例
        /// </summary>
        /// <param name="generalconfiginfo"></param>
        /// <returns></returns>
        public static bool SaveConfig(GeneralConfigInfo generalconfiginfo)
        {
            GeneralConfigFileManager gcf = new GeneralConfigFileManager();
            GeneralConfigFileManager.ConfigInfo = generalconfiginfo;
            return gcf.SaveConfig();
        }

        #region Helper

        /// <summary>
        /// 序列化配置信息为XML
        /// </summary>
        /// <param name="configinfo">配置信息</param>
        /// <param name="configFilePath">配置文件完整路径</param>
        public static GeneralConfigInfo Serialiaze(GeneralConfigInfo configinfo, string configFilePath)
        {
            lock (lockHelper)
            {
                XmlUtil.Serializer(configinfo, configFilePath);
            }
            return configinfo;
        }

        public static GeneralConfigInfo Deserialize(string configFilePath)
        {
            return (GeneralConfigInfo)XmlUtil.Load(typeof(GeneralConfigInfo), configFilePath);
        }

        #endregion

    }
    /// <summary>
    /// 基本设置管理类
    /// </summary>
    class GeneralConfigFileManager : DefaultConfigFileManager
    {
        private static GeneralConfigInfo m_configinfo;

        /// <summary>
        /// 文件修改时间
        /// </summary>
        private static DateTime m_fileoldchange;

        /// <summary>
        /// 初始化文件修改时间和对象实例
        /// </summary>
        static GeneralConfigFileManager()
        {
            m_fileoldchange = System.IO.File.GetLastWriteTime(ConfigFilePath);

            try
            {
                m_configinfo = (GeneralConfigInfo)DefaultConfigFileManager.DeserializeInfo(ConfigFilePath, typeof(GeneralConfigInfo));
            }
            catch
            {
                if (File.Exists(ConfigFilePath))
                {
                    m_configinfo = (GeneralConfigInfo)DefaultConfigFileManager.DeserializeInfo(ConfigFilePath, typeof(GeneralConfigInfo));
                }
            }
        }
        /// <summary>
        /// 当前配置类的实例
        /// </summary>
        public new static IConfigInfo ConfigInfo
        {
            get { return m_configinfo; }
            set { m_configinfo = (GeneralConfigInfo)value; }
        }

        /// <summary>
        /// 配置文件所在路径
        /// </summary>
        public static string filename = null;

        /// <summary>
        /// 获取配置文件所在路径
        /// </summary>
        public new static string ConfigFilePath
        {
            get
            {
                if (filename == null)
                {
                    filename = LEnvironment.Provider.ToAbsolute(@"Config\general.config");
                }
                return filename;
            }
        }

        /// <summary>
        /// 返回配置类实例
        /// </summary>
        /// <returns></returns>
        public static GeneralConfigInfo LoadConfig()
        {
            try
            {
                if (!File.Exists(ConfigFilePath))
                {
                    GeneralConfigFileManager secf = new GeneralConfigFileManager();

                    ConfigInfo = new GeneralConfigInfo();
                    secf.SaveConfig(ConfigFilePath, ConfigInfo);
                }
                else
                {
                    ConfigInfo = DefaultConfigFileManager.LoadConfig(ref m_fileoldchange, ConfigFilePath, ConfigInfo, true);
                }
            }
            catch
            {
                if (!File.Exists(ConfigFilePath))
                {
                    GeneralConfigFileManager secf = new GeneralConfigFileManager();
                    secf.SaveConfig(ConfigFilePath, ConfigInfo);
                }
            }
            return ConfigInfo as GeneralConfigInfo;
        }

        /// <summary>
        /// 保存配置类实例
        /// </summary>
        /// <returns></returns>
        public override bool SaveConfig()
        {
            return base.SaveConfig(ConfigFilePath, ConfigInfo);
        }
    }
    /// <summary>
    /// 基本设置描述类, 加[Serializable]标记为可序列化
    /// </summary>
    [Serializable]
    public class GeneralConfigInfo : IConfigInfo
    {
        public GeneralConfigInfo()
        {
            AppName = "南昌都市圈国民旅游休闲年票";
            CompanyName = "百景通";
            Copyright = " Copyright 2015 &copy; - <a href=\"http://www.jxgis.com/\">华宇股份</a>";
            ComPort = 0;
            ComBaud = 115200;
        }
        public string AppName { get; set; }
        public string AppLogo { get; set; }
        public string CompanyName { get; set; }
        public string Appurl { get; set; }
        /// <summary>
        /// 统计代码
        /// </summary>
        public string Statcode { get; set; }
        /// <summary>
        /// 是否允许新用户注册
        /// </summary>
        public int Regstatus { get; set; }
        /// <summary>
        /// 版权信息
        /// </summary>
        public string Copyright { get; set; }

        #region 串口配置
        /// <summary>
        /// 串口端口
        /// COM1 COM2 COM3 COM4
        /// 0    1    2    3
        /// </summary>
        public int ComPort { get; set; }
        /// <summary>
        /// 波特率
        /// </summary>
        public int ComBaud { get; set; }
        #endregion
    }
}
