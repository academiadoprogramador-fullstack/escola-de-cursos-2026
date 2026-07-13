namespace EscolaDeCursos.Aplicacao.Modulos.ModuloInstrutor;

public record ListarInstrutorDto(
    Guid Id,
    string Nome,
    string Email,
    string Telefone,
    string Graduacao
);

public record CadastrarInstrutorDto(
    string Nome,
    string Email,
    string Telefone,
    string Graduacao
);

public record EditarInstrutorDto(
    Guid Id,
    string Nome,
    string Email,
    string Telefone,
    string Graduacao
);

public record DetalhesInstrutorDto(
    Guid Id,
    string Nome,
    string Email,
    string Telefone,
    string Graduacao
);
