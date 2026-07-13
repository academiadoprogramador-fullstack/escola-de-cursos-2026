using System.ComponentModel.DataAnnotations;

namespace EscolaDeCursos.WebApp.Modulos.ModuloTurma;

public record ListarTurmaViewModel(
    Guid Id,
    string Nome,
    string NomeCurso,
    string NomeInstrutor,
    int NumeroMaximoAlunos,
    DateOnly DataInicio,
    DateOnly DataTermino,
    int QuantidadeMatriculas
);

public record CadastrarTurmaViewModel(
    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Nome\" deve conter entre 2 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Curso\" deve ser preenchido.")]
    Guid? CursoId,

    [Required(ErrorMessage = "O campo \"Instrutor\" deve ser preenchido.")]
    Guid? InstrutorId,

    [Required(ErrorMessage = "O campo \"Número Máximo de Alunos\" deve ser preenchido.")]
    [Range(1, 100, ErrorMessage = "O campo \"Número Máximo de Alunos\" deve estar entre 1 e 100.")]
    int? NumeroMaximoAlunos,

    [Required(ErrorMessage = "O campo \"Data de Início\" deve ser preenchido.")]
    DateOnly? DataInicio,

    [Required(ErrorMessage = "O campo \"Data de Término\" deve ser preenchido.")]
    DateOnly? DataTermino
);

public record EditarTurmaViewModel(
    Guid Id,

    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Nome\" deve conter entre 2 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Curso\" deve ser preenchido.")]
    Guid? CursoId,

    [Required(ErrorMessage = "O campo \"Instrutor\" deve ser preenchido.")]
    Guid? InstrutorId,

    [Required(ErrorMessage = "O campo \"Número Máximo de Alunos\" deve ser preenchido.")]
    [Range(1, 100, ErrorMessage = "O campo \"Número Máximo de Alunos\" deve estar entre 1 e 100.")]
    int? NumeroMaximoAlunos,

    [Required(ErrorMessage = "O campo \"Data de Início\" deve ser preenchido.")]
    DateOnly? DataInicio,

    [Required(ErrorMessage = "O campo \"Data de Término\" deve ser preenchido.")]
    DateOnly? DataTermino
);

public record ExcluirTurmaViewModel(
    Guid Id,
    string Nome,
    string NomeCurso,
    string NomeInstrutor,
    int NumeroMaximoAlunos,
    DateOnly DataInicio,
    DateOnly DataTermino
);
