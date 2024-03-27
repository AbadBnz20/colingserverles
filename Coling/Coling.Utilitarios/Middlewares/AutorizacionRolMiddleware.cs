﻿using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Utilitarios.Middlewares
{
    public class AutorizacionRolMiddleware : IFunctionsWorkerMiddleware
    {
        Dictionary<string, string> funcionesRolesAutorizados = new Dictionary<string, string>();

        public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
        {
            var solicitud = await context.GetHttpRequestDataAsync();
            if (solicitud is null)
            {
                return;
            }
            var nombreFuncion= solicitud.FunctionContext.FunctionDefinition.Name;
            var rolesClaim = (string?) context.Items["rolesclaim"];
            if (!EstaAutorizado(nombreFuncion, rolesClaim))
            {
                throw new Exception("Error: NO esta autorizado");
            }

            await next(context);
        }

        private bool EstaAutorizado(string nombreFuncion,string rolesClaim)
        {
            bool sw = false;


            var rolesFuncion = funcionesRolesAutorizados.TryGetValue(nombreFuncion, out string? roles);

            return sw;
        }

        public void SetFunctionAutorizadas(Dictionary<string, string> metodosAutorizados)
        {
            if (metodosAutorizados !=null)
            {
                foreach (var item in metodosAutorizados)
                {
                    funcionesRolesAutorizados.Add(item.Key, item.Value);
                }
            }
        }

    }
}
