using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using LCL.Domain.Model;

namespace LCL.Domain.Repositories.EntityFramework.ModelConfigurations
{
    public class SalesOrderTypeConfiguration : EntityTypeConfiguration<SalesOrder>
    {
        public SalesOrderTypeConfiguration()
        {
            HasKey<Guid>(s => s.ID);
            Property(s => s.ID)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Ignore(p => p.Subtotal);
        }
    }
}
