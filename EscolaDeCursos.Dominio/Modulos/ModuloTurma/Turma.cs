using EscolaDeCursos.Dominio.Compartilhado;
using EscolaDeCursos.Dominio.Modulos.ModuloCurso;
using EscolaDeCursos.Dominio.Modulos.ModuloInstrutor;
using EscolaDeCursos.Dominio.Modulos.ModuloMatricula;

namespace EscolaDeCursos.Dominio.Modulos.ModuloTurma;

public class Turma : EntidadeBase<Turma>
{
    public string Nome { get; set; } = string.Empty;
    public Curso Curso { get; set; } = null!;
    public Instrutor Instrutor { get; set; } = null!;
    public int NumeroMaximoAlunos { get; set; }
    public DateOnly DataInicio { get; set; }
    public DateOnly DataTermino { get; set; }
    public List<Matricula> Matriculas { get; set; } = [];

    public Turma()
    {
    }

    public Turma(
        string nome,
        Curso curso,
        Instrutor instrutor,
        int numeroMaximoAlunos,
        DateOnly dataInicio,
        DateOnly dataTermino
    ) : this()
    {
        Nome = nome;
        Curso = curso;
        Instrutor = instrutor;
        NumeroMaximoAlunos = numeroMaximoAlunos;
        DataInicio = dataInicio;
        DataTermino = dataTermino;
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (string.IsNullOrWhiteSpace(Nome) || Nome.Length < 2 || Nome.Length > 100)
            erros.Add("O campo \"Nome\" deve conter entre 2 e 100 caracteres.");

        if (NumeroMaximoAlunos < 1 || NumeroMaximoAlunos > 100)
            erros.Add("O campo \"Número Máximo de Alunos\" deve estar entre 1 e 100.");

        if (DataInicio == default)
            erros.Add("O campo \"Data de Início\" deve ser preenchido.");

        if (DataTermino == default)
            erros.Add("O campo \"Data de Término\" deve ser preenchido.");

        if (DataTermino <= DataInicio)
            erros.Add("A data de término deve ser posterior à data de início.");

        return erros;
    }

    public override void Atualizar(Turma entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
        Curso = entidadeAtualizada.Curso;
        Instrutor = entidadeAtualizada.Instrutor;
        NumeroMaximoAlunos = entidadeAtualizada.NumeroMaximoAlunos;
        DataInicio = entidadeAtualizada.DataInicio;
        DataTermino = entidadeAtualizada.DataTermino;
    }
}
