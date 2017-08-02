using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using LCL.Domain.Model;

namespace LCL.Domain.Repositories.EntityFramework.ModelConfigurations
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
