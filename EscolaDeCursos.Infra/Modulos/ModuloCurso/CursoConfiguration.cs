using EscolaDeCursos.Dominio.Modulos.ModuloCurso;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EscolaDeCursos.Infra.Modulos.ModuloCurso;

public sealed class CursoConfiguration : IEntityTypeConfiguration<Curso>
{
    public void Configure(EntityTypeBuilder<Curso> builder)
    {
        builder.ToTable("TBCurso");

        builder.HasKey(c => c.Id)
            .HasName("PK_TBCurso");

        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Property(c => c.Nome)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.Nivel)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(c => c.CargaHoraria)
            .IsRequired();

        builder.HasIndex(c => c.Nome)
            .IsUnique()
            .HasDatabaseName("UQ_TBCurso_Nome");

        builder.HasOne(c => c.Categoria)
            .WithMany()
            .HasForeignKey("CategoriaId")
            .HasConstraintName("FK_TBCurso_TBCategoria")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.Aulas)
            .WithOne(a => a.Curso)
            .HasForeignKey("CursoId")
            .HasConstraintName("FK_TBAula_TBCurso")
            .OnDelete(DeleteBehavior.Restrict);
    }
}
