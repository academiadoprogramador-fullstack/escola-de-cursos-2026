using EscolaDeCursos.Dominio.Compartilhado;

namespace EscolaDeCursos.Dominio.Modulos.ModuloCurso;

public class Aula : EntidadeBase<Aula>
{
    public string Nome { get; set; } = string.Empty;
    public int DuracaoEmMinutos { get; set; }
    public int Ordem { get; set; }
    public Curso Curso { get; set; } = null!;

    public Aula()
    {
    }

    public Aula(
        string nome,
        int duracaoEmMinutos,
        int ordem,
        Curso curso
    ) : this()
    {
        Nome = nome;
        DuracaoEmMinutos = duracaoEmMinutos;
        Ordem = ordem;
        Curso = curso;
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (string.IsNullOrWhiteSpace(Nome) || Nome.Length < 2 || Nome.Length > 100)
            erros.Add("O campo \"Nome\" deve conter entre 2 e 100 caracteres.");

        if (DuracaoEmMinutos < 0)
            erros.Add("O campo \"Duração\" não pode ser negativo.");

        return erros;
    }

    public override void Atualizar(Aula entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
        DuracaoEmMinutos = entidadeAtualizada.DuracaoEmMinutos;
        Ordem = entidadeAtualizada.Ordem;
    }
}
