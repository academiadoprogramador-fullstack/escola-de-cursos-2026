using EscolaDeCursos.Dominio.Modulos.ModuloCategoria;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EscolaDeCursos.Infra.Modulos.ModuloCategoria;

public sealed class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.ToTable("TBCategoria");

        builder.HasKey(c => c.Id)
            .HasName("PK_TBCategoria");

        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Property(c => c.Nome)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(c => new { c.UserId, c.Nome })
            .IsUnique()
            .HasDatabaseName("UQ_TBCategoria_UserId_Nome");
    }
}
