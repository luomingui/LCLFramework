using LCL.Domain.Model;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace LCL.Domain.Repositories.EntityFramework.ModelConfigurations
{
    public class RoleTypeConfiguration : EntityTypeConfiguration<Role>
    {
        public RoleTypeConfiguration()
        {
            HasKey<Guid>(c => c.ID);
            Property(c => c.ID)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(25);
        }
    }
}
