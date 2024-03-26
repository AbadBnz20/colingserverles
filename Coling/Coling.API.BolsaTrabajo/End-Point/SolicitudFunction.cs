using Coling.API.BolsaTrabajo.Contratos;
using Coling.API.BolsaTrabajo.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
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
