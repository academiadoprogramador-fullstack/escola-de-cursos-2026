using System.Text.RegularExpressions;
using EscolaDeCursos.Dominio.Compartilhado;

namespace EscolaDeCursos.Dominio.Modulos.ModuloInstrutor;

public class Instrutor : EntidadeBase<Instrutor>
{
    public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Graduacao { get; set; } = string.Empty;

    public Instrutor()
    {
    }

    public Instrutor(
        string nome,
        string telefone,
        string email,
        string graduacao
    ) : this()
    {
        Nome = nome;
        Telefone = telefone;
        Email = email;
        Graduacao = graduacao;
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (string.IsNullOrWhiteSpace(Nome) || Nome.Length < 2 || Nome.Length > 100)
            erros.Add("O campo \"Nome\" deve conter entre 2 e 100 caracteres.");

        if (!Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            erros.Add("O campo \"E-mail\" deve conter um endereço de e-mail válido.");

        if (!Regex.IsMatch(Telefone, @"^\(\d{2}\) \d{4,5}-\d{4}$"))
            erros.Add("O campo \"Telefone\" deve estar no formato (XX) XXXX-XXXX ou (XX) XXXXX-XXXX.");

        if (string.IsNullOrWhiteSpace(Graduacao) || Graduacao.Length < 2 || Graduacao.Length > 100)
            erros.Add("O campo \"Graduação\" deve conter entre 2 e 100 caracteres.");

        return erros;
    }

    public override void Atualizar(Instrutor entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
        Telefone = entidadeAtualizada.Telefone;
        Email = entidadeAtualizada.Email;
        Graduacao = entidadeAtualizada.Graduacao;
    }
}
