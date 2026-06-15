using DGII.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGII.Infrastructure.Configurations;

public class ComprobanteFiscalConfiguration : IEntityTypeConfiguration<ComprobanteFiscal>
{
    public void Configure(EntityTypeBuilder<ComprobanteFiscal> builder)
    {
        builder.ToTable("comprobantes_fiscales");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasColumnName("id")
            .UseIdentityColumn();

        builder.Property(c => c.RncCedula)
            .HasColumnName("rnc_cedula")
            .HasMaxLength(11)
            .IsRequired();

        builder.Property(c => c.NCF)
            .HasColumnName("ncf")
            .HasMaxLength(19)
            .IsRequired();

        builder.HasIndex(c => c.NCF).IsUnique();

        builder.HasOne<Contribuyente>()
            .WithMany()
            .HasForeignKey(cf => cf.RncCedula)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(c => c.Monto)
            .HasColumnName("monto")
            .HasColumnType("decimal(15,2)")
            .IsRequired();

        builder.Property(c => c.Itbis18)
            .HasColumnName("itbis18")
            .HasColumnType("decimal(15,2)")
            .IsRequired();
    }
}
