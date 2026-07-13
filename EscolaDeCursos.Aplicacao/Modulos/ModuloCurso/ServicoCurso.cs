using FluentResults;
using EscolaDeCursos.Dominio.Modulos.ModuloCategoria;
using EscolaDeCursos.Dominio.Modulos.ModuloCurso;
using EscolaDeCursos.Dominio.Modulos.ModuloTurma;
using EscolaDeCursos.Aplicacao.Compartilhado;

namespace EscolaDeCursos.Aplicacao.Modulos.ModuloCurso;

public class ServicoCurso : ServicoBase<Curso>
{
    private readonly IRepositorioCurso repositorioCurso;
    private readonly IRepositorioCategoria repositorioCategoria;
    private readonly IRepositorioTurma repositorioTurma;

    public ServicoCurso(
        IRepositorioCurso repositorioCurso,
        IRepositorioCategoria repositorioCategoria,
        IRepositorioTurma repositorioTurma
    )
    {
        this.repositorioCurso = repositorioCurso;
        this.repositorioCategoria = repositorioCategoria;
        this.repositorioTurma = repositorioTurma;
    }

    public Result Cadastrar(CadastrarCursoDto dto)
    {
        if (ExisteCursoComMesmoNome(dto.Nome))
            return Falha(nameof(dto.Nome), "Já existe um curso com este nome.");

        Result<Categoria> resultadoCategoria = SelecionarCategoria(dto.CategoriaId);

        if (resultadoCategoria.IsFailed)
            return resultadoCategoria.ToResult();

        Curso novoCurso = new Curso(
            dto.Nome,
            dto.Nivel,
            dto.CargaHoraria,
            resultadoCategoria.Value
        );

        Result resultadoValidacao = ValidarEntidade(novoCurso);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioCurso.Cadastrar(novoCurso);

        return Result.Ok();
    }

    public Result Editar(EditarCursoDto dto)
    {
        if (ExisteCursoComMesmoNome(dto.Nome, dto.Id))
            return Falha(nameof(dto.Nome), "Já existe um curso com este nome.");

        Result<Categoria> resultadoCategoria = SelecionarCategoria(dto.CategoriaId);

        if (resultadoCategoria.IsFailed)
            return resultadoCategoria.ToResult();

        Curso cursoAtualizado = new Curso(
            dto.Nome,
            dto.Nivel,
            dto.CargaHoraria,
            resultadoCategoria.Value
        );

        Result resultadoValidacao = ValidarEntidade(cursoAtualizado);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        bool conseguiuEditar = repositorioCurso.Editar(dto.Id, cursoAtualizado);

        if (!conseguiuEditar)
            return Falha(string.Empty, "Curso não encontrado.");

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        Curso? curso = repositorioCurso.SelecionarPorId(id);

        if (curso == null)
            return Falha(string.Empty, "Curso não encontrado.");

        if (curso.Aulas.Count > 0)
            return Falha(string.Empty, "Não é possível excluir este curso, pois ele possui aulas vinculadas.");

        if (PossuiTurmasVinculadas(id))
            return Falha(string.Empty, "Não é possível excluir este curso, pois ele possui turmas vinculadas.");

        repositorioCurso.Excluir(id);

        return Result.Ok();
    }

    public List<ListarCursoDto> SelecionarTodos()
    {
        return repositorioCurso
            .SelecionarTodos()
            .Select(c => new ListarCursoDto(
                c.Id,
                c.Nome,
                c.Nivel,
                c.CargaHoraria,
                c.Categoria.Nome
            ))
            .ToList();
    }

    public Result<DetalhesCursoDto> SelecionarPorId(Guid id)
    {
        Curso? curso = repositorioCurso.SelecionarPorId(id);

        if (curso == null)
            return Result.Fail("Curso não encontrado.");

        return Result.Ok(new DetalhesCursoDto(
            curso.Id,
            curso.Nome,
            curso.Nivel,
            curso.CargaHoraria,
            curso.Categoria.Id,
            curso.Categoria.Nome
        ));
    }

    public List<OpcaoCategoriaCursoDto> SelecionarCategorias()
    {
        return repositorioCategoria
            .SelecionarTodos()
            .Select(c => new OpcaoCategoriaCursoDto(c.Id, c.Nome))
            .ToList();
    }

    private Result<Categoria> SelecionarCategoria(Guid categoriaId)
    {
        Categoria? categoria = repositorioCategoria.SelecionarPorId(categoriaId);

        if (categoria == null)
            return Result.Fail<Categoria>(new Error("Selecione uma categoria válida.").WithMetadata("Campo", nameof(CadastrarCursoDto.CategoriaId)));

        return Result.Ok(categoria);
    }

    private bool ExisteCursoComMesmoNome(string nome, Guid? idIgnorado = null)
    {
        string nomeNormalizado = NormalizarNome(nome);

        return repositorioCurso
            .SelecionarTodos()
            .Any(c =>
                c.Id != idIgnorado &&
                NormalizarNome(c.Nome) == nomeNormalizado
            );
    }

    private static string NormalizarNome(string nome)
    {
        return nome.Trim().ToLowerInvariant();
    }

    private bool PossuiTurmasVinculadas(Guid cursoId)
    {
        return repositorioTurma
            .SelecionarTodos()
            .Any(t => t.Curso.Id == cursoId);
    }
}
