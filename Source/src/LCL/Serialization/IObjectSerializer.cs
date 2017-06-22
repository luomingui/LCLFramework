using System;

namespace LCL.Serialization
{
    public interface IObjectSerializer
    {
        byte[] Serialize<TObject>(TObject obj);
        TObject Deserialize<TObject>(byte[] stream);
    }
}
