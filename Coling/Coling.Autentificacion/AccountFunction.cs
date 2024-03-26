using Coling.Autentificacion.Modelo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.ComponentModel.DataAnnotations;

namespace Coling.Autentificacion
{
    public class AccountFunction
    {
        private readonly ILogger<AccountFunction> _logger;

        public AccountFunction(ILogger<AccountFunction> logger)
        {
            _logger = logger;
        }

        [Function("Login")]
   
        public async IActionResult Login([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
          HttpResponseData respuesta= null;
            var login = await req.ReadFromJsonAsync<Credenciales>() ?? throw new ValidationException("Sus credenciales deben ser completas");

        }
    }
}
