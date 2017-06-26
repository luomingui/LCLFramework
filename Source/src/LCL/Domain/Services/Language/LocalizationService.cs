using LCL.Caching;
using LCL.Config;
using LCL.Domain.Entities;
using LCL.Domain.Repositories;
using LCL.Infrastructure;
using LCL.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LCL.Domain.Services
{
    public class LocalizationService : ILocalizationService
    {
        private readonly LConfig _config;
        private readonly IWebHelper _webHelper;
        private const string LOCALSTRINGRESOURCES_ALL_KEY = "LCL.lsr.all-{0}";
        private string _xmlPath = "";
        public LocalizationService(LConfig config, IWebHelper webHelper)
        {
            this._config = config;
            this._webHelper = webHelper;
            this._xmlPath = this._webHelper.MapPath(this._config.LanguagePath);
        }
        public string GetResource(string resourceKey)
        {
            var model = GetStringResourceAll().FirstOrDefault(p => p.ResourceName == resourceKey);
            if (model == null)
                return resourceKey;
            return model.ResourceValue;
        }
        public List<LocaleStringResource> GetStringResourceAll()
        {
            string key = string.Format(LOCALSTRINGRESOURCES_ALL_KEY, this._config.LanguageName);
           
            
            return RF.Cache.Get(key, () =>
            {
                #region MyRegion
                var list = new List<LocaleStringResource>();

                //stored procedures aren't supported
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(this._xmlPath);
                var nodes = xmlDoc.SelectNodes(@"//Language/LocaleResource");
                foreach (XmlNode node in nodes)
                {
                    string name = node.Attributes["Name"].InnerText.Trim();
                    string value = "";
                    var valueNode = node.SelectSingleNode("Value");
                    if (valueNode != null)
                        value = valueNode.InnerText;
                    if (String.IsNullOrEmpty(name))
                        continue;

                    var res = new LocaleStringResource();
                    res.ResourceName = name;
                    res.ResourceValue = value;

                    list.Add(res);
                }

                return list;
                #endregion

            });
        }
        public virtual void InsertLocaleStringResource(LocaleStringResource localeStringResource)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(this._xmlPath);
            XmlNode root = xmlDoc.SelectSingleNode("Language");

            XmlElement xe1 = xmlDoc.CreateElement("LocaleResource");
            xe1.SetAttribute("Name", localeStringResource.ResourceName);

            XmlElement xesub1 = xmlDoc.CreateElement("Value");
            xesub1.InnerText = localeStringResource.ResourceValue; 

            xe1.AppendChild(xesub1);

            root.AppendChild(xe1);
            xmlDoc.Save(this._xmlPath); 
            string key = string.Format(LOCALSTRINGRESOURCES_ALL_KEY, this._config.LanguageName);
            RF.Cache.Remove(key);
        }
        public virtual void UpdateLocaleStringResource(LocaleStringResource localeStringResource)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(this._xmlPath);

            XmlNodeList nodeList = xmlDoc.SelectSingleNode("Language").ChildNodes;
            foreach (XmlNode xn in nodeList)
            {
                XmlElement xe = (XmlElement)xn;
                if (xe.GetAttribute("Name") == localeStringResource.ResourceName)
                {
                    // xe.SetAttribute("Name", "update李赞红");
                    XmlNodeList nls = xe.ChildNodes;
                    foreach (XmlNode xn1 in nls)
                    {
                        XmlElement xe2 = (XmlElement)xn1;
                        if (xe2.Name == "Value")
                        {
                            xe2.InnerText = localeStringResource.ResourceValue;
                            break;
                        }
                    }
                    break;
                }
            }
            xmlDoc.Save(this._xmlPath);
            string key = string.Format(LOCALSTRINGRESOURCES_ALL_KEY, this._config.LanguageName);
            RF.Cache.Remove(key);
        }
        public virtual void DeleteLocaleStringResource(LocaleStringResource localeStringResource)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(this._xmlPath);

            XmlNodeList nodeList = xmlDoc.SelectSingleNode("Language").ChildNodes;
            foreach (XmlNode xn in nodeList)
            {
                XmlElement xe = (XmlElement)xn;
                if (xe.GetAttribute("Name") == localeStringResource.ResourceName)
                {
                    xe.RemoveAll();
                    break;
                }
            }
            xmlDoc.Save(this._xmlPath);
            string key = string.Format(LOCALSTRINGRESOURCES_ALL_KEY, this._config.LanguageName);
            RF.Cache.Remove(key);
        }
    
    }

}
