using System.ComponentModel.DataAnnotations;
using EscolaDeCursos.Dominio.Modulos.ModuloCurso;

namespace EscolaDeCursos.WebApp.Modulos.ModuloCurso;

public record ListarCursoViewModel(
    Guid Id,
    string Nome,
    NivelCurso Nivel,
    int CargaHoraria,
    string NomeCategoria
);

public record CadastrarCursoViewModel(
    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Nome\" deve conter entre 2 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Nível\" deve ser preenchido.")]
    NivelCurso? Nivel,

    [Required(ErrorMessage = "O campo \"Carga Horária\" deve ser preenchido.")]
    [Range(2, 100, ErrorMessage = "O campo \"Carga Horária\" deve estar entre 2 e 100 horas.")]
    int? CargaHoraria,

    [Required(ErrorMessage = "O campo \"Categoria\" deve ser preenchido.")]
    Guid? CategoriaId
);

public record EditarCursoViewModel(
    Guid Id,

    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Nome\" deve conter entre 2 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Nível\" deve ser preenchido.")]
    NivelCurso? Nivel,

    [Required(ErrorMessage = "O campo \"Carga Horária\" deve ser preenchido.")]
    [Range(2, 100, ErrorMessage = "O campo \"Carga Horária\" deve estar entre 2 e 100 horas.")]
    int? CargaHoraria,

    [Required(ErrorMessage = "O campo \"Categoria\" deve ser preenchido.")]
    Guid? CategoriaId
);

public record ExcluirCursoViewModel(
    Guid Id,
    string Nome,
    string Nivel,
    int CargaHoraria,
    string NomeCategoria
);
