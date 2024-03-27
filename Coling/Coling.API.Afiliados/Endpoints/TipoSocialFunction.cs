using Coling.API.Afiliados.Contratos;
using Coling.API.Afiliados.Implementacion;
using Coling.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Net;

namespace Coling.API.Afiliados.Endpoints
{
    public class TipoSocialFunction
    {
        private readonly ILogger<TipoSocialFunction> _logger;
        private readonly ITipoSocialLogic tipoSocialLogic;
       public TipoSocialFunction(ILogger<TipoSocialFunction> logger,ITipoSocialLogic tipoSocialLogic)
        {
            _logger = logger;
            this.tipoSocialLogic = tipoSocialLogic; 
        }

        [Function("ListarTipoSocial")]
        [OpenApiOperation("listar", "TipoSocial", Description = "Sirve para insertar")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(List<TipoSocial>),
            Description = "Mostrara una lista")]
        public async Task<HttpResponseData> ListarTipoSocial([HttpTrigger(AuthorizationLevel.Function, "get", Route = "listartiposocial")] HttpRequestData req)
        {
            var listardireccion = tipoSocialLogic.ListarTipoSocial();
            var respuesta = req.CreateResponse(HttpStatusCode.OK);
            await respuesta.WriteAsJsonAsync(listardireccion.Result);
            return respuesta;
        }
        [Function("InsertarTipoSocial")]
        [OpenApiOperation("insertar", "TipoSocial", Description = "Sirve para insertar")]
        [OpenApiRequestBody("application/json", typeof(TipoSocial),
           Description = "Modelo")]
        public async Task<HttpResponseData> InserterTipoSocial([HttpTrigger(AuthorizationLevel.Function, "post", Route = "insertartiposocial")] HttpRequestData req)
        {
            try
            {
                var tiposocial = await req.ReadFromJsonAsync<TipoSocial>() ?? throw new Exception("Debe ingresar una direccion");
                bool guardado = await tipoSocialLogic.InsertarTipoSocial(tiposocial);
                if (guardado)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    return respuesta;
                }
                return req.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(ex.Message);
                return error;
            }
        }
        [Function("EliminarTipoSocial")]
        [OpenApiOperation("Eliminar", "TipoSocial", Description = "Sirve para eliminar")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(void),
            Description = " eliminara")]
        [OpenApiParameter(name: "idTiposocial", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "El idTiposocial")]
        public async Task<HttpResponseData> EliminarTipoSocial([HttpTrigger(AuthorizationLevel.Function, "get", Route = "eliminartiposocial")] HttpRequestData req)
        {
            try
            {
                string ID = req.Query["idTiposocial"];
                bool persona = await tipoSocialLogic.EliminarTipoSocial(int.Parse(ID));
                if (persona)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    return respuesta;
                }
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(ex.Message);
                return error;
            }

        }
        [Function("EditarTipoSocial")]
        [OpenApiOperation("editar", "TipoSocial", Description = "Sirve para obtener")]
        [OpenApiRequestBody("application/json", typeof(TipoSocial),
           Description = "Modelo")]
        [OpenApiParameter(name: "idTiposocial", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "El idTiposocial")]
        public async Task<HttpResponseData> EditarTipoSocial([HttpTrigger(AuthorizationLevel.Function, "post", Route = "editartiposocial")] HttpRequestData req)
        {
            try
            {
                string ID = req.Query["idTiposocial"];
                var tiposocial = await req.ReadFromJsonAsync<TipoSocial>() ?? throw new Exception("Debe ingresar una direccion con sus datos");

                bool seGuardo = await tipoSocialLogic.ModificarTipoSocial(tiposocial, int.Parse(ID));
                if (seGuardo)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    return respuesta;
                }
                return req.CreateResponse(HttpStatusCode.BadRequest);

            }
            catch (Exception ex)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(ex.Message);
                return error;
            }
        }
        [Function("ObtenerTipoSocial")]
        [OpenApiOperation("obtener", "TipoSocial", Description = "Sirve para obtener")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(TipoSocial),
            Description = "Mostrara un objeto")]
        [OpenApiParameter(name: "idTiposocial", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "El idTiposocial")]
        public async Task<HttpResponseData> ObtenerTipoSocial([HttpTrigger(AuthorizationLevel.Function, "get", Route = "obtenertiposocial")] HttpRequestData req)
        {
            string ID = req.Query["idTiposocial"];
            var direccion = tipoSocialLogic.ObtenerTipoSocial(int.Parse(ID));
            var respuesta = req.CreateResponse(HttpStatusCode.OK);
            await respuesta.WriteAsJsonAsync(direccion.Result);
            return respuesta;
        }
    }
}
