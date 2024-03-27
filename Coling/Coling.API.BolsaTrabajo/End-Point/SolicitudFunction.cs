using Coling.API.BolsaTrabajo.Contratos;
using Coling.API.BolsaTrabajo.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Net;

namespace Coling.API.BolsaTrabajo.End_Point
{
    public class SolicitudFunction
    {
        private readonly ILogger<SolicitudFunction> _logger;
        private readonly ISolicitudRepositorio repos;
        public SolicitudFunction(ILogger<SolicitudFunction> logger,ISolicitudRepositorio repos)
        {
            _logger = logger;
            this.repos = repos;
        }

        [Function("InsertarSolicitud")]
        [OpenApiOperation("insertar", "Solicitud", Description = "Sirve para insertar Solicitud")]
        [OpenApiRequestBody("application/json", typeof(Solicitud),
           Description = "Solicitud modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Solicitud),
            Description = "Mostrara  la Solicitud insertado")]
        public async Task<HttpResponseData> InsertarSolicitud([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "insertarsolicitud")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            try
            {
                var registro = await req.ReadFromJsonAsync<Solicitud>() ?? throw new Exception("Debe ingresar una Solicitud con todos sus datos");
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
        [Function("ListarSolicitud")]
        [OpenApiOperation("listar", "Solicitud", Description = "Sirve para insertar Solicitud")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(List<Solicitud>),
            Description = "Mostrara una lista de solicitudes")]
        public async Task<HttpResponseData> ListarSolicitud([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "listarsolicitud")] HttpRequestData req)
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
        [Function("ObtenerSolicitud")]
        [OpenApiOperation("obtener", "Solicitud", Description = "Sirve para obtener una Solicitud")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Solicitud),
            Description = "Mostrara una lista de Producto")]
        [OpenApiParameter(name: "idSolicitud", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "El idSolicitud del Solicitud")]
        public async Task<HttpResponseData> ObtenerSolicitud([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "obtenersolicitud")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            string ID = req.Query["idSolicitud"];
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
        [Function("EliminarSolicitud")]
        [OpenApiOperation("Eliminar", "Solicitud", Description = "Sirve para eliminar una Solicitud")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(void),
            Description = " eliminar Producto")]
        [OpenApiParameter(name: "idSolicitud", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "El idSolicitud del Solicitud")]
        public async Task<HttpResponseData> EliminarSolicitud([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "eliminarsolicitud")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            string ID = req.Query["idSolicitud"];
           

            try
            {
                bool validate = await repos.Delete(ID);
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
        [Function("EditarSolicitud")]
        [OpenApiOperation("Editar", "Solicitud", Description = "Sirve para editar una Solicitud")]
        [OpenApiRequestBody("application/json", typeof(Solicitud),
           Description = "Producto modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(void),
            Description = " editar Producto")]
        [OpenApiParameter(name: "idSolicitud", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "El idSolicitud del Solicitud")]
        public async Task<HttpResponseData> EditarSolicitud([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "editarsolicitud")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            try
            {
                var registro = await req.ReadFromJsonAsync<Solicitud>() ?? throw new Exception("Debe ingresar una Solicitud con todos sus datos");
                string ID = req.Query["idSolicitud"];
                bool sw = await repos.Update(registro, ID);
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
