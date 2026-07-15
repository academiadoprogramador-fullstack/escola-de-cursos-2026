using FluentResults;
using EscolaDeCursos.Dominio.Modulos.ModuloAluno;
using EscolaDeCursos.Dominio.Modulos.ModuloMatricula;
using EscolaDeCursos.Dominio.Modulos.ModuloTurma;
using EscolaDeCursos.Aplicacao.Compartilhado;

namespace EscolaDeCursos.Aplicacao.Modulos.ModuloMatricula;

public class ServicoMatricula : ServicoBase<Matricula>
{
    private readonly IRepositorioMatricula repositorioMatricula;
    private readonly IRepositorioTurma repositorioTurma;
    private readonly IRepositorioAluno repositorioAluno;

    public ServicoMatricula(
        IRepositorioMatricula repositorioMatricula,
        IRepositorioTurma repositorioTurma,
        IRepositorioAluno repositorioAluno
    )
    {
        this.repositorioMatricula = repositorioMatricula;
        this.repositorioTurma = repositorioTurma;
        this.repositorioAluno = repositorioAluno;
    }

    public Result Cadastrar(CadastrarMatriculaDto dto)
    {
        Turma? turma = repositorioTurma.SelecionarPorId(dto.TurmaId);

        if (turma == null)
            return Falha(nameof(dto.TurmaId), "Turma não encontrada.");

        Aluno? aluno = repositorioAluno.SelecionarPorId(dto.AlunoId);

        if (aluno == null)
            return Falha(nameof(dto.AlunoId), "Aluno não encontrado.");

        bool jaMatriculado = repositorioMatricula
            .SelecionarPorTurmaId(dto.TurmaId)
            .Any(m => m.Aluno.Id == dto.AlunoId);

        if (jaMatriculado)
            return Falha(nameof(dto.AlunoId), "Este aluno já está matriculado nesta turma.");

        if (turma.Matriculas.Count >= turma.NumeroMaximoAlunos)
            return Falha(string.Empty, "A turma atingiu o número máximo de alunos.");

        Matricula novaMatricula = new Matricula(aluno, turma);

        repositorioMatricula.Cadastrar(novaMatricula);

        return Result.Ok();
    }

    public Result Editar(EditarMatriculaDto dto)
    {
        Matricula? matricula = repositorioMatricula.SelecionarPorId(dto.Id);

        if (matricula == null)
            return Falha(string.Empty, "Matrícula não encontrada.");

        Turma? turma = repositorioTurma.SelecionarPorId(dto.TurmaId);

        if (turma == null)
            return Falha(nameof(dto.TurmaId), "Turma não encontrada.");

        Aluno? aluno = repositorioAluno.SelecionarPorId(dto.AlunoId);

        if (aluno == null)
            return Falha(nameof(dto.AlunoId), "Aluno não encontrado.");

        List<Matricula> matriculasDaTurma = repositorioMatricula.SelecionarPorTurmaId(dto.TurmaId);

        bool jaMatriculado = matriculasDaTurma.Any(m => m.Aluno.Id == dto.AlunoId && m.Id != dto.Id);

        if (jaMatriculado)
            return Falha(nameof(dto.AlunoId), "Este aluno já está matriculado nesta turma.");

        Matricula matriculaAtualizada = new Matricula(aluno, turma);

        repositorioMatricula.Editar(dto.Id, matriculaAtualizada);

        return Result.Ok();
    }

    public Result Remover(Guid id)
    {
        Matricula? matricula = repositorioMatricula.SelecionarPorId(id);

        if (matricula == null)
            return Falha(string.Empty, "Matrícula não encontrada.");

        repositorioMatricula.Excluir(id);

        return Result.Ok();
    }

    public List<ListarMatriculaDto> SelecionarTodos()
    {
        return repositorioMatricula
            .SelecionarTodos()
            .Select(m => new ListarMatriculaDto(m.Id, m.Aluno.Nome, m.Aluno.NumeroMatricula, m.Turma.Nome))
            .ToList();
    }

    public Result<DetalhesMatriculaDto> SelecionarPorId(Guid id)
    {
        Matricula? matricula = repositorioMatricula.SelecionarPorId(id);

        if (matricula == null)
            return Result.Fail("Matrícula não encontrada.");

        return Result.Ok(new DetalhesMatriculaDto(
            matricula.Id,
            matricula.Aluno.Id,
            matricula.Aluno.Nome,
            matricula.Aluno.NumeroMatricula,
            matricula.Turma.Id,
            matricula.Turma.Nome
        ));
    }

    public List<ListarMatriculaDto> SelecionarPorTurmaId(Guid turmaId)
    {
        return repositorioMatricula
            .SelecionarPorTurmaId(turmaId)
            .Select(m => new ListarMatriculaDto(m.Id, m.Aluno.Nome, m.Aluno.NumeroMatricula, m.Turma.Nome))
            .ToList();
    }

    public List<OpcaoAlunoMatriculaDto> SelecionarAlunosNaoMatriculados(Guid turmaId)
    {
        List<Guid> alunosMatriculadosIds = repositorioMatricula
            .SelecionarPorTurmaId(turmaId)
            .Select(m => m.Aluno.Id)
            .ToList();

        return repositorioAluno
            .SelecionarTodos()
            .Where(a => !alunosMatriculadosIds.Contains(a.Id))
            .Select(a => new OpcaoAlunoMatriculaDto(a.Id, a.Nome, a.NumeroMatricula))
            .ToList();
    }
}
