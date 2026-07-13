using EscolaDeCursos.Aplicacao;
using EscolaDeCursos.Infra;
using EscolaDeCursos.Infra.Compartilhado.Orm;
using EscolaDeCursos.WebApp.Compartilhado;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Configuração do container de injeção de dependência
builder.Services.AddInfraRepositories(builder.Configuration, builder.Logging);
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddPresentationConfig(builder.Configuration);

// Configura health checks do banco de dados
builder.Services.AddHealthChecks()
    .AddDbContextCheck<EscolaDeCursosDbContext>(
        name: "database_check",
        failureStatus: HealthStatus.Unhealthy,
        tags: ["ready"]
    );

var app = builder.Build();

// Middlewares de roteamento
app.UseRouting();
app.MapDefaultControllerRoute();

// Execução do Servidor
app.Run();
