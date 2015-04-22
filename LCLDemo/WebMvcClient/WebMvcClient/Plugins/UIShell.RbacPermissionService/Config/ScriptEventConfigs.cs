using LCL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace UIShell.RbacPermissionService
{
    /// <summary>
    /// 脚本任务计划类
    /// </summary>
    public class ScriptEventConfigs
    {
        /// <summary>
        /// 获取配置类实例
        /// </summary>
        /// <returns></returns>
        public static ScriptEventConfigInfo GetConfig()
        {
            return ScriptEventConfigFileManager.LoadConfig();
        }

        /// <summary>
        /// 保存配置类实例
        /// </summary>
        /// <returns></returns>
        public static bool SaveConfig(ScriptEventConfigInfo scripteventconfiginfo)
        {
            ScriptEventConfigFileManager scfm = new ScriptEventConfigFileManager();
            ScriptEventConfigFileManager.ConfigInfo = scripteventconfiginfo;
            return scfm.SaveConfig();
        }
    }
    /// <summary>
    /// 脚本类计划任务配置文件管理类
    /// </summary>
    public class ScriptEventConfigFileManager : DefaultConfigFileManager
    {
        private static ScriptEventConfigInfo m_configinfo = new ScriptEventConfigInfo();

        /// <summary>
        /// 文件修改时间
        /// </summary>
        private static DateTime m_fileoldchange;


        /// <summary>
        /// 初始化文件修改时间和对象实例
        /// </summary>
        static ScriptEventConfigFileManager()
        {
            m_fileoldchange = System.IO.File.GetLastWriteTime(ConfigFilePath);
        }

        public new static IConfigInfo ConfigInfo
        {
            get { return m_configinfo; }
            set { m_configinfo = (ScriptEventConfigInfo)value; }
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
                    filename =LEnvironment.Provider.ToAbsolute(@"Config\scriptevent_sqlserver.config").ToLower();
                }
                return filename;
            }

        }

        /// <summary>
        /// 返回配置类实例
        /// </summary>
        /// <returns></returns>
        public static ScriptEventConfigInfo LoadConfig()
        {
            try
            {
                ConfigInfo = DefaultConfigFileManager.LoadConfig(ref m_fileoldchange, ConfigFilePath, ConfigInfo, false);
            }
            catch
            {
                if (!File.Exists(ConfigFilePath))
                {
                    ScriptEventConfigFileManager secf = new ScriptEventConfigFileManager();
                    secf.SaveConfig(ConfigFilePath, ConfigInfo);
                }
            }

            return ConfigInfo as ScriptEventConfigInfo;
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
    [Serializable]
    public class ScriptEventConfigInfo : IConfigInfo
    {
        [XmlArray("scriptevent")]
        public ScriptEventInfo[] ScriptEvents = { };
    }
    /// <summary>
    /// 脚本任务设置信息
    /// </summary>
    [Serializable]
    public class ScriptEventInfo
    {
        private string _key;

        /// <summary>
        /// 任务标识
        /// </summary>
        [XmlAttribute("key")]
        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }
        private string _title;

        /// <summary>
        /// 标题
        /// </summary>
        [XmlAttribute("title")]
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        private string _script;

        /// <summary>
        /// 将要被执行的脚本内容
        /// </summary>
        [XmlText]
        public string Script
        {
            get { return _script; }
            set { _script = value; }
        }
        private int _timeofday;

        /// <summary>
        /// 每日固定时间运行的时间, -1 为按时间间隔运行
        /// </summary>
        [XmlAttribute("timeofday")]
        public int Timeofday
        {
            get { return _timeofday; }
            set { _timeofday = value; }
        }
        private int _miniutes;

        /// <summary>
        /// 按时间间隔运行的时间间隔(分钟), 当timeofday为-1时有效
        /// </summary>
        [XmlAttribute("miniutes")]
        public int Miniutes
        {
            get { return _miniutes; }
            set { _miniutes = value; }
        }

        /// <summary>
        /// 是否该被执行
        /// </summary>
        [XmlIgnore]
        public bool ShouldExecute
        {
            get { return true; }
        }

        private bool _enabled;
        /// <summary>
        /// 是否启用此任务
        /// </summary>
        [XmlAttribute("enabled")]
        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

    }
}
