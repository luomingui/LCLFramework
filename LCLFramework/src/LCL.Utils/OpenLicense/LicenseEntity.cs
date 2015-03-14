using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LCL.OpenLicense
{
    /// <summary>
    /// 许可证
    /// </summary>
    [Serializable]
    [XmlRoot(ElementName = "License")]
    public class LicenseEntity
    {
        [XmlElement(ElementName = "ID")]
        public Guid ID { get; set; }

        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "AssemblyName")]
        public string AssemblyName { get; set; }

        /// <summary>
        /// 过期时间(yyyy-MM-dd)
        /// </summary>
        [XmlElement(ElementName = "PastDate")]
        public string PastDate { get; set; }

        /// <summary>
        /// 使用次数
        /// </summary>
        [XmlElement(ElementName = "UseNumber")]
        public int UseNumber { get; set; }

        /// <summary>
        /// 版本升级
        /// </summary>
        [XmlElement(ElementName = "VersionUpgrade")]
        public bool VersionUpgrade { get; set; }

        /// <summary>
        /// 安装次数
        /// </summary>
        [XmlElement(ElementName = "InstallNumber")]
        public int InstallNumber { get; set; }

        //public string CreateXmlStr()
        //{
        //    StringBuilder builder = new StringBuilder();
        //    builder.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?> ");
        //    builder.AppendLine("<License xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"> ");
        //    builder.AppendLine("  <ID>" + Guid.NewGuid() + "</ID> ");
        //    builder.AppendLine("  <Name>" + Name + "</Name> ");
        //    builder.AppendLine("  <AssemblyName>" + AssemblyName + "</AssemblyName> ");
        //    builder.AppendLine("  <PastDate>" + PastDate + "</PastDate> ");
        //    builder.AppendLine("  <UseNumber>" + UseNumber + "</UseNumber> ");
        //    builder.AppendLine("  <VersionUpgrade>" + VersionUpgrade + "</VersionUpgrade> ");
        //    builder.AppendLine("  <InstallNumber>" + InstallNumber + "</InstallNumber> ");
        //    builder.AppendLine("</License> ");

        //    return builder.ToString();
        //}
    }
}
