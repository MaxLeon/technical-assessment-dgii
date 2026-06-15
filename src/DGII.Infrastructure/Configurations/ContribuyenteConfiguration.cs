using DGII.Domain.Entities;
using DGII.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGII.Infrastructure.Configurations;

public class ContribuyenteConfiguration : IEntityTypeConfiguration<Contribuyente>
{
    public void Configure(EntityTypeBuilder<Contribuyente> builder)
    {
        builder.ToTable("contribuyentes");

        builder.HasKey(c => c.RncCedula);

        builder.Property(c => c.RncCedula)
            .HasColumnName("rnc_cedula")
            .HasMaxLength(11)
            .IsRequired();

        builder.Property(c => c.Nombre)
            .HasColumnName("nombre")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(c => c.Tipo)
            .HasColumnName("tipo")
            .HasConversion<short>()
            .IsRequired();

        builder.Property(c => c.Estatus)
            .HasColumnName("estatus")
            .HasConversion<short>()
            .IsRequired();

    }
}
