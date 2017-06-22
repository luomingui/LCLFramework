using System;
using System.Runtime.Serialization;

namespace LCL
{
    [Serializable]
    public class LException : Exception
    {
        public LException()
        {
        }
        public LException(string message)
            : base(message)
        {
        }
        public LException(string messageFormat, params object[] args)
			: base(string.Format(messageFormat, args))
		{
		}
        protected LException(SerializationInfo
            info, StreamingContext context)
            : base(info, context)
        {
        }
        public LException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
