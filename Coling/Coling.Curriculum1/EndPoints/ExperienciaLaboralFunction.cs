using Coling.API.Curriculum.Contratos.Repositorio;
using Coling.API.Curriculum.Modelo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Net;

namespace Coling.API.Curriculum.EndPoints
{
    public class ExperienciaLaboralFunction
    {
        private readonly ILogger<ExperienciaLaboralFunction> _logger;
        private readonly IExperienciaLaboralRepositorio repos;
        public ExperienciaLaboralFunction(ILogger<ExperienciaLaboralFunction> logger, IExperienciaLaboralRepositorio repos)
        {
            _logger = logger;
            this.repos = repos; 
        }

        [Function("InsertarExperienciaLaboral")]
        [OpenApiOperation("insertar", "ExperienciaLaboral", Description = "Sirve para insertar")]
        [OpenApiRequestBody("application/json", typeof(ExperienciaLaboral),
           Description = "Modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(ExperienciaLaboral),
            Description = "Mostrara El dato insertado")]
        public async Task<HttpResponseData> InsertarExperienciaLaboral([HttpTrigger(AuthorizationLevel.Function, "post", Route = "insertarexperiencialaboral")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            try
            {
                var registro = await req.ReadFromJsonAsync<ExperienciaLaboral>() ?? throw new Exception("Debe ingresar experiencia con todos sus datos");
                registro.RowKey = Guid.NewGuid().ToString();
                registro.Timestamp = DateTime.UtcNow;
                bool sw = await repos.Create(registro);
                if (sw)
                {
                    respuesta = req.CreateResponse(HttpStatusCode.OK);
                    return respuesta;
                }
                else
                {
                    respuesta = req.CreateResponse(HttpStatusCode.BadRequest);
                    return respuesta;
                }
            }
            catch (Exception)
            {
                respuesta = req.CreateResponse(HttpStatusCode.InternalServerError);
                return respuesta;
            }
        }
        [Function("ListarExperienciaLaboral")]
        [OpenApiOperation("listar", "ExperienciaLaboral", Description = "Sirve para insertar")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(List<ExperienciaLaboral>),
            Description = "Mostrara una lista")]
        public async Task<HttpResponseData> ListarExperienciaLaboral([HttpTrigger(AuthorizationLevel.Function, "get", Route = "listarexperiencialaboral")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            try
            {
                var lista = repos.GetAll();
                respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(lista.Result);
                return respuesta;
            }
            catch (Exception)
            {
                respuesta = req.CreateResponse(HttpStatusCode.InternalServerError);
                return respuesta;
            }
        }
        [Function("EliminarExperiencialaboral")]
        [OpenApiOperation("Eliminar", "ExperienciaLaboral", Description = "Sirve para eliminar")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(void),
            Description = " eliminara")]
        [OpenApiParameter(name: "partitionkey", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "El partitionkey")]
        [OpenApiParameter(name: "rowkey", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "El rowkey")]
        public async Task<HttpResponseData> EliminarExperiencialaboral([HttpTrigger(AuthorizationLevel.Function, "get", Route = "eliminarexperiencialaboral")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            string partitionkey = req.Query["partitionkey"];
            string rowkey = req.Query["rowkey"];

            try
            {
                bool validate = await repos.Delete(partitionkey, rowkey);
                if (validate)
                {
                    respuesta = req.CreateResponse(HttpStatusCode.OK);
                    return respuesta;
                }
                else
                {
                    respuesta = req.CreateResponse(HttpStatusCode.BadRequest);
                    return respuesta;
                }
            }
            catch (Exception)
            {
                respuesta = req.CreateResponse(HttpStatusCode.InternalServerError);
                return respuesta;
            }
        }
        [Function("ObtenerExperiencialaboral")]
        [OpenApiOperation("obtener", "ExperienciaLaboral", Description = "Sirve para obtener")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(ExperienciaLaboral),
            Description = "Mostrara un objeto")]
        [OpenApiParameter(name: "idexperiencia", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "El idexperiencia")]
        public async Task<HttpResponseData> ObtenerExperiencialaboral([HttpTrigger(AuthorizationLevel.Function, "get", Route = "obtenerexperiencialaboral")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            string ID = req.Query["idexperiencia"];
            try
            {
                var lista = repos.Get(ID);
                respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(lista.Result);
                return respuesta;
            }
            catch (Exception)
            {
                respuesta = req.CreateResponse(HttpStatusCode.InternalServerError);
                return respuesta;
            }
        }
        [Function("EditarExperienciaLaboral")]
        [OpenApiOperation("editar", "ExperienciaLaboral", Description = "Sirve para obtener")]
        [OpenApiRequestBody("application/json", typeof(ExperienciaLaboral),
           Description = "Modelo")]
        public async Task<HttpResponseData> EditarExperienciaLaboral([HttpTrigger(AuthorizationLevel.Function, "post", Route = "editarexperiencialaboral")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            try
            {
                var registro = await req.ReadFromJsonAsync<ExperienciaLaboral>() ?? throw new Exception("Debe ingresar experiencia con todos sus datos");

                bool sw = await repos.Update(registro);
                if (sw)
                {
                    respuesta = req.CreateResponse(HttpStatusCode.OK);
                    return respuesta;
                }
                else
                {
                    respuesta = req.CreateResponse(HttpStatusCode.BadRequest);
                    return respuesta;
                }
            }
            catch (Exception)
            {
                respuesta = req.CreateResponse(HttpStatusCode.InternalServerError);
                return respuesta;
            }
        }
    }
}
