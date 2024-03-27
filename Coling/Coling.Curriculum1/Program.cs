using Coling.API.Curriculum.Contratos.Repositorio;
using Coling.API.Curriculum.Implementacion.Repositorio;
using Coling.Utilitarios.Attributes;
using Coling.Utilitarios.Middlewares;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

//var ensamblado = Assembly.GetExecutingAssembly();
//var tipoAtributo = typeof(ColingAuthorizeAttribure);

var host = new HostBuilder()
   
    .ConfigureServices(services =>
    {
        //services.AddAutoM
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddScoped<IInstitucionRepositorio, InstitucionRepositorio>();
        services.AddScoped<IGradoAcademicoRepositorio, GradoAcademicoRepositorio>();
        services.AddScoped<IProfesionRepositorio, ProfesionRepositorio>();
        services.AddScoped<ITipoEstudioRepositorio, TipoEstudioRepositorio>();
        services.AddScoped<IEstudiosRepositorio, EstudiosRepositorio>();
        services.AddScoped<IExperienciaLaboralRepositorio, ExperienciaLaboralRepositorio>();
        //services.AddSingleton<JwtMiddleware>();
    })
    .ConfigureFunctionsWebApplication()
    //.ConfigureFunctionsWebApplication(x =>
    //{
    //    x.UseMiddleware<JwtMiddleware>();
    //})
    .Build();
//var lista = ListarFuncionesAttribures(ensamblado, tipoAtributo);
host.Run();

//List<string> ListarFuncionesAttribures(Assembly ensambaldox, Type tipox)
//{
//    List<string> lista= new List<string>();
//    Type tipoFuncion = typeof(FunctionAttribute);
//    var metodos = ObtenerMetodosConAtributos(ensambaldox, tipox);

//    return lista;
//}

//MethodInfo[] ObtenerMetodosConAtributos(Type type, Type tipoatributo)
//{
//    return type.GetMethods().Where(m => m.GetCustomAttributes(tipoAtributo, inherit: true).Any()).ToArray();
//}