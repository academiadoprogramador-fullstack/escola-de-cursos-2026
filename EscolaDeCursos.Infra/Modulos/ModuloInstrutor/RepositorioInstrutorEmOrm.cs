using EscolaDeCursos.Infra.Compartilhado.Orm;
using EscolaDeCursos.Dominio.Modulos.ModuloInstrutor;

namespace EscolaDeCursos.Infra.Modulos.ModuloInstrutor;

public sealed class RepositorioInstrutorEmOrm(
    EscolaDeCursosDbContext dbContext
) : RepositorioBaseEmOrm<Instrutor>(dbContext), IRepositorioInstrutor;
