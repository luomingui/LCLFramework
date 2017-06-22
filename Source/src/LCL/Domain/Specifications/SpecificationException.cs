using System;
using System.Runtime.InteropServices;

namespace LCL.Domain.Specifications
{
    [Serializable]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [ComDefaultInterface(typeof(_Exception))]
    public class SpecificationException : LException
    {
        #region Ctor
        public SpecificationException() : base() { }
        public SpecificationException(string message) : base(message) { }
        public SpecificationException(string message, Exception innerException) : base(message, innerException) { }
        public SpecificationException(string format, params object[] args) : base(string.Format(format, args)) { }
        #endregion

    }
}
