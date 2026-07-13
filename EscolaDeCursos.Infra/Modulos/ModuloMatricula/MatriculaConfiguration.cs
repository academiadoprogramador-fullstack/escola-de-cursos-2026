using EscolaDeCursos.Dominio.Modulos.ModuloMatricula;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EscolaDeCursos.Infra.Modulos.ModuloMatricula;

public sealed class MatriculaConfiguration : IEntityTypeConfiguration<Matricula>
{
    public void Configure(EntityTypeBuilder<Matricula> builder)
    {
        builder.ToTable("TBMatricula");

        builder.HasKey(m => m.Id)
            .HasName("PK_TBMatricula");

        builder.Property(m => m.Id)
            .ValueGeneratedNever();

        builder.HasOne(m => m.Aluno)
            .WithMany()
            .HasForeignKey("AlunoId")
            .HasConstraintName("FK_TBMatricula_TBAluno")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex("TurmaId", "AlunoId")
            .IsUnique()
            .HasDatabaseName("UQ_TBMatricula_TurmaId_AlunoId");
    }
}
