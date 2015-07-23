using System;
namespace LCL
{
    /// <summary>
    /// Represents that the implemented classes are domain entities.
    /// </summary>
    public interface IEntity
    {
        Guid ID { get; set; }
        bool IsDelete { get; set; }
        DateTime AddDate { get; set; }
        DateTime UpdateDate { get; set; }
    }
}
