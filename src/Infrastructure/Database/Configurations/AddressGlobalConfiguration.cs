using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

internal class AddressGlobalConfiguration : IEntityTypeConfiguration<AddressGlobal>
{
    public void Configure(EntityTypeBuilder<AddressGlobal> builder)
    {
        builder.HasKey(a => a.Id);

        builder
            .Property(a => a.Country)
            .IsRequired();

        builder
            .Property(a => a.City)
            .IsRequired();

        builder
            .Property(a => a.Street)
            .IsRequired();

        builder
            .Property(a => a.Suite)
            .IsRequired();
        
        builder
            .HasOne(a => a.Office)
            .WithOne(o => o.AddressGlobal)
            .HasForeignKey<Office>(o => o.AddressGlobalId);
    }
}
