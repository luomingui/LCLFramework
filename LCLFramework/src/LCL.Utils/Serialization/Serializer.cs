
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace LCL.Serialization
{
    /// <summary>
    /// 序列化门户 API
    /// </summary>
    public static class Serializer
    {
        /// <summary>
        /// 使用二进制序列化对象。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] Serialize(object value)
        {
            if (value == null) return null;

            var fomatter = SerializationFormatterFactory.GetFormatter();
            var stream = new MemoryStream();
            fomatter.Serialize(stream, value);

            //var dto = Encoding.UTF8.GetString(stream.GetBuffer());
            var dto = stream.ToArray();
            return dto;
        }
        /// <summary>
        /// 使用二进制反序列化对象。
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static object Deserialize(byte[] bytes)
        {
            if (bytes == null) return null;

            //var bytes = Encoding.UTF8.GetBytes(dto as string);
            var stream = new MemoryStream(bytes);

            var bf = SerializationFormatterFactory.GetFormatter();
            var result = bf.Deserialize(stream);

            return result;
        }
    }
}
