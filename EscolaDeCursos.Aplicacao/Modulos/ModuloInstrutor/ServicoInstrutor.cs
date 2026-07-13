using FluentResults;
using EscolaDeCursos.Dominio.Modulos.ModuloInstrutor;
using EscolaDeCursos.Aplicacao.Compartilhado;

namespace EscolaDeCursos.Aplicacao.Modulos.ModuloInstrutor;

public class ServicoInstrutor : ServicoBase<Instrutor>
{
    private readonly IRepositorioInstrutor repositorioInstrutor;

    public ServicoInstrutor(
        IRepositorioInstrutor repositorioInstrutor
    )
    {
        this.repositorioInstrutor = repositorioInstrutor;
    }

    public Result Cadastrar(CadastrarInstrutorDto dto)
    {
        if (ExisteInstrutorComMesmoNome(dto.Nome))
            return Falha(nameof(dto.Nome), "Já existe um instrutor com este nome.");

        Instrutor novoInstrutor = new Instrutor(
            dto.Nome,
            dto.Telefone,
            dto.Email,
            dto.Graduacao
        );

        Result resultadoValidacao = ValidarEntidade(novoInstrutor);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioInstrutor.Cadastrar(novoInstrutor);

        return Result.Ok();
    }

    public Result Editar(EditarInstrutorDto dto)
    {
        if (ExisteInstrutorComMesmoNome(dto.Nome, dto.Id))
            return Falha(nameof(dto.Nome), "Já existe um instrutor com este nome.");

        Instrutor instrutorAtualizado = new Instrutor(
            dto.Nome,
            dto.Telefone,
            dto.Email,
            dto.Graduacao
        );

        Result resultadoValidacao = ValidarEntidade(instrutorAtualizado);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        bool conseguiuEditar = repositorioInstrutor.Editar(dto.Id, instrutorAtualizado);

        if (!conseguiuEditar)
            return Falha(string.Empty, "Instrutor não encontrado.");

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        Instrutor? instrutor = repositorioInstrutor.SelecionarPorId(id);

        if (instrutor == null)
            return Falha(string.Empty, "Instrutor não encontrado.");

        repositorioInstrutor.Excluir(id);

        return Result.Ok();
    }

    public List<ListarInstrutorDto> SelecionarTodos()
    {
        return repositorioInstrutor
            .SelecionarTodos()
            .Select(i => new ListarInstrutorDto(i.Id, i.Nome, i.Email, i.Telefone, i.Graduacao))
            .ToList();
    }

    public Result<DetalhesInstrutorDto> SelecionarPorId(Guid id)
    {
        Instrutor? instrutor = repositorioInstrutor.SelecionarPorId(id);

        if (instrutor == null)
            return Result.Fail("Instrutor não encontrado.");

        return Result.Ok(new DetalhesInstrutorDto(
            instrutor.Id,
            instrutor.Nome,
            instrutor.Email,
            instrutor.Telefone,
            instrutor.Graduacao
        ));
    }

    private bool ExisteInstrutorComMesmoNome(string nome, Guid? idIgnorado = null)
    {
        string nomeNormalizado = NormalizarNome(nome);

        return repositorioInstrutor
            .SelecionarTodos()
            .Any(i =>
                i.Id != idIgnorado &&
                NormalizarNome(i.Nome) == nomeNormalizado
            );
    }

    private static string NormalizarNome(string nome)
    {
        return nome.Trim().ToLowerInvariant();
    }
}
