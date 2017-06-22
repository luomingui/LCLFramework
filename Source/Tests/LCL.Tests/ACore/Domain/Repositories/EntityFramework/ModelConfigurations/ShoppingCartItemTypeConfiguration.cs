using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using LCL.Tests.Domain.Model;

namespace LCL.Tests.Domain.Repositories.EntityFramework.ModelConfigurations
{
    public class ShoppingCartItemTypeConfiguration : EntityTypeConfiguration<ShoppingCartItem>
    {
        public ShoppingCartItemTypeConfiguration()
        {
            HasKey(c => c.ID);
            Property(c => c.ID)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Ignore(p => p.LineAmount);
        }
    }
}
