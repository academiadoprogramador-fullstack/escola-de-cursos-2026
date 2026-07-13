using FluentResults;
using EscolaDeCursos.Dominio.Modulos.ModuloCurso;
using EscolaDeCursos.Dominio.Modulos.ModuloInstrutor;
using EscolaDeCursos.Dominio.Modulos.ModuloTurma;
using EscolaDeCursos.Aplicacao.Compartilhado;

namespace EscolaDeCursos.Aplicacao.Modulos.ModuloTurma;

public class ServicoTurma : ServicoBase<Turma>
{
    private readonly IRepositorioTurma repositorioTurma;
    private readonly IRepositorioCurso repositorioCurso;
    private readonly IRepositorioInstrutor repositorioInstrutor;

    public ServicoTurma(
        IRepositorioTurma repositorioTurma,
        IRepositorioCurso repositorioCurso,
        IRepositorioInstrutor repositorioInstrutor
    )
    {
        this.repositorioTurma = repositorioTurma;
        this.repositorioCurso = repositorioCurso;
        this.repositorioInstrutor = repositorioInstrutor;
    }

    public Result Cadastrar(CadastrarTurmaDto dto)
    {
        Result<Curso> resultadoCurso = SelecionarCurso(dto.CursoId);
        if (resultadoCurso.IsFailed)
            return resultadoCurso.ToResult();

        Result<Instrutor> resultadoInstrutor = SelecionarInstrutor(dto.InstrutorId);
        if (resultadoInstrutor.IsFailed)
            return resultadoInstrutor.ToResult();

        Turma novaTurma = new Turma(
            dto.Nome,
            resultadoCurso.Value,
            resultadoInstrutor.Value,
            dto.NumeroMaximoAlunos,
            dto.DataInicio,
            dto.DataTermino
        );

        Result resultadoValidacao = ValidarEntidade(novaTurma);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioTurma.Cadastrar(novaTurma);

        return Result.Ok();
    }

    public Result Editar(EditarTurmaDto dto)
    {
        Result<Curso> resultadoCurso = SelecionarCurso(dto.CursoId);
        if (resultadoCurso.IsFailed)
            return resultadoCurso.ToResult();

        Result<Instrutor> resultadoInstrutor = SelecionarInstrutor(dto.InstrutorId);
        if (resultadoInstrutor.IsFailed)
            return resultadoInstrutor.ToResult();

        Turma turmaAtualizada = new Turma(
            dto.Nome,
            resultadoCurso.Value,
            resultadoInstrutor.Value,
            dto.NumeroMaximoAlunos,
            dto.DataInicio,
            dto.DataTermino
        );

        Result resultadoValidacao = ValidarEntidade(turmaAtualizada);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        bool conseguiuEditar = repositorioTurma.Editar(dto.Id, turmaAtualizada);

        if (!conseguiuEditar)
            return Falha(string.Empty, "Turma não encontrada.");

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        Turma? turma = repositorioTurma.SelecionarPorId(id);

        if (turma == null)
            return Falha(string.Empty, "Turma não encontrada.");

        if (turma.Matriculas.Count > 0)
            return Falha(string.Empty, "Não é possível excluir esta turma, pois ela possui matrículas vinculadas.");

        repositorioTurma.Excluir(id);

        return Result.Ok();
    }

    public List<ListarTurmaDto> SelecionarTodos()
    {
        return repositorioTurma
            .SelecionarTodos()
            .Select(t => new ListarTurmaDto(
                t.Id,
                t.Nome,
                t.Curso.Nome,
                t.Instrutor.Nome,
                t.NumeroMaximoAlunos,
                t.DataInicio,
                t.DataTermino,
                t.Matriculas.Count
            ))
            .ToList();
    }

    public Result<DetalhesTurmaDto> SelecionarPorId(Guid id)
    {
        Turma? turma = repositorioTurma.SelecionarPorId(id);

        if (turma == null)
            return Result.Fail("Turma não encontrada.");

        return Result.Ok(new DetalhesTurmaDto(
            turma.Id,
            turma.Nome,
            turma.Curso.Id,
            turma.Curso.Nome,
            turma.Instrutor.Id,
            turma.Instrutor.Nome,
            turma.NumeroMaximoAlunos,
            turma.DataInicio,
            turma.DataTermino
        ));
    }

    public List<OpcaoCursoTurmaDto> SelecionarCursos()
    {
        return repositorioCurso
            .SelecionarTodos()
            .Select(c => new OpcaoCursoTurmaDto(c.Id, c.Nome))
            .ToList();
    }

    public List<OpcaoInstrutorTurmaDto> SelecionarInstrutores()
    {
        return repositorioInstrutor
            .SelecionarTodos()
            .Select(i => new OpcaoInstrutorTurmaDto(i.Id, i.Nome))
            .ToList();
    }

    private Result<Curso> SelecionarCurso(Guid cursoId)
    {
        Curso? curso = repositorioCurso.SelecionarPorId(cursoId);

        if (curso == null)
            return Result.Fail<Curso>(new Error("Selecione um curso válido.").WithMetadata("Campo", nameof(CadastrarTurmaDto.CursoId)));

        return Result.Ok(curso);
    }

    private Result<Instrutor> SelecionarInstrutor(Guid instrutorId)
    {
        Instrutor? instrutor = repositorioInstrutor.SelecionarPorId(instrutorId);

        if (instrutor == null)
            return Result.Fail<Instrutor>(new Error("Selecione um instrutor válido.").WithMetadata("Campo", nameof(CadastrarTurmaDto.InstrutorId)));

        return Result.Ok(instrutor);
    }
}
