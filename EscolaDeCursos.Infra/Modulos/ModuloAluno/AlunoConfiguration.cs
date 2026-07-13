using EscolaDeCursos.Dominio.Modulos.ModuloAluno;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EscolaDeCursos.Infra.Modulos.ModuloAluno;

public sealed class AlunoConfiguration : IEntityTypeConfiguration<Aluno>
{
    public void Configure(EntityTypeBuilder<Aluno> builder)
    {
        builder.ToTable("TBAluno");

        builder.HasKey(a => a.Id)
            .HasName("PK_TBAluno");

        builder.Property(a => a.Id)
            .ValueGeneratedNever();

        builder.Property(a => a.Nome)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(a => a.Telefone)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(a => a.Email)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(a => a.NumeroMatricula)
            .HasMaxLength(20)
            .IsRequired();

        builder.HasIndex(a => a.NumeroMatricula)
            .IsUnique()
            .HasDatabaseName("UQ_TBAluno_NumeroMatricula");
    }
}
