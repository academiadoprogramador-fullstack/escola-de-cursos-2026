using EscolaDeCursos.Dominio.Compartilhado;
using EscolaDeCursos.Dominio.Modulos.ModuloAluno;
using EscolaDeCursos.Dominio.Modulos.ModuloTurma;

namespace EscolaDeCursos.Dominio.Modulos.ModuloMatricula;

public class Matricula : EntidadeBase<Matricula>
{
    public Aluno Aluno { get; set; } = null!;
    public Turma Turma { get; set; } = null!;

    public Matricula()
    {
    }

    public Matricula(Aluno aluno, Turma turma) : this()
    {
        Aluno = aluno;
        Turma = turma;
    }

    public override List<string> Validar()
    {
        return [];
    }

    public override void Atualizar(Matricula entidadeAtualizada)
    {
        Aluno = entidadeAtualizada.Aluno;
        Turma = entidadeAtualizada.Turma;
    }
}
