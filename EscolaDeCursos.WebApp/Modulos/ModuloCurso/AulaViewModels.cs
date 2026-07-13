using System.ComponentModel.DataAnnotations;

namespace EscolaDeCursos.WebApp.Modulos.ModuloCurso;

public record ListarAulaViewModel(
    Guid Id,
    string Nome,
    int DuracaoEmMinutos,
    int Ordem
);

public record AdicionarAulaViewModel(
    Guid CursoId,

    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Nome\" deve conter entre 2 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Duração\" deve ser preenchido.")]
    [Range(0, int.MaxValue, ErrorMessage = "O campo \"Duração\" não pode ser negativo.")]
    int? DuracaoEmMinutos,

    [Required(ErrorMessage = "O campo \"Ordem\" deve ser preenchido.")]
    int? Ordem
);

public record EditarAulaViewModel(
    Guid Id,
    Guid CursoId,

    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Nome\" deve conter entre 2 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Duração\" deve ser preenchido.")]
    [Range(0, int.MaxValue, ErrorMessage = "O campo \"Duração\" não pode ser negativo.")]
    int? DuracaoEmMinutos,

    [Required(ErrorMessage = "O campo \"Ordem\" deve ser preenchido.")]
    int? Ordem
);

public record RemoverAulaViewModel(
    Guid Id,
    Guid CursoId
);
