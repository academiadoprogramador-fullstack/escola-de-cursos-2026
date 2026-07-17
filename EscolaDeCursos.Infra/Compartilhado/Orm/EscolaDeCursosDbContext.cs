using System.Reflection;
using EscolaDeCursos.Dominio.Compartilhado.Identity;
using EscolaDeCursos.Dominio.Modulos.ModuloAluno;
using EscolaDeCursos.Dominio.Modulos.ModuloCategoria;
using EscolaDeCursos.Dominio.Modulos.ModuloCurso;
using EscolaDeCursos.Dominio.Modulos.ModuloInstituicao;
using EscolaDeCursos.Dominio.Modulos.ModuloInstrutor;
using EscolaDeCursos.Dominio.Modulos.ModuloMatricula;
using EscolaDeCursos.Dominio.Modulos.ModuloTurma;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EscolaDeCursos.Infra.Compartilhado.Orm;

public sealed class EscolaDeCursosDbContext(
    DbContextOptions<EscolaDeCursosDbContext> options,
    IUserProvider? userProvider = null
) : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>(options)
{
    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<Curso> Cursos => Set<Curso>();
    public DbSet<Aula> Aulas => Set<Aula>();
    public DbSet<Instrutor> Instrutores => Set<Instrutor>();
    public DbSet<Aluno> Alunos => Set<Aluno>();
    public DbSet<Turma> Turmas => Set<Turma>();
    public DbSet<Matricula> Matriculas => Set<Matricula>();
    public DbSet<Instituicao> Instituicoes => Set<Instituicao>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        Assembly assembly = typeof(EscolaDeCursosDbContext).Assembly;

        modelBuilder.ApplyConfigurationsFromAssembly(assembly);

        Guid? userId = userProvider?.Id;

        modelBuilder.Entity<Categoria>()
            .HasQueryFilter(c => c.UserId == userId);

        modelBuilder.Entity<Curso>()
            .HasQueryFilter(c => c.UserId == userId);

        modelBuilder.Entity<Aula>()
            .HasQueryFilter(a => a.UserId == userId);

        modelBuilder.Entity<Instrutor>()
            .HasQueryFilter(i => i.UserId == userId);

        modelBuilder.Entity<Aluno>()
            .HasQueryFilter(a => a.UserId == userId);

        modelBuilder.Entity<Turma>()
            .HasQueryFilter(t => t.UserId == userId);

        modelBuilder.Entity<Matricula>()
            .HasQueryFilter(m => m.UserId == userId);
    }

    public override int SaveChanges()
    {
        Guid? userId = userProvider?.Id;

        if (!userId.HasValue)
        {
            throw new UnauthorizedAccessException(
                "Não é possível salvar entidades da instituição sem estar autenticado."
            );
        }

        foreach (var entry in ChangeTracker.Entries<IEntidadeUsuario>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    if (entry.Entity.UserId == Guid.Empty)
                    {
                        entry.Property(nameof(IEntidadeUsuario.UserId)).CurrentValue = userId.Value;
                    }
                    else if (entry.Entity.UserId != userId.Value)
                    {
                        throw new UnauthorizedAccessException(
                            "Tentativa de criar entidade para outra instituição."
                        );
                    }

                    break;

                case EntityState.Modified:
                    Guid idOriginalInstituicao = entry
                        .Property(nameof(IEntidadeUsuario.UserId))
                        .OriginalValue is Guid idOriginal
                        ? idOriginal
                        : Guid.Empty;

                    Guid idAtualInstituicao = entry
                        .Property(nameof(IEntidadeUsuario.UserId))
                        .OriginalValue is Guid idAtual
                        ? idAtual
                        : Guid.Empty;

                    if (idOriginalInstituicao != idAtualInstituicao)
                    {
                        throw new UnauthorizedAccessException(
                              "Não é permitido alterar a instituição de uma entidade."
                          );
                    }

                    if (idAtualInstituicao != userId.Value)
                    {
                        throw new UnauthorizedAccessException(
                            "Tentativa de modificar entidade de outra instituição."
                        );
                    }

                    break;

                case EntityState.Deleted:
                    Guid instituicaoOriginal = entry
                        .Property(nameof(IEntidadeUsuario.UserId))
                        .OriginalValue is Guid original
                        ? original
                        : Guid.Empty;

                    if (instituicaoOriginal != userId.Value)
                    {
                        throw new UnauthorizedAccessException(
                            "Tentativa de excluir entidade de outra instituicao."
                        );
                    }

                    break;

            }
        }

        return base.SaveChanges();
    }
}
