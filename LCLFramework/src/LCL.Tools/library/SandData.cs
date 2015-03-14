using System;

namespace SF.Tools
{
    /// <summary>
    /// 元数据
    /// </summary>
    [System.Diagnostics.DebuggerStepThrough]
    [Serializable]
    public class SandData
    {
        String key = "";
        Object value = "";
        /// <summary>
        /// 元数据
        /// </summary>
        public SandData( ) { }
        /// <summary>
        /// 元数据
        /// </summary>
        public SandData(String m_key) : this(m_key, null) { }
        /// <summary>
        /// 元数据
        /// </summary>
        public SandData(String m_key, Object m_value)
        {
            this.key = m_key;
            this.value = m_value;
        }
        public String Key
        {
            get { return key; }
            set { key = value; }
        }
        public Object Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        public override string ToString( )
        {
            string str = this.key.ToString();
            return str;
        }
    }
}
