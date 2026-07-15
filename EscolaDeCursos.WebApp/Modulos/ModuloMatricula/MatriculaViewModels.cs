using System.ComponentModel.DataAnnotations;

namespace EscolaDeCursos.WebApp.Modulos.ModuloMatricula;

public record ListarMatriculaViewModel(
    Guid Id,
    string NomeAluno,
    string NumeroMatriculaAluno,
    string NomeTurma
);

public record CadastrarMatriculaViewModel(
    [Required(ErrorMessage = "O campo \"Turma\" deve ser preenchido.")]
    Guid? TurmaId,

    [Required(ErrorMessage = "O campo \"Aluno\" deve ser preenchido.")]
    Guid? AlunoId
);

public record EditarMatriculaViewModel(
    Guid Id,

    [Required(ErrorMessage = "O campo \"Turma\" deve ser preenchido.")]
    Guid? TurmaId,

    [Required(ErrorMessage = "O campo \"Aluno\" deve ser preenchido.")]
    Guid? AlunoId
);

public record ExcluirMatriculaViewModel(
    Guid Id,
    string NomeAluno,
    string NumeroMatriculaAluno,
    string NomeTurma
);
