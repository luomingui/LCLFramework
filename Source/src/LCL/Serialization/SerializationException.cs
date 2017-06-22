using LCL;
using System;
using System.Runtime.InteropServices;

namespace LCL.Serialization
{
    [Serializable]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [ComDefaultInterface(typeof(_Exception))]
    public class SerializationException : LException
    {
        #region Ctor
        public SerializationException() : base() { }
        public SerializationException(string message) : base(message) { }
        public SerializationException(string message, Exception innerException) : base(message, innerException) { }
        public SerializationException(string format, params object[] args) : base(string.Format(format, args)) { }
        #endregion
    }
}
