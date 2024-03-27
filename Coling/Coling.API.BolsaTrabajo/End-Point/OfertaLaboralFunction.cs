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
    public class OfertaLaboralFunction
    {
        private readonly ILogger<OfertaLaboralFunction> _logger;
        private readonly IOfertaLaboralRepositorio repos;
        public OfertaLaboralFunction(ILogger<OfertaLaboralFunction> logger, IOfertaLaboralRepositorio repos)
        {
            _logger = logger;
            this.repos = repos;
        }

        [Function("InsertarOfertaLaboral")]
        [OpenApiOperation("insertar", "OfertaLaboral", Description = "Sirve para insertar OfertaLaboral")]
        [OpenApiRequestBody("application/json", typeof(OfertaLaboral),
           Description = "OfertaLaboral modelo")]
        public async Task<HttpResponseData> InsertarOfertaLaboral([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "insertarofertalaboral")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            try
            {
                var registro = await req.ReadFromJsonAsync<OfertaLaboral>() ?? throw new Exception("Debe ingresar una Oferta Laboral con todos sus datos");
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
        [Function("ListarOfertaLaboral")]
        [OpenApiOperation("listar", "OfertaLaboral", Description = "Sirve para insertar OfertaLaboral")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(List<OfertaLaboral>),
            Description = "Mostrara una lista de OfertaLaboral")]
        public async Task<HttpResponseData> ListarOfertaLaboral([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "listarofertalaboral")] HttpRequestData req)
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
        [Function("ObtenerOfertaLaboral")]
        [OpenApiOperation("obtener", "OfertaLaboral", Description = "Sirve para obtener una OfertaLaboral")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(OfertaLaboral),
            Description = "Mostrara una lista de OfertaLaboral")]
        [OpenApiParameter(name: "idOfertaLaboral", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "El idOfertaLaboral del OfertaLaboral")]
        public async Task<HttpResponseData> ObtenerOfertaLaboral([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "obtenerofertalaboral")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            string ID = req.Query["idOfertaLaboral"];
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
        [Function("EliminarOfertaLaboral")]
        [OpenApiOperation("Eliminar", "OfertaLaboral", Description = "Sirve para eliminar una OfertaLaboral")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(void),
            Description = " eliminar Producto")]
        [OpenApiParameter(name: "idOfertaLaboral", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "El idOfertaLaboral del OfertaLaboral")]
        public async Task<HttpResponseData> EliminarOfertaLaboral([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "eliminarofertalaboral")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            string ID = req.Query["idOfertaLaboral"];


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
        [Function("EditarOfertaLaboral")]
        [OpenApiOperation("Editar", "OfertaLaboral", Description = "Sirve para editar una OfertaLaboral")]
        [OpenApiRequestBody("application/json", typeof(OfertaLaboral),
           Description = "OfertaLaboral modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(void),
            Description = " editar Producto")]
        [OpenApiParameter(name: "idOfertaLaboral", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "El idOfertaLaboral del Solicitud")]
        public async Task<HttpResponseData> EditarOfertaLaboral([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "editarofertalaboral")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            try
            {
                var registro = await req.ReadFromJsonAsync<OfertaLaboral>() ?? throw new Exception("Debe ingresar una oferta laboral con todos sus datos");
                string ID = req.Query["idOfertaLaboral"];
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
