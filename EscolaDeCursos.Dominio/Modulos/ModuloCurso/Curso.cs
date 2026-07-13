using EscolaDeCursos.Dominio.Compartilhado;
using EscolaDeCursos.Dominio.Modulos.ModuloCategoria;

namespace EscolaDeCursos.Dominio.Modulos.ModuloCurso;

public class Curso : EntidadeBase<Curso>
{
    public string Nome { get; set; } = string.Empty;
    public NivelCurso Nivel { get; set; }
    public int CargaHoraria { get; set; }
    public Categoria Categoria { get; set; } = null!;
    public List<Aula> Aulas { get; set; } = [];

    public Curso()
    {
    }

    public Curso(
        string nome,
        NivelCurso nivel,
        int cargaHoraria,
        Categoria categoria
    ) : this()
    {
        Nome = nome;
        Nivel = nivel;
        CargaHoraria = cargaHoraria;
        Categoria = categoria;
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (string.IsNullOrWhiteSpace(Nome) || Nome.Length < 2 || Nome.Length > 100)
            erros.Add("O campo \"Nome\" deve conter entre 2 e 100 caracteres.");

        if (!Enum.IsDefined(Nivel))
            erros.Add("O campo \"Nível\" deve ser preenchido.");

        if (CargaHoraria < 2 || CargaHoraria > 100)
            erros.Add("O campo \"Carga Horária\" deve estar entre 2 e 100 horas.");

        return erros;
    }

    public override void Atualizar(Curso entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
        Nivel = entidadeAtualizada.Nivel;
        CargaHoraria = entidadeAtualizada.CargaHoraria;
        Categoria = entidadeAtualizada.Categoria;
    }
}
