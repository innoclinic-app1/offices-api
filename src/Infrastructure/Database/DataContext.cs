using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public class DataContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Office> Offices { get; set; } = null!;
    public DbSet<AddressLocal> AddressLocals { get; set; } = null!;
    public DbSet<AddressGlobal> AddressGlobals { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }
}
