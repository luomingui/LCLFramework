
using LCL.Serialization;
namespace LCL.DataPortal
{
    /// <summary>
    /// 一个适配 Serializer 接口的类
    /// </summary>
    internal static class InnerSerializer
    {
        internal static object SerializeObject(object value)
        {
            //如果想在 CompactMessageEncoder 中查看原始的 XML 值，
            //则把 InnerSerializer 内部的功能禁用掉，这样，WCF就会直接用 XML 把整个对象进行序列化。
            //return value;

            return Serializer.Serialize(value);
        }

        internal static object DeserializeObject(object dto)
        {
            //return dto;

            return Serializer.Deserialize(dto as byte[]);
        }
    }
}
