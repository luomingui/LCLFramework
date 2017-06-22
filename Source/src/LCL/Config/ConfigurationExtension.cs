using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Configuration;

namespace LCL.Config
{
    public static class ConfigurationExtension
    {
        /// <summary>
        /// 设置某个 AppSetting 的值到 configuration 中。
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        public static void SetAppSettingOrDefault<T>(this System.Configuration.Configuration configuration, string key, T defaultValue = default(T))
        {
            configuration.SetAppSettingOrDefault(key, defaultValue.ToString());
        }

        /// <summary>
        /// 设置某个 AppSetting 的值到 configuration 中。
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetAppSettingOrDefault(this System.Configuration.Configuration configuration, string key, string value)
        {
            var settings = configuration.AppSettings.Settings[key];
            if (settings == null)
            {
                settings = new KeyValueConfigurationElement(key, value);
                configuration.AppSettings.Settings.Add(settings);
            }
            else
            {
                settings.Value = value;
            }
        }
    }
}
