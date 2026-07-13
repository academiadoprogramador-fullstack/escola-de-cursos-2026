namespace EscolaDeCursos.Aplicacao.Modulos.ModuloMatricula;

public record ListarMatriculaDto(
    Guid Id,
    string NomeAluno,
    string NumeroMatriculaAluno
);

public record AdicionarMatriculaDto(
    Guid TurmaId,
    Guid AlunoId
);

public record RemoverMatriculaDto(
    Guid Id,
    Guid TurmaId
);
