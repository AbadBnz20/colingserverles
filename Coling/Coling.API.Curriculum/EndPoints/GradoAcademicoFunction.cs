using Coling.API.Curriculum.Contratos.Repositorio;
using Coling.API.Curriculum.Modelo;
using Coling.Shared.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Coling.API.Curriculum.EndPoints
{
    public class GradoAcademicoFunction
    {
        private readonly ILogger<GradoAcademicoFunction> _logger;
        private readonly IGradoAcademicoRepositorio repos;

        public GradoAcademicoFunction(ILogger<GradoAcademicoFunction> logger, IGradoAcademicoRepositorio repos)
        {
            _logger = logger;
            this.repos = repos;
        }

        [Function("InsertarGradoAcademico")]
        public async Task<HttpResponseData> InsertarGradoAcademico([HttpTrigger(AuthorizationLevel.Function, "post", Route = "insertargradoacademico")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            try
            {
                var registro = await req.ReadFromJsonAsync<GradoAcademico>() ?? throw new Exception("Debe ingresar un Grado Academico con todos sus datos");
                registro.RowKey = Guid.NewGuid().ToString();
                registro.Timestamp = DateTime.UtcNow;
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
        [Function("ListarGradoAcademico")]
        public async Task<HttpResponseData> ListarGradoAcademico([HttpTrigger(AuthorizationLevel.Function, "get", Route = "listargradoacademico")] HttpRequestData req)
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
        [Function("EliminarGradoAcademico")]
        public async Task<HttpResponseData> EliminarGradoAcademico([HttpTrigger(AuthorizationLevel.Function, "get", Route = "eliminargradoacademico")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            string partitionkey = req.Query["partitionkey"];
            string rowkey = req.Query["rowkey"];

            try
            {
                bool validate = await repos.Delete(partitionkey, rowkey);
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
        [Function("ObtenerGradoAcademico")]
        public async Task<HttpResponseData> ObtenerGradoAcademico([HttpTrigger(AuthorizationLevel.Function, "get", Route = "obtenergradoacademico")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            string ID = req.Query["idInstitucion"];
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

        [Function("EditarGradoAcademico")]
        public async Task<HttpResponseData> EditarGradoAcademico([HttpTrigger(AuthorizationLevel.Function, "post", Route = "editargradoacademico")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            try
            {
                var registro = await req.ReadFromJsonAsync<GradoAcademico>() ?? throw new Exception("Debe ingresar un grado academico con todos sus datos");

                bool sw = await repos.Update(registro);
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
