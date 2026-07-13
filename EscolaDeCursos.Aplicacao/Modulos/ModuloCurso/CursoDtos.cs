using EscolaDeCursos.Dominio.Modulos.ModuloCurso;

namespace EscolaDeCursos.Aplicacao.Modulos.ModuloCurso;

public record ListarCursoDto(
    Guid Id,
    string Nome,
    NivelCurso Nivel,
    int CargaHoraria,
    string NomeCategoria
);

public record CadastrarCursoDto(
    string Nome,
    NivelCurso Nivel,
    int CargaHoraria,
    Guid CategoriaId
);

public record EditarCursoDto(
    Guid Id,
    string Nome,
    NivelCurso Nivel,
    int CargaHoraria,
    Guid CategoriaId
);

public record DetalhesCursoDto(
    Guid Id,
    string Nome,
    NivelCurso Nivel,
    int CargaHoraria,
    Guid CategoriaId,
    string NomeCategoria
);
