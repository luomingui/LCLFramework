
using LCL;
using System;
using System.IO;
using System.Xml.Serialization;

namespace UIShell.RbacPermissionService
{
    #region MyRegion
    /// <summary>
    /// 
    /// </summary>
    public class ScheduleConfigs
    {
        /// <summary>
        /// 获取配置类实例
        /// </summary>
        /// <returns></returns>
        public static ScheduleConfigInfo GetConfig()
        {
            return ScheduleConfigFileManager.LoadConfig();
        }

        /// <summary>
        /// 保存配置类实例
        /// </summary>
        /// <returns></returns>
        public static bool SaveConfig(ScheduleConfigInfo scheduleconfiginfo)
        {
            ScheduleConfigFileManager scfm = new ScheduleConfigFileManager();
            ScheduleConfigFileManager.ConfigInfo = scheduleconfiginfo;
            return scfm.SaveConfig();
        }


    }
    /// <summary>
    /// 计划任务设置管理类
    /// </summary>
    public class ScheduleConfigFileManager : DefaultConfigFileManager
    {
        private static ScheduleConfigInfo m_configinfo;

        /// <summary>
        /// 文件修改时间
        /// </summary>
        private static DateTime m_fileoldchange;


        /// <summary>
        /// 初始化文件修改时间和对象实例
        /// </summary>
        static ScheduleConfigFileManager()
        {
            m_fileoldchange = System.IO.File.GetLastWriteTime(ConfigFilePath);
            m_configinfo = (ScheduleConfigInfo)DefaultConfigFileManager.DeserializeInfo(ConfigFilePath, typeof(ScheduleConfigInfo));
        }

        public new static IConfigInfo ConfigInfo
        {
            get { return m_configinfo; }
            set { m_configinfo = (ScheduleConfigInfo)value; }
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
                    filename = LEnvironment.Provider.ToAbsolute(@"Config\schedule.config").ToLower();
                }
                return filename;
            }
        }

        /// <summary>
        /// 返回配置类实例
        /// </summary>
        /// <returns></returns>
        public static ScheduleConfigInfo LoadConfig()
        {
            try
            {
                ConfigInfo = DefaultConfigFileManager.LoadConfig(ref m_fileoldchange, ConfigFilePath, ConfigInfo, true);
            }
            catch
            {
                if (!File.Exists(ConfigFilePath))
                {
                    ScriptEventConfigFileManager secf = new ScriptEventConfigFileManager();
                    secf.SaveConfig(ConfigFilePath, ConfigInfo);
                }
            }
            return ConfigInfo as ScheduleConfigInfo;
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
    /// 文件配置管理基类
    /// </summary>
    public class DefaultConfigFileManager
    {
        /// <summary>
        /// 文件所在路径变量
        /// </summary>
        private static string m_configfilepath;

        /// <summary>
        /// 临时配置对象变量
        /// </summary>
        private static IConfigInfo m_configinfo = null;

        /// <summary>
        /// 锁对象
        /// </summary>
        private static object m_lockHelper = new object();


        /// <summary>
        /// 文件所在路径
        /// </summary>
        public static string ConfigFilePath
        {
            get { return m_configfilepath; }
            set { m_configfilepath = value; }
        }


        /// <summary>
        /// 临时配置对象
        /// </summary>
        public static IConfigInfo ConfigInfo
        {
            get { return m_configinfo; }
            set { m_configinfo = value; }
        }

        /// <summary>
        /// 加载(反序列化)指定对象类型的配置对象
        /// </summary>
        /// <param name="fileoldchange">文件加载时间</param>
        /// <param name="configFilePath">配置文件所在路径</param>
        /// <param name="configinfo">相应的变量 注:该参数主要用于设置m_configinfo变量 和 获取类型.GetType()</param>
        /// <returns></returns>
        protected static IConfigInfo LoadConfig(ref DateTime fileoldchange, string configFilePath, IConfigInfo configinfo)
        {
            return LoadConfig(ref fileoldchange, configFilePath, configinfo, true);
        }


        /// <summary>
        /// 加载(反序列化)指定对象类型的配置对象
        /// </summary>
        /// <param name="fileoldchange">文件加载时间</param>
        /// <param name="configFilePath">配置文件所在路径(包括文件名)</param>
        /// <param name="configinfo">相应的变量 注:该参数主要用于设置m_configinfo变量 和 获取类型.GetType()</param>
        /// <param name="checkTime">是否检查并更新传递进来的"文件加载时间"变量</param>
        /// <returns></returns>
        protected static IConfigInfo LoadConfig(ref DateTime fileoldchange, string configFilePath, IConfigInfo configinfo, bool checkTime)
        {
            lock (m_lockHelper)
            {
                m_configfilepath = configFilePath;
                m_configinfo = configinfo;

                if (checkTime)
                {
                    DateTime m_filenewchange = System.IO.File.GetLastWriteTime(configFilePath);

                    //当程序运行中config文件发生变化时则对config重新赋值
                    if (fileoldchange != m_filenewchange)
                    {
                        fileoldchange = m_filenewchange;
                        m_configinfo = DeserializeInfo(configFilePath, configinfo.GetType());
                    }
                }
                else
                    m_configinfo = DeserializeInfo(configFilePath, configinfo.GetType());

                return m_configinfo;
            }
        }


        /// <summary>
        /// 反序列化指定的类
        /// </summary>
        /// <param name="configfilepath">config 文件的路径</param>
        /// <param name="configtype">相应的类型</param>
        /// <returns></returns>
        public static IConfigInfo DeserializeInfo(string configfilepath, Type configtype)
        {
            return (IConfigInfo)XmlUtil.Load(configtype, configfilepath);
        }

        /// <summary>
        /// 保存配置实例(虚方法需继承)
        /// </summary>
        /// <returns></returns>
        public virtual bool SaveConfig()
        {
            return true;
        }

        /// <summary>
        /// 保存(序列化)指定路径下的配置文件
        /// </summary>
        /// <param name="configFilePath">指定的配置文件所在的路径(包括文件名)</param>
        /// <param name="configinfo">被保存(序列化)的对象</param>
        /// <returns></returns>
        public bool SaveConfig(string configFilePath, IConfigInfo configinfo)
        {
            return XmlUtil.Serializer(configinfo, configFilePath);
        }
    }
    #endregion
    #region Model
    public interface IConfigInfo
    {

    }
    /// <summary>
    /// Config/schedule.config
    /// </summary>
    [Serializable]
    public class ScheduleConfigInfo : IConfigInfo
    {
        public ScheduleConfigInfo()
        { }

        [XmlElement("enabled")]
        public bool Enabled;

        [XmlArray("events")]
        public EventInfo[] Events;

        [XmlElement("minutes_interval")]
        public int TimerMinutesInterval;
    }
    [Serializable]
    [XmlRoot("event")]
    public class EventInfo
    {
        public EventInfo()
        {

        }
        private string _key;
        [XmlAttribute("key")]
        public string Key
        {
            get { return this._key; }
            set { this._key = value; }
        }

        private int _timeOfDay = -1;
        [XmlAttribute("time_of_day")]
        public int TimeOfDay
        {
            get { return this._timeOfDay; }
            set { this._timeOfDay = value; }
        }

        private int _minutes = 60;
        [XmlAttribute("minutes")]
        public int Minutes
        {
            get { return this._minutes; }
            set { this._minutes = value; }
        }

        private string _scheduleType;
        [XmlAttribute("type")]
        public string ScheduleType
        {
            get { return this._scheduleType; }
            set { this._scheduleType = value; }
        }

        private bool _enabled;
        [XmlAttribute("enabled")]
        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        private bool _isSystemEvent;
        [XmlAttribute("is_system_event")]
        public bool IsSystemEvent
        {
            get { return _isSystemEvent; }
            set { _isSystemEvent = value; }
        }
    }
    /*
     <?xml version="1.0"?>
<ScheduleConfigInfo xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <enabled>true</enabled>
  <events>
    <Event key="TagEvent" time_of_day="-1" minutes="720" type="Discuz.Event.TagsEvent, Discuz.Event" enabled="true" is_system_event="true" />
    <Event key="ForumEvent" time_of_day="1170" minutes="720" type="Discuz.Event.ForumEvent, Discuz.Event" enabled="true" is_system_event="true" />
    <Event key="ScriptEvent" time_of_day="-1" minutes="1" type="Discuz.Event.ScriptEvent, Discuz.Event" enabled="true" is_system_event="true" />
    <Event key="StatEvent" time_of_day="2" minutes="1440" type="Discuz.Event.StatEvent, Discuz.Event" enabled="true" is_system_event="true" />
    <Event key="NoticesEvent" time_of_day="120" minutes="1440" type="Discuz.Event.NoticesEvent, Discuz.Event" enabled="true" is_system_event="true" />
    <Event key="AttachmentEvent" time_of_day="2" minutes="1440" type="Discuz.Event.AttachmentEvent, Discuz.Event" enabled="true" is_system_event="true" />
    <Event key="InvitationEvent" time_of_day="2" minutes="1" type="Discuz.Event.InvitationEvent, Discuz.Event" enabled="true" is_system_event="true" />
  </events>
  <minutes_interval>1</minutes_interval>
</ScheduleConfigInfo>
     */
    #endregion
}
