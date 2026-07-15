namespace EscolaDeCursos.Aplicacao.Modulos.ModuloMatricula;

public record ListarMatriculaDto(
    Guid Id,
    string NomeAluno,
    string NumeroMatriculaAluno,
    string NomeTurma
);

public record CadastrarMatriculaDto(
    Guid TurmaId,
    Guid AlunoId
);

public record EditarMatriculaDto(
    Guid Id,
    Guid AlunoId,
    Guid TurmaId
);

public record DetalhesMatriculaDto(
    Guid Id,
    Guid AlunoId,
    string NomeAluno,
    string NumeroMatriculaAluno,
    Guid TurmaId,
    string NomeTurma
);
