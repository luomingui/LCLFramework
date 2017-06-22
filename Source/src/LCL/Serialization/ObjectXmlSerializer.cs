
using System;
using System.IO;
using NSXmlSerialization = System.Xml.Serialization;

namespace LCL.Serialization
{
    public class ObjectXmlSerializer : IObjectSerializer
    {
        #region IObjectSerializer Members
        public virtual byte[] Serialize<TObject>(TObject obj)
        {
            Type graphType = obj.GetType();
            NSXmlSerialization.XmlSerializer xmlSerializer = new NSXmlSerialization.XmlSerializer(graphType);
            byte[] ret = null;
            using (MemoryStream ms = new MemoryStream())
            {
                xmlSerializer.Serialize(ms, obj);
                ret = ms.ToArray();
                ms.Close();
            }
            return ret;
        }
        public virtual TObject Deserialize<TObject>(byte[] stream)
        {
            NSXmlSerialization.XmlSerializer xmlSerializer = new NSXmlSerialization.XmlSerializer(typeof(TObject));
            using (MemoryStream ms = new MemoryStream(stream))
            {
                TObject ret = (TObject)xmlSerializer.Deserialize(ms);
                ms.Close();
                return ret;
            }
        }

        #endregion
    }
}
