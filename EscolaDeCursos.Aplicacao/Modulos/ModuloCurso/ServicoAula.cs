using FluentResults;
using EscolaDeCursos.Dominio.Modulos.ModuloCurso;
using EscolaDeCursos.Aplicacao.Compartilhado;

namespace EscolaDeCursos.Aplicacao.Modulos.ModuloCurso;

public class ServicoAula : ServicoBase<Aula>
{
    private readonly IRepositorioAula repositorioAula;
    private readonly IRepositorioCurso repositorioCurso;

    public ServicoAula(
        IRepositorioAula repositorioAula,
        IRepositorioCurso repositorioCurso
    )
    {
        this.repositorioAula = repositorioAula;
        this.repositorioCurso = repositorioCurso;
    }

    public Result Adicionar(AdicionarAulaDto dto)
    {
        if (ExisteAulaComMesmoNome(dto.Nome))
            return Falha(nameof(dto.Nome), "Já existe uma aula com este nome.");

        if (ExisteOrdemRepetida(dto.CursoId, dto.Ordem))
            return Falha(nameof(dto.Ordem), "Já existe uma aula com esta ordem neste curso.");

        Curso? curso = repositorioCurso.SelecionarPorId(dto.CursoId);

        if (curso == null)
            return Falha(nameof(dto.CursoId), "Curso não encontrado.");

        Aula novaAula = new Aula(
            dto.Nome,
            dto.DuracaoEmMinutos,
            dto.Ordem,
            curso
        );

        Result resultadoValidacao = ValidarEntidade(novaAula);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioAula.Cadastrar(novaAula);

        return Result.Ok();
    }

    public Result Editar(EditarAulaDto dto)
    {
        if (ExisteAulaComMesmoNome(dto.Nome, dto.Id))
            return Falha(nameof(dto.Nome), "Já existe uma aula com este nome.");

        if (ExisteOrdemRepetida(dto.CursoId, dto.Ordem, dto.Id))
            return Falha(nameof(dto.Ordem), "Já existe uma aula com esta ordem neste curso.");

        Aula aulaAtualizada = new Aula(
            dto.Nome,
            dto.DuracaoEmMinutos,
            dto.Ordem,
            null!
        );

        Result resultadoValidacao = ValidarEntidade(aulaAtualizada);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        bool conseguiuEditar = repositorioAula.Editar(dto.Id, aulaAtualizada);

        if (!conseguiuEditar)
            return Falha(string.Empty, "Aula não encontrada.");

        return Result.Ok();
    }

    public Result Remover(Guid id)
    {
        Aula? aula = repositorioAula.SelecionarPorId(id);

        if (aula == null)
            return Falha(string.Empty, "Aula não encontrada.");

        repositorioAula.Excluir(id);

        return Result.Ok();
    }

    public List<ListarAulaDto> SelecionarPorCursoId(Guid cursoId)
    {
        return repositorioAula
            .Filtrar(a => a.Curso.Id == cursoId)
            .OrderBy(a => a.Ordem)
            .Select(a => new ListarAulaDto(a.Id, a.Nome, a.DuracaoEmMinutos, a.Ordem))
            .ToList();
    }

    public Result<DetalhesAulaDto> SelecionarPorId(Guid id)
    {
        Aula? aula = repositorioAula.SelecionarPorId(id);

        if (aula == null)
            return Result.Fail("Aula não encontrada.");

        return Result.Ok(new DetalhesAulaDto(
            aula.Id,
            aula.Nome,
            aula.DuracaoEmMinutos,
            aula.Ordem,
            aula.Curso.Id
        ));
    }

    private bool ExisteAulaComMesmoNome(string nome, Guid? idIgnorado = null)
    {
        string nomeNormalizado = NormalizarNome(nome);

        return repositorioAula
            .SelecionarTodos()
            .Any(a =>
                a.Id != idIgnorado &&
                NormalizarNome(a.Nome) == nomeNormalizado
            );
    }

    private bool ExisteOrdemRepetida(Guid cursoId, int ordem, Guid? idIgnorado = null)
    {
        return repositorioAula
            .Filtrar(a => a.Curso.Id == cursoId)
            .Any(a =>
                a.Id != idIgnorado &&
                a.Ordem == ordem
            );
    }

    private static string NormalizarNome(string nome)
    {
        return nome.Trim().ToLowerInvariant();
    }
}
