using EscolaDeCursos.Infra.Compartilhado.Orm;
using EscolaDeCursos.Dominio.Modulos.ModuloCurso;
using Microsoft.EntityFrameworkCore;

namespace EscolaDeCursos.Infra.Modulos.ModuloCurso;

public sealed class RepositorioCursoEmOrm(
    EscolaDeCursosDbContext dbContext
) : RepositorioBaseEmOrm<Curso>(dbContext), IRepositorioCurso
{
    public override Curso? SelecionarPorId(Guid idSelecionado)
    {
        return registros
            .Include(c => c.Categoria)
            .Include(c => c.Aulas)
            .SingleOrDefault(c => c.Id == idSelecionado);
    }

    public override List<Curso> SelecionarTodos()
    {
        return registros
            .Include(c => c.Categoria)
            .OrderBy(c => c.Nome)
            .ToList();
    }

    public override List<Curso> Filtrar(Func<Curso, bool> filtro)
    {
        return registros
            .Include(c => c.Categoria)
            .OrderBy(c => c.Nome)
            .Where(filtro)
            .ToList();
    }
}
