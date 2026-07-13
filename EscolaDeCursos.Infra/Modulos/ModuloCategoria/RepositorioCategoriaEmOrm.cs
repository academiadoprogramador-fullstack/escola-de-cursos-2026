using EscolaDeCursos.Infra.Compartilhado.Orm;
using EscolaDeCursos.Dominio.Modulos.ModuloCategoria;

namespace EscolaDeCursos.Infra.Modulos.ModuloCategoria;

public sealed class RepositorioCategoriaEmOrm(
    EscolaDeCursosDbContext dbContext
) : RepositorioBaseEmOrm<Categoria>(dbContext), IRepositorioCategoria;
