namespace EscolaDeCursos.WebApp.Modulos.ModuloTurma;

public record ListarMatriculaViewModel(
    Guid Id,
    string NomeAluno,
    string NumeroMatriculaAluno
);

public record AdicionarMatriculaViewModel(
    Guid TurmaId,

    [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "O campo \"Aluno\" deve ser preenchido.")]
    Guid? AlunoId
);

public record RemoverMatriculaViewModel(
    Guid Id,
    Guid TurmaId
);
