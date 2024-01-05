using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

internal class AddressLocalConfiguration : IEntityTypeConfiguration<AddressLocal>
{
    public void Configure(EntityTypeBuilder<AddressLocal> builder)
    {
        builder.HasKey(a => a.Id);
        
        builder
            .Property(a => a.Floor)
            .IsRequired();

        builder
            .Property(a => a.Room)
            .IsRequired();
        
        builder
            .HasOne(a => a.Office)
            .WithOne(o => o.AddressLocal)
            .HasForeignKey<Office>(o => o.AddressLocalId);
    }
}
