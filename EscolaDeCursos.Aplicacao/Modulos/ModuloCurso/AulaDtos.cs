namespace EscolaDeCursos.Aplicacao.Modulos.ModuloCurso;

public record ListarAulaDto(
    Guid Id,
    string Nome,
    int DuracaoEmMinutos,
    int Ordem
);

public record AdicionarAulaDto(
    string Nome,
    int DuracaoEmMinutos,
    int Ordem,
    Guid CursoId
);

public record EditarAulaDto(
    Guid Id,
    string Nome,
    int DuracaoEmMinutos,
    int Ordem,
    Guid CursoId
);

public record DetalhesAulaDto(
    Guid Id,
    string Nome,
    int DuracaoEmMinutos,
    int Ordem,
    Guid CursoId
);
