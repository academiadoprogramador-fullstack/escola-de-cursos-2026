using EscolaDeCursos.Aplicacao.Modulos.ModuloCategoria;
using EscolaDeCursos.Aplicacao.Modulos.ModuloCurso;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EscolaDeCursos.Aplicacao;

public static class InjecaoDependencia
{
    public static void AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddScoped<ServicoCategoria>();
        services.AddScoped<ServicoCurso>();
        services.AddScoped<ServicoAula>();
    }
}
