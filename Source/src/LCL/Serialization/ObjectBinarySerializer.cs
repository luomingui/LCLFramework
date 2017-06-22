using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace LCL.Serialization
{
    public class ObjectBinarySerializer : IObjectSerializer
    {
        #region Private Fields
        private readonly BinaryFormatter binaryFormatter = new BinaryFormatter();
        #endregion

        #region IObjectSerializer Members
        public virtual byte[] Serialize<TObject>(TObject obj)
        {
            byte[] ret = null;
            using (MemoryStream ms = new MemoryStream())
            {
                binaryFormatter.Serialize(ms, obj);
                ret = ms.ToArray();
                ms.Close();
            }
            return ret;
        }
        public virtual TObject Deserialize<TObject>(byte[] stream)
        {
            using (MemoryStream ms = new MemoryStream(stream))
            {
                TObject ret = (TObject)binaryFormatter.Deserialize(ms);
                ms.Close();
                return ret;
            }
        }

        #endregion
    }
}
