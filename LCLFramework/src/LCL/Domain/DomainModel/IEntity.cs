using System;
namespace LCL
{
    /// <summary>
    /// Represents that the implemented classes are domain entities.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Gets or sets the identifier of the entity.
        /// </summary>
        Guid ID { get; set; }
    }
}
