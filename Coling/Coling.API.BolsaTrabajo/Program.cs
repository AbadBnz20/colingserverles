using Coling.API.BolsaTrabajo.Contratos;
using Coling.API.BolsaTrabajo.Implementacion;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddScoped<ISolicitudRepositorio, SolicitudRepositorio>();

    })
    .Build();

host.Run();
