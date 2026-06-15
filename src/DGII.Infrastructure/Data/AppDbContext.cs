using DGII.Domain.Entities;
using DGII.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace DGII.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Contribuyente> Contribuyentes => Set<Contribuyente>();
    public DbSet<ComprobanteFiscal> ComprobantesFiscales => Set<ComprobanteFiscal>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ContribuyenteConfiguration());
        modelBuilder.ApplyConfiguration(new ComprobanteFiscalConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
