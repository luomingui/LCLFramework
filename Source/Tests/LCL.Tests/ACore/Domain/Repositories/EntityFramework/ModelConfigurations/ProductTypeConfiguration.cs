using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using LCL.Tests.Domain.Model;

namespace LCL.Tests.Domain.Repositories.EntityFramework.ModelConfigurations
{
    /// <summary>
    /// Represents the entity type configuration for the <see cref="Laptop"/> entity.
    /// </summary>
    public class ProductTypeConfiguration : EntityTypeConfiguration<Product>
    {
        #region Ctor
        /// <summary>
        /// Initializes a new instance of <c>LaptopTypeConfiguration</c> class.
        /// </summary>
        public ProductTypeConfiguration()
        {
            HasKey<Guid>(l => l.ID);
            Property(p => p.ID)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Description)
                .IsRequired();
            Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(40);
            Property(p => p.ImageUrl)
                .HasMaxLength(255);
        }
        #endregion
    }
}
