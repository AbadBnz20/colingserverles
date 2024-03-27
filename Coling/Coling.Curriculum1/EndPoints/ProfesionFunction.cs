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
    public class ProfesionFunction
    {
        private readonly ILogger<ProfesionFunction> _logger;

        private readonly IProfesionRepositorio repos;

        public ProfesionFunction(ILogger<ProfesionFunction> logger, IProfesionRepositorio repos)
        {
            _logger = logger;
            this.repos = repos;
        }

        [Function("InsertarProfesion")]
        [OpenApiOperation("insertar", "Profesion", Description = "Sirve para insertar")]
        [OpenApiRequestBody("application/json", typeof(Profesion),
           Description = "Modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Profesion),
            Description = "Mostrara El dato insertado")]
        public async Task<HttpResponseData> InsertarProfesion([HttpTrigger(AuthorizationLevel.Function, "post", Route = "insertarprofesion")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            try
            {
                var registro = await req.ReadFromJsonAsync<Profesion>() ?? throw new Exception("Debe ingresar una Profesion con todos sus datos");
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
        [Function("ListarProfesion")]
        [OpenApiOperation("listar", "Profesion", Description = "Sirve para insertar")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(List<Profesion>),
            Description = "Mostrara una lista")]
        public async Task<HttpResponseData> ListarProfesion([HttpTrigger(AuthorizationLevel.Function, "get", Route = "listarprofesion")] HttpRequestData req)
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
        [Function("EliminarProfesion")]
        [OpenApiOperation("Eliminar", "Profesion", Description = "Sirve para eliminar")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(void),
            Description = " eliminara")]
        [OpenApiParameter(name: "partitionkey", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "El partitionkey")]
        [OpenApiParameter(name: "rowkey", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "El rowkey")]
        public async Task<HttpResponseData> EliminarProfesion([HttpTrigger(AuthorizationLevel.Function, "get", Route = "eliminarprofesion")] HttpRequestData req)
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
        [Function("ObtenerProfesion")]
        [OpenApiOperation("obtener", "Profesion", Description = "Sirve para obtener")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Profesion),
            Description = "Mostrara un objeto")]
        [OpenApiParameter(name: "idProfesion", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "El idProfesion")]
        public async Task<HttpResponseData> ObtenerProfesion([HttpTrigger(AuthorizationLevel.Function, "get", Route = "obtenerprofesion")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            string ID = req.Query["idProfesion"];
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

        [Function("EditarProfesion")]
        [OpenApiOperation("editar", "Profesion", Description = "Sirve para obtener")]
        [OpenApiRequestBody("application/json", typeof(Profesion),
           Description = "Modelo")]
        public async Task<HttpResponseData> EditarProfesion([HttpTrigger(AuthorizationLevel.Function, "post", Route = "editarprofesion")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            try
            {
                var registro = await req.ReadFromJsonAsync<Profesion>() ?? throw new Exception("Debe ingresar una profesion  con todos sus datos");

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
