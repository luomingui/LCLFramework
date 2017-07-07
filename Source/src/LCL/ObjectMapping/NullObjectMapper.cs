
namespace LCL.ObjectMapping
{
    public sealed class NullObjectMapper : IObjectMapper
    {
        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static NullObjectMapper Instance { get { return SingletonInstance; } }
        private static readonly NullObjectMapper SingletonInstance = new NullObjectMapper();

        public TDestination Map<TDestination>(object source)
        {
            throw new LException("LCL.ObjectMapping.IObjectMapper should be implemented in order to map objects.");
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            throw new LException("LCL.ObjectMapping.IObjectMapper should be implemented in order to map objects.");
        }
    }
}