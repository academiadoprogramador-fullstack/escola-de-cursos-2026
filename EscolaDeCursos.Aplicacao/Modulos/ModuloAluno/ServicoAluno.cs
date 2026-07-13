using FluentResults;
using EscolaDeCursos.Dominio.Modulos.ModuloAluno;
using EscolaDeCursos.Dominio.Modulos.ModuloMatricula;
using EscolaDeCursos.Aplicacao.Compartilhado;

namespace EscolaDeCursos.Aplicacao.Modulos.ModuloAluno;

public class ServicoAluno : ServicoBase<Aluno>
{
    private readonly IRepositorioAluno repositorioAluno;
    private readonly IRepositorioMatricula repositorioMatricula;

    public ServicoAluno(
        IRepositorioAluno repositorioAluno,
        IRepositorioMatricula repositorioMatricula
    )
    {
        this.repositorioAluno = repositorioAluno;
        this.repositorioMatricula = repositorioMatricula;
    }

    public Result Cadastrar(CadastrarAlunoDto dto)
    {
        Aluno novoAluno = new Aluno(dto.Nome, dto.Telefone, dto.Email);

        Result resultadoValidacao = ValidarEntidade(novoAluno);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioAluno.Cadastrar(novoAluno);

        return Result.Ok();
    }

    public Result Editar(EditarAlunoDto dto)
    {
        Aluno alunoAtualizado = new Aluno(dto.Nome, dto.Telefone, dto.Email);

        Result resultadoValidacao = ValidarEntidade(alunoAtualizado);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        bool conseguiuEditar = repositorioAluno.Editar(dto.Id, alunoAtualizado);

        if (!conseguiuEditar)
            return Falha(string.Empty, "Aluno não encontrado.");

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        Aluno? aluno = repositorioAluno.SelecionarPorId(id);

        if (aluno == null)
            return Falha(string.Empty, "Aluno não encontrado.");

        if (PossuiMatriculasVinculadas(id))
            return Falha(string.Empty, "Não é possível excluir este aluno, pois ele possui matrículas vinculadas.");

        repositorioAluno.Excluir(id);

        return Result.Ok();
    }

    public List<ListarAlunoDto> SelecionarTodos()
    {
        return repositorioAluno
            .SelecionarTodos()
            .Select(a => new ListarAlunoDto(a.Id, a.Nome, a.Email, a.Telefone, a.NumeroMatricula))
            .ToList();
    }

    public Result<DetalhesAlunoDto> SelecionarPorId(Guid id)
    {
        Aluno? aluno = repositorioAluno.SelecionarPorId(id);

        if (aluno == null)
            return Result.Fail("Aluno não encontrado.");

        return Result.Ok(new DetalhesAlunoDto(
            aluno.Id,
            aluno.Nome,
            aluno.Email,
            aluno.Telefone,
            aluno.NumeroMatricula
        ));
    }

    private bool PossuiMatriculasVinculadas(Guid alunoId)
    {
        return repositorioMatricula
            .SelecionarTodos()
            .Any(m => m.Aluno.Id == alunoId);
    }
}
