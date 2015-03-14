using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace LCL.Serialization
{
    public class XmlFormatterSerializer
    {

        #region 反序列化
        /// <summary>
        /// 反序列化
        /// Deserializes from XML.
        /// <code>
        ///  string xmlPath = "d:\\config.xml";
        /// Config c = XmlUtil.DeserializeFromXml(xmlPath, typeof(Config)) as Config;
        /// </code>
        /// </summary>
        /// <param name="xmlFilePath">需要反序列化的XML文件的绝对路径</param>
        /// <param name="type">反序列化XML为哪种对象类型</param>
        /// <returns></returns>
        public static object DeserializeFromXml(string xmlFilePath, Type type)
        {
            object result = null;
            if (File.Exists(xmlFilePath))
            {
                using (StreamReader reader = new StreamReader(xmlFilePath))
                {
                    XmlSerializer xs = new XmlSerializer(type);
                    result = xs.Deserialize(reader);
                }
            }
            return result;
        }
        /// <summary> 

        /// 反序列化 

        /// </summary> 

        /// <param name="type">类型</param> 

        /// <param name="xml">XML字符串</param> 

        /// <returns></returns> 
        public static T DeserializeFromXml<T>(string xml,Type type)
        {
            try
            {
                using (StringReader sr = new StringReader(xml))
                {
                    XmlSerializer xmldes = new XmlSerializer(type);
                    return (T)xmldes.Deserialize(sr);
                }
            }
            catch
            {
                return default(T);
            }
        }
        #endregion 

        #region 序列化XML文件
        /// <summary> 
        /// 序列化XML文件 
        /// </summary> 
        /// <param name="type">类型</param> 
        /// <param name="obj">对象</param> 
        /// <returns></returns> 
        public static string SerializeToXml(object instantiation, Type type)
        {
            MemoryStream Stream = new MemoryStream();
            //创建序列化对象 
            XmlSerializer xml = new XmlSerializer(type);
            //序列化对象 
            xml.Serialize(Stream, instantiation);

            Stream.Position = 0;
            StreamReader sr = new StreamReader(Stream);
            string str = sr.ReadToEnd();
            return str;
        }
        /// <summary>
        /// 序列化
        /// 当需要将多个对象实例序列化到同一个XML文件中的时候,xmlRootName就是所有对象共同的根节点名称,
        /// 如果不指定,.net会默认给一个名称(ArrayOf+实体类名称)   
        /// Serializes to XML.
        /// <code>
        /// XmlUtil.SerializeToXml(config, config.GetType(), "d:\\config.xml", null);
        /// </code>
        /// </summary>
        /// <param name="srcObject">对象的实例</param>
        /// <param name="type">对象类型</param>
        /// <param name="xmlFilePath">序列化之后的xml文件的绝对路径</param>
        /// <param name="xmlRootName">xml文件中根节点名称</param>
        public static void SerializeToXml(object srcObject, Type type, string xmlFilePath, string xmlRootName)
        {
            if (srcObject != null && !string.IsNullOrEmpty(xmlFilePath))
            {
                type = type != null ? type : srcObject.GetType();

                using (StreamWriter sw = new StreamWriter(xmlFilePath))
                {
                    XmlSerializer xs = string.IsNullOrEmpty(xmlRootName) ?
                        new XmlSerializer(type) :
                        new XmlSerializer(type, new XmlRootAttribute(xmlRootName));
                    xs.Serialize(sw, srcObject);
                }
            }
        }
        #endregion 

    }
}
