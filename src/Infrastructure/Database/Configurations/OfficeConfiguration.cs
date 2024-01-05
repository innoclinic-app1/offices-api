using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

internal class OfficeConfiguration : IEntityTypeConfiguration<Office>
{
    public void Configure(EntityTypeBuilder<Office> builder)
    {
        builder.HasKey(o => o.Id);

        builder
            .HasOne(o => o.AddressLocal)
            .WithOne(a => a.Office)
            .HasForeignKey<Office>(o => o.AddressLocalId)
            .IsRequired();
        
        builder
            .HasOne(o => o.AddressGlobal)
            .WithOne(a => a.Office)
            .HasForeignKey<Office>(o => o.AddressGlobalId)
            .IsRequired();

        builder
            .Property(o => o.Capacity)
            .IsRequired();

        builder
            .Property(o => o.IsActive)
            .IsRequired();

        builder
            .Property(o => o.PhoneNumber)
            .HasMaxLength(15)
            .IsRequired();
    }
}
