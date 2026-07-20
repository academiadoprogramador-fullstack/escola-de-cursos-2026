using EscolaDeCursos.Dominio.Modulos.ModuloTurma;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EscolaDeCursos.Infra.Modulos.ModuloTurma;

public sealed class TurmaConfiguration : IEntityTypeConfiguration<Turma>
{
    public void Configure(EntityTypeBuilder<Turma> builder)
    {
        builder.ToTable("TBTurma");

        builder.HasKey(t => t.Id)
            .HasName("PK_TBTurma");

        builder.Property(t => t.Id)
            .ValueGeneratedNever();

        builder.Property(t => t.Nome)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(t => t.NumeroMaximoAlunos)
            .IsRequired();

        builder.Property(t => t.DataInicio)
            .HasColumnType("date")
            .IsRequired();

        builder.Property(t => t.DataTermino)
            .HasColumnType("date")
            .IsRequired();

        builder.HasOne(t => t.Curso)
            .WithMany()
            .HasForeignKey("CursoId")
            .HasConstraintName("FK_TBTurma_TBCurso")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.Instrutor)
            .WithMany()
            .HasForeignKey("InstrutorId")
            .HasConstraintName("FK_TBTurma_TBInstrutor")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(t => t.Matriculas)
            .WithOne(m => m.Turma)
            .HasForeignKey("TurmaId")
            .HasConstraintName("FK_TBMatricula_TBTurma")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(t => new { t.UserId, t.Nome, t.DataInicio, t.DataTermino })
            .IsUnique()
            .HasDatabaseName("UQ_TBTurma_UserId_Nome_DataInicio_DataTermino");
    }
}
