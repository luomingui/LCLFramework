using System;
using System.Runtime.Serialization;

namespace LCL
{
    [Serializable]
    public class LException : Exception
    {
        public Type EntityType { get; set; }
        public object Id { get; set; }
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
        public LException(Type entityType, object id)
            : this(entityType, id, null)
        {

        }

        public LException(Type entityType, object id, Exception innerException)
            : base("There is no such an entity. Entity type: {entityType.FullName}, id: {id}", innerException)
        {
            EntityType = entityType;
            Id = id;
        }
    }
}
