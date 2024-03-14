using Coling.API.Afiliados.Contratos;
using Coling.API.Afiliados.Implementacion;
using Coling.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Coling.API.Afiliados.Endpoints
{
    public class DireccionFunction
    {
        private readonly ILogger<DireccionFunction> _logger;
        private readonly IDireccionLogic direccionLogic;
        public DireccionFunction(ILogger<DireccionFunction> logger,IDireccionLogic direccionLogic)
        {
            _logger = logger;
            this.direccionLogic = direccionLogic;
        }

        [Function("ListarDirecciones")]
        public async Task<HttpResponseData> ListarDirecciones([HttpTrigger(AuthorizationLevel.Function, "get", Route = "listardirecciones")] HttpRequestData req)
        {
            var listardireccion =   direccionLogic.ListarDireccion();
            var respuesta = req.CreateResponse(HttpStatusCode.OK);
            await respuesta.WriteAsJsonAsync(listardireccion.Result);
            return respuesta;
        }
        [Function("InsertarDireccion")]
        public async Task<HttpResponseData> InserterDireccion([HttpTrigger(AuthorizationLevel.Function,"post", Route = "insertardireccion")] HttpRequestData req)
        {
            try
            {
                var direccion = await req.ReadFromJsonAsync<Direccion>() ?? throw new Exception("Debe ingresar una direccion");
                bool guardado = await direccionLogic.InserterDireccion(direccion);
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
        [Function("EliminarDireccion")]
        public async Task<HttpResponseData> EliminarDireccion([HttpTrigger(AuthorizationLevel.Function, "get", Route = "eliminardireccion")] HttpRequestData req)
        {
            try
            {
                string direccionID = req.Query["idDireccion"];
                bool persona = await direccionLogic.EliminarDireccion(int.Parse(direccionID));
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
        [Function("EditarDireccion")]
        public async Task<HttpResponseData> EditarDireccion([HttpTrigger(AuthorizationLevel.Function, "post", Route = "editardireccion")] HttpRequestData req)
        {
            try
            {
                string ID = req.Query["idDireccion"];
                var direccion = await req.ReadFromJsonAsync<Direccion>() ?? throw new Exception("Debe ingresar una direccion con sus datos");

                bool seGuardo = await direccionLogic.ModificarDireccion(direccion, int.Parse(ID));
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
        [Function("ObtenerDireccion")]
        public async Task<HttpResponseData> ObtenerDireccion([HttpTrigger(AuthorizationLevel.Function, "get", Route = "obtenerdireccion")] HttpRequestData req)
        {
            string ID = req.Query["idDireccion"];
            var direccion = direccionLogic.ObtenerDireccion(int.Parse(ID));
            var respuesta = req.CreateResponse(HttpStatusCode.OK);
            await respuesta.WriteAsJsonAsync(direccion.Result);
            return respuesta;
        }
    }
}
