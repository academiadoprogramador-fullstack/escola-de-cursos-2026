using FluentResults;
using EscolaDeCursos.Dominio.Modulos.ModuloCategoria;
using EscolaDeCursos.Dominio.Modulos.ModuloCurso;
using EscolaDeCursos.Aplicacao.Compartilhado;

namespace EscolaDeCursos.Aplicacao.Modulos.ModuloCategoria;

public class ServicoCategoria : ServicoBase<Categoria>
{
    private readonly IRepositorioCategoria repositorioCategoria;
    private readonly IRepositorioCurso repositorioCurso;

    public ServicoCategoria(
        IRepositorioCategoria repositorioCategoria,
        IRepositorioCurso repositorioCurso
    )
    {
        this.repositorioCategoria = repositorioCategoria;
        this.repositorioCurso = repositorioCurso;
    }

    public Result Cadastrar(CadastrarCategoriaDto dto)
    {
        if (ExisteCategoriaComMesmoNome(dto.Nome))
            return Falha(nameof(dto.Nome), "Já existe uma categoria com este nome.");

        Categoria novaCategoria = new Categoria(dto.Nome);

        Result resultadoValidacao = ValidarEntidade(novaCategoria);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioCategoria.Cadastrar(novaCategoria);

        return Result.Ok();
    }

    public Result Editar(EditarCategoriaDto dto)
    {
        if (ExisteCategoriaComMesmoNome(dto.Nome, dto.Id))
            return Falha(nameof(dto.Nome), "Já existe uma categoria com este nome.");

        Categoria categoriaAtualizada = new Categoria(dto.Nome);

        Result resultadoValidacao = ValidarEntidade(categoriaAtualizada);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        bool conseguiuEditar = repositorioCategoria.Editar(dto.Id, categoriaAtualizada);

        if (!conseguiuEditar)
            return Falha(string.Empty, "Categoria não encontrada.");

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        Categoria? categoria = repositorioCategoria.SelecionarPorId(id);

        if (categoria == null)
            return Falha(string.Empty, "Categoria não encontrada.");

        if (PossuiCursosVinculados(id))
            return Falha(string.Empty, "Não é possível excluir esta categoria, pois ela possui cursos vinculados.");

        repositorioCategoria.Excluir(id);

        return Result.Ok();
    }

    public List<ListarCategoriaDto> SelecionarTodos()
    {
        return repositorioCategoria
            .SelecionarTodos()
            .Select(c => new ListarCategoriaDto(c.Id, c.Nome))
            .ToList();
    }

    public Result<DetalhesCategoriaDto> SelecionarPorId(Guid id)
    {
        Categoria? categoria = repositorioCategoria.SelecionarPorId(id);

        if (categoria == null)
            return Result.Fail("Categoria não encontrada.");

        return Result.Ok(new DetalhesCategoriaDto(categoria.Id, categoria.Nome));
    }

    private bool ExisteCategoriaComMesmoNome(string nome, Guid? idIgnorado = null)
    {
        string nomeNormalizado = NormalizarNome(nome);

        return repositorioCategoria
            .SelecionarTodos()
            .Any(c =>
                c.Id != idIgnorado &&
                NormalizarNome(c.Nome) == nomeNormalizado
            );
    }

    private bool PossuiCursosVinculados(Guid categoriaId)
    {
        return repositorioCurso
            .SelecionarTodos()
            .Any(c => c.Categoria.Id == categoriaId);
    }

    private static string NormalizarNome(string nome)
    {
        return nome.Trim().ToLowerInvariant();
    }
}
