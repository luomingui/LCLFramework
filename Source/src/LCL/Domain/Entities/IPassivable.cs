namespace LCL.Domain.Entities
{
    public interface IPassivable
    {
        bool IsActive { get; set; }
    }
}