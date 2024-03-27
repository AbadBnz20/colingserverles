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
    public class TipoEstudioFunction
    {
        private readonly ILogger<TipoEstudioFunction> _logger;
        private readonly ITipoEstudioRepositorio repos;

        public TipoEstudioFunction(ILogger<TipoEstudioFunction> logger,ITipoEstudioRepositorio repos)
        {
            _logger = logger;
            this.repos = repos;
        }

        [Function("InsertarTipoEstudio")]
        [OpenApiOperation("insertar", "TipoEstudio", Description = "Sirve para insertar")]
        [OpenApiRequestBody("application/json", typeof(TipoEstudio),
           Description = "Modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(TipoEstudio),
            Description = "Mostrara El dato insertado")]
        public async Task<HttpResponseData> InsertarTipoEstudio([HttpTrigger(AuthorizationLevel.Function, "post", Route = "insertartipoestudio")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            try
            {
                var registro = await req.ReadFromJsonAsync<TipoEstudio>() ?? throw new Exception("Debe ingresar tipo estudio  con todos sus datos");
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
        [Function("ListarTipoEstudio")]
        [OpenApiOperation("listar", "TipoEstudio", Description = "Sirve para insertar")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(List<TipoEstudio>),
            Description = "Mostrara una lista")]
        public async Task<HttpResponseData> ListarTipoEstudio([HttpTrigger(AuthorizationLevel.Function, "get", Route = "listartipoestudio")] HttpRequestData req)
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
        [Function("EliminarTipoEstudio")]
        [OpenApiOperation("Eliminar", "TipoEstudio", Description = "Sirve para eliminar")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(void),
            Description = " eliminara")]
        [OpenApiParameter(name: "partitionkey", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "El partitionkey")]
        [OpenApiParameter(name: "rowkey", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "El rowkey")]
        public async Task<HttpResponseData> EliminarTipoEstudio([HttpTrigger(AuthorizationLevel.Function, "get", Route = "eliminartipoestudio")] HttpRequestData req)
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
        [Function("ObtenerTipoEstudio")]
        [OpenApiOperation("obtener", "TipoEstudio", Description = "Sirve para obtener")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(TipoEstudio),
            Description = "Mostrara un objeto")]
        [OpenApiParameter(name: "idTipoEstuido", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "El idEstudio")]
        public async Task<HttpResponseData> ObtenerTipoEstudio([HttpTrigger(AuthorizationLevel.Function, "get", Route = "obtenertipoestudio")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            string ID = req.Query["idTipoEstuido"];
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
        [Function("EditarTipoEstudio")]
        [OpenApiOperation("editar", "TipoEstudio", Description = "Sirve para obtener")]
        [OpenApiRequestBody("application/json", typeof(TipoEstudio),
           Description = "Modelo")]
        public async Task<HttpResponseData> EditarTipoEstudio([HttpTrigger(AuthorizationLevel.Function, "post", Route = "editartipoestudio")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            try
            {
                var registro = await req.ReadFromJsonAsync<TipoEstudio>() ?? throw new Exception("Debe ingresar tipo de estudio con todos sus datos");

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
