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
        public async Task<HttpResponseData> ListarTelefonos([HttpTrigger(AuthorizationLevel.Function, "get",  Route = "listartelefonos")] HttpRequestData req)
        {
            var listartelefono = telefonoLogic.ListarTelefonos();
            var respuesta = req.CreateResponse(HttpStatusCode.OK);
            await respuesta.WriteAsJsonAsync(listartelefono.Result);
            return respuesta;
        }
        [Function("InsertarTelefono")]
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
