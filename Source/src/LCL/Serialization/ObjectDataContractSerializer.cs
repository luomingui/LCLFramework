using System;
using System.IO;
using System.Runtime.Serialization;

namespace LCL.Serialization
{
    public class ObjectDataContractSerializer : IObjectSerializer
    {
        #region IObjectSerializer<TObject> Members
        public virtual byte[] Serialize<TObject>(TObject obj)
        {
            Type graphType = obj.GetType();
            DataContractSerializer js = new DataContractSerializer(graphType);
            byte[] ret = null;
            using (MemoryStream ms = new MemoryStream())
            {
                js.WriteObject(ms, obj);
                ret = ms.ToArray();
                ms.Close();
            }
            return ret;
        }
        public virtual TObject Deserialize<TObject>(byte[] stream)
        {
            DataContractSerializer js = new DataContractSerializer(typeof(TObject));
            using (MemoryStream ms = new MemoryStream(stream))
            {
                TObject ret = (TObject)js.ReadObject(ms);
                ms.Close();
                return ret;
            }
        }

        #endregion
    }
}
