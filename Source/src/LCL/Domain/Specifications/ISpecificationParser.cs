namespace LCL.Domain.Specifications
{
    public interface ISpecificationParser<TCriteria>
    {
        TCriteria Parse<T>(ISpecification<T> specification);
    }
}
