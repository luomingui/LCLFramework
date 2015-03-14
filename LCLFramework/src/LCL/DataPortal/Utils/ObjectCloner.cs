using LCL.Serialization;
using System.IO;

namespace LCL.DataPortal
{
    public static class ObjectCloner
    {
        /// <summary>
        /// 使用二进制序列化反序列化的方法复制一个对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T Clone<T>(T obj)
        {
            using (MemoryStream buffer = new MemoryStream())
            {
                var formatter = SerializationFormatterFactory.GetFormatter();
                formatter.Serialize(buffer, obj);
                buffer.Position = 0;
                object temp = formatter.Deserialize(buffer);
                return (T)temp;
            }
        }
    }
}
