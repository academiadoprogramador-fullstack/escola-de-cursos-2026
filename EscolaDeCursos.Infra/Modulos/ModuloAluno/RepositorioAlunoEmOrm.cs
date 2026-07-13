using EscolaDeCursos.Infra.Compartilhado.Orm;
using EscolaDeCursos.Dominio.Modulos.ModuloAluno;

namespace EscolaDeCursos.Infra.Modulos.ModuloAluno;

public sealed class RepositorioAlunoEmOrm(
    EscolaDeCursosDbContext dbContext
) : RepositorioBaseEmOrm<Aluno>(dbContext), IRepositorioAluno;
