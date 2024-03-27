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
    public class AfiliadoFunction
    {
        private readonly ILogger<AfiliadoFunction> _logger;
        private readonly IAfiliadosLogic afiliadosLogic;
       public AfiliadoFunction(ILogger<AfiliadoFunction> logger, IAfiliadosLogic afiliadosLogic)
        {
            _logger = logger;
            this.afiliadosLogic = afiliadosLogic;
        }

        [Function("ListarAfiliados")]
        [OpenApiOperation("listar", "Afiliado", Description = "Sirve para insertar")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(List<Afiliado>),
            Description = "Mostrara una lista")]
        public async Task<HttpResponseData> ListarAfiliados([HttpTrigger(AuthorizationLevel.Function, "get", Route = "listarafiliado")] HttpRequestData req)
        {
            var listardireccion = afiliadosLogic.ListarAfiliados();
            var respuesta = req.CreateResponse(HttpStatusCode.OK);
            await respuesta.WriteAsJsonAsync(listardireccion.Result);
            return respuesta;
        }
        [Function("InsertarAfiliados")]
        [OpenApiOperation("insertar", "Afiliado", Description = "Sirve para insertar")]
        [OpenApiRequestBody("application/json", typeof(Afiliado),
           Description = "Modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Afiliado),
            Description = "Mostrara El dato insertado")]
        public async Task<HttpResponseData> InsertarAfiliados([HttpTrigger(AuthorizationLevel.Function, "post", Route = "insertarafiliado")] HttpRequestData req)
        {
            try
            {
                var afiliado = await req.ReadFromJsonAsync<Afiliado>() ?? throw new Exception("Debe ingresar una direccion");
                bool guardado = await afiliadosLogic.InsertarAfiliado(afiliado);
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
        [Function("EliminarAfiliado")]
        [OpenApiOperation("Eliminar", "Afiliado", Description = "Sirve para eliminar")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(void),
            Description = " eliminara")]
        [OpenApiParameter(name: "idAfiliado", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "El idAfiliado")]
        public async Task<HttpResponseData> EliminarAfiliado([HttpTrigger(AuthorizationLevel.Function, "get", Route = "eliminarafiliado")] HttpRequestData req)
        {
            try
            {
                string ID = req.Query["idAfiliado"];
                bool persona = await afiliadosLogic.EliminarAfiliado(int.Parse(ID));
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
        [Function("EditarAfiliado")]
        [OpenApiOperation("editar", "Afiliado", Description = "Sirve para obtener")]
        [OpenApiRequestBody("application/json", typeof(Afiliado),
           Description = "Modelo")]
        [OpenApiParameter(name: "idAfiliado", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "El idAfiliado")]
        public async Task<HttpResponseData> EditarAfiliado([HttpTrigger(AuthorizationLevel.Function, "post", Route = "editarafiliado")] HttpRequestData req)
        {
            try
            {
                string ID = req.Query["idAfiliado"];
                var afiliado = await req.ReadFromJsonAsync<Afiliado>() ?? throw new Exception("Debe ingresar una direccion con sus datos");

                bool seGuardo = await afiliadosLogic.ModificarAfiliado(afiliado, int.Parse(ID));
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
        [Function("ObtenerAfiliado")]
        [OpenApiOperation("obtener", "Afiliado", Description = "Sirve para obtener")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Afiliado),
            Description = "Mostrara un objeto")]
        [OpenApiParameter(name: "idAfiliado", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "El idAfiliado")]
        public async Task<HttpResponseData> ObtenerAfiliado([HttpTrigger(AuthorizationLevel.Function, "get", Route = "obtenerafiliado")] HttpRequestData req)
        {
            string ID = req.Query["idAfiliado"];
            var direccion = afiliadosLogic.ObtenerAfiliado(int.Parse(ID));
            var respuesta = req.CreateResponse(HttpStatusCode.OK);
            await respuesta.WriteAsJsonAsync(direccion.Result);
            return respuesta;
        }
    }
}
