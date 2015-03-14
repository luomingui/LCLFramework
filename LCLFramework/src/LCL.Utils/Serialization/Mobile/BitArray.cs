
namespace LCL.Serialization.Mobile
{
    /// <summary>
    /// 低存储空间的多个 Boolean 值序列化器
    /// </summary>
    public struct BitContainer
    {
        private Bit _bits;

        public BitContainer(int intValue)
        {
            this._bits = (Bit)intValue;
        }

        public void SetValue(Bit bit, bool value)
        {
            if (value)
            {
                this._bits |= bit;
            }
            else
            {
                var reverse = (Bit)0x11111111 ^ bit;
                this._bits &= reverse;
            }
        }

        public bool GetValue(Bit bit)
        {
            return (this._bits & bit) == bit;
        }

        public int ToInt32( )
        {
            return (int)this._bits;
        }
    }

    public enum Bit
    {
        _1 = 1,
        _2 = 2,
        _4 = 4,
        _8 = 8,
        _16 = 16,
        _32 = 32,
        _64 = 64
    }
}
