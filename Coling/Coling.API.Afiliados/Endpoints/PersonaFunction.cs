using Coling.API.Afiliados.Contratos;
using Coling.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Net;

namespace Coling.API.Afiliados.endpoints
{
    public class PersonaFunction
    {
        private readonly ILogger<PersonaFunction> _logger;
        private readonly IPersonaLogic personaLogic;

        public PersonaFunction(ILogger<PersonaFunction> logger,IPersonaLogic personaLogic)
        {
            _logger = logger;
            this.personaLogic = personaLogic;
        }

        [Function("ListarPersonas")]
        [OpenApiOperation("listar", "Persona", Description = "Sirve para insertar")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(List<Persona>),
            Description = "Mostrara una lista")]
        public async Task<HttpResponseData> ListarPersonas([HttpTrigger(AuthorizationLevel.Function, "get", Route = "listarpersonas")] HttpRequestData req)
        {
           
            var listarPersona = personaLogic.ListarPersonaTodos();
            var respuesta = req.CreateResponse(HttpStatusCode.OK);
            await respuesta.WriteAsJsonAsync(listarPersona.Result);
            return respuesta;
        }
        [Function("InsertarPersona")]
        [OpenApiOperation("insertar", "Persona", Description = "Sirve para insertar")]
        [OpenApiRequestBody("application/json", typeof(Persona),
           Description = "Modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Persona),
            Description = "Mostrara El dato insertado")]
        public async Task<HttpResponseData> InsertarPersona([HttpTrigger(AuthorizationLevel.Function, "post", Route = "insertarpersona")] HttpRequestData req)
        {
            try
            {
                var per = await req.ReadFromJsonAsync<Persona>()?? throw new Exception("Debe ingresar una persona con sus datos");

                bool seGuardo = await personaLogic.InsertarPersona(per);
               
                if (seGuardo)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    return respuesta;
                }
                return req.CreateResponse(HttpStatusCode.BadRequest);
               
            } catch (Exception ex)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(ex.Message);
                return error;
            }
        }
        [Function("EliminarPersona")]
        [OpenApiOperation("Eliminar", "Persona", Description = "Sirve para eliminar")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(void),
            Description = " eliminara")]
        [OpenApiParameter(name: "idPersona", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "El idPersona")]
        public async Task<HttpResponseData> EliminarPersona([HttpTrigger(AuthorizationLevel.Function, "get", Route = "eliminarpersona")] HttpRequestData req)
        {
            try
            {
                string perID = req.Query["idPersona"];
                bool persona = await personaLogic.EliminarPersona(int.Parse(perID));
                if (persona)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    return respuesta;
                }
                return req.CreateResponse(HttpStatusCode.BadRequest);
            } catch (Exception ex)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(ex.Message);
                return error;
            }
            
        }
        [Function("EditarPersona")]
        [OpenApiOperation("editar", "Persona", Description = "Sirve para obtener")]
        [OpenApiRequestBody("application/json", typeof(Persona),
           Description = "Modelo")]
        [OpenApiParameter(name: "idPersona", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "El idPersona")]
        public async Task<HttpResponseData> EditarPersona([HttpTrigger(AuthorizationLevel.Function, "post", Route = "editarpersona")] HttpRequestData req)
        {
            try
            {
                string perID = req.Query["idPersona"];
                var per = await req.ReadFromJsonAsync<Persona>() ?? throw new Exception("Debe ingresar una persona con sus datos");

                bool seGuardo = await personaLogic.ModificarPersona(per,int.Parse(perID));

                //await respuesta.WriteAsJsonAsync(listarPersona.Result);
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
        [Function("ObtenerPersona")]
        [OpenApiOperation("obtener", "Persona", Description = "Sirve para obtener")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Persona),
            Description = "Mostrara un objeto")]
        [OpenApiParameter(name: "idPersona", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "El idPersona")]
        public async Task<HttpResponseData> ObtenerPersona([HttpTrigger(AuthorizationLevel.Function, "get", Route = "obtenerPersona")] HttpRequestData req)
        {
            string perID = req.Query["idPersona"];
            var persona = personaLogic.ObtenerPersona(int.Parse(perID));
            var respuesta = req.CreateResponse(HttpStatusCode.OK);
            await respuesta.WriteAsJsonAsync(persona.Result);
            return respuesta;
        }
    }
}
