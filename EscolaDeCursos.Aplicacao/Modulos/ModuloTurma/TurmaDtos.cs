namespace EscolaDeCursos.Aplicacao.Modulos.ModuloTurma;

public record ListarTurmaDto(
    Guid Id,
    string Nome,
    string NomeCurso,
    string NomeInstrutor,
    int NumeroMaximoAlunos,
    DateOnly DataInicio,
    DateOnly DataTermino,
    int QuantidadeMatriculas
);

public record CadastrarTurmaDto(
    string Nome,
    Guid CursoId,
    Guid InstrutorId,
    int NumeroMaximoAlunos,
    DateOnly DataInicio,
    DateOnly DataTermino
);

public record EditarTurmaDto(
    Guid Id,
    string Nome,
    Guid CursoId,
    Guid InstrutorId,
    int NumeroMaximoAlunos,
    DateOnly DataInicio,
    DateOnly DataTermino
);

public record DetalhesTurmaDto(
    Guid Id,
    string Nome,
    Guid CursoId,
    string NomeCurso,
    Guid InstrutorId,
    string NomeInstrutor,
    int NumeroMaximoAlunos,
    DateOnly DataInicio,
    DateOnly DataTermino
);
