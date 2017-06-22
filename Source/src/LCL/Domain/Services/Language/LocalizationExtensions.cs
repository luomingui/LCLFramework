

using LCL.Domain.Entities;
using LCL.Infrastructure;
using LCL.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
        /*

<?xml version="1.0" encoding="utf-8"?>
<Language Name="简体中文" IsDefault="true" IsRightToLeft="false">
  <LocaleResource Name="Menu.UCenter">
    <Value>用户中心</Value>
  </LocaleResource>
</Language>
         
         */
namespace LCL.Domain.Services
{
    public static class LocalizationExtensions
    {
        public static void AddOrUpdatePluginLocaleResource(this BasePlugin plugin, string resourceName, string resourceValue)
        {
            if (plugin == null)
                throw new ArgumentNullException("plugin");
            if (string.IsNullOrWhiteSpace(resourceName))
                throw new ArgumentNullException("resourceName");
            if (string.IsNullOrWhiteSpace(resourceValue))
                throw new ArgumentNullException("resourceValue");

            var localizationService = EngineContext.Current.Resolve<ILocalizationService>();
            string resName = localizationService.GetResource(resourceName);

            if (string.IsNullOrWhiteSpace(resName)||resName == resourceName)
            {
                localizationService.InsertLocaleStringResource(new LocaleStringResource {
                     ResourceName=resourceName,
                     ResourceValue=resourceValue
                });
            }
            else 
            {
                localizationService.UpdateLocaleStringResource(new LocaleStringResource
                {
                    ResourceName = resourceName,
                    ResourceValue = resourceValue
                });
            }
        }

        public static void DeletePluginLocaleResource(this BasePlugin plugin,string resourceName)
        {
            if (plugin == null)
                throw new ArgumentNullException("plugin");
            if (string.IsNullOrWhiteSpace(resourceName))
                throw new ArgumentNullException("resourceName");

            var localizationService = EngineContext.Current.Resolve<ILocalizationService>();
            localizationService.DeleteLocaleStringResource(new LocaleStringResource {
                ResourceName=resourceName
            });
        }
    }
}
