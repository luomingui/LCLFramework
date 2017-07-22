using LCL.Domain.Entities;

namespace LCL.ObjectMapping
{
    public interface IObjectMapper
    {
        TDestination Map<TDestination>(object source);
        TDestination Map<TSource, TDestination>(TSource source, TDestination destination);
    }
}
