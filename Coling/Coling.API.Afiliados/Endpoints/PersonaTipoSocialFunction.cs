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
    public class PersonaTipoSocialFunction
    {
        private readonly ILogger<PersonaTipoSocialFunction> _logger;
        private readonly IPersonaTipoSocial personaTipoSocial;
        public PersonaTipoSocialFunction(ILogger<PersonaTipoSocialFunction> logger, IPersonaTipoSocial personaTipoSocial)
        {
            _logger = logger;
            this.personaTipoSocial = personaTipoSocial;
        }

        [Function("ListarPersonaTipoSocial")]
        public async Task<HttpResponseData> ListarPersonaTipoSocial([HttpTrigger(AuthorizationLevel.Function, "get", Route = "listarpersonatiposocial")] HttpRequestData req)
        {
            var listardireccion = personaTipoSocial.ListarPersonaTipoSocial();
            var respuesta = req.CreateResponse(HttpStatusCode.OK);
            await respuesta.WriteAsJsonAsync(listardireccion.Result);
            return respuesta;
        }
        [Function("InsertarPersonaTipoSocial")]
        public async Task<HttpResponseData> InsertarPersonaTipoSocial([HttpTrigger(AuthorizationLevel.Function, "post", Route = "insertarPersonatiposocial")] HttpRequestData req)
        {
            try
            {
                var personatiposocial = await req.ReadFromJsonAsync<PersonaTipoSocial>() ?? throw new Exception("Debe ingresar una direccion");
                bool guardado = await personaTipoSocial.InsertarPersonaTipoSocial(personatiposocial);
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
        [Function("EliminarPersonaTipoSocial")]
        public async Task<HttpResponseData> EliminarPersonaTipoSocial([HttpTrigger(AuthorizationLevel.Function, "get", Route = "eliminarpersonatiposocial")] HttpRequestData req)
        {
            try
            {
                string ID = req.Query["idPersonaTiposocial"];
                bool persona = await personaTipoSocial.EliminarPersonaTipoSocial(int.Parse(ID));
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
        [Function("EditarPersonaTipoSocial")]
        public async Task<HttpResponseData> EditarPersonaTipoSocial([HttpTrigger(AuthorizationLevel.Function, "post", Route = "editarpersonatiposocial")] HttpRequestData req)
        {
            try
            {
                string ID = req.Query["idPersonaTiposocial"];
                var personatiposocial = await req.ReadFromJsonAsync<PersonaTipoSocial>() ?? throw new Exception("Debe ingresar una direccion con sus datos");

                bool seGuardo = await personaTipoSocial.ModificarPersonaTipoSocial(personatiposocial, int.Parse(ID));
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
        [Function("ObtenerPersonaTipoSocial")]
        public async Task<HttpResponseData> ObtenerPersonaTipoSocial([HttpTrigger(AuthorizationLevel.Function, "get", Route = "obtenerpersonatiposocial")] HttpRequestData req)
        {
            string ID = req.Query["idPersonaTiposocial"];
            var direccion = personaTipoSocial.ObtenerPersonaTipoSocial(int.Parse(ID));
            var respuesta = req.CreateResponse(HttpStatusCode.OK);
            await respuesta.WriteAsJsonAsync(direccion.Result);
            return respuesta;
        }
    }
}