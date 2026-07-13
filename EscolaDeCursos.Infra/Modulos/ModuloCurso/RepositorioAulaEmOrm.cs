using EscolaDeCursos.Infra.Compartilhado.Orm;
using EscolaDeCursos.Dominio.Modulos.ModuloCurso;
using Microsoft.EntityFrameworkCore;

namespace EscolaDeCursos.Infra.Modulos.ModuloCurso;

public sealed class RepositorioAulaEmOrm(
    EscolaDeCursosDbContext dbContext
) : RepositorioBaseEmOrm<Aula>(dbContext), IRepositorioAula
{
    public override Aula? SelecionarPorId(Guid idSelecionado)
    {
        return registros
            .Include(a => a.Curso)
            .SingleOrDefault(a => a.Id == idSelecionado);
    }

    public override List<Aula> SelecionarTodos()
    {
        return registros
            .Include(a => a.Curso)
            .OrderBy(a => a.Curso.Id)
            .ThenBy(a => a.Ordem)
            .ToList();
    }

    public override List<Aula> Filtrar(Func<Aula, bool> filtro)
    {
        return registros
            .Include(a => a.Curso)
            .OrderBy(a => a.Ordem)
            .Where(filtro)
            .ToList();
    }
}
