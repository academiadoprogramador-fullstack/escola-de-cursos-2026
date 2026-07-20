using EscolaDeCursos.Dominio.Modulos.ModuloCurso;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EscolaDeCursos.Infra.Modulos.ModuloCurso;

public sealed class AulaConfiguration : IEntityTypeConfiguration<Aula>
{
    public void Configure(EntityTypeBuilder<Aula> builder)
    {
        builder.ToTable("TBAula");

        builder.HasKey(a => a.Id)
            .HasName("PK_TBAula");

        builder.Property(a => a.Id)
            .ValueGeneratedNever();

        builder.Property(a => a.Nome)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(a => a.DuracaoEmMinutos)
            .IsRequired();

        builder.Property(a => a.Ordem)
            .IsRequired();

        builder.Property<Guid>("CursoId")
            .IsRequired();

        builder.HasIndex(a => a.Nome)
            .IsUnique()
            .HasDatabaseName("UQ_TBAula_Nome");

        builder.HasIndex(nameof(Aula.UserId), "CursoId", nameof(Aula.Ordem))
            .IsUnique()
            .HasDatabaseName("UQ_TBAula_UserId_CursoId_Ordem");
    }
}
