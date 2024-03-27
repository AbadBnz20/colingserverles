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
    public class TelefonoFunction
    {
        private readonly ILogger<TelefonoFunction> _logger;
        private readonly ITelefonoLogic telefonoLogic;
        public TelefonoFunction(ILogger<TelefonoFunction> logger,ITelefonoLogic telefonoLogic)
        {
            _logger = logger;
            this.telefonoLogic = telefonoLogic;
          
        }

        [Function("ListarTelefonos")]
        [OpenApiOperation("listar", "Telefono", Description = "Sirve para insertar")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(List<Telefono>),
            Description = "Mostrara una lista")]
        public async Task<HttpResponseData> ListarTelefonos([HttpTrigger(AuthorizationLevel.Function, "get",  Route = "listartelefonos")] HttpRequestData req)
        {
            var listartelefono = telefonoLogic.ListarTelefonos();
            var respuesta = req.CreateResponse(HttpStatusCode.OK);
            await respuesta.WriteAsJsonAsync(listartelefono.Result);
            return respuesta;
        }
        [Function("InsertarTelefono")]
        [OpenApiOperation("insertar", "Telefono", Description = "Sirve para insertar")]
        [OpenApiRequestBody("application/json", typeof(Telefono),
           Description = "Modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Telefono),
            Description = "Mostrara El dato insertado")]
        public async Task<HttpResponseData> InsertarTelefono([HttpTrigger(AuthorizationLevel.Function, "post", Route = "insertartelefono")] HttpRequestData req)
        {
            try
            {
                var telefono = await req.ReadFromJsonAsync<Telefono>() ?? throw new Exception("Debe ingresar una telefono");
                bool guardado = await telefonoLogic.InserterTelefono(telefono);
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
        [Function("EliminarTelefono")]
        [OpenApiOperation("Eliminar", "Telefono", Description = "Sirve para eliminar")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(void),
            Description = " eliminara")]
        [OpenApiParameter(name: "idTelefono", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "El idTelefono")]
        public async Task<HttpResponseData> EliminarTelefono([HttpTrigger(AuthorizationLevel.Function, "get", Route = "eliminartelefono")] HttpRequestData req)
        {
            try
            {
                string ID = req.Query["idTelefono"];
                bool telefono = await telefonoLogic.EliminarTelefono(int.Parse(ID));
                if (telefono)
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
        [Function("EditarTelefono")]
        [OpenApiOperation("editar", "Telefono", Description = "Sirve para obtener")]
        [OpenApiRequestBody("application/json", typeof(Telefono),
           Description = "Modelo")]
        [OpenApiParameter(name: "idTelefono", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "El idTelefono")]
        public async Task<HttpResponseData> EditarTelefono([HttpTrigger(AuthorizationLevel.Function, "post", Route = "editarTelefono")] HttpRequestData req)
        {
            try
            {
                string ID = req.Query["idTelefono"];
                var telefono = await req.ReadFromJsonAsync<Telefono>() ?? throw new Exception("Debe ingresar una Telefono con sus datos");

                bool seGuardo = await telefonoLogic.ModificarTelefono(telefono, int.Parse(ID));
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
        [Function("ObtenerTelefono")]
        [OpenApiOperation("obtener", "Telefono", Description = "Sirve para obtener")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Telefono),
            Description = "Mostrara un objeto")]
        [OpenApiParameter(name: "idDireccion", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "El idDireccion")]
        public async Task<HttpResponseData> ObtenerTelefono([HttpTrigger(AuthorizationLevel.Function, "get", Route = "obtenertelefono")] HttpRequestData req)
        {
            string ID = req.Query["idDireccion"];
            var direccion = telefonoLogic.ObtenerTelefono(int.Parse(ID));
            var respuesta = req.CreateResponse(HttpStatusCode.OK);
            await respuesta.WriteAsJsonAsync(direccion.Result);
            return respuesta;
        }
    }
}
