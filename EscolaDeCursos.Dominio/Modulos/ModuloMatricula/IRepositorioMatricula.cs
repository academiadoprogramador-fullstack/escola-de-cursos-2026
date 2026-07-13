using EscolaDeCursos.Dominio.Compartilhado;

namespace EscolaDeCursos.Dominio.Modulos.ModuloMatricula;

public interface IRepositorioMatricula : IRepositorio<Matricula>
{
    List<Matricula> SelecionarPorTurmaId(Guid turmaId);
}
