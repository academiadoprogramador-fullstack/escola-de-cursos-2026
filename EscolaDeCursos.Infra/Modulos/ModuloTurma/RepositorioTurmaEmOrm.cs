using EscolaDeCursos.Infra.Compartilhado.Orm;
using EscolaDeCursos.Dominio.Modulos.ModuloTurma;
using Microsoft.EntityFrameworkCore;

namespace EscolaDeCursos.Infra.Modulos.ModuloTurma;

public sealed class RepositorioTurmaEmOrm(
    EscolaDeCursosDbContext dbContext
) : RepositorioBaseEmOrm<Turma>(dbContext), IRepositorioTurma
{
    public override Turma? SelecionarPorId(Guid idSelecionado)
    {
        return registros
            .Include(t => t.Curso)
            .Include(t => t.Instrutor)
            .Include(t => t.Matriculas)
                .ThenInclude(m => m.Aluno)
            .SingleOrDefault(t => t.Id == idSelecionado);
    }

    public override List<Turma> SelecionarTodos()
    {
        return registros
            .Include(t => t.Curso)
            .Include(t => t.Instrutor)
            .Include(t => t.Matriculas)
            .OrderBy(t => t.DataInicio)
            .ThenBy(t => t.Nome)
            .ToList();
    }
}
