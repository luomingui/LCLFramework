using System;
using System.Configuration;
using System.Xml;

namespace LCL.Config
{
    public partial class LConfig : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            var config = new LConfig();
            var dynamicDiscoveryNode = section.SelectSingleNode("DynamicDiscovery");
            if (dynamicDiscoveryNode != null && dynamicDiscoveryNode.Attributes != null)
            {
                var attribute = dynamicDiscoveryNode.Attributes["Enabled"];
                if (attribute != null)
                    config.DynamicDiscovery = Convert.ToBoolean(attribute.Value);
            }

            var engineNode = section.SelectSingleNode("Engine");
            if (engineNode != null && engineNode.Attributes != null)
            {
                var attribute = engineNode.Attributes["Type"];
                if (attribute != null)
                    config.EngineType = attribute.Value;
            }

            var startupNode = section.SelectSingleNode("Startup");
            if (startupNode != null && startupNode.Attributes != null)
            {
                var attribute = startupNode.Attributes["IgnoreStartupTasks"];
                if (attribute != null)
                    config.IgnoreStartupTasks = Convert.ToBoolean(attribute.Value);
            }

            var themeNode = section.SelectSingleNode("Themes");
            if (themeNode != null && themeNode.Attributes != null)
            {
                var attribute = themeNode.Attributes["basePath"];
                if (attribute != null)
                    config.ThemeBasePath = attribute.Value;
            }

            var userAgentStringsNode = section.SelectSingleNode("UserAgentStrings");
            if (userAgentStringsNode != null && userAgentStringsNode.Attributes != null)
            {
                var attribute = userAgentStringsNode.Attributes["databasePath"];
                if (attribute != null)
                    config.UserAgentStringsPath = attribute.Value;
            }
            var lang = section.SelectSingleNode("Language");
            if (lang != null && lang.Attributes != null) {
                var attribute = lang.Attributes["Path"];
                if (attribute != null)
                    config.LanguagePath = attribute.Value;
                attribute = lang.Attributes["name"];
                if (attribute != null)
                    config.LanguageName = attribute.Value;
            }
            var log = section.SelectSingleNode("Logger");
            if (log != null && log.Attributes != null)
            {
                var attribute = log.Attributes["Path"];
                if (attribute != null)
                    config.LoggerPath = attribute.Value;
                attribute = log.Attributes["providerName"];
                if (attribute != null)
                    config.LoggerType = attribute.Value;
            }
            return config;
        }
        public bool DynamicDiscovery { get; private set; }
        public string EngineType { get; private set; }
        public string ThemeBasePath { get; private set; }
        public bool IgnoreStartupTasks { get; private set; }
        public string UserAgentStringsPath { get; private set; }
        public string LanguagePath { get; set; }
        public string LanguageName { get; set; }
        public string LoggerType { get; set; }
        public string LoggerPath { get; set; }
    }
}
