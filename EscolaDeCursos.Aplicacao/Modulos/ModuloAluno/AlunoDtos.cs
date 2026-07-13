namespace EscolaDeCursos.Aplicacao.Modulos.ModuloAluno;

public record ListarAlunoDto(
    Guid Id,
    string Nome,
    string Email,
    string Telefone,
    string NumeroMatricula
);

public record CadastrarAlunoDto(
    string Nome,
    string Email,
    string Telefone
);

public record EditarAlunoDto(
    Guid Id,
    string Nome,
    string Email,
    string Telefone
);

public record DetalhesAlunoDto(
    Guid Id,
    string Nome,
    string Email,
    string Telefone,
    string NumeroMatricula
);
