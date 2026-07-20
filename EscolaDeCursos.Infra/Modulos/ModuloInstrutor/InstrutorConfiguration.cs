using EscolaDeCursos.Dominio.Modulos.ModuloInstrutor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EscolaDeCursos.Infra.Modulos.ModuloInstrutor;

public sealed class InstrutorConfiguration : IEntityTypeConfiguration<Instrutor>
{
    public void Configure(EntityTypeBuilder<Instrutor> builder)
    {
        builder.ToTable("TBInstrutor");

        builder.HasKey(i => i.Id)
            .HasName("PK_TBInstrutor");

        builder.Property(i => i.Id)
            .ValueGeneratedNever();

        builder.Property(i => i.Nome)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(i => i.Telefone)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(i => i.Email)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(i => i.Graduacao)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(i => new { i.UserId, i.Nome })
            .IsUnique()
            .HasDatabaseName("UQ_TBInstrutor_UserId_Nome");
    }
}
