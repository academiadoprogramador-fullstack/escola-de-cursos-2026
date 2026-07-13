using EscolaDeCursos.Infra.Compartilhado.Orm;
using EscolaDeCursos.Dominio.Modulos.ModuloMatricula;
using Microsoft.EntityFrameworkCore;

namespace EscolaDeCursos.Infra.Modulos.ModuloMatricula;

public sealed class RepositorioMatriculaEmOrm(
    EscolaDeCursosDbContext dbContext
) : RepositorioBaseEmOrm<Matricula>(dbContext), IRepositorioMatricula
{
    public override Matricula? SelecionarPorId(Guid idSelecionado)
    {
        return registros
            .Include(m => m.Aluno)
            .Include(m => m.Turma)
            .SingleOrDefault(m => m.Id == idSelecionado);
    }

    public List<Matricula> SelecionarPorTurmaId(Guid turmaId)
    {
        return registros
            .Include(m => m.Aluno)
            .Include(m => m.Turma)
            .Where(m => m.Turma.Id == turmaId)
            .OrderBy(m => m.Aluno.Nome)
            .ToList();
    }
}
